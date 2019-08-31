using System;
using IPA;
using IPA.Logging;
using UnityEngine.SceneManagement;

namespace Particular
{
    public class Plugin : IBeatSaberPlugin
    {
        public static Logger log;

        public void Init(object _, Logger logger)
        {
            log = logger;
        }

        public void OnApplicationStart()
        {

        }

        public void OnApplicationQuit()
        {

        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {

        }

        public void OnActiveSceneChanged(Scene old, Scene scene)
        {

        }

        public void OnSceneUnloaded(Scene scene)
        {

        }

        public void OnUpdate()
        {

        }

        public void OnFixedUpdate()
        {

        }
    }
}
