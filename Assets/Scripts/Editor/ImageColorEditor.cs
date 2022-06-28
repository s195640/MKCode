using UnityEditor;
using UnityEngine.UI;
using cna.ui;
using cna;
using cna.poo;
using UnityEngine;

[CustomEditor(typeof(ImageColor))]
public class ImageColorEditor : Editor {

    private Image image;
    private ImageColor imageColor;
    private Color_Enum colorEnum;
    private Color_Enum colorSetEnum = Color_Enum.NA;
    private int colorMod;
    private int colorModSet = 5;

    public ImageColor ImageColor { get { if (imageColor == null) { imageColor = (ImageColor)target; } return imageColor; } }
    public Image Image { get { if (image == null) { image = ImageColor.GetComponent<Image>(); } return image; } }

    public void OnEnable() {
        UpdateUI_Color();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        UpdateUI_Color();
    }

    public void UpdateUI_Color() {
        colorEnum = ImageColor.ColorEnum;
        colorMod = ImageColor.ColorMod;
        if (colorEnum != colorSetEnum || colorMod != colorModSet) {
            colorSetEnum = colorEnum;
            colorModSet = colorMod;
            Image.color = ImageColor.getNewColor(colorSetEnum, colorModSet, Image.color);
        }
    }
}

