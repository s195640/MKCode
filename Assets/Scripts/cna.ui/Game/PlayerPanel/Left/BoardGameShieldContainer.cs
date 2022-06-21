using System.Collections.Generic;
using System.Linq;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class BoardGameShieldContainer : MonoBehaviour {
        [SerializeField] private AddressableImage[] shields;

        List<Image_Enum> shieldImages = new List<Image_Enum>();

        public void UpdateUI(HexItemDetail hex) {
            List<Image_Enum> shieldImages = BasicUtil.getAllShieldsAtPos(D.G, hex.GridPosition);
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
