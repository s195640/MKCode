using cna.poo;
namespace cna {
    public partial class HeroicTaleVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(3);
            ar.AddGameEffect(GameEffect_Enum.AC_HeroicTale01);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(6 + ar.CardModifier);
            ar.AddGameEffect(GameEffect_Enum.AC_HeroicTale02);
            return ar;
        }
    }
}
