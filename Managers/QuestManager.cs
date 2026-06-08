
using StardewModdingAPI;

namespace SmartVillage
{
    public class QuestManager
    {
        private IModHelper Helper;
        private IMonitor Logger;

        public QuestManager(IModHelper helper, IMonitor logger)
        {
            Helper = helper;
            Logger = logger;
        }

        public void GenerateDailyQuests()
        {
            Logger.Log("Sinh nhiệm vụ mới cho ngày...", LogLevel.Debug);
        }

        public void Tick()
        {
            // TODO: check quest completion, trigger quest events
        }
    }
}
