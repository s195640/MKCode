using cna.poo;
namespace cna {
    public partial class UndergroundTravelVO : CardSpellVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.CS_UndergroundTravel);
            ar.AddLog("Underground Travel activated");
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.CS_UndergroundAttack);
            ar.AddGameEffect(GameEffect_Enum.CS_UndergroundAttackFort);
            ar.AddLog("Underground Attack activated");
            return ar;
        }
    }
}
