using cna.poo;
namespace cna {
    public partial class BLUE_WhoNeedsMagicVO : CardSkillVO {
        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }
        public void acceptCallback_00(GameAPI ar) {
            if (ar.P.ManaPoolAvailable > 0) {
                ar.SelectOptions(acceptCallback_01,
                    new OptionVO("-1 Pool, +3 Basic", Image_Enum.I_die_gold),
                    new OptionVO("+2 Basic", Image_Enum.I_x));
            } else {
                acceptCallback(ar, 2);
            }
        }


        public void acceptCallback_01(GameAPI ar) {
            int mod = 2;
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.P.ManaPoolAvailable--;
                    mod = 3;
                    break;
                }
                case 1: {
                    mod = 2;
                    break;
                }
            }
            acceptCallback(ar, mod);
        }

        public void acceptCallback(GameAPI ar, int mod) {
            ar.AddCardState(ar.SelectedUniqueCardId, CardState_Enum.Discard);
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
