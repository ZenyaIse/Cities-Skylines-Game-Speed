namespace GameSpeedMod
{
    public class GameSpeedParameters
    {
        public int AreaCostMultiplier;
        public float MilestonePopulationThreshholdMultiplier;
        public int LoanMultiplier;
        public int ConstructionCostMultiplier;
        public int LevelupRequirement;
        public int OilOreDepletionRate;
        public int ConstructionTimeMultiplier;
        public int TimeFlowMultiplier_x10;
        public int DemandDropAfterBuildingCreated;

        public GameSpeedParameters(int gameSpeedIndex)
        {
            switch (gameSpeedIndex)
            {
                case 0: // Normal
                    AreaCostMultiplier = 1;
                    MilestonePopulationThreshholdMultiplier = 1.0f;
                    LoanMultiplier = 1;
                    ConstructionCostMultiplier = 1;
                    LevelupRequirement = 100;
                    OilOreDepletionRate = 100;
                    ConstructionTimeMultiplier = 1;
                    TimeFlowMultiplier_x10 = 10;
                    DemandDropAfterBuildingCreated = 0;
                    break;

                case 1: // Slow
                    AreaCostMultiplier = 2;
                    MilestonePopulationThreshholdMultiplier = 1.25f;
                    LoanMultiplier = 2;
                    ConstructionCostMultiplier = 2;
                    LevelupRequirement = 150;
                    OilOreDepletionRate = 65;
                    ConstructionTimeMultiplier = 2;
                    TimeFlowMultiplier_x10 = 15;
                    DemandDropAfterBuildingCreated = 10;
                    break;

                case 2: // Epic
                    AreaCostMultiplier = 3;
                    MilestonePopulationThreshholdMultiplier = 1.5f;
                    LoanMultiplier = 3;
                    ConstructionCostMultiplier = 3;
                    LevelupRequirement = 200;
                    OilOreDepletionRate = 50;
                    ConstructionTimeMultiplier = 3;
                    TimeFlowMultiplier_x10 = 20;
                    DemandDropAfterBuildingCreated = 20;
                    break;

                case 3: // Marathon
                    AreaCostMultiplier = 5;
                    MilestonePopulationThreshholdMultiplier = 1.75f;
                    LoanMultiplier = 4;
                    ConstructionCostMultiplier = 5;
                    LevelupRequirement = 300;
                    OilOreDepletionRate = 33;
                    ConstructionTimeMultiplier = 5;
                    TimeFlowMultiplier_x10 = 30;
                    DemandDropAfterBuildingCreated = 40;
                    break;

                case 4: // 1001 Nights
                    AreaCostMultiplier = 8;
                    MilestonePopulationThreshholdMultiplier = 2.0f;
                    LoanMultiplier = 5;
                    ConstructionCostMultiplier = 8;
                    LevelupRequirement = 400;
                    OilOreDepletionRate = 25;
                    ConstructionTimeMultiplier = 8;
                    TimeFlowMultiplier_x10 = 40;
                    DemandDropAfterBuildingCreated = 80;
                    break;

                default:
                    AreaCostMultiplier = 1;
                    MilestonePopulationThreshholdMultiplier = 1.0f;
                    LoanMultiplier = 1;
                    ConstructionCostMultiplier = 1;
                    LevelupRequirement = 100;
                    OilOreDepletionRate = 100;
                    ConstructionTimeMultiplier = 1;
                    TimeFlowMultiplier_x10 = 10;
                    DemandDropAfterBuildingCreated = 0;
                    break;
            }
        }
    }
}
