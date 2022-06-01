using cna.poo;
namespace cna {
    public partial class RefreshingWalkVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.ActionMovement(2);
            if (ar.LocalPlayer.PlayerTurnPhase != TurnPhase_Enum.Battle) {
                ar.Healing(1);
            }
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.ActionMovement(4 + ar.CardModifier);
            if (ar.LocalPlayer.PlayerTurnPhase != TurnPhase_Enum.Battle) {
                ar.Healing(2);
            }
            return ar;
        }
    }
}
