using cna.poo;

namespace cna {
    public partial class BattleVersatilityVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            switch (ar.P.Battle.BattlePhase) {
                case BattlePhase_Enum.Attack: {
                    ar.BattleAttack(new AttackData(2));
                    break;
                }
                case BattlePhase_Enum.Block: {
                    ar.BattleBlock(new AttackData(2));
                    break;
                }
                case BattlePhase_Enum.RangeSiege: {
                    ar.BattleRange(new AttackData(1));
                    break;
                }
            }
            return ar;
        }
        public override void ActionPaymentComplete_01(GameAPI ar) {
            switch (ar.P.Battle.BattlePhase) {
                case BattlePhase_Enum.Attack: {
                    ar.SelectOptions(acceptCallback_01,
                        new OptionVO("Attack 4", Image_Enum.I_attack),
                        new OptionVO("Fire Attack 4", Image_Enum.I_attack),
                        new OptionVO("Range 3", Image_Enum.I_bow),
                        new OptionVO("Siege 2", Image_Enum.I_catapult)
                        );
                    break;
                }
                case BattlePhase_Enum.Block: {
                    ar.SelectOptions(acceptCallback_01,
                        new OptionVO("Block 4", Image_Enum.I_attack),
                        new OptionVO("Fire Block 4", Image_Enum.I_attack)
                        );
                    break;
                }
                case BattlePhase_Enum.RangeSiege:
                    ar.SelectOptions(acceptCallback_01,
                        new OptionVO("Range 3", Image_Enum.I_bow),
                        new OptionVO("Siege 2", Image_Enum.I_catapult)
                        );
                    break;
            }
        }


        public void acceptCallback_01(GameAPI ar) {
            switch (ar.P.Battle.BattlePhase) {
                case BattlePhase_Enum.Attack: {
                    switch (ar.SelectedButtonIndex) {
                        case 0: {
                            ar.BattleAttack(new AttackData(4 + ar.CardModifier));
                            break;
                        }
                        case 1: {
                            AttackData a = new AttackData();
                            a.Fire = 4 + ar.CardModifier;
                            ar.BattleAttack(a);
                            break;
                        }
                        case 2: {
                            ar.BattleRange(new AttackData(3 + ar.CardModifier));
                            break;
                        }
                        case 3: {
                            ar.BattleSiege(new AttackData(2 + ar.CardModifier));
                            break;
                        }
                    }
                    break;
                }
                case BattlePhase_Enum.Block: {
                    if (ar.SelectedButtonIndex == 0) {
                        ar.BattleBlock(new AttackData(4 + ar.CardModifier));
                    } else {
                        AttackData a = new AttackData();
                        a.Fire = 4 + ar.CardModifier;
                        ar.BattleBlock(a);
                    }
                    break;
                }
                case BattlePhase_Enum.RangeSiege:
                    if (ar.SelectedButtonIndex == 0) {
                        ar.BattleRange(new AttackData(3 + ar.CardModifier));
                    } else {
                        ar.BattleSiege(new AttackData(2 + ar.CardModifier));
                    }
                    break;
            }
            ar.FinishCallback(ar);
        }
    }

}