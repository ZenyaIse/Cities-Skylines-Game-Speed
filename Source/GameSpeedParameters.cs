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
                    break;

                case 1: // Slow
                    AreaCostMultiplier = 3;
                    MilestonePopulationThreshholdMultiplier = 1.25f;
                    DemandMaxValue = 80;
                    LoanMultiplier = 2;
                    ConstructionCostMultiplier = 2;
                    AdvertisingCampaignBaseCost = 500;
                    AdvertisingCampaignCostPer100Pop = 50;
                    break;

                case 2: // Epic
                    AreaCostMultiplier = 5;
                    MilestonePopulationThreshholdMultiplier = 1.5f;
                    DemandMaxValue = 60;
                    LoanMultiplier = 3;
                    ConstructionCostMultiplier = 3;
                    AdvertisingCampaignBaseCost = 1000;
                    AdvertisingCampaignCostPer100Pop = 100;
                    break;
                case 3: // Marathon
                    AreaCostMultiplier = 10;
                    MilestonePopulationThreshholdMultiplier = 1.75f;
                    DemandMaxValue = 50;
                    LoanMultiplier = 4;
                    ConstructionCostMultiplier = 5;
                    AdvertisingCampaignBaseCost = 2000;
                    AdvertisingCampaignCostPer100Pop = 200;
                    break;

                case 4: // 1001 Nights
                    AreaCostMultiplier = 20;
                    MilestonePopulationThreshholdMultiplier = 2.0f;
                    DemandMaxValue = 40;
                    LoanMultiplier = 5;
                    ConstructionCostMultiplier = 8;
                    AdvertisingCampaignBaseCost = 4000;
                    AdvertisingCampaignCostPer100Pop = 400;
                    break;

                default:
                    AreaCostMultiplier = 1;
                    MilestonePopulationThreshholdMultiplier = 1.0f;
                    DemandMaxValue = 100;
                    LoanMultiplier = 1;
                    ConstructionCostMultiplier = 1;
                    AdvertisingCampaignBaseCost = 0;
                    AdvertisingCampaignCostPer100Pop = 0;
                    break;
            }
        }
    }
}
