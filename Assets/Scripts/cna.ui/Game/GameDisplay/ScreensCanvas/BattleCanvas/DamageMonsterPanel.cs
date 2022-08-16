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

        public void UpdateUI(GameAPI ar) {
            CalculateBattleResults(ar);
            DisplayBattleResults(ar);
        }

        public void DisplayBattleResults(GameAPI ar) {
            avatarArmorText[0].text = "" + avatarArmor;
            avatarArmorText[1].text = "" + avatarArmor;
            if (unitIdUsedForCombat > 0) {
                defendingUnitCardSlot.gameObject.SetActive(true);
                defendingUnitCardSlot.SetupUI(ar.P, unitIdUsedForCombat, CardHolder_Enum.AssignDamageContent);
            } else {
                defendingUnitCardSlot.gameObject.SetActive(false);
            }
            if (ar.P.Battle.SelectedMonsters.Count > 0) {
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

        public void CalculateBattleResults(GameAPI ar) {
            Clear();
            unitIdUsedForCombat = ar.P.Battle.SelectedUnit;
            if (ar.P.Battle.SelectedMonsters.Count > 0) {
                avatarArmor = ar.P.Armor;
                monsterId = ar.P.Battle.SelectedMonsters[0];
                MonsterDetailsVO MonsterDetails = D.B.MonsterDetails[monsterId];
                avatarDamage = MonsterDetails.Brutal ? MonsterDetails.Damage * 2 : MonsterDetails.Damage;
                if (unitIdUsedForCombat > 0) {
                    CardUnitVO unitCard = (CardUnitVO)D.Cards[unitIdUsedForCombat];
                    bool resistPhysical = unitCard.UnitResistance.Contains(Image_Enum.I_resistphysical);
                    bool resistFire = unitCard.UnitResistance.Contains(Image_Enum.I_resistfire);
                    bool resistIce = unitCard.UnitResistance.Contains(Image_Enum.I_resistice);
                    int unitArmor = unitCard.UnitArmor;

                    //  Add Game Effects & Banners
                    if (ar.P.Deck.Banners.ContainsKey(unitIdUsedForCombat)) {
                        int bannerUniqueId = ar.P.Deck.Banners[unitIdUsedForCombat];
                        CardVO banner = D.Cards.Find(c => c.UniqueId == bannerUniqueId);
                        if (banner.CardImage == Image_Enum.CT_banner_of_glory) {
                            unitArmor++;
                        } else if (banner.CardImage == Image_Enum.CT_banner_of_protection) {
                            unitArmor++;
                            resistFire = true;
                            resistIce = true;
                        }
                    }

                    ar.P.GameEffects.Keys.ForEach(ge => {
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
                    if ((MonsterDetails.NormalAttack && resistPhysical) ||
                        (MonsterDetails.ColdFireAttack && resistFire && resistIce) ||
                       (MonsterDetails.FireAttack && resistFire) ||
                       (MonsterDetails.ColdFireAttack && resistIce)) {
                        avatarDamage -= unitArmor;
                    }
                    if (avatarDamage > 0) {
                        unitWounded = true;
                        avatarDamage -= unitArmor;
                        unitPoisoned = MonsterDetails.Poison;
                        unitParalyzed = MonsterDetails.Paralyze;
                    }
                }
                if (avatarDamage > 0) {
                    avatarWounds = avatarDamage / avatarArmor;
                    if (avatarWounds * avatarArmor < avatarDamage) {
                        avatarWounds++;
                    }
                    avatarPoisoned = MonsterDetails.Poison;
                    avatarParalyzed = MonsterDetails.Paralyze;
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


        public string AssignDamage(GameAPI ar) {
            CardMonsterVO monsterCard = (CardMonsterVO)D.Cards[monsterId];
            string msg = "[Damage " + monsterCard.CardTitle + "] :: ";

            PlayerDeckData deck = ar.P.Deck;
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
                    deck.Hand.Add(ar.DrawWoundCard());
                }
                if (avatarPoisoned) {
                    avatarMsg += ", is poisoned +" + avatarWounds + " wounds to discard";
                    for (int i = 0; i < avatarWounds; i++) {
                        int woundid = ar.DrawWoundCard();
                        deck.Hand.Add(woundid);
                        deck.AddState(woundid, CardState_Enum.Discard);
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
            BattleData battle = ar.P.Battle;
            battle.Monsters[monsterId].Assigned = true;
            battle.SelectedMonsters.Clear();
            battle.SelectedUnit = 0;
            return msg;
        }

        public string isAllowedToAssignDamage(GameAPI ar) {
            string msg = "";
            if (unitIdUsedForCombat > 0) {
                if (ar.P.Deck.State.ContainsKey(unitIdUsedForCombat)) {
                    bool wounded = ar.P.Deck.State[unitIdUsedForCombat].Contains(CardState_Enum.Unit_Wounded);
                    bool paralyzed = ar.P.Deck.State[unitIdUsedForCombat].Contains(CardState_Enum.Unit_Paralyzed);
                    bool usedInBattle = ar.P.Deck.State[unitIdUsedForCombat].Contains(CardState_Enum.Unit_UsedInBattle);
                    
                    if (wounded) {
                        msg = "Can not assign damage to Wounded units!";
                    } else if (paralyzed) {
                        msg = "Can not assign damage to Paralyzed units!";
                    } else if (usedInBattle) {
                        msg = "This unit has already been used in this combat, you can not assign damage to this unit again!";
                    }
                } else {
                    bool intoTheHeat = ar.P.GameEffects.ContainsKeyAny(GameEffect_Enum.AC_IntoTheHeat01, GameEffect_Enum.AC_IntoTheHeat02);
                    if (intoTheHeat) {
                        msg = "Into The Heat effect is in play, you can not assign damage to any unit!";
                    }
                }
            }
            return msg;
        }
    }
}
