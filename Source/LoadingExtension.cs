using ICities;
using ColossalFramework;

namespace GameSpeedMod
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            EconomyManager em = Singleton<EconomyManager>.instance;
            GameSpeedManager gsm = Singleton<GameSpeedManager>.instance;

            if (mode == LoadMode.NewGame || mode == LoadMode.LoadGame || mode == LoadMode.NewGameFromScenario)
            {
                Loans.SetLoans();
                Parks.SetVisitorsLevelupRequirement();
                Industries.SetProductionLevelupRequirement();
                Prefabs.SetBldPrefabs();
                TimeFlow.SetTimeFlow();
            }

            if (mode == LoadMode.NewGame || mode == LoadMode.NewGameFromScenario)
            {
                int moneyToAdd = 40 * (gsm.Parameters.ConstructionCostMultiplier - 1) * 100000;
                em.AddResource(EconomyManager.Resource.LoanAmount, moneyToAdd, ItemClass.Service.None, ItemClass.SubService.None, ItemClass.Level.None);
            }
        }

        public override void OnLevelUnloading()
        {
            Loans.ResetLoans();
            Parks.ResetVisitorsLevelupRequirement();
            Industries.ResetProductionLevelupRequirement();
        }
    }
}
