using cna.poo;
namespace cna {
    public partial class SpaceBendingVO : CardSpellVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.CS_SpaceBending);
            ar.AddLog("Space Bending activated");
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.CS_TimeBending);
            ar.AddLog("Time Bending activated");
            return ar;
        }
    }
}
