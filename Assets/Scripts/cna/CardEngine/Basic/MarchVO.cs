﻿using cna.poo;

namespace cna {
    public partial class MarchVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.ActionMovement(2);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.ActionMovement(4 + ar.CardModifier);
            return ar;
        }
    }
}
