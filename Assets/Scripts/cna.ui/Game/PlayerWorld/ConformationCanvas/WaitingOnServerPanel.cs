using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cna.ui {
    public class WaitingOnServerPanel : BasePanel {

        private GameAPI ar;

        public void Update() {
            if (ar == null || !ar.P.WaitOnServer) {
                gameObject.SetActive(false);
            }
        }

        public void SetupUI(GameAPI ar) {
            this.ar = ar;
            gameObject.SetActive(true);
        }
    }
}
