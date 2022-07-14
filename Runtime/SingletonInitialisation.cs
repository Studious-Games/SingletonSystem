using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Studious.SingletonSystem
{
    public static class SingletonInitialisation
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialization()
        {
            InstantiateSingletons();
        }

        private static void InstantiateSingletons()
        {
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(SingletonAttribute), true).Length > 0));

            foreach (var type in types)
            {
                IEnumerable<MethodInfo> methods = type.GetMethods().ToList().Where(x => x.IsStatic == true && x.Name == "get_Instance");

                foreach (var method in methods)
                {
                    var test = method.Invoke(null, null);

                    SingletonAttribute attr = (SingletonAttribute)Attribute.GetCustomAttribute(type, typeof(SingletonAttribute));
                    SingletonInstance scriptInstance = new SingletonInstance(method, attr, type, test);

                    SingletonLocator.Register(scriptInstance);
                }
            }
        }

    }

}