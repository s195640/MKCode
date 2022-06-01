using cna.poo;
namespace cna {
    public partial class WingsofWindVO : CardSpellVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.CS_WingsOfWind);
            ar.AddLog("Wings of Wind activated");
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.LocalPlayer.Movement = 0;
            ar.AddGameEffect(GameEffect_Enum.CS_WingsOfNight);
            ar.AddLog("Wings of Night activated, Move set to 0");
            return ar;
        }
    }
}
