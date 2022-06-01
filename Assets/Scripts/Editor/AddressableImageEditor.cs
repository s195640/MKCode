using UnityEditor;
using UnityEngine.UI;
using cna.ui;
using cna;
using cna.poo;

[CustomEditor(typeof(AddressableImage))]
public class AddressableImageEditor : Editor {

    private Image image;
    private AddressableImage ai;
    private Image_Enum imageEnum;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        ai = (AddressableImage)target;
        image = ai.gameObject.GetComponent<Image>();
        imageEnum = ai.ImageEnum;
        if (image.sprite == null || !imageEnum.ToString().EndsWith(image.sprite.name)) {
            if (D.SpriteMap != null) {
                if (D.SpriteMap.ContainsKey(imageEnum)) {
                    image.sprite = D.SpriteMap[imageEnum];
                }
            } else {
                AppEngineHelper.UpdateSpriteMap();
            }
        }
    }
}