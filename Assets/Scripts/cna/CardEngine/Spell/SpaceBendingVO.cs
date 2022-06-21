using cna.poo;
namespace cna {
    public partial class SpaceBendingVO : CardSpellVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.CS_SpaceBending);
            ar.AddLog("Space Bending activated");
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.CS_TimeBending);
            ar.AddLog("Time Bending activated");
            return ar;
        }
    }
}
