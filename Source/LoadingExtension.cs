using ICities;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
using System.Text.RegularExpressions;

namespace GameSpeedMod
{
    public class LoadingExtension : LoadingExtensionBase
    {
        private AdvertisingCampaignPanel adPanel;

        public override void OnLevelLoaded(LoadMode mode)
        {
            EconomyManager em = Singleton<EconomyManager>.instance;
            GameSpeedManager gsm = Singleton<GameSpeedManager>.instance;

            if (mode == LoadMode.NewGame || mode == LoadMode.NewGameFromScenario)
            {
                int moneyToAdd = 40 * (gsm.Parameters.ConstructionCostMultiplier - 1) * 100000;
                em.AddResource(EconomyManager.Resource.LoanAmount, moneyToAdd, ItemClass.Service.None, ItemClass.SubService.None, ItemClass.Level.None);

                gsm.StartAdvertisingCampaign();
            }

            if (mode == LoadMode.NewGame || mode == LoadMode.LoadGame || mode == LoadMode.NewGameFromScenario)
            {
                Loans.SetLoans();

                createAdvertisingCampaignPanel();

                Singleton<UnlockManager>.instance.EventMilestoneUnlocked += delegate (MilestoneInfo info)
                {
                    if (Regex.IsMatch(info.m_name, @"^Milestone\d+$"))
                    {
                        Debug.Log("GameSpeedMod >>> Started advertising campaign because " + info.m_name + " unlocked: ");
                        gsm.StartAdvertisingCampaign();
                    }
                };
            }
        }

        public override void OnLevelUnloading()
        {
            Loans.ResetLoans();
        }

        private void createAdvertisingCampaignPanel()
        {
            UIView v = UIView.GetAView();
            adPanel = v.AddUIComponent(typeof(AdvertisingCampaignPanel)) as AdvertisingCampaignPanel;
            //GameObject obj = new GameObject("AdvertisingCampaignPanel");
            //obj.transform.parent = v.cachedTransform;
            //adPanel = obj.AddComponent<AdvertisingCampaignPanel>();
            adPanel.name = "AdvertisingCampaignPanel";
            adPanel.absolutePosition = new Vector3(550, v.fixedHeight - 400);

            UIInput.eventProcessKeyEvent += UIInput_eventProcessKeyEvent;

            //UIComponent infoPanel = v.FindUIComponent("InfoPanel");
            //UIButton infoPanelToggleButton = infoPanel.AddUIComponent<UIButton>();
            //infoPanelToggleButton.normalBgSprite = "InfoIconBasePressed";
            //infoPanelToggleButton.normalFgSprite = "InfoIconElectricity";
            //infoPanelToggleButton.width = 30f;
            //infoPanelToggleButton.height = 30f;
            //infoPanelToggleButton.absolutePosition = new Vector3(v.fixedWidth - 170, 10);
            //toggleButton.tooltip = "Extended Disasters";
            //infoPanelToggleButton.eventClick += demandEventClick;

            //GameObject toggleButtonObject = new GameObject("AdvertisingCampaignButton");
            //toggleButtonObject.transform.parent = v.transform;
            //toggleButtonObject.transform.localPosition = Vector3.zero;
            //UIButton toggleButton = toggleButtonObject.AddComponent<UIButton>();
            //toggleButton.normalBgSprite = "InfoIconBasePressed";
            //toggleButton.normalFgSprite = "InfoIconElectricity";
            //toggleButton.width = 30f;
            //toggleButton.height = 30f;
            //toggleButton.absolutePosition = new Vector3(v.fixedWidth - 170, 10);
            //toggleButton.tooltip = "Extended Disasters";
            //toggleButton.eventClick += demandEventClick;
            //addClickEventToDemandPanel();
        }

        private void UIInput_eventProcessKeyEvent(EventType eventType, KeyCode keyCode, EventModifiers modifiers)
        {
            if (eventType == EventType.KeyDown && modifiers == EventModifiers.Control && keyCode == KeyCode.D)
            {
                if (adPanel != null)
                {
                    adPanel.isVisible = !adPanel.isVisible;
                }
            }
        }
    }
}
