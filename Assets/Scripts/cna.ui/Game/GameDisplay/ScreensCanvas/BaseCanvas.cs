using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cna.ui {
    public abstract class BaseCanvas : MonoBehaviour {
        private int seed = 0;
        private string gameid = "";
        public virtual void SetupUI() { }

        public void CheckSetupUI() {
            if (!gameid.Equals(D.G.GameId) || seed != D.GLD.Seed) {
                gameid = D.G.GameId;
                seed = D.GLD.Seed;
                SetupUI();
            }
        }
    }
}
