using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var scripts = SingletonInitialisation.SingletonScripts.Where(x=> x.ScriptScene == scene.name);

            foreach (var script in scripts)
            {
                script.ScriptType.Invoke(null, null);
            }
        }

        private static void OnApplicationQuit()
        {
            Application.quitting -= OnApplicationQuit;
        }

    }
}