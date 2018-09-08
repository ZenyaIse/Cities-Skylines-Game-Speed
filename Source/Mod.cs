using ICities;
using ColossalFramework;

namespace GameSpeedMod
{
    public class Mod : IUserMod
    {
        public static string ModNameEng = "City Growth Speed Mod";

        public string Name
        {
            get { return ModNameEng; }
        }

        public string Description
        {
            get { return "Makes milestones progress and city growth slower. Ver 2018/8/23"; }
        }

        #region Options UI

        public void OnSettingsUI(UIHelperBase helper)
        {
            GameSpeedManager gsom = Singleton<GameSpeedManager>.instance;

            helper.AddDropdown("Game speed", gsom.GameSpeeds, gsom.values.GameSpeedIndex, delegate (int sel)
            {
                gsom.values.GameSpeedIndex = sel;
                gsom.AfterOptionChanged();
            });

            helper.AddCheckbox("Do not scale milestone threshhold with water area ratio", gsom.values.IsMilestonePopulationThreshholdUnscaled, delegate (bool isChecked)
            {
                gsom.values.IsMilestonePopulationThreshholdUnscaled = isChecked;
                gsom.AfterOptionChanged();
            });

            helper.AddCheckbox("No rewards for unlocking Milestones", gsom.values.NoReward, delegate (bool isChecked)
            {
                gsom.values.NoReward = isChecked;
                gsom.AfterOptionChanged();
            });

            helper.AddCheckbox("Hard Mode", gsom.values.IsHardMode, delegate (bool isChecked)
            {
                gsom.values.IsHardMode = isChecked;
                gsom.AfterOptionChanged();
            });
        }

        #endregion
    }
}
