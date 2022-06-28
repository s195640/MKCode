using UnityEditor;
using UnityEngine.UI;
using cna.ui;
using cna;
using cna.poo;
using UnityEngine;

[CustomEditor(typeof(AddressableImage))]
public class AddressableImageEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        AddressableImage ai = (AddressableImage)target;
        Image image = ai.GetComponent<Image>();
        Image_Enum imageEnum = ai.ImageEnum;
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