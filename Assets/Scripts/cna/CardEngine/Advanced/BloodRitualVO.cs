using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BloodRitualVO : CardActionVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.AddWound(1);
            ar.CrystalRed(1);
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("+1 Blue Mana", Image_Enum.I_mana_blue),
                new OptionVO("+1 Green Mana", Image_Enum.I_mana_green),
                new OptionVO("+1 White Mana", Image_Enum.I_mana_white),
                new OptionVO("+1 Red Mana", Image_Enum.I_mana_red),
                new OptionVO("+1 Gold Mana", Image_Enum.I_mana_gold),
                new OptionVO("+1 Black Mana", Image_Enum.I_mana_black)
                );
        }

        public void acceptCallback_00(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: { ar.ManaBlue(1); break; }
                case 1: { ar.ManaGreen(1); break; }
                case 2: { ar.ManaWhite(1); break; }
                case 3: { ar.ManaRed(1); break; }
                case 4: { ar.ManaGold(1); break; }
                case 5: { ar.ManaBlack(1); break; }
            }
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.AddWound(1);
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("+1 Blue Mana", Image_Enum.I_mana_blue),
                new OptionVO("+1 Green Mana", Image_Enum.I_mana_green),
                new OptionVO("+1 White Mana", Image_Enum.I_mana_white),
                new OptionVO("+1 Red Mana", Image_Enum.I_mana_red),
                new OptionVO("+1 Gold Mana", Image_Enum.I_mana_gold),
                new OptionVO("+1 Black Mana", Image_Enum.I_mana_black)
                );
        }

        public void acceptCallback_01(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: { ar.ManaBlue(1); break; }
                case 1: { ar.ManaGreen(1); break; }
                case 2: { ar.ManaWhite(1); break; }
                case 3: { ar.ManaRed(1); break; }
                case 4: { ar.ManaGold(1); break; }
                case 5: { ar.ManaBlack(1); break; }
            }
            ar.SelectOptions(acceptCallback_01a,
                new OptionVO("+1 Blue Mana", Image_Enum.I_mana_blue),
                new OptionVO("+1 Green Mana", Image_Enum.I_mana_green),
                new OptionVO("+1 White Mana", Image_Enum.I_mana_white),
                new OptionVO("+1 Red Mana", Image_Enum.I_mana_red),
                new OptionVO("+1 Gold Mana", Image_Enum.I_mana_gold),
                new OptionVO("+1 Black Mana", Image_Enum.I_mana_black)
                );
        }

        public void acceptCallback_01a(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: { ar.ManaBlue(1); break; }
                case 1: { ar.ManaGreen(1); break; }
                case 2: { ar.ManaWhite(1); break; }
                case 3: { ar.ManaRed(1); break; }
                case 4: { ar.ManaGold(1); break; }
                case 5: { ar.ManaBlack(1); break; }
            }
            ar.SelectOptions(acceptCallback_01b,
                new OptionVO("+1 Blue Mana", Image_Enum.I_mana_blue),
                new OptionVO("+1 Green Mana", Image_Enum.I_mana_green),
                new OptionVO("+1 White Mana", Image_Enum.I_mana_white),
                new OptionVO("+1 Red Mana", Image_Enum.I_mana_red),
                new OptionVO("+1 Gold Mana", Image_Enum.I_mana_gold),
                new OptionVO("+1 Black Mana", Image_Enum.I_mana_black)
                );
        }

        public void acceptCallback_01b(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: { ar.ManaBlue(1); break; }
                case 1: { ar.ManaGreen(1); break; }
                case 2: { ar.ManaWhite(1); break; }
                case 3: { ar.ManaRed(1); break; }
                case 4: { ar.ManaGold(1); break; }
                case 5: { ar.ManaBlack(1); break; }
            }
            ar.SelectOptions(acceptCallback_01c,
                new OptionVO("Buy Blue Crystal", Image_Enum.I_crystal_blue),
                new OptionVO("Buy Green Crystal", Image_Enum.I_crystal_green),
                new OptionVO("Buy White Crystal", Image_Enum.I_crystal_white),
                new OptionVO("Buy Red Crystal", Image_Enum.I_crystal_red),
                new OptionVO("None", Image_Enum.I_x)
                );
        }

        public void acceptCallback_01c(ActionResultVO ar) {
            List<Crystal_Enum> cost = new List<Crystal_Enum>();
            switch (ar.SelectedButtonIndex) {
                case 0: { cost.Add(Crystal_Enum.Blue); break; }
                case 1: { cost.Add(Crystal_Enum.Green); break; }
                case 2: { cost.Add(Crystal_Enum.White); break; }
                case 3: { cost.Add(Crystal_Enum.Red); break; }
                case 4: { ar.FinishCallback(ar); return; }
            }
            ar.PayForAction(cost, acceptCallback_01d, false);
        }
        public void acceptCallback_01d(ActionResultVO ar) {
            ar.AddCrystal(ar.Payment[0].ManaUsedAs);
            ar.FinishCallback(ar);
        }

    }
}
