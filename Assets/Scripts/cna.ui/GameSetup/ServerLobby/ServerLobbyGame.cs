using System;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class ServerLobbyGame : UnityEngine.MonoBehaviour {
        [Header("GameObjects")]
        [SerializeField] private Button joinButton;
        [SerializeField] private TextMeshProUGUI hostName;
        [SerializeField] private TextMeshProUGUI gameStatus;

        [Header("Other")]
        [SerializeField] private LobbyData lobbyGameData;

        public LobbyData LobbyGameData { get => lobbyGameData; set => lobbyGameData = value; }

        public void UpdateUI(LobbyData lgd, Action<LobbyData> joinGame = null) {
            lobbyGameData = lgd;
            hostName.text = lgd.HostPlayer.Name;
            gameStatus.text = lgd.GameStarted ? "(In Progress)" : "(New Game)";
            if (joinGame != null) {
                joinButton.onClick.AddListener(delegate { joinGame(lgd); });
            }
        }
    }
}
