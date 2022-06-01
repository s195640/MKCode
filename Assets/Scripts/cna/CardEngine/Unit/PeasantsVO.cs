using cna.poo;

namespace cna {
    public partial class PeasantsVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(2));
            } else {
                ar.BattleAttack(new AttackData(2));
            }
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(2);
            return ar;
        }
        public override ActionResultVO ActionValid_02(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.ActionMovement(2);
            return ar;
        }
    }
}
