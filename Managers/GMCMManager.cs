
using StardewModdingAPI;
using GenericModConfigMenu;

namespace SmartVillage
{
    public class GMCMManager
    {
        private IModHelper Helper;
        private IMonitor Logger;

        public GMCMManager(IModHelper helper, IMonitor logger)
        {
            Helper = helper;
            Logger = logger;
        }

        public void RegisterConfig()
        {
            var gmcm = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (gmcm == null)
            {
                Logger.Log("GMCM not found, skipping config menu", LogLevel.Warn);
                return;
            }

            gmcm.RegisterModConfig(
                mod: Helper.ModRegistry.ModID,
                reset: () => SmartVillageConfig.Reset(),
                save: () => SmartVillageConfig.Save()
            );

            gmcm.AddTextOption(
                mod: Helper.ModRegistry.ModID,
                name: () => "NPC Interaction Mode",
                tooltip: () => "Choose AI behavior mode",
                getValue: () => SmartVillageConfig.NPCMode,
                setValue: value => SmartVillageConfig.NPCMode = value,
                allowedValues: new string[] { "Simple", "Advanced", "Dynamic" }
            );

            gmcm.AddNumberOption(
                mod: Helper.ModRegistry.ModID,
                name: () => "Max Memory Entries",
                tooltip: () => "Max number of previous conversations NPC can remember",
                getValue: () => SmartVillageConfig.MaxMemory,
                setValue: val => SmartVillageConfig.MaxMemory = val,
                min: 10,
                max: 1000
            );
        }
    }

    public static class SmartVillageConfig
    {
        public static string NPCMode = "Advanced";
        public static int MaxMemory = 100;

        public static void Reset()
        {
            NPCMode = "Advanced";
            MaxMemory = 100;
        }

        public static void Save()
        {
        }
    }
}
