using cna.poo;
namespace cna {
    public partial class RefreshingWalkVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.ActionMovement(2);
            if (ar.P.PlayerTurnPhase != TurnPhase_Enum.Battle) {
                ar.Healing(1);
            }
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.ActionMovement(4 + ar.CardModifier);
            if (ar.P.PlayerTurnPhase != TurnPhase_Enum.Battle) {
                ar.Healing(2);
            }
            return ar;
        }
    }
}
