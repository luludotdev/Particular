using System;
using BeatSaberMarkupLanguage.Settings;
using IPA;
using Logger = IPA.Logging.Logger;
using IPA.Utilities;
using LibConf;
using LibConf.Providers;
using Particular.Settings;
using UnityEngine.SceneManagement;

namespace Particular
{
    public class Plugin : IBeatSaberPlugin
    {
        public static Logger log;
        public static IConfigProvider config;

        public void Init(object _, Logger logger)
        {
            log = logger;
        }

        public void OnApplicationStart()
        {
            config = Conf.CreateConfig(ConfigType.YAML, BeatSaber.UserDataPath, "Particular");
        }

        public void OnApplicationQuit()
        {

        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {

        }

        public void OnActiveSceneChanged(Scene old, Scene scene)
        {
            if (scene.name == "MenuCore")
            {
                BSMLSettings.instance.AddSettingsMenu("Particular", "Particular.Settings.ParticularSettings.bsml", ParticularSettings.instance);
            }
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
