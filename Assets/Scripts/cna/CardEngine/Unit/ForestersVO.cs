using cna.poo;

namespace cna {
    public partial class ForestersVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.ActionMovement(2);
            ar.AddGameEffect(GameEffect_Enum.Foresters);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.BattleBlock(new AttackData(3));
            return ar;
        }
    }
}
