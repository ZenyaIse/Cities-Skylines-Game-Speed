using System;
using ColossalFramework;

namespace GameSpeedMod
{
    public static class TimeFlow
    {
        public static void SetTimeFlow()
        {
            int m = Singleton<GameSpeedManager>.instance.Parameters.TimeFlowMultiplier_x10;

            SimulationManager.instance.m_timePerFrame = new TimeSpan(1476562500L * 10L / (long)m);
        }

        public static void SetGameDateTime(DateTime dateTime)
        {
            var sm = SimulationManager.instance;
            sm.m_timeOffsetTicks = dateTime.Ticks - sm.m_currentFrameIndex * sm.m_timePerFrame.Ticks;
            sm.m_currentGameTime = dateTime;

            //sm.m_currentDayTimeHour = (float)sm.m_currentGameTime.TimeOfDay.TotalHours;
            //sm.m_dayTimeFrame = (uint)(SimulationManager.DAYTIME_FRAMES * sm.m_currentDayTimeHour / 24f);
            //sm.m_dayTimeOffsetFrames = sm.m_dayTimeFrame - sm.m_currentFrameIndex & SimulationManager.DAYTIME_FRAMES - 1;
        }
    }
}
