using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class GameSetup : MonoBehaviour {

        [SerializeField] private StartupLobby startupLobby;
        [SerializeField] private ServerLobby serverLobby;
        [SerializeField] private GameLobby gameLobby;
        [SerializeField] private ServerMessage serverMessage;
        [SerializeField] private GameObject disableScreen;

        void Start() {
            serverLobby.gameObject.SetActive(false);
            gameLobby.gameObject.SetActive(false);
            serverMessage.gameObject.SetActive(false);
            disableScreen.gameObject.SetActive(false);
            startupLobby.gameObject.SetActive(true);
        }

        public void UpdateUI() {
            Clear();
            switch (D.ClientState) {
                case ClientState_Enum.NOT_CONNECTED: {
                    startupLobby.gameObject.SetActive(true);
                    startupLobby.UpdateUI();
                    break;
                }
                case ClientState_Enum.CONNECTING: {
                    disableScreen.gameObject.SetActive(true);
                    break;
                }
                case ClientState_Enum.CONNECTING_FAILED: {
                    disableScreen.gameObject.SetActive(false);
                    serverMessage.LoginFailed(() => { D.ClientState = ClientState_Enum.NOT_CONNECTED; D.A.UpdateUI(); });
                    break;
                }
                case ClientState_Enum.CONNECTED: {
                    serverLobby.gameObject.SetActive(true);
                    serverLobby.UpdateUI();
                    break;
                }
                case ClientState_Enum.SINGLE_PLAYER:
                case ClientState_Enum.CONNECTED_HOST:
                case ClientState_Enum.CONNECTED_PLAYER: {
                    gameLobby.gameObject.SetActive(true);
                    gameLobby.UpdateUI();
                    break;
                }
            }
        }

        private void Clear() {
            serverLobby.gameObject.SetActive(false);
            gameLobby.gameObject.SetActive(false);
            serverMessage.gameObject.SetActive(false);
            disableScreen.gameObject.SetActive(false);
            startupLobby.gameObject.SetActive(false);
        }
    }
}
