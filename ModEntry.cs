
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace SmartVillage
{
    public class ModEntry : Mod
    {
        public static IMonitor Logger;
        public AIManager AI;
        public QuestManager Quest;
        public GMCMManager GMCM;
        public I18nManager I18n;

        public override void Entry(IModHelper helper)
        {
            Logger = this.Monitor;
            Logger.Log("SmartVillage Mod Demo loaded", LogLevel.Info);

            I18n = new I18nManager(helper);
            I18n.LoadLanguage("en");

            GMCM = new GMCMManager(helper, Logger);
            GMCM.RegisterConfig();

            AI = new AIManager(helper, Logger, I18n);
            Quest = new QuestManager(helper, Logger);

            helper.Events.GameLoop.DayStarted += OnDayStarted;
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
            helper.Events.GameLoop.TimeChanged += OnTimeChanged;
        }

        private void OnDayStarted(object sender, DayStartedEventArgs e)
        {
            Logger.Log("=== Ngày mới bắt đầu ===", LogLevel.Info);
            AI.UpdateNPCs();
            Quest.GenerateDailyQuests();
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady) return;
            AI.Tick();
            Quest.Tick();
        }

        private int lastCheckedTime = -1;
        private void OnTimeChanged(object sender, TimeChangedEventArgs e)
        {
            int gameHour = e.NewTime / 100;
            if (gameHour >= 5 && lastCheckedTime < 5) AI.CompleteQuest(0);
            lastCheckedTime = gameHour;
        }
    }
}
