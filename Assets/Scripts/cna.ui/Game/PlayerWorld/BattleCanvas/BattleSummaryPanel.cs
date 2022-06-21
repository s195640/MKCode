using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace cna.ui {
    public class BattleSummaryPanel : MonoBehaviour {

        [SerializeField] private TextMeshProUGUI TotalMonsters;
        [SerializeField] private TextMeshProUGUI MonstersKilled;
        [SerializeField] private TextMeshProUGUI FameGained;
        [SerializeField] private TextMeshProUGUI RepChange;

        [SerializeField] private int totalMonsters;
        [SerializeField] private int dead;
        [SerializeField] private int fame;
        [SerializeField] private int rep;

        [SerializeField] private Tilemap StructureTilemap;


        public void UpdateUI() {
            totalMonsters = 0;
            dead = 0;
            fame = 0;
            rep = 0;
            bool negStructure = false;
            D.LocalPlayer.Battle.Monsters.Values.ForEach(md => {
                CardMonsterVO m = (CardMonsterVO)D.Cards[md.Uniqueid];
                if (!md.Summoned) {
                    totalMonsters++;
                }
                if (md.Dead) {
                    dead++;
                    fame += m.MonsterFame;
                    if (md.Structure == Image_Enum.SH_MaraudingOrcs) {
                        rep++;
                    } else if (md.Structure == Image_Enum.SH_Draconum) {
                        rep += 2;
                    }
                }
                if (md.FortifiedStructure) {
                    negStructure = true;
                }
            });
            if (negStructure) {
                rep--;
            }
            D.LocalPlayer.GameEffects.Keys.ForEach(ge => {
                switch (ge) {
                    case GameEffect_Enum.CT_SwordOfJustice01:
                    case GameEffect_Enum.CT_SwordOfJustice02: { fame += dead; break; }
                }
            });

            TotalMonsters.text = "" + totalMonsters;
            MonstersKilled.text = "" + dead;
            FameGained.text = "" + fame;
            RepChange.text = "" + rep;
        }

        public void ExitBattle(GameAPI ar) {
            CNAMap<V2IntVO, WrapList<int>> monsterData = ar.P.Board.MonsterData;
            //  determine the Conflict Structure Site
            Image_Enum structure = Image_Enum.NA;
            ar.P.Battle.Monsters.Values.ForEach(md => {
                if (md.Structure != Image_Enum.NA && md.Structure != Image_Enum.SH_MaraudingOrcs && md.Structure != Image_Enum.SH_Draconum) {
                    structure = md.Structure;
                }
            });
            //  has adventure site been conquered
            bool alreadyConquered = BasicUtil.getAllShieldsAtPos(D.G, ar.P.CurrentGridLoc).Count > 0;
            //  clear monsters
            bool adventureSiteCleared = true;
            ar.P.Battle.Monsters.Values.ForEach(md => {
                if (md.Dead) {
                    if (md.CityStructure) {
                        //  City you can add tokens without beating the city
                        ar.AddShieldLocation(md.Location);
                    }
                    if (monsterData.ContainsKey(md.Location)) {
                        monsterData[md.Location].Remove(md.Uniqueid);
                        if (monsterData[md.Location].Count == 0) {
                            monsterData.Remove(md.Location);
                        }
                    }
                } else {
                    if (md.Structure != Image_Enum.NA && md.Structure != Image_Enum.SH_MaraudingOrcs && md.Structure != Image_Enum.SH_Draconum) {
                        adventureSiteCleared = false;
                    }
                }
            });


            //  Grant Rewards 
            if (adventureSiteCleared) {
                switch (structure) {
                    case Image_Enum.SH_Keep: {
                        ar.AddShieldLocation();
                        ar.AddGameEffect(GameEffect_Enum.SH_Keep_Own);
                        BasicUtil.CalculatePlayerHandBonus(ar.P);
                        break;
                    }
                    case Image_Enum.SH_City_Blue: {
                        ar.AddGameEffect(GameEffect_Enum.SH_City_Blue_Own);
                        BasicUtil.CalculatePlayerHandBonus(ar.P);
                        break;
                    }
                    case Image_Enum.SH_City_Red: {
                        ar.AddGameEffect(GameEffect_Enum.SH_City_Red_Own);
                        BasicUtil.CalculatePlayerHandBonus(ar.P);
                        break;
                    }
                    case Image_Enum.SH_City_Green: {
                        ar.AddGameEffect(GameEffect_Enum.SH_City_Green_Own);
                        BasicUtil.CalculatePlayerHandBonus(ar.P);
                        break;
                    }
                    case Image_Enum.SH_City_White: {
                        ar.AddGameEffect(GameEffect_Enum.SH_City_White_Own);
                        BasicUtil.CalculatePlayerHandBonus(ar.P);
                        break;
                    }
                }
                if (!alreadyConquered) {
                    switch (structure) {
                        case Image_Enum.SH_MageTower: {
                            ar.AddShieldLocation();
                            Reward_Spell(1);
                            ar.AddLog("[Reward] +1 Spell");
                            break;
                        }
                        case Image_Enum.SH_AncientRuins: {
                            ar.AddShieldLocation();
                            List<string> msg = new List<string>() { "[Reward] " };
                            D.Cards[monsterData[ar.P.CurrentGridLoc].Values[0]].Rewards.ForEach(r => {
                                switch (r) {
                                    case Reward_Enum.Action: { msg.Add("+1 Action"); Reward_Advanced(1); break; }
                                    case Reward_Enum.Spell: { msg.Add("+1 Spell"); Reward_Spell(1); break; }
                                    case Reward_Enum.Unit: { msg.Add("+1 Unit"); Reward_Unit(1); break; }
                                    case Reward_Enum.Artifact: { msg.Add("+1 Artifact"); Reward_Artifact(1); break; }
                                    case Reward_Enum.Blue: { msg.Add("+1 Blue Crystal"); Reward_Blue(1); break; }
                                    case Reward_Enum.Red: { msg.Add("+1 Red Crystal"); Reward_Red(1); break; }
                                    case Reward_Enum.Green: { msg.Add("+1 Green Crystal"); Reward_Green(1); break; }
                                    case Reward_Enum.White: { msg.Add("+1 White Crystal"); Reward_White(1); break; }
                                }
                            });
                            ar.AddLog(string.Join(", ", msg));
                            monsterData.Remove(ar.P.CurrentGridLoc);
                            break;
                        }
                        case Image_Enum.SH_Dungeon: {
                            ar.AddShieldLocation();
                            Reward_Artifact(1);
                            ar.AddLog("[Reward] +1 Artifact");
                            break;
                        }
                        case Image_Enum.SH_Tomb: {
                            ar.AddShieldLocation();
                            Reward_Artifact(1);
                            Reward_Spell(1);
                            ar.AddLog("[Reward] +1 Spell, +1 Artifact");
                            break;
                        }
                        case Image_Enum.SH_MonsterDen: {
                            ar.AddShieldLocation();
                            Reward_Random(2);
                            ar.AddLog("[Reward] +2 Random Crystals");
                            break;
                        }
                        case Image_Enum.SH_SpawningGround: {
                            ar.AddShieldLocation();
                            Reward_Random(3);
                            Reward_Artifact(1);
                            ar.AddLog("[Reward] +3 Random Crystals, +1 Artifact");
                            break;
                        }
                        case Image_Enum.SH_Monastery: {
                            ar.AddShieldLocation();
                            ar.P.Board.MonasteryCount--;
                            Reward_Artifact(1);
                            ar.AddLog("[Reward] +1 Artifact");
                            break;
                        }
                    }
                }
            } else {
                if (structure == Image_Enum.SH_SpawningGround) {
                    if (monsterData[ar.P.CurrentGridLoc].Count == 1) {
                        monsterData[ar.P.CurrentGridLoc].Add(D.Scenario.DrawMonster(MonsterType_Enum.Brown));
                    }
                }
            }

            //  determine if one Safe Space or need to force withdraw
            if (structure == Image_Enum.SH_Keep || structure == Image_Enum.SH_MageTower || structure == Image_Enum.SH_City_Blue || structure == Image_Enum.SH_City_Green || structure == Image_Enum.SH_City_White || structure == Image_Enum.SH_City_Red) {
                if (monsterData.ContainsKey(ar.P.CurrentGridLoc) && monsterData[ar.P.CurrentGridLoc].Values.Count > 0) {
                    ar.P.GridLocationHistory.RemoveAt(0);
                    BasicUtil.UpdateMovementGameEffects(ar.P);
                    ar.AddLog("You were forced to retreat from the space you are on.");
                }
            }


            //  Fame
            ar.Fame(fame);
            int oldLevel = BasicUtil.GetPlayerLevel(ar.P.Fame.X);
            int newLevel = BasicUtil.GetPlayerLevel(ar.P.TotalFame);
            if (newLevel > oldLevel) {
                Reward_LevelUp(1);
            }
            //  Rep
            ar.Rep(rep);
            ar.AddLog(string.Format("[Battle Results] - Total Monsters {0}, Monsters Killed {1}, Fame {2}, Rep {3}.", totalMonsters, dead, fame, rep));
        }

        #region Reward
        public void AddGameEffect(GameEffect_Enum ge, params int[] cards) {
            D.LocalPlayer.AddGameEffect(ge, cards);
            D.B.UpdateMonsterDetails();
        }

        public void Reward_Add(int blue, int red, int green, int white, int random, int artifact, int spell, int advanced, int unit, int level) {
            if (!D.LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.Rewards)) {
                AddGameEffect(GameEffect_Enum.Rewards, blue, red, green, white, random, 0, 0, artifact, spell, advanced, unit, level);
            } else {
                List<int> ged = D.LocalPlayer.GameEffects[GameEffect_Enum.Rewards].Values;
                ged[0] += blue;
                ged[1] += red;
                ged[2] += green;
                ged[3] += white;
                ged[4] += random;
                ged[7] += artifact;
                ged[8] += spell;
                ged[9] += advanced;
                ged[10] += unit;
                ged[11] += level;
            }
        }

        public void Reward_Blue(int val) {
            Reward_Add(val, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }
        public void Reward_Red(int val) {
            Reward_Add(0, val, 0, 0, 0, 0, 0, 0, 0, 0);
        }
        public void Reward_Green(int val) {
            Reward_Add(0, 0, val, 0, 0, 0, 0, 0, 0, 0);
        }
        public void Reward_White(int val) {
            Reward_Add(0, 0, 0, val, 0, 0, 0, 0, 0, 0);
        }
        public void Reward_Random(int val) {
            Reward_Add(0, 0, 0, 0, val, 0, 0, 0, 0, 0);
        }
        public void Reward_Artifact(int val) {
            Reward_Add(0, 0, 0, 0, 0, val, 0, 0, 0, 0);
        }
        public void Reward_Spell(int val) {
            Reward_Add(0, 0, 0, 0, 0, 0, val, 0, 0, 0);
        }
        public void Reward_Advanced(int val) {
            Reward_Add(0, 0, 0, 0, 0, 0, 0, val, 0, 0);
        }
        public void Reward_Unit(int val) {
            Reward_Add(0, 0, 0, 0, 0, 0, 0, 0, val, 0);
        }
        public void Reward_LevelUp(int val) {
            Reward_Add(0, 0, 0, 0, 0, 0, 0, 0, 0, val);
        }
        #endregion
    }
}
