using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

using Object = UnityEngine.Object;

namespace Studious.Singleton
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
            //SceneManager.sceneUnloaded += OnSceneUnLoaded;
        }

        private static void OnSceneUnLoaded(Scene scene)
        {
            //var scripts = SingletonInitialisation.SingletonScripts.Where(x => x.ScriptScene == scene.name);

            //foreach (var script in scripts)
            //{
            //    Debug.Log($"Scene to unload on : {script.ScriptUnloadScene}");
            //}
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LoadSingletons(scene);
            UnLoadSingletons(scene);
        }

        private static void UnLoadSingletons(Scene scene)
        {
            var scripts = SingletonInitialisation.SingletonScripts.Where(x => x.SingletonAttribute.SceneUnload == scene.name && x.SingletonAttribute.Persistent == true);

            foreach (var script in scripts)
            {
                var test = GameObject.Find(script.SingletonAttribute.Name);
                Object.Destroy(test);
            }
        }

        private static void LoadSingletons(Scene scene)
        {
            var scripts = SingletonInitialisation.SingletonScripts.Where(x => x.SingletonAttribute.Scene == scene.name );

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