using cna.poo;

namespace cna {
    public partial class PeasantsVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(2));
            } else {
                ar.BattleAttack(new AttackData(2));
            }
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(2);
            return ar;
        }
        public override GameAPI ActionValid_02(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.ActionMovement(2);
            return ar;
        }
    }
}
