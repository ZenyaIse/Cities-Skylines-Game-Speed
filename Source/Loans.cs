using ColossalFramework;

namespace GameSpeedMod
{
    public static class Loans
    {
        private static int[] m_amount_orig = null;
        private static int[] m_length_orig = null;

        public static void SetLoans()
        {
            EconomyManager em = Singleton<EconomyManager>.instance;
            GameSpeedManager gs = Singleton<GameSpeedManager>.instance;

            if (em.m_properties == null || em.m_properties.m_banks == null) return;

            m_amount_orig = new int[3];
            m_length_orig = new int[3];

            for (int i = 0; i < 3; i++)
            {
                int oldAmount = em.m_properties.m_banks[i].m_loanOffers[0].m_amount;
                int oldLength = em.m_properties.m_banks[i].m_loanOffers[0].m_length;

                int newAmount = oldAmount * gs.Parameters.LoanMultiplier;
                int newLength = oldLength * (1 + gs.Parameters.LoanMultiplier) / 2; // Halve the effect to prevent too long loan length.

                m_amount_orig[i] = oldAmount;
                m_length_orig[i] = oldLength;

                em.m_properties.m_banks[i].m_loanOffers[0].m_amount = newAmount;
                em.m_properties.m_banks[i].m_loanOffers[0].m_length = newLength;

                Logger.Add(em.m_properties.m_banks[i].m_bankName, "amount", oldAmount, newAmount, "length", oldLength, newLength);
            }
        }

        public static void ResetLoans()
        {
            if (m_amount_orig == null || m_length_orig == null) return;

            EconomyManager em = Singleton<EconomyManager>.instance;

            for (int i = 0; i < 3; i++)
            {
                em.m_properties.m_banks[i].m_loanOffers[0].m_amount = m_amount_orig[i];
                em.m_properties.m_banks[i].m_loanOffers[0].m_length = m_length_orig[i];
            }

            m_amount_orig = null;
            m_length_orig = null;

            Logger.Add("Reset loans");
        }
    }
}
