using System;
using System.Reflection;
using BeatSaberMarkupLanguage.Settings;
using Harmony;
using IPA;
using Logger = IPA.Logging.Logger;
using IPA.Utilities;
using LibConf;
using LibConf.Providers;
using Particular.Controllers;
using Particular.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Particular
{
    public class Plugin : IBeatSaberPlugin
    {
        public static Logger log;
        public static IConfigProvider config;

        private static GameObject _controller;

        public void Init(object _, Logger logger)
        {
            log = logger;
        }

        public void OnApplicationStart()
        {
            config = Conf.CreateConfig(ConfigType.YAML, BeatSaber.UserDataPath, "Particular");

            try
            {
                HarmonyInstance harmony = HarmonyInstance.Create("com.jackbaron.beatsaber.particular");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                log.Error(
                    "This plugin requires Harmony. Make sure you installed the plugin properly, " +
                    "as the Harmony DLL should have been installed with it."
                );

                log.Critical(e);
            }
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

            if (_controller == null)
            {
                _controller = new GameObject("ParticularController");
                GameObject.DontDestroyOnLoad(_controller);

                _controller.AddComponent<WorldParticleController>();
                _controller.AddComponent<CameraNoiseController>();
            }

            WorldParticleController.instance?.OnActiveSceneChanged(scene);
            CameraNoiseController.instance?.OnActiveSceneChanged(scene);
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
