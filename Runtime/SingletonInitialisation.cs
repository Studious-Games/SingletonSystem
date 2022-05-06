using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Studious.Singleton
{
    public class SingletonInitialisation
    {
        public static List<ScriptInstance> SingletonScripts = new List<ScriptInstance>();
        private static string _loadingScene;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnEnterPlayMode()
        {
            _loadingScene = SceneManager.GetActiveScene().name;
            InstantiateSingletons();
        }

        private static void InstantiateSingletons()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetName().Name == "SingletonTests")
                    continue;

                foreach (Type type in assembly.GetTypes())
                {
                    var attribs = type.GetCustomAttributes(typeof(SingletonAttribute), true);

                    if (attribs != null && attribs.Length > 0)
                    {
                        foreach (var method in type.GetRuntimeMethods())
                        {
                            if (method.IsStatic == false) continue;
                            if (method.Name != "get_Instance") continue;

                            SingletonAttribute attr = (SingletonAttribute)Attribute.GetCustomAttribute(type, typeof(SingletonAttribute));

                            if (attr.Persistent == false)
                            {
                                ScriptInstance instance = new ScriptInstance(method, attr.Scene);
                                SingletonScripts.Add(instance);
                            }

                            if (attr.Scene == null || attr.Scene == _loadingScene)
                            {
                                method.Invoke(null, null);
                            }
                        }
                    }
                }
            }
        }
    }

    public class ScriptInstance
    {
        public MethodInfo ScriptType;
        public string ScriptScene;

        public ScriptInstance(MemberInfo scriptType, string scriptName)
        {
            ScriptType = (MethodInfo)scriptType;
            ScriptScene = scriptName;
        }
    }
}