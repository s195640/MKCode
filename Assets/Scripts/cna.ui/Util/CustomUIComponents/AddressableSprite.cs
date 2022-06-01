using cna.poo;
using UnityEngine;

namespace cna.ui {
    [RequireComponent(typeof(SpriteRenderer))]
    public class AddressableSprite : MonoBehaviour {
        private SpriteRenderer sr;
        [SerializeField] private Image_Enum imageEnum = Image_Enum.NA;
        public Image_Enum ImageEnum { get => imageEnum; set { imageEnum = value; UpdateUI(); } }
        public SpriteRenderer Sr { get { if (sr == null) { sr = gameObject.GetComponent<SpriteRenderer>(); } return sr; } }

        public void Start() {
            UpdateUI();
        }

        public void UpdateUI() {
            if (Sr.sprite == null || !imageEnum.ToString().EndsWith(Sr.sprite.name)) {
                Sr.sprite = D.SpriteMap[ImageEnum];
            }
        }
    }
}
