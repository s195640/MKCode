using System;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    [CreateAssetMenu(fileName = "Data", menuName = "CustomObjects/Avatar", order = 1)]
    public class AvatarMetaData : ScriptableObject {
        [SerializeField] private string avatarName;
        [SerializeField] private Image_Enum avatarId;
        [SerializeField] private Image_Enum avatarShieldId;
        [SerializeField] private Color avatarColor;
        [SerializeField] private string avatarDesc;
        [SerializeField] private Color avatarImageColor = Color.white;

        public string AvatarName { get => avatarName; set => avatarName = value; }
        public Color AvatarColor { get => avatarColor; set => avatarColor = value; }
        public string AvatarDesc { get => avatarDesc; set => avatarDesc = value; }
        public Color AvatarImageColor { get => avatarImageColor; set => avatarImageColor = value; }
        public Image_Enum AvatarId { get => avatarId; set => avatarId = value; }
        public Image_Enum AvatarShieldId { get => avatarShieldId; set => avatarShieldId = value; }

        public override string ToString() {
            return JsonUtility.ToJson(this, true);
        }
    }
}