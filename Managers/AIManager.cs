
using StardewModdingAPI;
using System.Collections.Generic;

namespace SmartVillage
{
    public class AIManager
    {
        private IModHelper Helper;
        private IMonitor Logger;
        private I18nManager I18n;
        private List<string> DailyQuests;

        public AIManager(IModHelper helper, IMonitor logger, I18nManager i18n)
        {
            Helper = helper;
            Logger = logger;
            I18n = i18n;
            DailyQuests = new List<string>();
        }

        public void UpdateNPCs()
        {
            Logger.Log("Cập nhật trạng thái NPC...", LogLevel.Debug);
            string greeting = I18n.T("npc_greeting");
            Logger.Log($"NPC Greeting: {greeting}", LogLevel.Info);
            string questText = I18n.T("quest_start");
            DailyQuests.Clear();
            DailyQuests.Add(questText);
            Logger.Log($"Daily Quest: {questText}", LogLevel.Info);
        }

        public void Tick()
        {
            foreach (var quest in DailyQuests)
                Logger.Log($"Checking quest progress: {quest}", LogLevel.Trace);
        }

        public void CompleteQuest(int index)
        {
            if (index < 0 || index >= DailyQuests.Count) return;
            string completeText = I18n.T("quest_complete");
            Logger.Log($"Quest Completed: {completeText}", LogLevel.Info);
            DailyQuests.RemoveAt(index);
        }
    }
}
