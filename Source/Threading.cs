using ColossalFramework;
using ICities;

namespace GameSpeedMod
{
    public class Threading : ThreadingExtensionBase
    {
        public override void OnAfterSimulationFrame()
        {
            Singleton<Citizens>.instance.SimulationStepImpl();
        }
    }
}
