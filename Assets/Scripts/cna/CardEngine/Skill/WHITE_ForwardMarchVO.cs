using cna.poo;
namespace cna {
    public partial class WHITE_ForwardMarchVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            int i = 0;
            CNAMap<int, WrapList<CardState_Enum>> state = ar.LocalPlayer.Deck.State;
            ar.LocalPlayer.Deck.Unit.ForEach(u => {
                if (state.ContainsKey(u)) {
                    if (!state[u].ContainsAny(CardState_Enum.Unit_Exhausted, CardState_Enum.Unit_Paralyzed,
                        CardState_Enum.Unit_Poisoned, CardState_Enum.Unit_Wounded)) {
                        i++;
                    }
                } else {
                    i++;
                }
            });
            if (i > 3) {
                i = 3;
            }
            ar.ActionMovement(i);
            ar.TurnPhase(TurnPhase_Enum.Move);
            return ar;
        }
    }
}
