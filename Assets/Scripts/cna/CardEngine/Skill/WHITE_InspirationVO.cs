using cna.poo;
namespace cna {
    public partial class WHITE_InspirationVO : CardSkillVO {
        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }

        public void acceptCallback_00(GameAPI ar) {
            CNAMap<int, WrapList<CardState_Enum>> state = ar.P.Deck.State;
            bool exhausted = state[ar.SelectedUniqueCardId].ContainsAny(CardState_Enum.Unit_Exhausted);
            bool wounded = state[ar.SelectedUniqueCardId].ContainsAny(CardState_Enum.Unit_Wounded);
            bool poisoned = state[ar.SelectedUniqueCardId].ContainsAny(CardState_Enum.Unit_Poisoned);
            if (exhausted && wounded) {
                ar.SelectOptions(acceptCallback_01,
                    new OptionVO("Remove Exhausted", Image_Enum.I_rest),
                    new OptionVO("Heal Wound", Image_Enum.I_healHand)
                    );
            } else if (exhausted) {
                ar.RemoveCardState(ar.SelectedUniqueCardId, CardState_Enum.Unit_Exhausted);
            } else if (poisoned) {
                ar.RemoveCardState(ar.SelectedUniqueCardId, CardState_Enum.Unit_Poisoned);
            } else {
                ar.RemoveCardState(ar.SelectedUniqueCardId, CardState_Enum.Unit_Wounded);
            }
            ar.FinishCallback(ar);
        }
        public void acceptCallback_01(GameAPI ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.RemoveCardState(ar.SelectedUniqueCardId, CardState_Enum.Unit_Exhausted);
                    break;
                }
                case 1: {
                    bool poisoned = ar.P.Deck.State[ar.SelectedUniqueCardId].ContainsAny(CardState_Enum.Unit_Poisoned);
                    if (poisoned) {
                        ar.RemoveCardState(ar.SelectedUniqueCardId, CardState_Enum.Unit_Poisoned);
                    } else {
                        ar.RemoveCardState(ar.SelectedUniqueCardId, CardState_Enum.Unit_Wounded);
                    }
                    break;
                }
            }
            ar.FinishCallback(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, GameAPI ar) {
            string msg = "";
            if (cardHolder != CardHolder_Enum.PlayerUnitHand) {
                return "You must selet a unit from your hand!";
            } else {
                if (D.LocalPlayer.Deck.State.ContainsKey(card.UniqueId)) {
                    if (!D.LocalPlayer.Deck.State[card.UniqueId].ContainsAny(CardState_Enum.Unit_Exhausted, CardState_Enum.Unit_Wounded, CardState_Enum.Unit_Poisoned)) {
                        return "The Unit must be Exhausted or Wounded!";
                    }
                } else {
                    return "The Unit must be Exhausted or Wounded!";
                }
            }
            return msg;
        }
    }
}
