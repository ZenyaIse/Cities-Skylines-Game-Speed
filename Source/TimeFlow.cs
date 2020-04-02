using System;
using ColossalFramework;

namespace GameSpeedMod
{
    public static class TimeFlow
    {
        public static void SetTimeFlow()
        {
            int m = Singleton<GameSpeedManager>.instance.Parameters.TimeFlowMultiplier_x10;

            DateTime dateTime = SimulationManager.instance.m_ThreadingWrapper.simulationTime;
            SimulationManager.instance.m_timePerFrame = new TimeSpan(1476562500L * 10L / (long)m);
            SetGameDateTime(dateTime);
        }

        public static void SetGameDateTime(DateTime dateTime)
        {
            var sm = SimulationManager.instance;
            sm.m_timeOffsetTicks = dateTime.Ticks - sm.m_currentFrameIndex * sm.m_timePerFrame.Ticks;
            sm.m_currentGameTime = dateTime;
        }
    }
}
