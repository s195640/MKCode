using System;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class GameLobbyAvatarButton : MonoBehaviour {

        [Header("UI Attr")]
        [SerializeField] private Button avatarButton;
        [SerializeField] private TextMeshProUGUI avatarName;
        [SerializeField] private Image avatarColor;
        [SerializeField] private AddressableImage avatarImageAddr;
        [SerializeField] private Image avatarImage;

        [Header("Local Attr")]
        [SerializeField] private bool hasLoaded = false;
        [SerializeField] private Image_Enum avatarEnum;

        public Image_Enum AvatarEnum { get => avatarEnum; set => avatarEnum = value; }

        public void UpdateUI(Image_Enum avatarEnum, Action<Image_Enum> buttonCallback = null) {
            UpdateUI(D.AvatarMetaDataMap[avatarEnum], buttonCallback);
        }

        public void UpdateUI(AvatarMetaData avatarMetaData, Action<Image_Enum> buttonCallback = null) {
            AvatarEnum = avatarMetaData.AvatarId;
            avatarName.text = avatarMetaData.AvatarName;
            avatarColor.color = avatarMetaData.AvatarColor;
            avatarImage.color = avatarMetaData.AvatarImageColor;
            avatarImageAddr.ImageEnum = avatarMetaData.AvatarId;
            if (buttonCallback == null && !hasLoaded) {
                hasLoaded = true;
            } else {
                if (!hasLoaded) {
                    hasLoaded = true;
                    avatarButton.onClick.AddListener(() => buttonCallback(AvatarEnum));
                }
            }
        }
    }
}
