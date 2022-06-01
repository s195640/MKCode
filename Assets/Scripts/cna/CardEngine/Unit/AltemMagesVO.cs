using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class AltemMagesVO : CardUnitVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("Blue Mana", Image_Enum.I_mana_blue),
                new OptionVO("Red Mana", Image_Enum.I_mana_red),
                new OptionVO("White Mana", Image_Enum.I_mana_white),
                new OptionVO("Green Mana", Image_Enum.I_mana_green)
                );
        }

        public void acceptCallback_00(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: { ar.ManaBlue(1); break; }
                case 1: { ar.ManaRed(1); break; }
                case 2: { ar.ManaWhite(1); break; }
                case 3: { ar.ManaGreen(1); break; }
            }
            ar.SelectOptions(acceptCallback_00a,
            new OptionVO("Blue Mana", Image_Enum.I_mana_blue),
            new OptionVO("Red Mana", Image_Enum.I_mana_red),
            new OptionVO("White Mana", Image_Enum.I_mana_white),
            new OptionVO("Green Mana", Image_Enum.I_mana_green)
            );
        }

        public void acceptCallback_00a(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: { ar.ManaBlue(1); break; }
                case 1: { ar.ManaRed(1); break; }
                case 2: { ar.ManaWhite(1); break; }
                case 3: { ar.ManaGreen(1); break; }
            }
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("Cold Fire +5", Image_Enum.I_attack_coldfire),
                new OptionVO("Cold Fire +7", Image_Enum.I_attack_coldfire),
                new OptionVO("Cold Fire +9", Image_Enum.I_attack_coldfire)
                );
        }

        public void acceptCallback_01(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    AttackData a = new AttackData();
                    a.ColdFire += 5;
                    if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                        ar.BattleBlock(a);
                    } else {
                        ar.BattleAttack(a);
                    }
                    ar.FinishCallback(ar);
                    break;
                }
                case 1: {
                    List<Crystal_Enum> cost = new List<Crystal_Enum>();
                    cost.Add(Crystal_Enum.Blue);
                    cost.Add(Crystal_Enum.Red);
                    ar.PayForAction(cost, acceptCallback_01a, false);
                    break;
                }
                case 2: {
                    List<Crystal_Enum> cost = new List<Crystal_Enum>();
                    cost.Add(Crystal_Enum.Blue);
                    cost.Add(Crystal_Enum.Red);
                    ar.PayForAction(cost, acceptCallback_01b);
                    break;
                }
            }
        }

        public void acceptCallback_01a(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.ColdFire += 7;
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(a);
            } else {
                ar.BattleAttack(a);
            }
            ar.FinishCallback(ar);
        }

        public void acceptCallback_01b(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.ColdFire += 9;
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(a);
            } else {
                ar.BattleAttack(a);
            }
            ar.FinishCallback(ar);
        }

        public override ActionResultVO ActionValid_02(ActionResultVO ar) {
            ar.AddGameEffect(GameEffect_Enum.CUE_AltemMages01);
            return ar;
        }
    }
}
