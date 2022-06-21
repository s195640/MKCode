using System;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class GameLobbyPlayer : UnityEngine.MonoBehaviour {

        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private GameLobbyAvatarButton avatarButton;
        [SerializeField] private Toggle readyButton;
        [SerializeField] private GameObject disable;

        [SerializeField] private PlayerData playerData;
        [SerializeField] private bool hasLoaded = false;

        public PlayerData PlayerData { get => playerData; set => playerData = value; }

        public void UpdateUI(PlayerData pd, Action<Image_Enum> AvatarButtonCallback = null, Action<bool> PlayerReadyCallback = null) {
            playerData = pd;
            playerName.text = playerData.Name;
            avatarButton.UpdateUI(pd.Avatar, AvatarButtonCallback);
            if (readyButton.isOn != pd.PlayerReady) {
                readyButton.isOn = pd.PlayerReady;
            }
            disable.SetActive(pd.Key != D.LocalPlayerKey);

            if (PlayerReadyCallback == null && !hasLoaded) {
                hasLoaded = true;
            } else {
                if (!hasLoaded) {
                    hasLoaded = true;
                    readyButton.onValueChanged.AddListener(delegate { PlayerReadyCallback(readyButton.isOn); });
                }
            }
        }
    }
}
