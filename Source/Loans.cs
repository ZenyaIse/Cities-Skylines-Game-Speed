using ColossalFramework;
using UnityEngine;

namespace GameSpeedMod
{
    public static class Loans
    {
        private static bool isAlreadySet = false;

        public static void SetLoans()
        {
            if (isAlreadySet) return;

            EconomyManager em = Singleton<EconomyManager>.instance;
            GameSpeedManager gs = Singleton<GameSpeedManager>.instance;

            if (em.m_properties == null || em.m_properties.m_banks == null) return;

            for (int i = 0; i < 3; i++)
            {
                int oldAmount = em.m_properties.m_banks[i].m_loanOffers[0].m_amount;
                int oldLength = em.m_properties.m_banks[i].m_loanOffers[0].m_length;

                int newAmount = oldAmount * gs.Parameters.LoanMultiplier;
                int newLength = oldLength * (1 + gs.Parameters.LoanMultiplier) / 2; // Halve the effect to prevent too long loan length.

                em.m_properties.m_banks[i].m_loanOffers[0].m_amount = newAmount;
                em.m_properties.m_banks[i].m_loanOffers[0].m_length = newLength;

                ModLogger.Add(em.m_properties.m_banks[i].m_bankName, "amount", oldAmount, newAmount, "length", oldLength, newLength);
            }

            isAlreadySet = true;
        }

        public static void ResetLoans()
        {
            if (!isAlreadySet) return;

            EconomyManager em = Singleton<EconomyManager>.instance;
            GameSpeedManager gs = Singleton<GameSpeedManager>.instance;

            for (int i = 0; i < 3; i++)
            {
                em.m_properties.m_banks[i].m_loanOffers[0].m_amount /= gs.Parameters.LoanMultiplier;

                int value = em.m_properties.m_banks[i].m_loanOffers[0].m_length;
                em.m_properties.m_banks[i].m_loanOffers[0].m_length = Mathf.RoundToInt(value * 2f / (1 + gs.Parameters.LoanMultiplier));
            }

            ModLogger.Add("Reset loans");

            isAlreadySet = false;
        }
    }
}
