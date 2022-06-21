using System.Collections.Generic;
using cna.poo;

namespace cna {
    public partial class ManaDrawVO : CardActionVO {

        private List<ManaPoolData> manaDieCalc;
        private int manaDieIndex = -1;

        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.ManaPool(1);
            return ar;
        }
        public override void ActionPaymentComplete_01(GameAPI ar) {
            manaDieCalc = new List<ManaPoolData>();
            manaDieCalc = ar.P.ManaPool;
            OptionVO[] p = new OptionVO[manaDieCalc.Count];
            for (int i = 0; i < manaDieCalc.Count; i++) {
                p[i] = new OptionVO("Mana Die", BasicUtil.Convert_CrystalToManaDieImageId(manaDieCalc[i].ManaColor));
            }
            ar.SelectOptions(acceptCallback_01, p);
        }

        public void acceptCallback_01(GameAPI ar) {
            manaDieIndex = ar.SelectedButtonIndex;
            ar.SelectOptions(acceptCallback_02,
                new OptionVO("+2 Blue Mana", Image_Enum.I_mana_blue),
                new OptionVO("+2 Red Mana", Image_Enum.I_mana_red),
                new OptionVO("+2 Green Mana", Image_Enum.I_mana_green),
                new OptionVO("+2 White Mana", Image_Enum.I_mana_white),
                new OptionVO("+2 Black Mana", Image_Enum.I_mana_black)
                );
        }

        public void acceptCallback_02(GameAPI ar) {
            Crystal_Enum manaDie = Crystal_Enum.NA;
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    manaDie = Crystal_Enum.Blue;
                    ar.ManaBlue(2);
                    break;
                }
                case 1: {
                    manaDie = Crystal_Enum.Red;
                    ar.ManaRed(2);
                    break;
                }
                case 2: {
                    manaDie = Crystal_Enum.Green;
                    ar.ManaGreen(2);
                    break;
                }
                case 3: {
                    manaDie = Crystal_Enum.White;
                    ar.ManaWhite(2);
                    break;
                }
                case 4: {
                    manaDie = Crystal_Enum.Black;
                    ar.ManaBlack(2);
                    break;
                }
            }
            manaDieCalc[manaDieIndex].ManaColor = manaDie;
            manaDieCalc[manaDieIndex].Status = ManaPool_Enum.ManaDraw;
            ar.FinishCallback(ar);
        }
    }
}
