using System.Collections.Generic;
using cna.poo;

namespace cna.ui {
    public class UIEngine : UnityEngine.MonoBehaviour {

        private List<wsData> wsQueue = new List<wsData>();


        //public UIEngine() {
        //GlobalGameData.Initialize(UpdateUI);
        //}
        public void OnWsEvent(wsData e) {
            wsQueue.Add(e);
        }

        public void UpdateUI() {
            //switch (GE.Local.State) {
            //    case AppState_Enum.StartupLobby: {
            //        SetActiveCanvas(startupLobby);
            //        startupLobby.UpdateUI();
            //        break;
            //    }
            //    case AppState_Enum.ServerLobby: {
            //        SetActiveCanvas(serverLobby);
            //        serverLobby.UpdateUI();
            //        break;
            //    }
            //    case AppState_Enum.GameLobby: {
            //        SetActiveCanvas(gameLobby);
            //        gameLobby.UpdateUI();
            //        break;
            //    }
            //}
        }




        //private void Start() {
        //    chatWindow.gameObject.SetActive(false);
        //    serverMessage.gameObject.SetActive(false);
        //    SetActiveCanvas(startupLobby);
        //}


        //[SerializeField] private StartupLobby startupLobby;
        //[SerializeField] private ServerLobby serverLobby;
        //[SerializeField] private GameLobby gameLobby;
        //[SerializeField] private ServerMessage serverMessage;
        //[SerializeField] private GameObject disableScreen;
        //[SerializeField] private ChatWindow chatWindow;

        //private List<wsData> wSMessages = new List<wsData>();




        //private void Update() {
        //    GE.DispatchMessageQueue();
        //    if (wSMessages.Count > 0) {
        //        foreach (wsData m in wSMessages.ToArray()) {
        //            Debug.Log(m.ToString());
        //            wSMessages.Remove(m);
        //            switch (m.type) {
        //                case mType_Enum.OnConnect: {
        //                    GE.Local.State = AppState_Enum.ServerLobby;
        //                    GE.Local.Player.Key = m.intMsg;
        //                    wsMsg.REQUEST_GAMES();
        //                    UpdateUI();
        //                    break;
        //                }
        //                case mType_Enum.OnDisconnect: {
        //                    if (GE.Local.State == AppState_Enum.GameLobby) {
        //                        //  Non Host Leaves
        //                        if (GE.GameData.Gld.Host.Key == GE.Local.Player.Key) {
        //                            int index = GE.GameData.Gld.Players.FindIndex(p => p.Key == m.intMsg);
        //                            if (index > 0) {
        //                                GE.GameData.Gld.Players.RemoveAt(index);
        //                                wsMsg.GAME_DATA();
        //                                UpdateUI();
        //                            }
        //                        } else {
        //                            //  Clinet is IN a game the host leaves
        //                            if (GE.GameData.Gld.Host.Key == m.intMsg) {
        //                                GE.Local.State = AppState_Enum.ServerLobby;
        //                                GE.GameData.Gld = null;
        //                                wsMsg.REQUEST_GAMES();
        //                                UpdateUI();
        //                            }
        //                        }
        //                    } else if (GE.Local.State == AppState_Enum.ServerLobby) {
        //                        int index = GE.Local.LobbyGameDataList.FindIndex(g => g.HostPlayer.Key == m.intMsg);
        //                        if (index >= 0) {
        //                            GE.Local.LobbyGameDataList.RemoveAt(index);
        //                            UpdateUI();
        //                        }
        //                    }
        //                    break;
        //                }
        //                case mType_Enum.OnServerDisconnect: {
        //                    serverMessage.DisplaySystemMessage(m.intMsg);
        //                    break;
        //                }
        //                case mType_Enum.RequestGameList: {
        //                    if (GE.Local.State == AppState_Enum.GameLobby) {
        //                        if (GE.GameData.Gld.Host.Key == GE.Local.Player.Key) {
        //                            wsMsg.SEND_LOBBY_GAME(new LobbyData(GE.Local.Player), m.sender);
        //                        }
        //                    }
        //                    break;
        //                }
        //                case mType_Enum.LobbyGame: {
        //                    if (GE.Local.State == AppState_Enum.ServerLobby) {
        //                        GE.Local.add(m.getData<LobbyData>());
        //                        UpdateUI();
        //                    }
        //                    break;
        //                }
        //                case mType_Enum.RequestJoinGame: {
        //                    if (m.intMsg == GE.Local.Player.Key) {
        //                        PlayerBase playerToAdd = m.getData<PlayerBase>();
        //                        if (!GE.GameData.Gld.Players.Exists(p => p.Key == playerToAdd.Key)) {
        //                            GE.GameData.Gld.Players.Add(new GLDPlayer(playerToAdd));
        //                        }
        //                        wsMsg.GAME_DATA();
        //                        UpdateUI();
        //                    }
        //                    break;
        //                }
        //                case mType_Enum.GameData: {
        //                    GE.GameData.Update(m.getData<GameData>());
        //                    if (GE.GameData.Gld.Players.Exists(p => p.Key == GE.Local.Player.Key)) {
        //                        GE.Local.State = AppState_Enum.GameLobby;
        //                        GE.Local.LobbyGameDataList.Clear();
        //                    }
        //                    UpdateUI();
        //                    break;
        //                }
        //                case mType_Enum.GameData_Demand:
        //                case mType_Enum.GameData_Request: {
        //                    GE.GameData.Update(m.getData<GameData>());
        //                    wsMsg.GAME_DATA();
        //                    UpdateUI();
        //                    break;
        //                }
        //                case mType_Enum.GameData_Destroy: {
        //                    if (GE.Local.State == AppState_Enum.GameLobby) {
        //                        if (GE.GameData.Gld.Host.Key == m.intMsg) {
        //                            GE.Local.State = AppState_Enum.ServerLobby;
        //                            GE.GameData.Gld = null;
        //                            wsMsg.REQUEST_GAMES();
        //                            UpdateUI();
        //                        }
        //                    } else if (GE.Local.State == AppState_Enum.ServerLobby) {
        //                        int index = GE.Local.LobbyGameDataList.FindIndex(g => g.HostPlayer.Key == m.intMsg);
        //                        GE.Local.LobbyGameDataList.RemoveAt(index);
        //                        UpdateUI();
        //                    }
        //                    break;
        //                }
        //                case mType_Enum.Chat: {
        //                    chatWindow.addNewChatItem(m.getData<ChatItemData>());
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        //private void SetActiveCanvas(MonoBehaviour go) {
        //    if (go == startupLobby) {
        //        chatWindow.gameObject.SetActive(false);
        //    } else {
        //        chatWindow.gameObject.SetActive(true);
        //    }
        //    startupLobby.gameObject.SetActive(go == startupLobby);
        //    serverLobby.gameObject.SetActive(go == serverLobby);
        //    gameLobby.gameObject.SetActive(go == gameLobby);
        //    disableScreen.SetActive(false);
        //}



