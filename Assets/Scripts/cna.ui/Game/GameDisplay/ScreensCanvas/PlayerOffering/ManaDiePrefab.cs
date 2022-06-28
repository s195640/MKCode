using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class ManaDiePrefab : MonoBehaviour {

        private int dieId = -1;
        private Crystal_Enum mana;
        [SerializeField] private AddressableImage manaImage;

        public Crystal_Enum Mana { get => mana; set { mana = value; setManaImage(); } }
        public Image_Enum ManaImage { get => manaImage.ImageEnum; }
        public int DieId { get => dieId; set => dieId = value; }

        public void SetupUI(Crystal_Enum mana, int dieId) {
            Mana = mana;
            DieId = dieId;
        }

        public void UpdateUI() { }


        private void setManaImage() {
            switch (mana) {
                case Crystal_Enum.Black: { manaImage.ImageEnum = Image_Enum.I_mana_black; break; }
                case Crystal_Enum.Blue: { manaImage.ImageEnum = Image_Enum.I_mana_blue; break; }
                case Crystal_Enum.Red: { manaImage.ImageEnum = Image_Enum.I_mana_red; break; }
                case Crystal_Enum.White: { manaImage.ImageEnum = Image_Enum.I_mana_white; break; }
                case Crystal_Enum.Green: { manaImage.ImageEnum = Image_Enum.I_mana_green; break; }
                case Crystal_Enum.Gold: { manaImage.ImageEnum = Image_Enum.I_mana_gold; break; }
            }
        }
    }
}
