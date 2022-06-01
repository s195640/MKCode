using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class ManaStormVO : CardActionVO {
        private List<Crystal_Enum> manaDieCalc;

        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            manaDieCalc = new List<Crystal_Enum>();
            List<OptionVO> p = new List<OptionVO>();
            ar.G.Board.ManaPool.ForEach(m => {
                if (m == Crystal_Enum.Blue ||
                m == Crystal_Enum.Green ||
                m == Crystal_Enum.Red ||
                m == Crystal_Enum.White) {
                    manaDieCalc.Add(m);
                    p.Add(new OptionVO("Mana Die", BasicUtil.Convert_CrystalToManaDieImageId(m)));
                }
            });
            ar.SelectOptions(acceptCallback_00, p.ToArray());
        }

        public void acceptCallback_00(ActionResultVO ar) {
            ar.AddCrystal(manaDieCalc[ar.SelectedButtonIndex]);
            ar.G.Board.ManaPool.Remove(manaDieCalc[ar.SelectedButtonIndex]);
            ar.G.Board.ManaPool.Add((Crystal_Enum)UnityEngine.Random.Range(1, 7));
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.ManaPool(3);
            ar.AddGameEffect(GameEffect_Enum.AC_ManaStorm);
            int total = ar.G.Board.ManaPool.Count;
            ar.G.Board.ManaPool.Clear();
            for (int i = 0; i < total; i++) {
                ar.G.Board.ManaPool.Add((Crystal_Enum)UnityEngine.Random.Range(1, 7));
            }
            ar.FinishCallback(ar);

        }
    }
}