        //private void OnApplicationQuit() {
        //    GE.Clean();
        //}




        ////private void Update() {
        ////    GE.DispatchMessageQueue();
        ////    if (wSMessages.Count > 0) {
        ////        foreach (EventMessageWrapper m in wSMessages.ToArray()) {
        ////            wSMessages.Remove(m);
        ////            Debug.Log(m);
        ////            switch (m.MessageType) {
        ////                case MessageType_Enum.CLOSE: {
        ////                    GE.Data = null;
        ////                    SetActiveCanvas(startupLobby.gameObject);
        ////                    serverMessage.DisplaySystemMessage(m.Code, m.Message);
        ////                    wSMessages.Clear();
        ////                    return;
        ////                }
        ////                case MessageType_Enum.RESET: {
        ////                    GE.Reset();
        ////                    wSMessages.Clear();
        ////                    UpdateUI();
        ////                    return;
        ////                }
        ////                case MessageType_Enum.TEXT: {
        ////                    char code = m.Message[0];
        ////                    string msg = m.Message.Substring(1);
        ////                    switch (code) {
        ////                        case 'L': {
        ////                            GE.LobbyData = new LobbyData(msg);
        ////                            GE.LobbyGameData = null;
        ////                            UpdateUI();
        ////                            break;
        ////                        }
        ////                        case 'M': {
        ////                            //  update lobby game
        ////                            GE.LobbyGameData = new LobbyGameData(msg);
        ////                            UpdateUI();
        ////                            break;
        ////                        }
        ////                    }
        ////                    break;
        ////                }
        ////            }
        ////        }
        ////    }
        ////}

        ////public void UpdateUI() {
        ////    if (GE.Data != null) {

        ////    } else if (GE.LobbyGameData != null) {
        ////        SetActiveCanvas(gameLobby.gameObject);
        ////        gameLobby.UpdateUI();
        ////    } else if (GE.LobbyData != null) {
        ////        SetActiveCanvas(serverLobby.gameObject);
        ////        serverLobby.UpdateUI();
        ////    } else {
        ////        SetActiveCanvas(startupLobby.gameObject);
        ////        startupLobby.UpdateUI();
        ////    }
        ////}



        ////public void ExitToLobbyButton() {
        ////    Debug.Log("ExitLobbyButton");
        ////    GE.Close();
        ////    serverMessage.gameObject.SetActive(false);
        ////    UpdateUI();
        ////}
        ////public void beforeMsgSend() {
        ////    UpdateUI();
        ////}


    }
}
