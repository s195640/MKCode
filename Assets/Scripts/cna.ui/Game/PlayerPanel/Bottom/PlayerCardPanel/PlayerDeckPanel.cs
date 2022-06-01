using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class PlayerDeckPanel : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI playerDrawSize;
        [SerializeField] private NormalCardSlot playerDiscardSlot;

        public void UpdateUI() {
            PlayerDeckData pDeck = D.LocalPlayer.Deck;
            playerDrawSize.text = "" + pDeck.Deck.Count;
            int discordDeckCount = pDeck.Discard.Count;
            if (discordDeckCount > 0) {
                playerDiscardSlot.SetupUI(pDeck.Discard[discordDeckCount - 1], CardHolder_Enum.DiscardDeck);
            } else {
                playerDiscardSlot.SetupUI(0, CardHolder_Enum.NA);
            }
        }
    }
}
