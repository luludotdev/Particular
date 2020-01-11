using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Particular.Settings
{
    internal class SettingStore
    {
        public float SlashParticles { get; private set; }

        public float ExplosionParticles { get; private set; }

        public float SlashParticleLifetime { get; private set; }

        public float ExplosionParticleLifetime { get; private set; }

        public bool RainbowParticles { get; private set; }

        public bool CameraNoise { get; private set; }

        public int CameraNoiseBrightness { get; private set; }

        public bool WorldParticles { get; private set; }

        public SettingStore(ParticularSettings s)
        {
            SlashParticles = s.SlashParticles;
            ExplosionParticles = s.ExplosionParticles;
            SlashParticleLifetime = s.SlashParticleLifetime;
            ExplosionParticleLifetime = s.ExplosionParticleLifetime;
            RainbowParticles = s.RainbowParticles;
            CameraNoise = s.CameraNoise;
            CameraNoiseBrightness = s.CameraNoiseBrightness;
            WorldParticles = s.WorldParticles;
        }

        public void Update(ParticularSettings s)
        {
            if (s == null)
                return;
            SlashParticles = s.SlashParticles;
            ExplosionParticles = s.ExplosionParticles;
            SlashParticleLifetime = s.SlashParticleLifetime;
            ExplosionParticleLifetime = s.ExplosionParticleLifetime;
            RainbowParticles = s.RainbowParticles;
            CameraNoise = s.CameraNoise;
            CameraNoiseBrightness = s.CameraNoiseBrightness;
            WorldParticles = s.WorldParticles;
        }

        private string _settingString;

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_settingString))
            {
                _settingString = $"SlashParticles: {SlashParticles}, SlashParticleLifetime: {SlashParticleLifetime}, ExplosionParticles: {ExplosionParticles}, " +
                    $"ExplosionParticleLifetime: {ExplosionParticleLifetime}, CameraNoise: {CameraNoise}, CameraNoiseBrightness: {CameraNoiseBrightness}," +
                    $" WorldParticles: {WorldParticles}, RainbowParticles: {RainbowParticles}";
            }
            return _settingString;
        }
    }
}
