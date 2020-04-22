using ICities;
using ColossalFramework;

namespace GameSpeedMod
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            GameSpeedManager gsm = Singleton<GameSpeedManager>.instance;

            if (mode == LoadMode.NewGame || mode == LoadMode.LoadGame || mode == LoadMode.NewGameFromScenario)
            {
                gsm.SetAll();
            }

            if (mode == LoadMode.NewGame || mode == LoadMode.NewGameFromScenario)
            {
                int cash_thousands = (int)(Singleton<EconomyManager>.instance.LastCashAmount / 100000);
                int moneyToAdd = (cash_thousands * 40 * (gsm.Parameters.ConstructionCostMultiplier - 1) / 70) * 100000;
                Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.LoanAmount, moneyToAdd, ItemClass.Service.None, ItemClass.SubService.None, ItemClass.Level.None);
            }

            ModLogger.Write();
        }

        public override void OnLevelUnloading()
        {
            Singleton<GameSpeedManager>.instance.ResetAll();
        }
    }
}
