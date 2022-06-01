using cna.poo;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public abstract class CardSlot : MonoBehaviour {

        private ActionCardSlot actionCardSlot;
        public ActionCardSlot ActionCard { get { if (actionCardSlot == null) { actionCardSlot = FindObjectOfType<ActionCardSlot>(); } return actionCardSlot; } }

        [Header("Shared")]
        [SerializeField] private int uniqueCardId;
        [SerializeField] private CardVO card;
        [SerializeField] private CardHolder_Enum cardHolder;

        public int UniqueCardId { get => uniqueCardId; set => uniqueCardId = value; }
        public CardVO Card { get => card; set => card = value; }
        public CardHolder_Enum CardHolder { get => cardHolder; set => cardHolder = value; }

        public void SetSelected(bool isSelected) {
            GetComponent<Outline>().enabled = isSelected;
        }

        public abstract void SetupUI(int key, CardHolder_Enum holder);
        public abstract void UpdateUI();
        public abstract void OnClickCard();

    }

}
