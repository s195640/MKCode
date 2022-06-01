using System;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class GameLobbyAvatar : MonoBehaviour {
        [SerializeField] private GameObject disable;
        [SerializeField] private GameLobbyAvatarButton avatarButton;
        [SerializeField] private TextMeshProUGUI avatarDescription;
        [SerializeField] private Button exitButton;
        [SerializeField] private Image_Enum avatar_Enum;


        public void UpdateUI() {
            AvatarMetaData avatarMetaData = D.AvatarMetaDataMap[avatar_Enum];
            avatarDescription.text = avatarMetaData.AvatarDesc;
            disable.SetActive(D.G.Gld.Players.Find(p => p.Avatar.Equals(avatarMetaData.AvatarId)) != null);
        }

        public void UpdateUI(Image_Enum avatarEnum, Action<Image_Enum> buttonCallback) {
            avatar_Enum = avatarEnum;
            avatarButton.UpdateUI(avatar_Enum, buttonCallback);
            UpdateUI();
        }
    }
}
