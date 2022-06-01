using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class CNA_SimpleText : ICNA_Base {

        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI textWithNoImage;
        public override void SetupUI(string buttonText, Color buttonColor, Image_Enum buttonImageid = Image_Enum.NA, bool isActive = true) {
            image.color = buttonColor;
            textWithNoImage.text = buttonText;
        }
    }
}
