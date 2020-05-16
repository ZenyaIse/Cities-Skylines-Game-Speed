using ColossalFramework;
using UnityEngine;

namespace GameSpeedMod
{
    public class Citizens : Singleton<Citizens>
    {
        public void OnAfterSimulationFrame()
        {
            if (Singleton<GameSpeedManager>.instance.Parameters.TimeFlowDice())
            {
                return;
            }

            CitizenManager cm = Singleton<CitizenManager>.instance;

            int num = (int)(Singleton<SimulationManager>.instance.m_currentFrameIndex & 4095u);
            int num2 = num * 256;
            int num3 = (num + 1) * 256 - 1;
            for (int i = num2; i <= num3; i++)
            {
                if ((cm.m_citizens.m_buffer[i].m_flags & Citizen.Flags.Created) != Citizen.Flags.None)
                {
                    CitizenInfo citizenInfo = cm.m_citizens.m_buffer[i].GetCitizenInfo((uint)i);
                    if (citizenInfo != null)
                    {
                        updateAge((uint)i, ref cm.m_citizens.m_buffer[i]);
                    }
                }
            }
        }

        private void updateAge(uint citizenID, ref Citizen data)
        {
            int num = data.Age;
            if (num < 0 || num % 15 == 0)
            {
                // Just Finished SchoolOrWork etc. (by vanilla)
            }
            else
            {
                data.Age = num - 1;
            }
        }
    }
}
