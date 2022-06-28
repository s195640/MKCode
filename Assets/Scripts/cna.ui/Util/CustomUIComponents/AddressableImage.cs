using cna.poo;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    [RequireComponent(typeof(Image))]
    public class AddressableImage : MonoBehaviour {
        [SerializeField] private Image_Enum imageEnum = Image_Enum.NA;
        private Image image;

        public Image_Enum ImageEnum { get => imageEnum; set { imageEnum = value; UpdateUI(); } }
        public Image Image { get { if (image == null) { image = gameObject.GetComponent<Image>(); } return image; } }


        public void Start() {
            UpdateUI();
        }

        public void UpdateUI() {
            if (Image.sprite == null || !imageEnum.ToString().EndsWith(Image.sprite.name)) {
                Image.sprite = D.SpriteMap[imageEnum];
            }
        }
    }
}
