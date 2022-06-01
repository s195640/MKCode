using System.Collections.Generic;
using System.Linq;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class BoardGameShieldContainer : MonoBehaviour {
        [SerializeField] private AddressableImage[] shields;

        List<Image_Enum> shieldImages = new List<Image_Enum>();

        public void UpdateUI(HexItemDetail hex) {
            if (D.G.Monsters.Shield.Keys.Contains(hex.GridPosition)) {
                List<Image_Enum> shieldImages = D.G.Monsters.Shield[hex.GridPosition].Values;
                if (!this.shieldImages.SequenceEqual(shieldImages)) {
                    this.shieldImages = shieldImages;
                    foreach (AddressableImage a in shields) {
                        a.gameObject.SetActive(false);
                    }
                    for (int i = 0; i < this.shieldImages.Count; i++) {
                        shields[i].gameObject.SetActive(true);
                        shields[i].ImageEnum = this.shieldImages[i];
                    }
                }
            }
        }
    }
}
