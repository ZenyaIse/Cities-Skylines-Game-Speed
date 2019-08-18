namespace GameSpeedMod
{
    public class GameSpeedParameters
    {
        public int AreaCostMultiplier;
        public float MilestonePopulationThreshholdMultiplier;
        public int DemandMaxValue;
        public int LoanMultiplier;
        public int ConstructionCostMultiplier;
        public int AdvertisingCampaignBaseCost;
        public int AdvertisingCampaignCostPer100Pop;
        public int LevelupRequirement;
        public int OilOreDepletionRate;

        public GameSpeedParameters(int gameSpeedIndex)
        {
            switch (gameSpeedIndex)
            {
                case 0: // Normal
                    AreaCostMultiplier = 1;
                    MilestonePopulationThreshholdMultiplier = 1.0f;
                    DemandMaxValue = 100;
                    LoanMultiplier = 1;
                    ConstructionCostMultiplier = 1;
                    AdvertisingCampaignBaseCost = 0;
                    AdvertisingCampaignCostPer100Pop = 0;
                    LevelupRequirement = 100;
                    OilOreDepletionRate = 100;
                    break;

                case 1: // Slow
                    AreaCostMultiplier = 2;
                    MilestonePopulationThreshholdMultiplier = 1.25f;
                    DemandMaxValue = 70;
                    LoanMultiplier = 2;
                    ConstructionCostMultiplier = 2;
                    AdvertisingCampaignBaseCost = 500;
                    AdvertisingCampaignCostPer100Pop = 100;
                    LevelupRequirement = 150;
                    OilOreDepletionRate = 65;
                    break;

                case 2: // Epic
                    AreaCostMultiplier = 3;
                    MilestonePopulationThreshholdMultiplier = 1.5f;
                    DemandMaxValue = 50;
                    LoanMultiplier = 3;
                    ConstructionCostMultiplier = 3;
                    AdvertisingCampaignBaseCost = 1000;
                    AdvertisingCampaignCostPer100Pop = 200;
                    LevelupRequirement = 200;
                    OilOreDepletionRate = 50;
                    break;

                case 3: // Marathon
                    AreaCostMultiplier = 5;
                    MilestonePopulationThreshholdMultiplier = 1.75f;
                    DemandMaxValue = 40;
                    LoanMultiplier = 4;
                    ConstructionCostMultiplier = 5;
                    AdvertisingCampaignBaseCost = 2000;
                    AdvertisingCampaignCostPer100Pop = 400;
                    LevelupRequirement = 300;
                    OilOreDepletionRate = 33;
                    break;

                case 4: // 1001 Nights
                    AreaCostMultiplier = 8;
                    MilestonePopulationThreshholdMultiplier = 2.0f;
                    DemandMaxValue = 30;
                    LoanMultiplier = 5;
                    ConstructionCostMultiplier = 8;
                    AdvertisingCampaignBaseCost = 4000;
                    AdvertisingCampaignCostPer100Pop = 800;
                    LevelupRequirement = 400;
                    OilOreDepletionRate = 25;
                    break;

                default:
                    AreaCostMultiplier = 1;
                    MilestonePopulationThreshholdMultiplier = 1.0f;
                    DemandMaxValue = 100;
                    LoanMultiplier = 1;
                    ConstructionCostMultiplier = 1;
                    AdvertisingCampaignBaseCost = 0;
                    AdvertisingCampaignCostPer100Pop = 0;
                    LevelupRequirement = 100;
                    OilOreDepletionRate = 100;
                    break;
            }
        }
    }
}
