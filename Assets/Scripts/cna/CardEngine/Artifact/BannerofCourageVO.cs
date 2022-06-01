using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class BannerofCourageVO : CardArtifactVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }

        public void acceptCallback_00(ActionResultVO ar) {
            ar.AddBanner(ar.SelectedUniqueCardId, ar.UniqueCardId);
            ar.FinishCallback(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, ActionResultVO ar) {
            string msg = "";
            if (cardHolder != CardHolder_Enum.PlayerUnitHand) {
                return "You must selet a unit from your hand!";
            }
            return msg;
        }


        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            List<int> unitsToReady = new List<int>();
            ar.LocalPlayer.Deck.State.Keys.ForEach(s => {
                if (ar.LocalPlayer.Deck.State[s].Contains(CardState_Enum.Unit_Exhausted)) {
                    unitsToReady.Add(s);
                }
            });
            unitsToReady.ForEach(s => ar.RemoveCardState(s, CardState_Enum.Unit_Exhausted));
            return ar;
        }

        public override ActionResultVO ActionValid_02(ActionResultVO ar) {
            ar.RemoveCardState(ar.UniqueCardId, CardState_Enum.Unit_Exhausted);
            ar.AddGameEffect(GameEffect_Enum.CT_BannerOfCourageUsed, ar.UniqueCardId);
            return ar;
        }

        public override ActionResultVO checkAllowedToUse(ActionResultVO ar) {
            if (ar.ActionIndex == 2) {
                bool exhausted = ar.LocalPlayer.Deck.State.ContainsKey(ar.UniqueCardId) && ar.LocalPlayer.Deck.State[ar.UniqueCardId].Contains(CardState_Enum.Unit_Exhausted);
                if (!exhausted) {
                    ar.ErrorMsg = "Unit must be exhausted to use player this banner";
                } else if (ar.LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.CT_BannerOfCourageUsed) && ar.LocalPlayer.GameEffects[GameEffect_Enum.CT_BannerOfCourageUsed].Contains(ar.UniqueCardId)) {
                    ar.ErrorMsg = "This banner has already been used this turn.";
                } else if (ar.LocalPlayer.PlayerTurnPhase == TurnPhase_Enum.Battle) {
                    ar.ErrorMsg = "You can not play this banner during a battle.";
                }
                return ar;
            } else {
                return base.checkAllowedToUse(ar);
            }
        }
    }
}
