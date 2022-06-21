using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ManaBoltVO : CardSpellVO {

        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("Ice Attack 8", Image_Enum.I_crystal_blue),
                new OptionVO("Cold Fire Attack 7", Image_Enum.I_crystal_red),
                new OptionVO("Range Ice Attack 6", Image_Enum.I_crystal_white),
                new OptionVO("Siege Ice Attack 5", Image_Enum.I_crystal_green)
                );
        }

        public void acceptCallback_00(GameAPI ar) {
            List<Crystal_Enum> cost = new List<Crystal_Enum>();
            switch (ar.SelectedButtonIndex) {
                case 0: { cost.Add(Crystal_Enum.Blue); break; }
                case 1: { cost.Add(Crystal_Enum.Red); break; }
                case 2: { cost.Add(Crystal_Enum.White); break; }
                case 3: { cost.Add(Crystal_Enum.Green); break; }
            }
            ar.PayForAction(cost, acceptCallback_00a);
        }

        public void acceptCallback_00a(GameAPI ar) {
            AttackData a = new AttackData();
            switch (ar.Payment[0].ManaUsedAs) {
                case Crystal_Enum.Blue: { a.Cold = 8; ar.BattleAttack(a); break; }
                case Crystal_Enum.Red: { a.ColdFire = 7; ar.BattleAttack(a); break; }
                case Crystal_Enum.White: { a.Cold = 6; ar.BattleRange(a); break; }
                case Crystal_Enum.Green: { a.Cold = 5; ar.BattleSiege(a); break; }
            }
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(GameAPI ar) {
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("Ice Attack 11", Image_Enum.I_crystal_blue),
                new OptionVO("Cold Fire Attack 10", Image_Enum.I_crystal_red),
                new OptionVO("Range Ice Attack 9", Image_Enum.I_crystal_white),
                new OptionVO("Siege Ice Attack 8", Image_Enum.I_crystal_green)
                );
        }

        public void acceptCallback_01(GameAPI ar) {
            List<Crystal_Enum> cost = new List<Crystal_Enum>();
            switch (ar.SelectedButtonIndex) {
                case 0: { cost.Add(Crystal_Enum.Blue); break; }
                case 1: { cost.Add(Crystal_Enum.Red); break; }
                case 2: { cost.Add(Crystal_Enum.White); break; }
                case 3: { cost.Add(Crystal_Enum.Green); break; }
            }
            ar.PayForAction(cost, acceptCallback_01a);
        }

        public void acceptCallback_01a(GameAPI ar) {
            AttackData a = new AttackData();
            switch (ar.Payment[0].ManaUsedAs) {
                case Crystal_Enum.Blue: { a.Cold = 11; ar.BattleAttack(a); break; }
                case Crystal_Enum.Red: { a.ColdFire = 10; ar.BattleAttack(a); break; }
                case Crystal_Enum.White: { a.Cold = 9; ar.BattleRange(a); break; }
                case Crystal_Enum.Green: { a.Cold = 8; ar.BattleSiege(a); break; }
            }
            ar.FinishCallback(ar);
        }
    }
}
