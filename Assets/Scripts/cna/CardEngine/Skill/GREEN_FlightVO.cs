using cna.poo;
namespace cna {
    public partial class GREEN_FlightVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.GREEN_Flight);
            ar.AddLog("Flight activated");
            return ar;
        }
    }
}
