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

            return demandValue;
        }

        public override int OnUpdateDemand(int lastDemand, int nextDemand, int targetDemand)
        {
            if (Singleton<GameSpeedManager>.instance.values.GameSpeedIndex == 0) // Normal
            {
                return nextDemand;
            }

            if (targetDemand > lastDemand)
            {
                nextDemand = Mathf.Min(lastDemand + 1, targetDemand);
            }
            else if (targetDemand < lastDemand)
            {
                nextDemand = Mathf.Max(lastDemand - 1, targetDemand);
            }
            else
            {
                nextDemand = targetDemand;
            }

            return nextDemand;
        }
    }
}
