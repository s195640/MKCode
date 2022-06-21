using cna.poo;
namespace cna {
    public partial class BLUE_IDontGiveaDamnVO : CardSkillVO {
        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }

        public void acceptCallback_00(GameAPI ar) {
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Discard);
            int mod = 2;
            CardType_Enum cardType = D.Cards[ar.SelectedUniqueCardId].CardType;
            if (cardType == CardType_Enum.Advanced || cardType == CardType_Enum.Spell || cardType == CardType_Enum.Artifact) {
                mod = 3;
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
