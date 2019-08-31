using LibConf.Providers;

namespace Particular.Utils
{
    internal static class LibConfExtensions
    {
        internal static void SaveDefault(this IConfigProvider provider, string section, string key, string value)
        {
            string v = provider.GetString(section, key, null);
            if (v == null)
            {
                provider.SetString(section, key, value, true);
            }
        }

        internal static void SaveDefault(this IConfigProvider provider, string section, string key, bool value)
        {
            bool? v = provider.GetBoolean(section, key, null);
            if (v == null)
            {
                provider.SetBoolean(section, key, value, true);
            }
        }

        internal static void SaveDefault(this IConfigProvider provider, string section, string key, int value)
        {
            int? v = provider.GetInt(section, key, null);
            if (v == null)
            {
                provider.SetInt(section, key, value, true);
            }
        }

        internal static void SaveDefault(this IConfigProvider provider, string section, string key, float value)
        {
            float? v = provider.GetFloat(section, key, null);
            if (v == null)
            {
                provider.SetFloat(section, key, value, true);
            }
        }
    }
}
