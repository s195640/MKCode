using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class GREEN_UniversalPowerVO : CardSkillVO {

        public override void PayForAction(GameAPI ar) {
            List<Crystal_Enum> cost = new List<Crystal_Enum>() { Crystal_Enum.Black, Crystal_Enum.Blue, Crystal_Enum.Green, Crystal_Enum.Red, Crystal_Enum.White };
            ar.PayForAction(cost, OnClick_PaymentAccept, false);
        }

        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }

        public void acceptCallback_00(GameAPI ar) {
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Discard);
            int mod = 3;
            if (BasicUtil.Convert_ColorIdToCrystalId(D.Cards[ar.SelectedUniqueCardId].CardColor) == ar.Payment[0].ManaUsedAs) {
                mod = 4;
            }
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
