using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class DamageMonsterPanel : MonoBehaviour {
        [SerializeField] private ButtonContainer avatarButton;
        [SerializeField] private ButtonContainer assignDamageButton;
        [SerializeField] private AddressableImage[] avatarShields;
        [SerializeField] private TextMeshProUGUI[] avatarArmorText;
        [SerializeField] private AddressableImage avatarImage;
        [SerializeField] private NormalCardSlot defendingUnitCardSlot;

        [Header("BattleResults")]
        [SerializeField] private GameObject battleResults;
        [SerializeField] private GameObject unitText;
        [SerializeField] private GameObject noUnitText;
        [SerializeField] private GameObject unitDamageImages;
        [SerializeField] private GameObject unitBlood;
        [SerializeField] private GameObject unitPoison;
        [SerializeField] private GameObject unitParalyze;
        [SerializeField] private TextMeshProUGUI leftOverDamage;
        [SerializeField] private GameObject avatarNoDamage;
        [SerializeField] private GameObject avatarDamageImages;
        [SerializeField] private GameObject avatarBlood;
        [SerializeField] private GameObject avatarPoison;
        [SerializeField] private GameObject avatarParalyze;
        [SerializeField] private TextMeshProUGUI avatarBloodText;
        [SerializeField] private TextMeshProUGUI avatarPoisonText;

        [Header("Calculated Unit")]
        [SerializeField] private int unitIdUsedForCombat = 0;
        [SerializeField] private bool unitWounded = false;
        [SerializeField] private bool unitPoisoned = false;
        [SerializeField] private bool unitParalyzed = false;

        [Header("Calculated Avatar")]
        [SerializeField] private int monsterId = 0;
        [SerializeField] private int avatarArmor = 2;
        [SerializeField] private int avatarDamage = 0;
        [SerializeField] private int avatarWounds = 0;
        [SerializeField] private bool avatarPoisoned = false;
        [SerializeField] private bool avatarParalyzed = false;

        public void Start() {
            Image_Enum aImage = D.LocalPlayer.Avatar;
            avatarImage.ImageEnum = aImage;
            Image_Enum sImage = D.AvatarMetaDataMap[aImage].AvatarShieldId;
            avatarShields[0].ImageEnum = sImage;
            avatarShields[1].ImageEnum = sImage;
        }

        public void UpdateUI() {
            CalculateBattleResults();
            DisplayBattleResults();
        }

        public void DisplayBattleResults() {
            avatarArmorText[0].text = "" + avatarArmor;
            avatarArmorText[1].text = "" + avatarArmor;
            if (unitIdUsedForCombat > 0) {
                defendingUnitCardSlot.gameObject.SetActive(true);
                defendingUnitCardSlot.SetupUI(unitIdUsedForCombat, CardHolder_Enum.AssignDamageContent);
            } else {
                defendingUnitCardSlot.gameObject.SetActive(false);
            }
            if (D.LocalPlayer.Battle.SelectedMonsters.Count > 0) {
                battleResults.SetActive(true);
                if (unitIdUsedForCombat > 0) {
                    unitText.SetActive(true);
                    noUnitText.SetActive(false);
                    unitDamageImages.SetActive(true);
                    unitBlood.SetActive(unitWounded);
                    unitPoison.SetActive(unitPoisoned);
                    unitParalyze.SetActive(unitParalyzed);
                } else {
                    unitText.SetActive(false);
                    noUnitText.SetActive(true);
                    unitDamageImages.SetActive(false);
                }
                if (avatarDamage < 0) {
                    avatarDamage = 0;
                }
                leftOverDamage.text = "" + avatarDamage;
                if (avatarWounds > 0) {
                    avatarNoDamage.SetActive(false);
                    avatarDamageImages.SetActive(true);
                    avatarBlood.SetActive(true);
                    avatarBloodText.text = "" + avatarWounds + "x";
                    if (avatarPoisoned) {
                        avatarPoison.SetActive(true);
                        avatarPoisonText.text = "" + avatarWounds + "x";
                    } else {
                        avatarPoison.SetActive(false);
                    }
                    avatarParalyze.SetActive(avatarParalyzed);
                } else {
                    avatarDamageImages.SetActive(false);
                    avatarNoDamage.SetActive(true);
                }
                assignDamageButton.Active = true;
            } else {
                battleResults.SetActive(false);
                assignDamageButton.Active = false;
            }
        }

        public void CalculateBattleResults() {
            Clear();
            unitIdUsedForCombat = D.LocalPlayer.Battle.SelectedUnit;
            if (D.LocalPlayer.Battle.SelectedMonsters.Count > 0) {
                avatarArmor = D.LocalPlayer.Armor;
                monsterId = D.LocalPlayer.Battle.SelectedMonsters[0];
                CardMonsterVO monsterCard = (CardMonsterVO)D.Cards[monsterId];
                avatarDamage = monsterCard.MonsterEffects.Contains(UnitEffect_Enum.Brutal) ? monsterCard.MonsterDamage * 2 : monsterCard.MonsterDamage;
                if (unitIdUsedForCombat > 0) {
                    CardUnitVO unitCard = (CardUnitVO)D.Cards[unitIdUsedForCombat];
                    bool resistPhysical = unitCard.UnitResistance.Contains(Image_Enum.I_resistphysical);
                    bool resistFire = unitCard.UnitResistance.Contains(Image_Enum.I_resistfire);
                    bool resistIce = unitCard.UnitResistance.Contains(Image_Enum.I_resistice);
                    int unitArmor = unitCard.UnitArmor;

                    //  Add Game Effects & Banners
                    if (D.LocalPlayer.Deck.Banners.ContainsKey(unitIdUsedForCombat)) {
                        int bannerUniqueId = D.LocalPlayer.Deck.Banners[unitIdUsedForCombat];
                        CardVO banner = D.Cards.Find(c => c.UniqueId == bannerUniqueId);
                        if (banner.CardImage == Image_Enum.CT_banner_of_glory) {
                            unitArmor++;
                        } else if (banner.CardImage == Image_Enum.CT_banner_of_protection) {
                            unitArmor++;
                            resistFire = true;
                            resistIce = true;
                        }
                    }

                    D.LocalPlayer.GameEffects.Keys.ForEach(ge => {
                        switch (ge) {
                            case GameEffect_Enum.CT_BannerOfGlory: {
                                unitArmor++;
                                break;
                            }
                            case GameEffect_Enum.CUE_AltemGuardians02: {
                                resistPhysical = true;
                                resistFire = true;
                                resistIce = true;
                                break;
                            }
                        }
                    });


                    //  End
                    if ((monsterCard.isNormalAttack && resistPhysical) ||
                        (monsterCard.MonsterEffects.Contains(UnitEffect_Enum.ColdFireAttack) && resistFire && resistIce) ||
                       (monsterCard.MonsterEffects.Contains(UnitEffect_Enum.FireAttack) && resistFire) ||
                       (monsterCard.MonsterEffects.Contains(UnitEffect_Enum.ColdAttack) && resistIce)) {
                        avatarDamage -= unitArmor;
                    }
                    if (avatarDamage > 0) {
                        unitWounded = true;
                        avatarDamage -= unitArmor;
                        unitPoisoned = monsterCard.MonsterEffects.Contains(UnitEffect_Enum.Poison);
                        unitParalyzed = monsterCard.MonsterEffects.Contains(UnitEffect_Enum.Paralyze);
                    }
                }
                if (avatarDamage > 0) {
                    avatarWounds = avatarDamage / avatarArmor;
                    if (avatarWounds * avatarArmor < avatarDamage) {
                        avatarWounds++;
                    }
                    avatarPoisoned = monsterCard.MonsterEffects.Contains(UnitEffect_Enum.Poison);
                    avatarParalyzed = monsterCard.MonsterEffects.Contains(UnitEffect_Enum.Paralyze);
                }
            }
        }


        public void Clear() {
            unitIdUsedForCombat = 0;
            unitWounded = false;
            unitPoisoned = false;
            unitParalyzed = false;
            avatarDamage = 0;
            avatarWounds = 0;
            avatarPoisoned = false;
            avatarParalyzed = false;
        }


        public string AssignDamage() {
            CardMonsterVO monsterCard = (CardMonsterVO)D.Cards[monsterId];
            string msg = "[Damage " + monsterCard.CardTitle + "] :: ";

            PlayerDeckData deck = D.LocalPlayer.Deck;
            if (unitIdUsedForCombat > 0) {
                CardUnitVO unitCard = (CardUnitVO)D.Cards[unitIdUsedForCombat];
                string unitMsg = "(Unit " + unitCard.CardTitle;
                deck.AddState(unitIdUsedForCombat, CardState_Enum.Unit_UsedInBattle);
                if (unitWounded) {
                    deck.AddState(unitIdUsedForCombat, CardState_Enum.Unit_Wounded);
                    unitMsg += " is wounded";
                    if (unitPoisoned) {
                        deck.AddState(unitIdUsedForCombat, CardState_Enum.Unit_Poisoned);
                        unitMsg += ", is poisoned";
                    }
                    if (unitParalyzed) {
                        deck.AddState(unitIdUsedForCombat, CardState_Enum.Unit_Paralyzed);
                        unitMsg += ", is paralyzed";
                    }
                } else {
                    unitMsg += " resists no damage taken.";
                }
                unitMsg += ") ";
                msg += unitMsg;
            }
            string avatarMsg = "(Avatar ";
            if (avatarWounds > 0) {
                avatarMsg += " is wounded +" + avatarWounds + " wounds to hand";
                for (int i = 0; i < avatarWounds; i++) {
                    //deck.Hand.Add(D.Scenario.DrawWound());
                }
                if (avatarPoisoned) {
                    avatarMsg += ", is poisoned +" + avatarWounds + " wounds to discard";
                    for (int i = 0; i < avatarWounds; i++) {
                        //int woundid = D.Scenario.DrawWound();
                        //deck.Hand.Add(woundid);
                        //deck.AddState(woundid, CardState_Enum.Discard);
                    }
                }
                if (avatarParalyzed) {
                    avatarMsg += ", is paralyzed and discards all cards";
                    deck.Hand.ForEach(c => {
                        if (!deck.State.ContainsKey(c) && D.Cards[c].CardType != CardType_Enum.Wound) {
                            deck.AddState(c, CardState_Enum.Discard);
                        }
                    });
                }
            } else {
                avatarMsg += " takes no damage";
            }
            avatarMsg += ")";
            msg += avatarMsg;
            BattleData battle = D.LocalPlayer.Battle;
            battle.Monsters[monsterId].Assigned = true;
            battle.SelectedMonsters.Clear();
            battle.SelectedUnit = 0;
            return msg;
        }

        public string isAllowedToAssignDamage() {
            string msg = "";
            if (unitIdUsedForCombat > 0) {
                if (D.LocalPlayer.Deck.State.ContainsKey(unitIdUsedForCombat)) {
                    bool wounded = D.LocalPlayer.Deck.State[unitIdUsedForCombat].Contains(CardState_Enum.Unit_Wounded);
                    bool paralyzed = D.LocalPlayer.Deck.State[unitIdUsedForCombat].Contains(CardState_Enum.Unit_Paralyzed);
                    bool usedInBattle = D.LocalPlayer.Deck.State[unitIdUsedForCombat].Contains(CardState_Enum.Unit_UsedInBattle);
                    bool intoTheHeat = D.LocalPlayer.GameEffects.ContainsKeyAny(GameEffect_Enum.AC_IntoTheHeat01, GameEffect_Enum.AC_IntoTheHeat02);
                    if (wounded) {
                        msg = "Can not assign damage to Wounded units!";
                    } else if (paralyzed) {
                        msg = "Can not assign damage to Paralyzed units!";
                    } else if (usedInBattle) {
                        msg = "This unit has already been used in this combat, you can not assign damage to this unit again!";
                    } else if (intoTheHeat) {
                        msg = "Into The Heat effect is in play, you can not assign damage to any unit!";
                    }
                }

            }

            return msg;
        }
    }
}
