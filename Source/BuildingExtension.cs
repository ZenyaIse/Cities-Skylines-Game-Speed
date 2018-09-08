using ICities;
using ColossalFramework;

namespace GameSpeedMod
{
    public class BuildingExtension : BuildingExtensionBase
    {
        public override void OnBuildingCreated(ushort id)
        {
            base.OnBuildingCreated(id);

            if (Singleton<BuildingManager>.exists)
            {
                BuildingManager bm = Singleton<BuildingManager>.instance;

                Building bld = bm.m_buildings.m_buffer[id];

                if (bld.Info.m_buildingAI.GetType() == typeof(MonumentAI))
                {
                    Singleton<GameSpeedManager>.instance.OnMonumentBuilt(bld.Info.name);
                }
            }
        }
    }
}
