using System.Collections.Generic;
using cna.poo;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class ServerLobby : MonoBehaviour {
        [Header("GameObjects")]
        [SerializeField] private Button createButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Transform gameListContent;
        [SerializeField] private GameObject disableScreen;

        [Header("Prefab")]
        [SerializeField] private ServerLobbyGame ServerLobbyGame_Pref;

        private List<ServerLobbyGame> serverLobbyGames = new List<ServerLobbyGame>();


        public void UpdateUI() {
            disableScreen.SetActive(D.ClientState == ClientState_Enum.CONNECTED_JOINING_GAME);
            updateGameList();
        }

        private void updateGameList() {
            //  Remove
            foreach (ServerLobbyGame serverLobbyGame in serverLobbyGames.ToArray()) {
                if (!D.LobbyDataList.Contains(serverLobbyGame.LobbyGameData)) {
                    Destroy(serverLobbyGame.gameObject);
                    serverLobbyGames.Remove(serverLobbyGame);
                }
            }
            //  Add
            foreach (LobbyData lgd in D.LobbyDataList) {
                if (!serverLobbyGames.Exists(g => g.LobbyGameData.Equals(lgd))) {
                    ServerLobbyGame go = Instantiate(ServerLobbyGame_Pref, new Vector3(0, 0, 0), Quaternion.identity);
                    go.UpdateUI(lgd, JoinGame_OnClick);
                    go.transform.SetParent(gameListContent, false);
                    serverLobbyGames.Add(go);
                }
            }
        }

        public void CreateGame_OnClick() {
            disableScreen.SetActive(true);
            D.ClientState = ClientState_Enum.CONNECTED_HOST;
            D.C.CreateNewGame();
            D.C.Send_CreateNewGame();
            D.A.UpdateUI();
        }

        public void JoinGame_OnClick(LobbyData lgd) {
            disableScreen.SetActive(true);
            D.ClientState = ClientState_Enum.CONNECTED_JOINING_GAME;
            D.GLD.GameId = lgd.GameId;
            D.GLD.HostKey = lgd.HostPlayer.Key;
            D.C.Send_JoinGame(lgd.HostPlayer.Key, lgd.GameId);
        }

        public void Exit_OnClick() {
            D.C.CloseConnection();
        }
    }
}
