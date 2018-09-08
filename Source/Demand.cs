using ICities;
using ColossalFramework;
using UnityEngine;

namespace GameSpeedMod
{
    public class Demand : DemandExtensionBase
    {
        public override int OnCalculateResidentialDemand(int originalDemand)
        {
            return GetModdedDemand(originalDemand);
        }

        public override int OnCalculateCommercialDemand(int originalDemand)
        {
            return GetModdedDemand(originalDemand);
        }

        public override int OnCalculateWorkplaceDemand(int originalDemand)
        {
            return GetModdedDemand(originalDemand);
        }

        public static int GetModdedDemand(int demandValue)
        {
            GameSpeedManager gs = Singleton<GameSpeedManager>.instance;

            if (gs.values.IsHardMode)
            {
                demandValue -= 20;
            }

            int demandMaxValue = gs.Parameters.DemandMaxValue;
            int demandRestorePercent = Mathf.Min(gs.DemandRestorePercent, 100);

            demandMaxValue += (100 - demandMaxValue) * demandRestorePercent / 100;

            demandValue = (demandValue * demandMaxValue + 99) / 100;

            return demandValue;
        }

        public static int GetModdedDemand_Inverse(int demandValue)
        {
            GameSpeedManager gs = Singleton<GameSpeedManager>.instance;

            float k = gs.Parameters.DemandMaxValue * 0.01f;
            float r = Mathf.Min(gs.DemandRestorePercent, 100) * 0.01f;

            demandValue = (int)(demandValue / (k + (1 - k) * r));

            if (gs.values.IsHardMode)
            {
                demandValue += 20;
            }

            return demandValue;
        }
    }
}
