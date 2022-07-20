using System.Collections.Generic;
using cna.poo;

namespace cna {
    public partial class ManaPullVO : CardActionVO {

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
                new OptionVO("Blue Mana/Crystal", Image_Enum.I_mana_blue),
                new OptionVO("Red Mana/Crystal", Image_Enum.I_mana_red),
                new OptionVO("Green Mana/Crystal", Image_Enum.I_mana_green),
                new OptionVO("White Mana/Crystal", Image_Enum.I_mana_white),
                new OptionVO("+2 Black Mana", Image_Enum.I_mana_black)
                );
        }

        public void acceptCallback_02(GameAPI ar) {
            Crystal_Enum manaDie = Crystal_Enum.NA;
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    manaDie = Crystal_Enum.Blue;
                    ar.ManaBlue(1);
                    ar.CrystalBlue(1);
                    break;
                }
                case 1: {
                    manaDie = Crystal_Enum.Red;
                    ar.ManaRed(1);
                    ar.CrystalRed(1);
                    break;
                }
                case 2: {
                    manaDie = Crystal_Enum.Green;
                    ar.ManaGreen(1);
                    ar.CrystalGreen(1);
                    break;
                }
                case 3: {
                    manaDie = Crystal_Enum.White;
                    ar.ManaWhite(1);
                    ar.CrystalWhite(1);
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
