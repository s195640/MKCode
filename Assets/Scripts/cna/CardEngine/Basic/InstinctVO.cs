
using cna.poo;

namespace cna {
    public partial class InstinctVO : CardActionVO {

        public override void ActionPaymentComplete_00(GameAPI ar) {
            acceptCallback(ar, 2);
        }

        public override void ActionPaymentComplete_01(GameAPI ar) {
            acceptCallback(ar, 4);
        }


        public void acceptCallback(GameAPI ar, int mod) {
            switch (ar.P.PlayerTurnPhase) {
                case TurnPhase_Enum.StartTurn:
                case TurnPhase_Enum.Move: {
                    ar.TurnPhase(TurnPhase_Enum.Move);
                    ar.ActionMovement(mod);
                    break;
                }
                case TurnPhase_Enum.Influence: {
                    ar.TurnPhase(TurnPhase_Enum.Influence);
                    ar.ActionInfluence(mod);
                    break;
                }
                case TurnPhase_Enum.Battle: {
                    if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                        ar.BattleBlock(new AttackData(mod));
                    } else {
                        ar.BattleAttack(new AttackData(mod));
                    }
                    break;
                }
            }
            ar.FinishCallback(ar);
        }

    }
}
