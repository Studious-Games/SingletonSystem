using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

using Object = UnityEngine.Object;

namespace Studious.SingletonSystem
{
    #if UNITY_EDITOR
    [InitializeOnLoad]
    #endif
    public static class SceneController 
    {
        static SceneController()
        {
            Application.quitting += OnApplicationQuit;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LoadSingletons(scene);
            UnLoadSingletons(scene);
        }

        private static void UnLoadSingletons(Scene scene)
        {
            var scripts = SingletonLocator.GetAllBySceneUnload(scene).ToList();

            foreach (var script in scripts)
            {
                var test = GameObject.Find(script.SingletonAttribute.Name);
                Object.Destroy(test);
            }
        }

        private static void LoadSingletons(Scene scene)
        {
            var scripts = SingletonLocator.GetAllBySceneLoad(scene).ToList();

            foreach (var script in scripts)
            {
                script.ScriptType.Invoke(null, null);
            }
        }

        private static void OnApplicationQuit()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Application.quitting -= OnApplicationQuit;
        }
    }
}