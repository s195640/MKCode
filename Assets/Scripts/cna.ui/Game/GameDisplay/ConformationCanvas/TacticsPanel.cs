using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class TacticsPanel : BasePanel {
        [SerializeField] ActionCardSlot ActionCard;
        [SerializeField] private NormalCardSlot[] cardSlots;
        [SerializeField] private CNA_Button accept;
        private int index = -1;
        private List<int> cards;

        public override void UpdateUI() {
            if (D.G.GameStatus == Game_Enum.Tactics) {
                Clear();
                if (!gameObject.activeSelf) {
                    index = -1;
                    if (D.LocalPlayer.Deck.TacticsCardId == 0) {
                        gameObject.SetActive(true);
                        cards = D.Scenario.isDay ? D.Scenario.TacticsDayDeck : D.Scenario.TacticsNightDeck;
                        for (int i = 0; i < 6; i++) {
                            cardSlots[i].SetupUI(D.LocalPlayer, cards[i], CardHolder_Enum.TacticsOffering);
                        }
                    }
                } else {
                    UpdateUI_Tactics();
                }
                accept.Active = D.LocalPlayer.Equals(D.CurrentTurn) && D.LocalPlayer.PlayerTurnPhase == TurnPhase_Enum.TacticsSelect;
            }
        }

        private void UpdateUI_Tactics() {
            for (int i = 0; i < 6; i++) {
                cardSlots[i].UpdateUI_Tactics();
            }
            ActionCard.UpdateUI_Tactics();
        }

        public void OnClick_Accept() {
            if (D.LocalPlayer.Equals(D.CurrentTurn) && D.LocalPlayer.PlayerTurnPhase == TurnPhase_Enum.TacticsSelect) {
                if (index >= 0) {
                    bool selected = false;
                    D.G.Players.ForEach(p => selected |= p.Deck.TacticsCardId.Equals(cardSlots[index].UniqueCardId));
                    Image_Enum t = D.Cards[cardSlots[index].UniqueCardId].CardImage;
                    D.DummyPlayer.DummyTacticsUsed.ForEach(i => selected |= i == t);
                    if (!selected) {
                        gameObject.SetActive(false);
                        D.LocalPlayer.Deck.TacticsCardId = cardSlots[index].UniqueCardId;
                        GameAPI ar = new GameAPI(cardSlots[index].UniqueCardId, CardState_Enum.NA);
                        ar.Card.OnClick_ActionButton(ar);
                    } else {
                        ActionCard.Msg("Please Select a tactics card that no one else has already taken!");
                        accept.ShakeButton();
                    }
                } else {
                    ActionCard.Msg("Please Select a tactics card");
                    accept.ShakeButton();
                }
            } else {
                ActionCard.Msg("It is not your turn!");
                accept.ShakeButton();
            }
        }

        public void OnClick_Card(int index) {
            this.index = index;
        }
    }
}
