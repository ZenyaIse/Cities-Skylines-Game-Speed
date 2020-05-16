using ICities;
using ColossalFramework;
using ColossalFramework.UI;
using ColossalFramework.Plugins;
using System.Reflection;

namespace GameSpeedMod
{
    public class Mod : IUserMod
    {
        public const string ModNameEng = "Game Speed Mod";
        public const string Version = "2020/5/16";

        public string Name
        {
            get { return ModNameEng; }
        }

        public string Description
        {
            get { return "Slower milestones progress and city growth (ver. " + Version + ")"; }
        }

        #region Options UI

        private UIDropDown gameSpeedDropDown;
        private UICheckBox populationThresholdUnscaledCheckBox;
        private UICheckBox hardModeCheckBox;

        private bool freezeUI = false;

        public static void UpdateUI()
        {
            foreach (PluginManager.PluginInfo current in Singleton<PluginManager>.instance.GetPluginsInfo())
            {
                if (current.isEnabled)
                {
                    IUserMod[] instances = current.GetInstances<IUserMod>();
                    MethodInfo method = instances[0].GetType().GetMethod("GameSpeedModUpdateUI", BindingFlags.Instance | BindingFlags.Public);
                    if (method != null)
                    {
                        method.Invoke(instances[0], new object[] { });
                        return;
                    }
                }
            }
        }

        public void GameSpeedModUpdateUI()
        {
            if (gameSpeedDropDown != null && populationThresholdUnscaledCheckBox != null && hardModeCheckBox != null)
            {
                GameSpeedManager gsm = Singleton<GameSpeedManager>.instance;

                freezeUI = true;
                gameSpeedDropDown.selectedIndex = gsm.values.GameSpeedIndex;
                populationThresholdUnscaledCheckBox.isChecked = gsm.values.IsMilestonePopulationThresholdUnscaled;
                hardModeCheckBox.isChecked = gsm.values.IsHardMode;
                freezeUI = false;
            }
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            GameSpeedManager gsm = Singleton<GameSpeedManager>.instance;

            gameSpeedDropDown = (UIDropDown)helper.AddDropdown("Game speed", gsm.GameSpeeds, gsm.values.GameSpeedIndex, delegate (int sel)
            {
                if (!freezeUI)
                {
                    gsm.values.GameSpeedIndex = sel;
                    gsm.AfterOptionChanged();
                }
            });

            helper.AddSpace(20);

            populationThresholdUnscaledCheckBox = (UICheckBox)helper.AddCheckbox(
                "Do not scale milestone population threshold with water area ratio",
                gsm.values.IsMilestonePopulationThresholdUnscaled,
                delegate (bool isChecked)
            {
                if (!freezeUI)
                {
                    gsm.values.IsMilestonePopulationThresholdUnscaled = isChecked;
                    gsm.AfterOptionChanged();
                }
            });

            helper.AddSpace(20);

            hardModeCheckBox = (UICheckBox)helper.AddCheckbox("Hard Mode", gsm.values.IsHardMode, delegate (bool isChecked)
            {
                if (!freezeUI)
                {
                    gsm.values.IsHardMode = isChecked;
                    gsm.AfterOptionChanged();
                }
            });
        }

        #endregion
    }
}
