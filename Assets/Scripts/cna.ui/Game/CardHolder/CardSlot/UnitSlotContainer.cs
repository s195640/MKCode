using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class UnitSlotContainer : MonoBehaviour {
        [SerializeField] private CardVO card;

        [SerializeField] private ActionCardSlot ActionCardSlot;
        [SerializeField] private AddressableImage unitImage;
        [SerializeField] private TextMeshProUGUI unitName;
        [SerializeField] private TextMeshProUGUI unitLevel;
        [SerializeField] private TextMeshProUGUI unitArmor;
        [SerializeField] private TextMeshProUGUI unitCost;
        [SerializeField] private AddressableImage unitCostImage;
        [SerializeField] private AddressableImage unitLoc01;
        [SerializeField] private AddressableImage unitLoc02;
        [SerializeField] private AddressableImage unitLoc03;
        [SerializeField] private AddressableImage unitResit01;
        [SerializeField] private AddressableImage unitResit02;
        [SerializeField] private AddressableImage unitResit03;
        [SerializeField] private AddressableImage unitExhaustedShield;
        [SerializeField] private GameObject BondsOfLoyalty;

        [SerializeField] private Transform Unit2CardContainer;
        [SerializeField] private Transform Unit3CardContainer;
        [SerializeField] private ICNA_Base[] UnitButtons;

        [Header("Unit State")]
        [SerializeField] private GameObject unitMarker;
        [SerializeField] private GameObject unitUsedInbattle;
        [SerializeField] private GameObject unitWoundContainer;
        [SerializeField] private GameObject unitWounded;
        [SerializeField] private GameObject unitPoisoned;
        [SerializeField] private GameObject unitExhausted;
        [SerializeField] private GameObject unitParalyzed;

        [Header("Banners")]
        [SerializeField] private GameObject unitBanner;
        [SerializeField] private GameObject unitBannerGlory;
        [SerializeField] private GameObject unitBannerFear;
        [SerializeField] private GameObject unitBannerProtection;
        [SerializeField] private GameObject unitBannerCourage;




        public void SetupUI(CardVO card, List<CardState_Enum> cardState, Image_Enum unitExhaustedShield = Image_Enum.NA, int banner = 0) {
            this.card = card;
            unitImage.ImageEnum = card.CardImage;
            unitName.text = card.CardTitle;
            unitLevel.text = getUnitLevel(card.UnitLevel);
            unitArmor.text = "" + card.UnitArmor;
            unitCost.text = "" + card.UnitCost;
            unitCostImage.ImageEnum = card.UnitLevel > 2 ? Image_Enum.I_influencegold : Image_Enum.I_influencegrey;
            this.unitExhaustedShield.ImageEnum = unitExhaustedShield;
            setUpRecruitLocations();
            setUpResistance();
            setupActions();
            UpdateUI(cardState, banner);
        }

        public void UpdateUI(List<CardState_Enum> cardState, int banner) {
            unitUsedInbattle.SetActive(cardState.Contains(CardState_Enum.Unit_UsedInBattle));
            unitMarker.SetActive(
                cardState.Contains(CardState_Enum.Unit_Exhausted) ||
                cardState.Contains(CardState_Enum.Unit_Wounded) ||
                cardState.Contains(CardState_Enum.Unit_Poisoned) ||
                cardState.Contains(CardState_Enum.Unit_Paralyzed)
                );
            unitExhausted.SetActive(cardState.Contains(CardState_Enum.Unit_Exhausted));
            unitParalyzed.SetActive(cardState.Contains(CardState_Enum.Unit_Paralyzed));
            unitWoundContainer.SetActive(cardState.Contains(CardState_Enum.Unit_Wounded) || cardState.Contains(CardState_Enum.Unit_Poisoned));
            unitWounded.SetActive(cardState.Contains(CardState_Enum.Unit_Wounded));
            unitPoisoned.SetActive(cardState.Contains(CardState_Enum.Unit_Poisoned));
            BondsOfLoyalty.SetActive(cardState.Contains(CardState_Enum.Unit_BondsOfLoyalty));
            bool glory = false;
            bool fear = false;
            bool protection = false;
            bool courage = false;
            if (banner > 0) {
                CardVO bannerCard = D.Cards[banner];
                glory = bannerCard.CardImage == Image_Enum.CT_banner_of_glory;
                fear = bannerCard.CardImage == Image_Enum.CT_banner_of_fear;
                protection = bannerCard.CardImage == Image_Enum.CT_banner_of_protection;
                courage = bannerCard.CardImage == Image_Enum.CT_banner_of_courage;
            }
            unitBanner.SetActive(glory || fear || protection || courage);
            unitBannerGlory.SetActive(glory);
            unitBannerFear.SetActive(fear);
            unitBannerProtection.SetActive(protection);
            unitBannerCourage.SetActive(courage);
        }

        private string getUnitLevel(int level) {
            switch (level) {
                case 1: { return "I"; }
                case 2: { return "II"; }
                case 3: { return "III"; }
                case 4: { return "IV"; }
                default: { return "I"; }
            }
        }

        private void setUpRecruitLocations() {
            switch (card.UnitRecruitLocation.Count) {
                case 1: {
                    unitLoc01.gameObject.SetActive(true);
                    unitLoc01.ImageEnum = card.UnitRecruitLocation[0];
                    unitLoc02.gameObject.SetActive(false);
                    unitLoc03.gameObject.SetActive(false);
                    break;
                }
                case 2: {
                    unitLoc01.gameObject.SetActive(true);
                    unitLoc01.ImageEnum = card.UnitRecruitLocation[0];
                    unitLoc02.gameObject.SetActive(true);
                    unitLoc02.ImageEnum = card.UnitRecruitLocation[1];
                    unitLoc03.gameObject.SetActive(false);
                    break;
                }
                case 3: {
                    unitLoc01.gameObject.SetActive(true);
                    unitLoc01.ImageEnum = card.UnitRecruitLocation[0];
                    unitLoc02.gameObject.SetActive(true);
                    unitLoc02.ImageEnum = card.UnitRecruitLocation[1];
                    unitLoc03.gameObject.SetActive(true);
                    unitLoc03.ImageEnum = card.UnitRecruitLocation[2];
                    break;
                }
            }
        }

        private void setUpResistance() {
            switch (card.UnitResistance.Count) {
                case 0: {
                    unitResit01.gameObject.SetActive(false);
                    unitResit02.gameObject.SetActive(false);
                    unitResit03.gameObject.SetActive(false);
                    break;
                }
                case 1: {
                    unitResit01.gameObject.SetActive(true);
                    unitResit01.ImageEnum = card.UnitResistance[0];
                    unitResit02.gameObject.SetActive(false);
                    unitResit03.gameObject.SetActive(false);
                    break;
                }
                case 2: {
                    unitResit01.gameObject.SetActive(true);
                    unitResit01.ImageEnum = card.UnitResistance[0];
                    unitResit02.gameObject.SetActive(true);
                    unitResit02.ImageEnum = card.UnitResistance[1];
                    unitResit03.gameObject.SetActive(false);
                    break;
                }
                case 3: {
                    unitResit01.gameObject.SetActive(true);
                    unitResit01.ImageEnum = card.UnitResistance[0];
                    unitResit02.gameObject.SetActive(true);
                    unitResit02.ImageEnum = card.UnitResistance[1];
                    unitResit03.gameObject.SetActive(true);
                    unitResit03.ImageEnum = card.UnitResistance[2];
                    break;
                }
            }
        }

        private void setupActions() {
            if (card.Actions.Count == 2) {
                UnitButtons[0].SetupUI(card.Actions[0], UIUtil.getColor(card.Costs[0][0]));
                UnitButtons[1].SetupUI(card.Actions[1], UIUtil.getColor(card.Costs[1][0]));
                Unit2CardContainer.gameObject.SetActive(true);
                Unit3CardContainer.gameObject.SetActive(false);
            } else {
                UnitButtons[2].SetupUI(card.Actions[0], UIUtil.getColor(card.Costs[0][0]));
                UnitButtons[3].SetupUI(card.Actions[1], UIUtil.getColor(card.Costs[1][0]));
                UnitButtons[4].SetupUI(card.Actions[2], UIUtil.getColor(card.Costs[2][0]));
                Unit2CardContainer.gameObject.SetActive(false);
                Unit3CardContainer.gameObject.SetActive(true);
            }
        }

        public void OnClick_ActionUnitButton(int i) {
            ICNA_Base clicked = UnitButtons[i];
            int index = card.Actions.Count == 2 ? i : (i - 2);
            ActionResultVO ar = new ActionResultVO(D.G.Clone(), card.UniqueId, CardState_Enum.Unit_Exhausted, index);
            ActionCardSlot.OnClick_ActionUnitButton(ar, clicked);
        }
    }
}