using System.Collections.Generic;
using System.Linq;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class PlayerShieldTokenPrefab : MonoBehaviour {
        [SerializeField] private AddressableSprite[] shields;
        private Grid mainGrid;
        private Vector3 screenLocation;
        private V2IntVO location;
        private List<Image_Enum> shieldImages;

        public V2IntVO Location { get => location; set => location = value; }

        public void SetupUI(Grid mainGrid, V2IntVO location, List<Image_Enum> shieldImages) {
            this.shieldImages = new List<Image_Enum>();
            this.location = location;
            this.mainGrid = mainGrid;
            screenLocation = mainGrid.CellToWorld(location.Vector3Int);
            transform.position = screenLocation;
            UpdateUI(shieldImages);
        }

        public void UpdateUI(List<Image_Enum> shieldImages) {
            if (!this.shieldImages.SequenceEqual(shieldImages)) {
                this.shieldImages = shieldImages;
                foreach (AddressableSprite a in shields) {
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
