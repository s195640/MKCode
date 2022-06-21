using cna.poo;

namespace cna {
    public partial class ForestersVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.ActionMovement(2);
            ar.AddGameEffect(GameEffect_Enum.Foresters);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.BattleBlock(new AttackData(3));
            return ar;
        }
    }
}
