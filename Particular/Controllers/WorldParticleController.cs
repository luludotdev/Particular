using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Particular.Utils;

namespace Particular.Controllers
{
    internal class WorldParticleController : MonoBehaviour
    {
        internal static WorldParticleController instance;

        internal void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        internal void OnActiveSceneChanged(Scene _)
        {
            ForceUpdate();
        }

        internal void ForceUpdate()
        {
            StartCoroutine(ForceUpdateAsync());
        }

        private IEnumerator ForceUpdateAsync()
        {
            ParticleSystem dustPS = null;
            int i = -1;
            int maxTries = 50;

            while (dustPS == null)
            {
                yield return new WaitForSeconds(0.2f);

                List<ParticleSystem> pss = MonoBehaviourExtensions.FindObjectsOfTypeAll<ParticleSystem>();
                dustPS = pss.Where(x => x.name == "DustPS").FirstOrDefault();

                if (dustPS == null) i++;
                if (i > maxTries) break;
            }

            if (dustPS == null)
            {
                Plugin.log.Debug("Failed to find DustPS");
                yield break;
            }

            bool enabled = Plugin.config.GetBoolean("global", "world-particles") ?? true;
            dustPS?.gameObject?.SetActive(enabled);
        }
    }
}
