using cna.poo;
namespace cna {
    public partial class UndergroundTravelVO : CardSpellVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.CS_UndergroundTravel);
            ar.AddLog("Underground Travel activated");
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.CS_UndergroundAttack);
            ar.AddGameEffect(GameEffect_Enum.CS_UndergroundAttackFort);
            ar.AddLog("Underground Attack activated");
            return ar;
        }
    }
}
