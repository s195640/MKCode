using cna.poo;

namespace cna {
    public partial class RED_MotivationVO : CardSkillVO {
        private Crystal_Enum addMana = Crystal_Enum.Red;
        public override void ActionPaymentComplete_00(GameAPI ar) {
            int currentPlayerFame = ar.P.TotalFame;
            bool lowest = true;
            ar.G.Players.ForEach(p => {
                if (!p.DummyPlayer && p.Key != ar.P.Key && p.TotalFame <= currentPlayerFame) {
                    lowest = lowest && false;
                }
            });
            if (lowest) {
                ar.AddMana(addMana);
            }
            ar.DrawCard(2, ar.FinishCallback);
        }
    }
}
