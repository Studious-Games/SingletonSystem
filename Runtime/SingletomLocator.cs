using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace Studious.SingletonSystem
{
    public static class SingletonLocator
    {
        private static readonly List<SingletonInstance> _services = new List<SingletonInstance>();

        public static bool IsRegistered(Type t)
        {
            return _services.Where(x => x.Type == t).Any();
        }

        public static void Register(SingletonInstance instance)
        {
            if (IsRegistered(instance.Type))
                throw new SingletonLocatorException($"{instance.Type.Name} has been already registered.");

            _services.Add(instance);
        }

        public static T Get<T>()
        {
            if (!IsRegistered(typeof(T)))
                return default;

            return _services.Select(item => item.SingletonClass).OfType<T>().FirstOrDefault(item => item.GetType() == typeof(T));
        }

        public static IEnumerable<SingletonInstance> GetAllBySceneUnload(Scene scene)
        {
            return _services.Where(x => x.SingletonAttribute.SceneUnload == scene.name);
        }

        public static IEnumerable<SingletonInstance> GetAllBySceneLoad(Scene scene)
        {
            return _services.Where(x => x.SingletonAttribute.Scene == scene.name);
        }
    }

    public class SingletonLocatorException : Exception
    {
        public SingletonLocatorException(string message) : base(message) { }
    }

}



