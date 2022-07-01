using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class PlayerDeckPanel : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI playerDrawSize;
        [SerializeField] private NormalCardSlot playerDiscardSlot;

        public void UpdateUI(PlayerData pd, bool disableClick = false) {
            PlayerDeckData pDeck = pd.Deck;
            playerDrawSize.text = "" + pDeck.Deck.Count;
            int discordDeckCount = pDeck.Discard.Count;
            if (discordDeckCount > 0) {
                playerDiscardSlot.SetupUI(pd, pDeck.Discard[discordDeckCount - 1], CardHolder_Enum.DiscardDeck);
            } else {
                playerDiscardSlot.SetupUI(pd, 0, CardHolder_Enum.NA);
            }
            if (disableClick) {
                playerDiscardSlot.UpdateUI_DisableClick();
            }
        }
    }
}
