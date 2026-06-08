
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using StardewModdingAPI;

namespace SmartVillage
{
    public class I18nManager
    {
        private IModHelper Helper;
        public Dictionary<string, string> Strings;

        public I18nManager(IModHelper helper)
        {
            Helper = helper;
            Strings = new Dictionary<string, string>();
        }

        public void LoadLanguage(string langCode)
        {
            string path = Path.Combine(Helper.DirectoryPath, "i18n", $"{langCode}.json");
            if (!File.Exists(path))
            {
                Helper.Monitor.Log($"i18n file {langCode}.json not found, using default en.json", LogLevel.Warn);
                path = Path.Combine(Helper.DirectoryPath, "i18n", "en.json");
            }

            var json = File.ReadAllText(path);
            Strings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public string T(string key)
        {
            if (Strings.ContainsKey(key)) return Strings[key];
            return key;
        }
    }
}
