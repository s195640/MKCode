using cna.poo;
namespace cna {
    public partial class AmbushVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.AC_Ambush01);
            ar.ActionMovement(2);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.AC_Ambush02);
            ar.ActionMovement(4 + ar.CardModifier);
            return ar;
        }
    }
}
