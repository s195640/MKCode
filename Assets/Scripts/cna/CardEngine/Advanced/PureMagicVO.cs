using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class PureMagicVO : CardActionVO {

        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("+4 Move", Image_Enum.I_crystal_green),
                new OptionVO("+4 Influence", Image_Enum.I_crystal_white),
                new OptionVO("+4 Block", Image_Enum.I_crystal_blue),
                new OptionVO("+4 Attack", Image_Enum.I_crystal_red)
                );
        }

        public void acceptCallback_00(GameAPI ar) {
            List<Crystal_Enum> cost = new List<Crystal_Enum>();
            switch (ar.SelectedButtonIndex) {
                case 0: { cost.Add(Crystal_Enum.Green); break; }
                case 1: { cost.Add(Crystal_Enum.White); break; }
                case 2: { cost.Add(Crystal_Enum.Blue); break; }
                case 3: { cost.Add(Crystal_Enum.Red); break; }
            }
            ar.PayForAction(cost, acceptCallback_00a);
        }
        public void acceptCallback_00a(GameAPI ar) {
            switch (ar.Payment[0].ManaUsedAs) {
                case Crystal_Enum.Green: { ar.ActionMovement(4); break; }
                case Crystal_Enum.White: { ar.ActionInfluence(4); break; }
                case Crystal_Enum.Blue: { ar.BattleBlock(new AttackData(4)); break; }
                case Crystal_Enum.Red: { ar.BattleAttack(new AttackData(4)); break; }
            }
            ar.FinishCallback(ar);
        }


        public override void ActionPaymentComplete_01(GameAPI ar) {
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("+7 Move", Image_Enum.I_crystal_green),
                new OptionVO("+7 Influence", Image_Enum.I_crystal_white),
                new OptionVO("+7 Block", Image_Enum.I_crystal_blue),
                new OptionVO("+7 Attack", Image_Enum.I_crystal_red)
                );
        }

        public void acceptCallback_01(GameAPI ar) {
            List<Crystal_Enum> cost = new List<Crystal_Enum>();
            switch (ar.SelectedButtonIndex) {
                case 0: { cost.Add(Crystal_Enum.Green); break; }
                case 1: { cost.Add(Crystal_Enum.White); break; }
                case 2: { cost.Add(Crystal_Enum.Blue); break; }
                case 3: { cost.Add(Crystal_Enum.Red); break; }
            }
            ar.PayForAction(cost, acceptCallback_01a);
        }
        public void acceptCallback_01a(GameAPI ar) {
            switch (ar.Payment[0].ManaUsedAs) {
                case Crystal_Enum.Green: { ar.ActionMovement(7 + ar.CardModifier); break; }
                case Crystal_Enum.White: { ar.ActionInfluence(7 + ar.CardModifier); break; }
                case Crystal_Enum.Blue: { ar.BattleBlock(new AttackData(7 + ar.CardModifier)); break; }
                case Crystal_Enum.Red: { ar.BattleAttack(new AttackData(7 + ar.CardModifier)); break; }
            }
            ar.FinishCallback(ar);
        }
    }
}
