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
                EconomyManager.Bank bank = em.m_properties.m_banks[i];
                EconomyManager.LoanInfo li = bank.m_loanOffers[0];

                int newAmount = li.m_amount * gs.Parameters.LoanMultiplier;
                int newLength = li.m_length * (1 + gs.Parameters.LoanMultiplier) / 2; // Halve the effect to prevent too long loan length.

                m_amount_orig[i] = li.m_amount;
                li.m_amount = newAmount;

                m_length_orig[i] = li.m_length;
                li.m_length = newLength;

                bank.m_loanOffers[0] = li;
                em.m_properties.m_banks[i] = bank;
            }
        }

        public static void ResetLoans()
        {
            if (m_amount_orig == null || m_length_orig == null) return;

            EconomyManager em = Singleton<EconomyManager>.instance;

            for (int i = 0; i < 3; i++)
            {
                EconomyManager.Bank bank = em.m_properties.m_banks[i];
                EconomyManager.LoanInfo li = bank.m_loanOffers[0];

                li.m_amount = m_amount_orig[i];

                li.m_length = m_length_orig[i];

                bank.m_loanOffers[0] = li;
                em.m_properties.m_banks[i] = bank;
            }
        }
    }
}
