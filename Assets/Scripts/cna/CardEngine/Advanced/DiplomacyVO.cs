using cna.poo;
namespace cna {
    public partial class DiplomacyVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.AC_Diplomacy01);
            ar.ActionInfluence(2);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.AddGameEffect(GameEffect_Enum.AC_Diplomacy02);
            ar.ActionInfluence(4 + ar.CardModifier);
            return ar;
        }
    }
}
