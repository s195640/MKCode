using cna.poo;

namespace cna {
    public partial class WHITE_MotivationVO : CardSkillVO {
        private Crystal_Enum addMana = Crystal_Enum.White;
        public override void ActionPaymentComplete_00(GameAPI ar) {
            int currentPlayerFame = BasicUtil.GetPlayerTotalFame(ar.P.Fame, ar.G.GameData.FamePerLevel);
            bool lowest = true;
            ar.G.Players.ForEach(p => {
                if (!p.DummyPlayer && p.Key != ar.P.Key && p.Fame.X <= currentPlayerFame) {
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
