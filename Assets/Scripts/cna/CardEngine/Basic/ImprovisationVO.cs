﻿
using cna.poo;

namespace cna {
    public partial class ImprovisationVO : CardActionVO {

        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }
        public void acceptCallback_00(ActionResultVO ar) {
            acceptCallback(ar, 3);
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.SelectSingleCard(acceptCallback_01);
        }

        public void acceptCallback_01(ActionResultVO ar) {
            acceptCallback(ar, 5 + ar.CardModifier);
            ar.FinishCallback(ar);
        }

        public void acceptCallback(ActionResultVO ar, int mod) {
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Discard);
            switch (ar.LocalPlayer.PlayerTurnPhase) {
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
                    if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                        ar.BattleBlock(new AttackData(mod));
                    } else {
                        ar.BattleAttack(new AttackData(mod));
                    }
                    break;
                }
            }
        }
    }
}
