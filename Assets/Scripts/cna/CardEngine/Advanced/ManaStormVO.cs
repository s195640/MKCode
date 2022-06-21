using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ManaStormVO : CardActionVO {
        private List<ManaPoolData> manaDieCalc;

        public override void ActionPaymentComplete_00(GameAPI ar) {
            manaDieCalc = new List<ManaPoolData>();
            List<OptionVO> p = new List<OptionVO>();
            ar.P.ManaPool.ForEach(m => {
                switch (m.ManaColor) {
                    case Crystal_Enum.Blue:
                    case Crystal_Enum.Green:
                    case Crystal_Enum.Red:
                    case Crystal_Enum.White: {
                        manaDieCalc.Add(m);
                        p.Add(new OptionVO("Mana Die", BasicUtil.Convert_CrystalToManaDieImageId(m.ManaColor)));
                        break;
                    }
                }
            });
            ar.SelectOptions(acceptCallback_00, p.ToArray());
        }

        public void acceptCallback_00(GameAPI ar) {
            ar.AddCrystal(manaDieCalc[ar.SelectedButtonIndex].ManaColor);
            manaDieCalc[ar.SelectedButtonIndex].ManaColor = (Crystal_Enum)UnityEngine.Random.Range(1, 7);
            manaDieCalc[ar.SelectedButtonIndex].Status = ManaPool_Enum.ManaStorm;
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(GameAPI ar) {
            ar.ManaPool(3);
            ar.AddGameEffect(GameEffect_Enum.AC_ManaStorm);
            ar.P.ManaPool.ForEach(mp => {
                mp.Status = ManaPool_Enum.ManaStorm;
                mp.ManaColor = (Crystal_Enum)UnityEngine.Random.Range(1, 7);
            });
            ar.FinishCallback(ar);

        }
    }
}
