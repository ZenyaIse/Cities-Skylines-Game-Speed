using System;
using ColossalFramework;

namespace GameSpeedMod
{
    public static class TimeFlow
    {
        public static void SetTimeFlow()
        {
            if (SimulationManager.exists && SimulationManager.instance.m_ThreadingWrapper != null)
            {
                int m = Singleton<GameSpeedManager>.instance.Parameters.TimeFlowMultiplier_x10;
                setTimePerFrame(m);
                Logger.Add("Time flow (x times)", m / 10);
            }
        }

        public static void ResetTimeFlow()
        {
            if (SimulationManager.exists && SimulationManager.instance.m_ThreadingWrapper != null)
            {
                setTimePerFrame(10);
                Logger.Add("Reset time flow");
            }
        }

        private static void setTimePerFrame(int multiplier_x10)
        {
            DateTime dateTime = SimulationManager.instance.m_ThreadingWrapper.simulationTime;
            SimulationManager.instance.m_timePerFrame = new TimeSpan(1476562500L * 10L / (long)multiplier_x10);
            setGameDateTime(dateTime);
        }

        private static void setGameDateTime(DateTime dateTime)
        {
            var sm = SimulationManager.instance;
            sm.m_timeOffsetTicks = dateTime.Ticks - sm.m_currentFrameIndex * sm.m_timePerFrame.Ticks;
            sm.m_currentGameTime = dateTime;
        }
    }
}
