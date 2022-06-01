using System.Collections.Generic;
using cna.poo;

namespace cna {
    public partial class ManaDrawVO : CardActionVO {

        private List<Crystal_Enum> manaDieCalc;
        private int manaDieIndex = -1;

        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.ManaPool(1);
            return ar;
        }
        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            manaDieCalc = new List<Crystal_Enum>();
            ar.G.Board.ManaPool.ForEach(m => manaDieCalc.Add(m));
            OptionVO[] p = new OptionVO[manaDieCalc.Count];
            for (int i = 0; i < manaDieCalc.Count; i++) {
                p[i] = new OptionVO("Mana Die", BasicUtil.Convert_CrystalToManaDieImageId(manaDieCalc[i]));
            }
            ar.SelectOptions(acceptCallback_01, p);
        }

        public void acceptCallback_01(ActionResultVO ar) {
            manaDieIndex = ar.SelectedButtonIndex;
            ar.SelectOptions(acceptCallback_02,
                new OptionVO("+2 Blue Mana", Image_Enum.I_mana_blue),
                new OptionVO("+2 Red Mana", Image_Enum.I_mana_red),
                new OptionVO("+2 Green Mana", Image_Enum.I_mana_green),
                new OptionVO("+2 White Mana", Image_Enum.I_mana_white),
                new OptionVO("+2 Black Mana", Image_Enum.I_mana_black)
                );
        }

        public void acceptCallback_02(ActionResultVO ar) {
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
            ar.G.Board.ManaPool.Remove(manaDieCalc[manaDieIndex]);
            ar.G.Board.ManaPool.Add(manaDie);
            ar.FinishCallback(ar);
        }
    }
}
