using UnityEngine;
using ColossalFramework;
using ColossalFramework.UI;
using ColossalFramework.Globalization;

namespace GameSpeedMod
{
    public class AdvertisingCampaignPanel : UIPanel
    {
        public int Counter = 0;
        private UIProgressBar currentEffectivenessProgressBar;
        private UILabel demandInfluenceLabel;
        private UILabel residentialDemandInfluenceLabel;
        private UILabel commercialDemandInfluenceLabel;
        private UILabel workpaceDemandInfluenceLabel;
        private UIButton fundCampaignBtn;

        public override void Awake()
        {
            base.Awake();

            backgroundSprite = "MenuPanel";
            canFocus = true;

            width = 270;
            height = 260;

            isVisible = false;
        }

        public override void Start()
        {
            base.Start();

            UILabel lTitle = AddUIComponent<UILabel>();
            lTitle.position = new Vector3(10, -15);
            lTitle.text = "Advertising Campaign";

            int y = -50;
            int h = -25;

            addLabel(y, "Current effectiveness:");
            y += h;
            currentEffectivenessProgressBar = addProgressBar(y);
            y += h;

            demandInfluenceLabel = addLabel(y);
            y += h;
            residentialDemandInfluenceLabel = addLabel(y);
            y += h;
            commercialDemandInfluenceLabel = addLabel(y);
            y += h;
            workpaceDemandInfluenceLabel = addLabel(y);
            y += h;

            fundCampaignBtn = AddUIComponent<UIButton>();
            fundCampaignBtn.normalBgSprite = "ButtonMenu";
            fundCampaignBtn.disabledBgSprite = "ButtonMenuDisabled";
            //fundCampaignBtn.focusedBgSprite = "ButtonMenuFocused";
            fundCampaignBtn.hoveredBgSprite = "ButtonMenuHovered";
            fundCampaignBtn.position = new Vector3(10, y);
            fundCampaignBtn.size = new Vector2(250, 50);
            fundCampaignBtn.eventClick += FundCampaignBtn_eventClick;

            UIButton closeBtn = AddUIComponent<UIButton>();
            closeBtn.position = new Vector3(230, -5);
            closeBtn.size = new Vector2(30, 30);
            closeBtn.normalFgSprite = "buttonclose";
            closeBtn.eventClick += closeBtn_eventClick;
        }

        private void FundCampaignBtn_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            EconomyManager em = Singleton<EconomyManager>.instance;

            int moneyToTake = getCostOfAdCampaign() * 100;
            if (em.LastCashAmount >= moneyToTake)
            {
                Singleton<GameSpeedManager>.instance.StartAdvertisingCampaign();

                em.AddResource(EconomyManager.Resource.LoanAmount, -moneyToTake, ItemClass.Service.None, ItemClass.SubService.None, ItemClass.Level.None);

                updateControls();
            }
        }

        protected override void OnVisibilityChanged()
        {
            //isVisible = isVisibleOverride;
            //base.OnVisibilityChanged();

            if (isVisible)
            {
                updateControls();
            }
        }

        public void ShowOverride()
        {
            //isVisibleOverride = true;
            Show();
        }

        private void closeBtn_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            //isVisibleOverride = false;
            Hide();
        }

        private UILabel addLabel(int y, string text="")
        {
            UILabel l = AddUIComponent<UILabel>();
            l.position = new Vector3(10, y);
            //l.textScale = 0.7f;
            l.text = text;

            return l;
        }

        private UIProgressBar addProgressBar(int y)
        {
            UIProgressBar b = AddUIComponent<UIProgressBar>();
            b.backgroundSprite = "LevelBarBackground";
            b.progressSprite = "LevelBarForeground";
            b.progressColor = Color.green;
            b.position = new Vector3(10, y);
            b.width = 250;
            b.value = 0f;

            return b;
        }

        public override void Update()
        {
            base.Update();

            if (!isVisible) return;

            if (--Counter > 0) return;
            Counter = 30;

            updateControls();
        }

        private void updateControls()
        {
            int demandMaxValue = Singleton<GameSpeedManager>.instance.Parameters.DemandMaxValue;
            int demandRestorePercent = Singleton<GameSpeedManager>.instance.GetDemandRestorePercent();

            int residentialDemand = Mathf.Max(0, Singleton<ZoneManager>.instance.m_actualResidentialDemand);
            int commercialDemand = Mathf.Max(0, Singleton<ZoneManager>.instance.m_actualCommercialDemand);
            int workplaceDemand = Mathf.Max(0, Singleton<ZoneManager>.instance.m_actualWorkplaceDemand);

            int residentialDemandFunded = residentialDemand * 100 / demandMaxValue;
            int commercialDemandFunded = commercialDemand * 100 / demandMaxValue;
            int workplaceDemandFunded = workplaceDemand * 100 / demandMaxValue;

            currentEffectivenessProgressBar.value = demandRestorePercent * 0.01f;

            if (demandRestorePercent > 0)
            {
                demandInfluenceLabel.text = "Current target demand";
                residentialDemandInfluenceLabel.text = string.Format("Residential: +{0}", residentialDemand);
                commercialDemandInfluenceLabel.text = string.Format("Commercial: +{0}", commercialDemand);
                workpaceDemandInfluenceLabel.text = string.Format("Industry and office: +{0}", workplaceDemand);
                fundCampaignBtn.enabled = false;
            }
            else
            {
                demandInfluenceLabel.text = "Target demand if funded";
                residentialDemandInfluenceLabel.text = string.Format("Residential: +{0}", residentialDemandFunded);
                commercialDemandInfluenceLabel.text = string.Format("Commercial: +{0}", commercialDemandFunded);
                workpaceDemandInfluenceLabel.text = string.Format("Industry and office: +{0}", workplaceDemandFunded);
                fundCampaignBtn.enabled = true;
            }

            fundCampaignBtn.text = string.Format("Funding an ad campaign\n(Cost {0})", getCostOfAdCampaign().ToString(Settings.moneyFormat, LocaleManager.cultureInfo));
        }

        private int getPopulation()
        {
            if (Singleton<DistrictManager>.exists)
            {
                return (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_populationData.m_finalCount;
            }
            return 0;
        }

        private int getCostOfAdCampaign()
        {
            GameSpeedParameters p = Singleton<GameSpeedManager>.instance.Parameters;
            return p.AdvertisingCampaignBaseCost + (getPopulation() / 100) * p.AdvertisingCampaignCostPer100Pop;
        }
    }
}
