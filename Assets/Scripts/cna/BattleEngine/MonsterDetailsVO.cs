using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class MonsterDetailsVO {

        public CardVO monsterCard;
        public CNAMap<GameEffect_Enum, WrapList<int>> gameEffects;

        private int armor;
        private int damage;
        private int fame;

        private bool normalAttack;
        private bool fireAttack;
        private bool coldAttack;
        private bool coldFireAttack;

        private bool fireResistance;
        private bool iceResistance;
        private bool physicalResistance;

        private bool brutal;
        private bool paralyze;
        private bool poison;
        private bool swiftness;
        private bool summoner;
        private bool fortified;
        private bool doubleFortified;

        public int UniqueId { get => monsterCard.UniqueId; }
        public string CardTitle { get => monsterCard.CardTitle; }
        public Image_Enum MonsterImage { get => monsterCard.CardImage; }
        public Image_Enum MonsterImageBack { get => monsterCard.MonsterBackCardId; }
        public int Armor { get => armor; set => armor = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Fame { get => fame; set => fame = value; }
        public bool NormalAttack { get => normalAttack; set => normalAttack = value; }
        public bool FireAttack { get => fireAttack; set => fireAttack = value; }
        public bool ColdAttack { get => coldAttack; set => coldAttack = value; }
        public bool ColdFireAttack { get => coldFireAttack; set => coldFireAttack = value; }
        public bool FireResistance { get => fireResistance; set => fireResistance = value; }
        public bool IceResistance { get => iceResistance; set => iceResistance = value; }
        public bool PhysicalResistance { get => physicalResistance; set => physicalResistance = value; }
        public bool Brutal { get => brutal; set => brutal = value; }
        public bool Paralyze { get => paralyze; set => paralyze = value; }
        public bool Poison { get => poison; set => poison = value; }
        public bool Swiftness { get => swiftness; set => swiftness = value; }
        public bool Summoner { get => summoner; set => summoner = value; }
        public bool Fortified { get => fortified; set => fortified = value; }
        public bool DoubleFortified { get => doubleFortified; set => doubleFortified = value; }
        public UnitEffect_Enum AttackType {
            get {
                if (FireAttack) {
                    return UnitEffect_Enum.FireAttack;
                } else if (ColdAttack) {
                    return UnitEffect_Enum.ColdAttack;
                } else if (ColdFireAttack) {
                    return UnitEffect_Enum.ColdFireAttack;
                } else if (Summoner) {
                    return UnitEffect_Enum.Summoner;
                } else {
                    return UnitEffect_Enum.None;
                }
            }
        }


        public int TotalSymbols {
            get {
                int total = 0;
                if (fireAttack) total++;
                if (coldAttack) total++;
                if (coldFireAttack) total++;
                if (fireResistance) total++;
                if (iceResistance) total++;
                if (physicalResistance) total++;
                if (brutal) total++;
                if (paralyze) total++;
                if (poison) total++;
                if (swiftness) total++;
                if (fortified) total++;
                if (doubleFortified) total++;
                return total;
            }
        }
        public MonsterDetailsVO() { }

        public MonsterDetailsVO(CardVO monsterCard, CNAMap<GameEffect_Enum, WrapList<int>> gameEffects) {
            this.monsterCard = monsterCard;
            this.gameEffects = gameEffects;
            Calculate();
        }

        public void UpdateBattleEffects(CNAMap<GameEffect_Enum, WrapList<int>> gameEffects) {
            this.gameEffects = gameEffects;
            Calculate();
        }

        public void Calculate() {
            if (monsterCard.CardType == CardType_Enum.Monster) {
                armor = monsterCard.MonsterArmor;
                damage = monsterCard.MonsterDamage;
                fame = monsterCard.MonsterFame;
                monsterCard.MonsterEffects.ForEach(me => {
                    switch (me) {
                        case UnitEffect_Enum.FireResistance: {
                            fireResistance = true;
                            break;
                        }
                        case UnitEffect_Enum.IceResistance: {
                            iceResistance = true;
                            break;
                        }
                        case UnitEffect_Enum.PhysicalResistance: {
                            physicalResistance = true;
                            break;
                        }
                        case UnitEffect_Enum.ColdFireAttack: {
                            coldFireAttack = true;
                            break;
                        }
                        case UnitEffect_Enum.FireAttack: {
                            fireAttack = true;
                            break;
                        }
                        case UnitEffect_Enum.ColdAttack: {
                            coldAttack = true;
                            break;
                        }
                        case UnitEffect_Enum.Brutal: {
                            brutal = true;
                            break;
                        }
                        case UnitEffect_Enum.Paralyze: {
                            paralyze = true;
                            break;
                        }
                        case UnitEffect_Enum.Poison: {
                            poison = true;
                            break;
                        }
                        case UnitEffect_Enum.Swiftness: {
                            swiftness = true;
                            break;
                        }
                        case UnitEffect_Enum.Fortified: {
                            fortified = true;
                            break;
                        }
                        case UnitEffect_Enum.Summoner: {
                            summoner = true;
                            break;
                        }
                        case UnitEffect_Enum.DoubleFortified: {
                            fortified = true;
                            doubleFortified = true;
                            break;
                        }
                    }
                });
                normalAttack = AttackType == UnitEffect_Enum.None;
                Calculate_BattleEffects();
            }
        }

        public void Calculate_BattleEffects() {
            foreach (GameEffect_Enum ge in gameEffects.Keys) {
                List<int> monsters = gameEffects[ge].Values;
                monsters.ForEach(m => {
                    if (m == UniqueId) {
                        bool isDemolish = gameEffects.ContainsKey(GameEffect_Enum.CS_Demolish) && gameEffects[GameEffect_Enum.CS_Demolish].Contains(m);
                        bool isUndergroundAttackFort = gameEffects.ContainsKey(GameEffect_Enum.CS_UndergroundAttackFort);
                        switch (ge) {
                            case GameEffect_Enum.SH_Keep: {
                                if (!isDemolish && !isUndergroundAttackFort)
                                    addFortificationLevel();
                                break;
                            }
                            case GameEffect_Enum.SH_MageTower: {
                                if (!isDemolish && !isUndergroundAttackFort)
                                    addFortificationLevel();
                                break;
                            }
                            case GameEffect_Enum.SH_City_Blue: {
                                if (!isDemolish && !isUndergroundAttackFort)
                                    addFortificationLevel();
                                if (ColdFireAttack) {
                                    Damage += 1;
                                } else if (ColdAttack || FireAttack) {
                                    Damage += 2;
                                }
                                break;
                            }
                            case GameEffect_Enum.SH_City_Green: {
                                if (!isDemolish && !isUndergroundAttackFort)
                                    addFortificationLevel();
                                if (NormalAttack) {
                                    Poison = true;
                                }
                                break;
                            }
                            case GameEffect_Enum.SH_City_Red: {
                                if (!isDemolish && !isUndergroundAttackFort)
                                    addFortificationLevel();
                                if (NormalAttack) {
                                    Brutal = true;
                                }
                                break;
                            }
                            case GameEffect_Enum.SH_City_White: {
                                if (!isDemolish && !isUndergroundAttackFort)
                                    addFortificationLevel();
                                Armor++;
                                break;
                            }
                            case GameEffect_Enum.BLUE_ResistanceBreak: {
                                if (Armor > 1 && PhysicalResistance) {
                                    Armor--;
                                }
                                if (Armor > 1 && FireResistance) {
                                    Armor--;
                                }
                                if (Armor > 1 && IceResistance) {
                                    Armor--;
                                }
                                break;
                            }
                            case GameEffect_Enum.AC_IceShield: {
                                if (Armor > 1) {
                                    Armor -= 3;
                                    if (Armor < 1) {
                                        Armor = 1;
                                    }
                                }
                                break;
                            }
                            case GameEffect_Enum.CS_Expose: {
                                FireResistance = false;
                                IceResistance = false;
                                PhysicalResistance = false;
                                Fortified = false;
                                DoubleFortified = false;
                                break;
                            }
                            case GameEffect_Enum.CS_MassExpose01: {
                                Fortified = false;
                                DoubleFortified = false;
                                break;
                            }
                            case GameEffect_Enum.CS_MassExpose02: {
                                FireResistance = false;
                                IceResistance = false;
                                PhysicalResistance = false;
                                break;
                            }
                            case GameEffect_Enum.CS_Tremor01: {
                                Armor -= 3;
                                if (Armor < 1) {
                                    Armor = 1;
                                }
                                break;
                            }
                            case GameEffect_Enum.CS_Tremor02: {
                                Armor -= 2;
                                if (Armor < 1) {
                                    Armor = 1;
                                }
                                break;
                            }
                            case GameEffect_Enum.CS_Earthquake01: {
                                int val = Fortified ? 6 : 3;
                                Armor -= val;
                                if (Armor < 1) {
                                    Armor = 1;
                                }
                                break;
                            }
                            case GameEffect_Enum.CS_Earthquake02: {
                                int val = Fortified ? 4 : 2;
                                Armor -= val;
                                if (Armor < 1) {
                                    Armor = 1;
                                }
                                break;
                            }
                            case GameEffect_Enum.CS_Chill: {
                                FireResistance = false;
                                break;
                            }
                            case GameEffect_Enum.CS_LethalChill: {
                                Armor -= 4;
                                if (Armor < 1) {
                                    Armor = 1;
                                }
                                break;
                            }
                            case GameEffect_Enum.CS_Disintegrate: {
                                Armor--;
                                if (Armor < 1) {
                                    Armor = 1;
                                }
                                break;
                            }
                            case GameEffect_Enum.CS_Demolish: {
                                Armor--;
                                if (Armor < 1) {
                                    Armor = 1;
                                }
                                break;
                            }
                            case GameEffect_Enum.CT_SwordOfJustice02: {
                                PhysicalResistance = false;
                                break;
                            }
                            case GameEffect_Enum.CUE_AmotepFreezers: {
                                Armor -= 3;
                                if (Armor < 1) {
                                    Armor = 1;
                                }
                                break;
                            }
                        }
                    }
                });
            }
        }

        private void addFortificationLevel() {
            if (doubleFortified) { } else if (fortified) { doubleFortified = true; } else { fortified = true; }
        }

        public static MonsterDetailsVO operator +(MonsterDetailsVO a, MonsterDetailsVO b) {
            MonsterDetailsVO md = new MonsterDetailsVO();
            md.Armor = a.Armor + b.Armor;
            md.Fame = a.Fame + b.Fame;
            md.FireResistance = a.FireResistance || b.FireResistance;
            md.IceResistance = a.IceResistance || b.IceResistance;
            md.PhysicalResistance = a.PhysicalResistance || b.PhysicalResistance;
            md.Fortified = a.Fortified || b.Fortified;
            md.DoubleFortified = a.DoubleFortified || b.DoubleFortified;
            return md;
        }

    }
}
