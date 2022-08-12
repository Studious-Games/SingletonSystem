using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

using UnityObject = UnityEngine.Object;

namespace Studious.SingletonSystem
{

    public static class Singleton<T> where T : MonoBehaviour
    {
        private static T _instance;

        private static bool _persistent => _attribute.Persistent;
        private static bool _automatic => _attribute.Automatic;
        private static string _name => _attribute.Name;
        private static HideFlags _hideFlags => _attribute.HideFlags;

        private static readonly SingletonAttribute _attribute;
        private static readonly HashSet<T> _awoken;
        private static readonly object _lock = new object();

        public static bool Instantiated
        {
            get
            {
                lock (_lock)
                {
                    if (Application.isPlaying)
                    {
                        return _instance != null;
                    }
                    else
                    {
                        return FindInstances().Length == 1;
                    }
                }
            }
        }

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (Application.isPlaying)
                    {
                        if (_instance == null)
                        {
                            Instantiate();
                        }

                        return _instance;
                    }
                    else
                    {
                        return Instantiate();
                    }
                }
            }
        }

        static Singleton()
        {
            _awoken = new HashSet<T>();
            _attribute = typeof(T).GetCustomAttribute<SingletonAttribute>();
        }

        private static T[] FindInstances()
        {
            // Fails here on hidden hide flags
            return UnityObject.FindObjectsOfType<T>();
        }

        public static T Instantiate()
        {
            lock (_lock)
            {
                var instances = FindInstances();

                if (instances.Length == 1)
                {
                    _instance = instances[0];
                    _instance.gameObject.hideFlags = _hideFlags;
                    _instance.name = _name ?? typeof(T).Name;


                    UnityObject.DontDestroyOnLoad(_instance);
                }
                else if (instances.Length == 0)
                {
                    if (_automatic)
                    {
                        // Create the parent game object with the proper hide flags
                        var singleton = new GameObject(_name ?? typeof(T).Name);
                        singleton.hideFlags = _hideFlags;

                        // Instantiate the component, letting Awake assign the real instance variable
                        var _instance = singleton.AddComponent<T>();
                        _instance.hideFlags = _hideFlags;

                        // Sometimes in the editor, for example when creating a new scene,
                        // AddComponent seems to call Awake add a later frame, making this call
                        // fail for exactly one frame. We'll force-awake it if need be.
                        Awake(_instance);

                        // Make the singleton persistent if need be
                        if (_persistent && Application.isPlaying)
                        {
                            UnityObject.DontDestroyOnLoad(singleton);
                        }
                    }
                    else
                    {
                        throw new UnityException($"Missing '{typeof(T)}' singleton in the scene.");
                    }
                }
                else if (instances.Length > 1)
                {
                    throw new UnityException($"More than one '{typeof(T)}' singleton in the scene.");
                }

                return _instance;
            }
        }

        public static void Awake(T instance)
        {
            if (_awoken.Contains(instance))
            {
                return;
            }

            if (_instance != null)
            {
                throw new UnityException($"More than one '{typeof(T)}' singleton in the scene.");
            }

            _instance = instance;
            _awoken.Add(instance);
        }

        public static void OnDestroy(T instance)
        {
            if (_instance == instance)
            {
                _instance = null;
            }
            else
            {
                throw new UnityException($"Trying to destroy invalid instance of '{typeof(T)}' singleton.");
            }
        }
    }
}
