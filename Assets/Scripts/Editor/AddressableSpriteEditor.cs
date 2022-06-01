using UnityEditor;
using cna.ui;
using cna;
using cna.poo;
using UnityEngine;

[CustomEditor(typeof(AddressableSprite))]
public class AddressableSpriteEditor : Editor {
    private SpriteRenderer rs;
    private AddressableSprite a;
    private Image_Enum imageEnum;

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        a = (AddressableSprite)target;
        rs = a.gameObject.GetComponent<SpriteRenderer>();
        imageEnum = a.ImageEnum;
        if (rs.sprite == null || !imageEnum.ToString().EndsWith(rs.sprite.name)) {
            if (D.SpriteMap != null) {
                if (D.SpriteMap.ContainsKey(imageEnum)) {
                    rs.sprite = D.SpriteMap[imageEnum];
                }
            } else {
                AppEngineHelper.UpdateSpriteMap();
            }
        }
    }
}

