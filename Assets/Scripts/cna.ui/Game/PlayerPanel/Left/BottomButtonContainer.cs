using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class BottomButtonContainer : MonoBehaviour {

        [SerializeField] private ActionCardSlot A;
        [Header("One Button")]
        [SerializeField] private GameObject oneButtonContainer;
        [SerializeField] private CNA_Button centerButton;
        [Header("Two Button")]
        [SerializeField] private GameObject twoButtonContainer;
        [SerializeField] private CNA_Button leftButton;
        [SerializeField] private CNA_Button rightButton;

        private BottomButton_Enum B1 = BottomButton_Enum.NA;
        private BottomButton_Enum B2 = BottomButton_Enum.NA;
        private BottomButton_Enum B;
        private CNA_Button buttonClicked;

        public CNA_Button ButtonClicked { get => buttonClicked; set => buttonClicked = value; }

        public void UpdateUI() {
            calculateButtons();
            displayButtons();
        }

        private void calculateButtons() {
            PlayerData localPlayer = D.LocalPlayer;
            B1 = BottomButton_Enum.NA;
            B2 = BottomButton_Enum.NA;
            switch (A.CardHolder) {
                case CardHolder_Enum.PlayerHand: {
                    if (A.Card.CardType == CardType_Enum.Wound) {
                        if (localPlayer.PlayerTurnPhase == TurnPhase_Enum.Resting) {
                            B1 = BottomButton_Enum.Heal;
                            B2 = BottomButton_Enum.Discard;
                        } else {
                            B1 = BottomButton_Enum.Heal;
                        }
                    } else {
                        B1 = BottomButton_Enum.Basic;
                        B2 = BottomButton_Enum.Discard;
                    }
                    break;
                }
                case CardHolder_Enum.UnitOffering: {
                    if (A.Card.CardType == CardType_Enum.Advanced) {
                        B1 = BottomButton_Enum.Train;
                    } else {
                        B1 = BottomButton_Enum.Recruit;
                        CardVO bondsOfLoyalty = null;
                        localPlayer.Deck.Skill.ForEach(s => {
                            CardVO card = D.Cards[s];
                            if (card.CardImage == Image_Enum.SKW_bonds_of_loyalty) {
                                bondsOfLoyalty = card;
                            }
                        });
                        if (bondsOfLoyalty != null) {
                            bool used = false;
                            localPlayer.Deck.State.Keys.ForEach(u => {
                                if (localPlayer.Deck.State[u].ContainsAny(CardState_Enum.Unit_BondsOfLoyalty)) {
                                    used = true;
                                }
                            });
                            if (!used) {
                                B2 = BottomButton_Enum.Recruit_BondsOfLoyalty;
                            }
                        }
                    }
                    break;
                }
                case CardHolder_Enum.PlayerUnitHand: {
                    B1 = BottomButton_Enum.Disband;
                    if (localPlayer.Deck.State.ContainsKey(A.Card.UniqueId) &&
                        localPlayer.Deck.State[A.Card.UniqueId].Values.Contains(CardState_Enum.Unit_Wounded)) {
                        B2 = BottomButton_Enum.HealUnit;
                    }
                    break;
                }
                case CardHolder_Enum.BoardHex: {
                    if (A.Hex.IsPlayerAdjacent) {
                        if (A.Hex.Terrain == Image_Enum.TH_UnexploredBasic || A.Hex.Terrain == Image_Enum.TH_UnexploredAdvanced) {
                            B1 = BottomButton_Enum.Explore;
                        } else if (A.Hex.Structure == Image_Enum.SH_MaraudingOrcs || A.Hex.Structure == Image_Enum.SH_Draconum) {
                            if (A.Hex.Monsters.Count > 0) {
                                B1 = BottomButton_Enum.Provoke;
                            }
                        }
                    }
                    if (A.Hex.GridPosition.Equals(localPlayer.CurrentGridLoc)) {
                        bool cleared = D.G.Monsters.Shield.ContainsKey(A.Hex.PlayerLocation);
                        if (cleared) {
                            switch (A.Hex.Structure) {
                                case Image_Enum.SH_City_Red: { B1 = BottomButton_Enum.CityRed_Artifact; break; }
                                case Image_Enum.SH_City_White: { B1 = BottomButton_Enum.CityWhite_UnitAdd; break; }
                            }
                        } else {
                            switch (A.Hex.Structure) {
                                case Image_Enum.SH_SpawningGround: { B1 = BottomButton_Enum.Adventure_SpawningGround; break; }
                                case Image_Enum.SH_MonsterDen: { B1 = BottomButton_Enum.Adventure_MonsterDen; break; }
                                case Image_Enum.SH_Monastery: { B1 = BottomButton_Enum.Adventure_MonasteryHeal; B2 = BottomButton_Enum.Adventure_MonasteryBurn; break; }
                                case Image_Enum.SH_AncientRuins: { B1 = BottomButton_Enum.Adventure_AncientRuins; break; }
                            }
                        }
                        switch (A.Hex.Structure) {
                            case Image_Enum.SH_Village: {
                                B1 = BottomButton_Enum.Adventure_VillageHeal;
                                B2 = BottomButton_Enum.Adventure_VillageRaid;
                                break;
                            }
                            case Image_Enum.SH_Dungeon: { B1 = BottomButton_Enum.Adventure_Dungeon; break; }
                            case Image_Enum.SH_Tomb: { B1 = BottomButton_Enum.Adventure_Tomb; break; }
                        }
                    }
                    break;
                }
                case CardHolder_Enum.PlayerSkillHand: {
                    B1 = BottomButton_Enum.SkillAction;
                    break;
                }
                case CardHolder_Enum.GameEffect: {
                    if (A.Card.GameEffectClickable) {
                        B1 = BottomButton_Enum.GameEffectAction;
                    }
                    break;
                }
                case CardHolder_Enum.AdvancedOffering: {
                    if (localPlayer.GameEffects.ContainsKeyAny(GameEffect_Enum.AC_Learning01, GameEffect_Enum.AC_Learning02)) {
                        B1 = BottomButton_Enum.Learn;
                        if (localPlayer.GameEffects.ContainsKey(GameEffect_Enum.SH_City_Green_Own)) {
                            B2 = BottomButton_Enum.CityGreen_LearnAction;
                        }
                    } else {
                        if (localPlayer.GameEffects.ContainsKey(GameEffect_Enum.SH_City_Green_Own)) {
                            B1 = BottomButton_Enum.CityGreen_LearnAction;
                        }
                    }
                    break;
                }
                case CardHolder_Enum.SpellOffering: {
                    B1 = BottomButton_Enum.LearnSpell;
                    break;
                }
                default: {
                    break;
                }
            }
        }

        private void displayButtons() {
            if (B1 == BottomButton_Enum.NA && B2 == BottomButton_Enum.NA) {
                oneButtonContainer.SetActive(false);
                twoButtonContainer.SetActive(false);
            } else if (B1 != BottomButton_Enum.NA && B2 != BottomButton_Enum.NA) {
                oneButtonContainer.SetActive(false);
                twoButtonContainer.SetActive(true);
                configureButton(B1, leftButton);
                configureButton(B2, rightButton);
            } else {
                oneButtonContainer.SetActive(true);
                twoButtonContainer.SetActive(false);
                configureButton(B1, centerButton);
            }
        }

        private void configureButton(BottomButton_Enum buttonType, CNA_Button button) {
            string text = "";
            Color color = Color.white;
            Image_Enum buttonImageid = Image_Enum.NA;
            bool isActive = true;
            switch (buttonType) {
                case BottomButton_Enum.Heal: {
                    text = "Heal";
                    color = CNAColor.ActionHeal;
                    buttonImageid = Image_Enum.I_healHand;
                    break;
                }
                case BottomButton_Enum.Discard: {
                    text = "Discard";
                    color = CNAColor.ActionDiscard;
                    break;
                }
                case BottomButton_Enum.Basic: {
                    text = "Basic";
                    color = CNAColor.ActionBasic;
                    break;
                }
                case BottomButton_Enum.Recruit: {
                    text = "Recruit";
                    color = CNAColor.ActionRecruit;
                    break;
                }
                case BottomButton_Enum.Recruit_BondsOfLoyalty: {
                    text = "Loyalty";
                    color = CNAColor.ActionRecruitBondsOfLoyalty;
                    break;
                }
                case BottomButton_Enum.Disband: {
                    text = "Disband";
                    color = CNAColor.ActionDisband;
                    break;
                }
                case BottomButton_Enum.Explore: {
                    text = "Explore";
                    color = CNAColor.ActionExplore;
                    break;
                }
                case BottomButton_Enum.Provoke: {
                    text = "Provoke";
                    color = CNAColor.ActionProvoke;
                    break;
                }
                case BottomButton_Enum.HealUnit: {
                    text = "Heal";
                    color = CNAColor.ActionHeal;
                    buttonImageid = Image_Enum.I_healHand;
                    break;
                }
                case BottomButton_Enum.SkillAction: {
                    text = "Activate Skill";
                    color = CNAColor.ActionSkill;
                    break;
                }
                case BottomButton_Enum.GameEffectAction: {
                    text = "Activate";
                    color = CNAColor.ActionGameEffect;
                    break;
                }
                case BottomButton_Enum.Learn: {
                    text = "Learn";
                    color = CNAColor.ActionLearn;
                    break;
                }
                case BottomButton_Enum.Train: {
                    text = "Train (6)";
                    color = CNAColor.ActionTrain;
                    break;
                }
                case BottomButton_Enum.Adventure_AncientRuins:
                case BottomButton_Enum.Adventure_Dungeon:
                case BottomButton_Enum.Adventure_Tomb:
                case BottomButton_Enum.Adventure_MonsterDen:
                case BottomButton_Enum.Adventure_SpawningGround: {
                    text = "Adventure";
                    color = CNAColor.Adventure;
                    break;
                }
                case BottomButton_Enum.Adventure_VillageHeal: {
                    text = "Heal (3)";
                    buttonImageid = Image_Enum.I_healHand;
                    color = CNAColor.Adventure_VillageHeal;
                    break;
                }
                case BottomButton_Enum.Adventure_VillageRaid: {
                    text = "Raid (-1)";
                    buttonImageid = Image_Enum.I_angry;
                    color = CNAColor.Adventure_VillageRaid;
                    break;
                }
                case BottomButton_Enum.Adventure_MonasteryHeal: {
                    text = "Heal (2)";
                    buttonImageid = Image_Enum.I_healHand;
                    color = CNAColor.Adventure_VillageHeal;
                    break;
                }
                case BottomButton_Enum.Adventure_MonasteryBurn: {
                    text = "Raid (-3)";
                    buttonImageid = Image_Enum.I_angry;
                    color = CNAColor.Adventure_VillageRaid;
                    break;
                }
                case BottomButton_Enum.CityRed_Artifact: {
                    text = "Artifact (12)";
                    color = CNAColor.CityRed_Artifact;
                    break;
                }
                case BottomButton_Enum.CityWhite_UnitAdd: {
                    text = "Add Unit (2)";
                    color = CNAColor.CityWhite_UnitAdd;
                    break;
                }
                case BottomButton_Enum.LearnSpell: {
                    text = "Spell (7 + mana)";
                    color = CNAColor.LearnSpell;
                    break;
                }
                case BottomButton_Enum.CityGreen_LearnAction: {
                    text = "Add Unit (6)";
                    color = CNAColor.CityGreen_LearnAction;
                    break;
                }
            }

            button.SetupUI(text, color, buttonImageid, isActive);
        }

        public void OnClick_LeftButton() {
            B = B1;
            ButtonClicked = leftButton;
            OnClick();
        }
        public void OnClick_RightButton() {
            B = B2;
            ButtonClicked = rightButton;
            OnClick();
        }
        public void OnClick_CenterButton() {
            B = B1;
            ButtonClicked = centerButton;
            OnClick();
        }

        private void OnClick() {
            switch (B) {
                case BottomButton_Enum.Heal: {
                    A.OnClick_HealWound();
                    break;
                }
                case BottomButton_Enum.Discard: {
                    A.OnClick_ActionDiscardCard();
                    break;
                }
                case BottomButton_Enum.Basic: {
                    A.OnClick_ActionBasicCard();
                    break;
                }
                case BottomButton_Enum.Recruit: {
                    A.OnClick_Recruit();
                    break;
                }
                case BottomButton_Enum.Recruit_BondsOfLoyalty: {
                    A.OnClick_Recruit_BondsOfLoyalty();
                    break;
                }
                case BottomButton_Enum.Disband: {
                    A.OnClick_Disband();
                    break;
                }
                case BottomButton_Enum.Explore: {
                    A.OnClick_Explore();
                    break;
                }
                case BottomButton_Enum.Provoke: {
                    A.OnClick_ProvokeMonster();
                    break;
                }
                case BottomButton_Enum.HealUnit: {
                    A.OnClick_HealUnit();
                    break;
                }
                case BottomButton_Enum.SkillAction: {
                    A.OnClick_ActionSkillButton();
                    break;
                }
                case BottomButton_Enum.GameEffectAction: {
                    A.OnClick_ActionGameEffect();
                    break;
                }
                case BottomButton_Enum.Learn: {
                    A.OnClick_Learn();
                    break;
                }
                case BottomButton_Enum.Train: {
                    A.OnClick_Train();
                    break;
                }
                case BottomButton_Enum.Adventure_Dungeon: {
                    A.OnClick_AdventureDungeon();
                    break;
                }
                case BottomButton_Enum.Adventure_Tomb: {
                    A.OnClick_AdventureTomb();
                    break;
                }
                case BottomButton_Enum.Adventure_SpawningGround: {
                    A.OnClick_AdventureSpawningGround();
                    break;
                }
                case BottomButton_Enum.Adventure_MonsterDen: {
                    A.OnClick_AdventureMonsterDen();
                    break;
                }
                case BottomButton_Enum.Adventure_VillageHeal: {
                    A.OnClick_VillageHeal();
                    break;
                }
                case BottomButton_Enum.Adventure_VillageRaid: {
                    A.OnClick_VillageRaid();
                    break;
                }
                case BottomButton_Enum.Adventure_MonasteryHeal: {
                    A.OnClick_MonasteryHeal();
                    break;
                }
                case BottomButton_Enum.Adventure_MonasteryBurn: {
                    A.OnClick_MonasteryBurn();
                    break;
                }
                case BottomButton_Enum.CityRed_Artifact: {
                    A.OnClick_CityRed_Artifact();
                    break;
                }
                case BottomButton_Enum.CityWhite_UnitAdd: {
                    A.OnClick_CityWhite_UnitAdd();
                    break;
                }
                case BottomButton_Enum.CityGreen_LearnAction: {
                    A.OnClick_CityGreen_LearnAction();
                    break;
                }
                case BottomButton_Enum.LearnSpell: {
                    A.OnClick_LearnSpell();
                    break;
                }
                case BottomButton_Enum.Adventure_AncientRuins: {
                    A.OnClick_AdventureAncientRuins();
                    break;
                }
            }
        }

        public void Shake() {
            ButtonClicked.ShakeButton();
        }
    }
}
