using ICities;
using ColossalFramework;
using UnityEngine;

namespace GameSpeedMod
{
    public class BuildingExtension : BuildingExtensionBase
    {
        public override void OnBuildingCreated(ushort id)
        {
            base.OnBuildingCreated(id);

            if (Singleton<BuildingManager>.exists)
            {
                ZoneManager zm = Singleton<ZoneManager>.instance;
                ItemClass.Service service = Singleton<BuildingManager>.instance.m_buildings.m_buffer[id].Info.GetService();
                int drop = Singleton<GameSpeedManager>.instance.Parameters.DemandDropAfterBuildingCreated;

                switch (service)
                {
                    case ItemClass.Service.Residential:
                        zm.m_actualResidentialDemand = Mathf.Max(0, zm.m_actualResidentialDemand - drop);
                        break;
                    case ItemClass.Service.Commercial:
                        zm.m_actualCommercialDemand = Mathf.Max(0, zm.m_actualCommercialDemand - drop);
                        break;
                    case ItemClass.Service.Industrial:
                        zm.m_actualWorkplaceDemand = Mathf.Max(0, zm.m_actualWorkplaceDemand - drop);
                        break;
                    case ItemClass.Service.Office:
                        zm.m_actualWorkplaceDemand = Mathf.Max(0, zm.m_actualWorkplaceDemand - drop);
                        break;
                }
            }
        }
    }
}
