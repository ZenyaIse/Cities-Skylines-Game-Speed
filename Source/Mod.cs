using ICities;
using ColossalFramework;

namespace GameSpeedMod
{
    public class Mod : IUserMod
    {
        public const string ModNameEng = "Game Speed Mod";
        public const string Version = "2020/5/6";

        public string Name
        {
            get { return ModNameEng; }
        }

        public string Description
        {
            get { return "Slower milestones progress and city growth (ver. " + Version + ")"; }
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

            helper.AddSpace(20);

            helper.AddCheckbox("Do not scale milestone population threshold with water area ratio", gsom.values.IsMilestonePopulationThreshholdUnscaled, delegate (bool isChecked)
            {
                gsom.values.IsMilestonePopulationThreshholdUnscaled = isChecked;
                gsom.AfterOptionChanged();
            });

            helper.AddSpace(20);

            helper.AddCheckbox("Hard Mode", gsom.values.IsHardMode, delegate (bool isChecked)
            {
                gsom.values.IsHardMode = isChecked;
                gsom.AfterOptionChanged();
            });
        }

        #endregion
    }
}
