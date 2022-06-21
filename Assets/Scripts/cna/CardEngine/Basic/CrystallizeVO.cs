using System.Collections.Generic;
using cna.poo;

namespace cna {
    public partial class CrystallizeVO : CardActionVO {
        public override void ActionPaymentComplete_00(GameAPI ar) {
            List<Crystal_Enum> cost = new List<Crystal_Enum>();
            cost.Add(Crystal_Enum.Green);
            cost.Add(Crystal_Enum.Blue);
            cost.Add(Crystal_Enum.Red);
            cost.Add(Crystal_Enum.White);
            ar.PayForAction(cost, acceptCallback_00, false);
        }

        public void acceptCallback_00(GameAPI ar) {
            ar.AddCrystal(ar.Payment[0].ManaUsedAs);
            ar.FinishCallback(ar);
        }


        public override void ActionPaymentComplete_01(GameAPI ar) {
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("Blue Crystal", Image_Enum.I_crystal_blue),
                new OptionVO("Red Crystal", Image_Enum.I_crystal_red),
                new OptionVO("Green Crystal", Image_Enum.I_crystal_green),
                new OptionVO("White Crystal", Image_Enum.I_crystal_white)
                );
        }

        public void acceptCallback_01(GameAPI ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.CrystalBlue(1);
                    break;
                }
                case 1: {
                    ar.CrystalRed(1);
                    break;
                }
                case 2: {
                    ar.CrystalGreen(1);
                    break;
                }
                case 3: {
                    ar.CrystalWhite(1);
                    break;
                }
            }
            ar.FinishCallback(ar);
        }
    }
}
