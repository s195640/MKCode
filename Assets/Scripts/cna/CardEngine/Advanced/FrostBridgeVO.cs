using cna.poo;
namespace cna {
    public partial class FrostBridgeVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.AC_FrostBridge01);
            ar.ActionMovement(2);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.AC_FrostBridge02);
            ar.ActionMovement(4 + ar.CardModifier);
            return ar;
        }
    }
}
