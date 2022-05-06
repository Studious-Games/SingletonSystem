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
        private static List<Type> _singletons = new List<Type>();
        private static string _loadingScene;

        //---------------------------------------------------------------------------------
        //
        //---------------------------------------------------------------------------------
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnEnterPlayMode()
        {
            _loadingScene = SceneManager.GetActiveScene().name;
            InstantiateSingletons();
        }

        //---------------------------------------------------------------------------------
        //
        //---------------------------------------------------------------------------------
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
                            method.Invoke(null, null);
                        }
                    }
                }
            }
        }
    }
}