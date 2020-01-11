using System;
using Particular.Utils;
using Particular.Settings;
using Harmony;
using UnityEngine;

namespace Particular.Patches
{
    [HarmonyPatch(typeof(NoteCutParticlesEffect))]
    [HarmonyPatch("SpawnParticles")]
    internal class NoteCutParticlesEffect_SpawnParticles
    {
        internal static void Prefix(ref NoteCutParticlesEffect __instance, ref Color32 color, ref int sparkleParticlesCount, ref int explosionParticlesCount, ref float lifetimeMultiplier,
            ref ParticleSystem[] ____sparklesPS, ref ParticleSystem ____explosionPS)
        {
            sparkleParticlesCount = Mathf.FloorToInt(sparkleParticlesCount * Plugin.Settings.SlashParticles);
            explosionParticlesCount = Mathf.FloorToInt(explosionParticlesCount * Plugin.Settings.ExplosionParticles);
            lifetimeMultiplier *= Plugin.Settings.SlashParticleLifetime;
            if (Plugin.Settings.RainbowParticles)
                color = UnityEngine.Random.ColorHSV();
            ParticleSystem.MainModule slashMain = ____sparklesPS[0].main;
            slashMain.maxParticles = int.MaxValue;
            ParticleSystem.MainModule explosionMain = ____explosionPS.main;
            explosionMain.maxParticles = int.MaxValue;
            explosionMain.startLifetimeMultiplier = Plugin.Settings.ExplosionParticleLifetime;
        }
    }
}
