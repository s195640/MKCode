using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class RED_PolarizationVO : CardSkillVO {
        public override void PayForAction(ActionResultVO ar) {
            List<Crystal_Enum> cost = new List<Crystal_Enum>() { Crystal_Enum.Black, Crystal_Enum.Blue, Crystal_Enum.Green, Crystal_Enum.Red, Crystal_Enum.White, Crystal_Enum.Gold };
            ar.PayForAction(cost, OnClick_PaymentAccept, false);
        }


        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            switch (ar.Payment[0].ManaUsedAs) {
                case Crystal_Enum.Black: {
                    ar.ManaGold(1);
                    break;
                }
                case Crystal_Enum.Gold: {
                    ar.ManaBlack(1);
                    break;
                }
                case Crystal_Enum.Red: {
                    ar.ManaBlue(1);
                    break;
                }
                case Crystal_Enum.Blue: {
                    ar.ManaRed(1);
                    break;
                }
                case Crystal_Enum.Green: {
                    ar.ManaWhite(1);
                    break;
                }
                case Crystal_Enum.White: {
                    ar.ManaGreen(1);
                    break;
                }
            }
            return ar;
        }
    }
}
