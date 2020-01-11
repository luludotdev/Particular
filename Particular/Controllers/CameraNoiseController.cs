using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Particular.Utils;

namespace Particular.Controllers
{
    internal class CameraNoiseController : MonoBehaviour
    {
        internal static CameraNoiseController instance;

        private Texture2D _originalTex;
        private Texture2D _noiseTex;

        internal void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this);

            UpdateTexture();
        }

        private void UpdateTexture()
        {
            int brightness = Plugin.Settings.CameraNoiseBrightness;
            byte v = Convert.ToByte(Mathf.Clamp(brightness, 0, 255));

            _noiseTex = Texture2D.blackTexture;
            Color32[] pixels = _noiseTex.GetPixels32();
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = new Color32(v, v, v, 255);
            }

            _noiseTex.SetPixels32(pixels);
            _noiseTex.Apply();
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
            BlueNoiseDitheringUpdater ditheringUpdater = null;
            int i = -1;
            int maxTries = 50;

            while (ditheringUpdater == null)
            {
                yield return new WaitForSeconds(0.2f);

                GameObject helper = GameObject.Find("BlueNoiseHelper");
                ditheringUpdater = helper?.GetComponent<BlueNoiseDitheringUpdater>();

                if (ditheringUpdater == null) i++;
                if (i > maxTries) break;
            }

            if (ditheringUpdater == null)
            {
                Plugin.log.Debug("Failed to find BlueNoiseDitheringUpdater");
                yield break;
            }

            BlueNoiseDithering blueNoiseDithering;
            try
            {
                blueNoiseDithering = ditheringUpdater.GetField<BlueNoiseDithering>("_blueNoiseDithering");
            }
            catch (NullReferenceException ex)
            {
                Plugin.log.Critical("Failed to get BlueNoiseDithering");
                Plugin.log.Critical(ex);

                yield break;
            }

            if (_originalTex == null)
            {
                try
                {
                    Plugin.log.Debug("Caching original camera noise texture.");
                    _originalTex = blueNoiseDithering?.GetField<Texture2D>("_noiseTexture");
                }
                catch (NullReferenceException ex)
                {
                    Plugin.log.Critical("BlueNoiseDitheringUpdater noise texture could not be cached!");
                    Plugin.log.Critical(ex);

                    yield break;
                }
            }

            UpdateTexture();

            bool enabled = Plugin.Settings.CameraNoise;
            if (enabled)
            {
                blueNoiseDithering.SetField("_noiseTexture", _originalTex);
            }
            else
            {
                blueNoiseDithering.SetField("_noiseTexture", _noiseTex);
            }
        }
    }
}
