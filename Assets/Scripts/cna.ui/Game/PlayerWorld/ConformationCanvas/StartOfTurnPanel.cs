using TMPro;
using UnityEngine;

namespace cna.ui {
    public class StartOfTurnPanel : BasePanel {
        [SerializeField] private TextMeshProUGUI headText;
        [SerializeField] private TextMeshProUGUI bodyText;
        bool forceDeclareEndOfRound;
        bool isExhausted;
        bool endOfRoundDeclared;

        public void SetupUI(bool forceDeclareEndOfRound, bool isExhausted, bool endOfRoundDeclared) {
            this.forceDeclareEndOfRound = forceDeclareEndOfRound;
            this.isExhausted = isExhausted;
            this.endOfRoundDeclared = endOfRoundDeclared;
            gameObject.SetActive(true);
            string msg = "It is your turn!  ";
            if (forceDeclareEndOfRound) {
                msg = "Both your deed deck and your hand are empty.  Your turn is forfeit and End of Round has been declared.";
            } else {
                if (endOfRoundDeclared) {
                    msg = "End of Round has been declared.  This will be your last turn.  ";
                }
                if (isExhausted) {
                    msg += "You are Exhausted, you started your hand without any NON Wound cards in your hand.  You will not be able to take any Move, Influence, or Battle actions this turn, if possible one wound card will be discarded.";
                }
            }
            bodyText.text = msg;
        }

        public void OnClick_Okay() {
            gameObject.SetActive(false);
            D.A.OnClick_StartofTurnOkay(forceDeclareEndOfRound, isExhausted, endOfRoundDeclared);
        }
    }
}
