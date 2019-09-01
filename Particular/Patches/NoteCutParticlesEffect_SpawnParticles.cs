using System;
using Particular.Utils;
using Harmony;
using UnityEngine;

namespace Particular.Patches
{
    [HarmonyPatch(typeof(NoteCutParticlesEffect))]
    [HarmonyPatch("SpawnParticles")]
    internal class NoteCutParticlesEffect_SpawnParticles
    {
        internal static void Prefix(ref NoteCutParticlesEffect __instance, ref int sparkleParticlesCount, ref int explosionParticlesCount)
        {
            ParticleSystem[] sparklesPS;
            try
            {
                sparklesPS = (ParticleSystem[])__instance.GetField("_sparklesPS");
            }
            catch (NullReferenceException)
            {
                Plugin.log.Critical("Failed to get _sparklesPS field!");
                return;
            }

            ParticleSystem explosionPS;
            try
            {
                explosionPS = (ParticleSystem)__instance.GetField("_explosionPS");
            }
            catch (NullReferenceException)
            {
                Plugin.log.Critical("Failed to get _explosionPS field!");
                return;
            }

            float slashMultiplier = Plugin.config.GetFloat("particles", "slash-particles") ?? 1f;
            float explosionMultiplier = Plugin.config.GetFloat("particles", "explosion-particles") ?? 1f;
            float lifetimeMultiplier = Plugin.config.GetFloat("particles", "particle-lifetime") ?? 1f;

            float sparkles = sparkleParticlesCount * slashMultiplier;
            float explosion = explosionParticlesCount * explosionMultiplier;

            sparkleParticlesCount = Mathf.FloorToInt(sparkles);
            explosionParticlesCount = Mathf.FloorToInt(explosion);

            foreach (ParticleSystem ps in sparklesPS)
            {
                ParticleSystem.MainModule psMain = ps.main;

                psMain.maxParticles = int.MaxValue;
                psMain.startLifetimeMultiplier = lifetimeMultiplier;
            }

            ParticleSystem.MainModule explosionMain = explosionPS.main;

            explosionMain.maxParticles = int.MaxValue;
            explosionMain.startLifetimeMultiplier = lifetimeMultiplier;
        }
    }
}
