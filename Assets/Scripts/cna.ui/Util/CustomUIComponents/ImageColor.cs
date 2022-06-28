using cna.poo;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class ImageColor : MonoBehaviour {
        [SerializeField] private Color_Enum colorEnum = Color_Enum.NA;
        [SerializeField] [Range(0, 10)] private int colorMod = 5;
        private Image image;
        private Color_Enum colorSetEnum = Color_Enum.NA;
        private int colorModSet = 5;

        public Color_Enum ColorEnum { get => colorEnum; set { colorEnum = value; UpdateUI(); } }
        public Image Image { get { if (image == null) { image = gameObject.GetComponent<Image>(); } return image; } }
        public int ColorMod { get => colorMod; set { colorMod = value; UpdateUI(); } }



        public void Start() {
            UpdateUI();
        }

        public void UpdateUI() {
            if (colorEnum != colorSetEnum || colorMod != colorModSet) {
                colorSetEnum = colorEnum;
                colorModSet = colorMod;
                Image.color = getNewColor(colorSetEnum, colorModSet, Image.color);
            }
        }
        public Color getNewColor(Color_Enum cEnum, int cMod, Color defColor) {
            Color color;
            switch (cEnum) {
                case Color_Enum.A_Tovak: { color = CNAColor.Tovak; break; }
                case Color_Enum.A_Goldyx: { color = CNAColor.Goldyx; break; }
                case Color_Enum.A_Arythea: { color = CNAColor.Arythea; break; }
                case Color_Enum.A_Norawas: { color = CNAColor.Norawas; break; }
                default: { color = defColor; break; }
            }
            float change = getPercent(cMod);
            return new Color(color.r * change, color.g * change, color.b * change, color.a);
        }

        private float getPercent(int val) {
            switch (val) {
                case 0: { return .5f; }
                case 1: { return .6f; }
                case 2: { return .7f; }
                case 3: { return .8f; }
                case 4: { return .9f; }
                default:
                case 5: { return 1f; }
                case 6: { return 1.1f; }
                case 7: { return 1.2f; }
                case 8: { return 1.3f; }
                case 9: { return 1.4f; }
                case 10: { return 1.5f; }
            }
        }
    }
}
