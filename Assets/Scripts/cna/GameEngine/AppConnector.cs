using System;
using System.Collections.Generic;
using cna.connector;
using cna.poo;
using UnityEditor;
using UnityEngine;

namespace cna {
    public class AppConnector : MonoBehaviour {
        internal Queue<ChatItemData> chatQueue = new Queue<ChatItemData>();
        internal Queue<LogData> logQueue = new Queue<LogData>();
        internal Queue<wsData> wsQueue = new Queue<wsData>();
        [SerializeField] internal wsData latestMsgReceive;
        [SerializeField] internal wsMsg latestMsgSent;
        [SerializeField] internal List<LobbyData> lobbyDataList;


        private void Update() {
            if (D.Connector != null) {
                D.Connector.DispatchMessageQueue();
                if (wsQueue.Count > 0) {
                    wsData m = wsQueue.Dequeue();
                    latestMsgReceive = m;
                    //Debug.Log(m.ToString());
                    //Debug.Log(m.type);
                    switch (m.type) {
                        case mType_Enum.OnConnect: {
                            if (D.Connector.Player.Key != m.intMsg || D.ClientState < ClientState_Enum.CONNECTED) {
                                D.Connector.Player.Key = m.intMsg;
                                D.ClientState = ClientState_Enum.CONNECTED;
                                Send_RequestGameList();
                                D.A.UpdateUI();
                            }
                            break;
                        }
                        case mType_Enum.OnDisconnect: {
                            //Debug.Log("OnDisconnect");
                            switch (D.ClientState) {
                                case ClientState_Enum.CONNECTED_JOINING_GAME:
                                case ClientState_Enum.CONNECTED: {
                                    lobbyDataList.RemoveAll(l => l.HostPlayer.Key == m.intMsg);
                                    D.A.UpdateUI();
                                    break;
                                }
                                case ClientState_Enum.CONNECTED_PLAYER: {
                                    if (D.HostPlayer.Key == m.intMsg) {
                                        D.ClientState = ClientState_Enum.CONNECTED;
                                        Clean();
                                        Send_RequestGameList();
                                        D.A.UpdateUI();
                                    }
                                    break;
                                }
                                case ClientState_Enum.CONNECTED_HOST: {
                                    if (D.G.Players.RemoveAll(p => p.Key == m.intMsg) > 0) {
                                        Send_HostSendsGameDataToClients();
                                        D.A.UpdateUI();
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                        case mType_Enum.OnServerDisconnect: {
                            if (D.ClientState == ClientState_Enum.CONNECTING) {
                                D.ClientState = ClientState_Enum.CONNECTING_FAILED;
                                D.A.UpdateUI();
                            } else {
                                Reconnect();
                            }
                            break;
                        }
                        case mType_Enum.OnReconnect: {
                            //Debug.Log("App OnReconnect " + D.ClientState);
                            switch (D.ClientState) {
                                case ClientState_Enum.CONNECTED_JOINING_GAME:
                                case ClientState_Enum.CONNECTED: {
                                    break;
                                }
                                case ClientState_Enum.CONNECTED_PLAYER: {
                                    Send_JoinGame(D.G.HostPlayerKey, D.G.GameId);
                                    break;
                                }
                                case ClientState_Enum.CONNECTED_HOST: {
                                    Send_HostSendsGameDataToClients();
                                    D.A.UpdateUI();
                                    break;
                                }
                            }
                            break;
                        }
                        case mType_Enum.RequestGameList: {
                            if (D.ClientState == ClientState_Enum.CONNECTED_HOST) {
                                Send_OpenLobbyGame(m.sender);
                            }
                            break;
                        }
                        case mType_Enum.LobbyGame: {
                            if (D.ClientState == ClientState_Enum.CONNECTED) {
                                LobbyData ld = m.getData<LobbyData>();
                                if (!D.LobbyDataList.Contains(ld)) {
                                    D.LobbyDataList.Add(ld);
                                }
                                D.A.UpdateUI();
                            }
                            break;
                        }
                        case mType_Enum.GameData_Destroy: {
                            if (D.ClientState == ClientState_Enum.CONNECTED) {
                                lobbyDataList.RemoveAll(l => l.HostPlayer.Key == m.sender);
                                D.A.UpdateUI();
                            } else if (D.ClientState == ClientState_Enum.CONNECTED_PLAYER) {
                                D.ClientState = ClientState_Enum.CONNECTED;
                                Clean();
                                Send_RequestGameList();
                                D.A.UpdateUI();
                            }
                            break;
                        }
                        case mType_Enum.RequestJoinGame: {
                            if (D.ClientState == ClientState_Enum.CONNECTED_HOST && m.gameHostKey.Equals(D.HostPlayerKey)) {
                                if (D.G.GameStatus == Game_Enum.New_Game) {
                                    PlayerData playerToAdd = m.getData<PlayerData>();
                                    if (!D.G.Players.Exists(p => p.Key == playerToAdd.Key)) {
                                        Send_JoinGameRejected(m.sender, D.G.GameId);
                                    } else {
                                        Send_HostSendsGameDataToClients();
                                    }
                                } else {
                                    PlayerData playerToAdd = m.getData<PlayerData>();
                                    if (!D.G.Players.Exists(p => p.Key == playerToAdd.Key)) {
                                        D.G.Players.Add(playerToAdd);
                                        D.A.UpdateUI();
                                    }
                                    Send_HostSendsGameDataToClients();
                                }
                            }
                            break;
                        }
                        case mType_Enum.RequestJoinGameRejected: {
                            if (D.ClientState == ClientState_Enum.CONNECTED_JOINING_GAME) {
                                Debug.Log("Join Request Rejected");
                                D.ClientState = ClientState_Enum.CONNECTED;
                                D.A.UpdateUI();
                            }
                            break;
                        }
                        case mType_Enum.GameData_Request: {
                            if (D.ClientState == ClientState_Enum.CONNECTED_HOST && m.gameHostKey.Equals(D.HostPlayerKey)) {
                                //  TODO - Check Request
                                UpdateGameData(m.getData<Data>());
                                Send_HostSendsGameDataToClients();
                                D.A.UpdateUI();
                            }
                            break;
                        }
                        case mType_Enum.GameData_Demand: {
                            if (D.ClientState == ClientState_Enum.CONNECTED_HOST && m.gameHostKey.Equals(D.HostPlayerKey)) {
                                Data g = m.getData<Data>();
                                UpdateGameData(g);
                                Send_HostSendsGameDataToClients();
                                D.A.UpdateUI();
                            }
                            break;
                        }
                        case mType_Enum.PlayerData_ToHost: {
                            if (D.ClientState == ClientState_Enum.CONNECTED_HOST && m.gameHostKey.Equals(D.HostPlayerKey)) {
                                PlayerData p = m.getData<PlayerData>();
                                UpdatePlayerData(p);
                                Send_HostSendsPlayerDataToClients(p);
                                D.A.UpdateUI();
                            }
                            break;
                        }
                        case mType_Enum.GameData_Host: {
                            if (m.gameHostKey.Equals(D.HostPlayerKey)) {
                                if (D.ClientState == ClientState_Enum.CONNECTED_JOINING_GAME) {
                                    D.ClientState = ClientState_Enum.CONNECTED_PLAYER;
                                }
                                UpdateGameData(m.getData<Data>());
                                D.A.UpdateUI();
                            }
                            break;
                        }
                        case mType_Enum.PlayerData_FromHost: {
                            if (m.gameHostKey.Equals(D.HostPlayerKey)) {
                                if (D.ClientState == ClientState_Enum.CONNECTED_JOINING_GAME) {
                                    D.ClientState = ClientState_Enum.CONNECTED_PLAYER;
                                }
                                UpdatePlayerData(m.getData<PlayerData>());
                                D.A.UpdateUI();
                            }
                            break;
                        }
                        case mType_Enum.Chat: {
                            chatQueue.Enqueue(m.getData<ChatItemData>());
                            break;
                        }
                        case mType_Enum.GameLog: {
                            logQueue.Enqueue(m.getData<LogData>());
                            break;
                        }
                    }
                }
            }
        }
        public void OnWsEvent(wsData e) {
            wsQueue.Enqueue(e);
        }

        public void StartNewSoloGame(string un) {
            Clean();
            D.ClientState = ClientState_Enum.SINGLE_PLAYER;
            D.Connector = new SoloConnector(un, OnWsEvent);
            CreateNewGame();
            D.A.UpdateUI();
        }

        public void ConnectToServer(string un, string pw, string url) {
            Clean();
            D.ClientState = ClientState_Enum.CONNECTING;
            D.Connector = new MultiConnector(un, pw, url, OnWsEvent);
            D.A.UpdateUI();
        }


        public void CreateNewGame() {
            D.G = new Data(Guid.NewGuid().ToString(), D.Connector.Player);
            D.G.GameStatus = Game_Enum.CHAR_CREATION;
        }

        public void CloseConnection() {
            Clean();
            D.ClientState = ClientState_Enum.NOT_CONNECTED;
            D.Connector.Close();
            D.A.UpdateUI();
        }

        public void Reconnect() {
            ((MultiConnector)D.Connector).Reconnect();
        }

        public void QuitGame() {
            switch (D.ClientState) {
                case ClientState_Enum.SINGLE_PLAYER: {
                    D.ClientState = ClientState_Enum.NOT_CONNECTED;
                    break;
                }
                case ClientState_Enum.CONNECTED_HOST: {
                    D.ClientState = ClientState_Enum.CONNECTED;
                    D.C.Send_DestroyGame();
                    D.C.Send_RequestGameList();
                    break;
                }
                case ClientState_Enum.CONNECTED_PLAYER: {
                    D.ClientState = ClientState_Enum.CONNECTED;
                    D.G.Players.RemoveAll(p => p.Key == D.LocalPlayerKey);
                    D.C.Send_GameData();
                    D.C.Send_RequestGameList();
                    break;
                }
            }
            D.A.UpdateUI();
        }
        public void Clean() {
            D.A.New();
            lobbyDataList = new List<LobbyData>();
        }

        public void UpdateGameData(Data data) {
            D.G.UpdateData(data);
        }
        public void UpdatePlayerData(PlayerData playerData) {
            D.G.Players.Find(p => p.Key == playerData.Key).UpdateData(playerData);
        }

        #region Messages
        private void Send(wsMsg msg) {
            latestMsgSent = msg;
            D.Send(msg);
        }
        public void Send_RequestGameList() {
            wsMsg msg = new wsMsg();
            msg.d = new wsData(mType_Enum.RequestGameList, D.Connector.Player.Key);
            Send(msg);
        }
        public void Send_OpenLobbyGame(int requesterKey) {
            wsMsg msg = new wsMsg();
            msg.u.Add(requesterKey);
            msg.d = new wsData(mType_Enum.LobbyGame, new LobbyData(D.LocalPlayer.Name, D.LocalPlayerKey, D.G.GameId), D.LocalPlayerKey);
            Send(msg);
        }
        public void Send_DestroyGame() {
            wsMsg msg = new wsMsg();
            msg.d = new wsData(mType_Enum.GameData_Destroy, D.LocalPlayerKey);
            Send(msg);
        }
        public void Send_CreateNewGame() {
            wsMsg msg = new wsMsg();
            msg.d = new wsData(mType_Enum.LobbyGame, new LobbyData(D.LocalPlayer.Name, D.LocalPlayerKey, D.G.GameId), D.LocalPlayerKey);
            Send(msg);
        }
        public void Send_JoinGame(int gameHostKey, string gameid) {
            wsMsg msg = new wsMsg();
            msg.u.Add(gameHostKey);
            //msg.d = new wsData(mType_Enum.RequestJoinGame, gameid, D.Connector.Player, D.Connector.Player.Key);
            msg.d = new wsData(mType_Enum.RequestJoinGame, gameHostKey, D.Connector.Player, D.Connector.Player.Key);
            Send(msg);
        }
        public void Send_JoinGameRejected(int requesterKey, string gameid) {
            wsMsg msg = new wsMsg();
            msg.u.Add(requesterKey);
            msg.d = new wsData(mType_Enum.RequestJoinGameRejected, gameid, D.LocalPlayerKey);
            Send(msg);
        }
        public void Send_GameDataRequest(Data gdClone, string gameid) {
            if (D.isHost) {
                UpdateGameData(gdClone);
                Send_HostSendsGameDataToClients();
                D.A.UpdateUI();
            } else {
                wsMsg msg = new wsMsg();
                msg.u.Add(gdClone.GameData.HostKey);
                //msg.d = new wsData(mType_Enum.GameData_Request, gameid, gdClone, D.LocalPlayerKey);
                msg.d = new wsData(mType_Enum.GameData_Request, gdClone.GameData.HostKey, gdClone, D.LocalPlayerKey);
                Send(msg);
            }
        }
        public void Send_GameData() {
            if (D.isHost) {
                Send_HostSendsGameDataToClients();
            } else {
                wsMsg msg = new wsMsg();
                msg.u.Add(D.HostPlayer.Key);
                //msg.d = new wsData(mType_Enum.GameData_Demand, D.G.GameId, D.G, D.LocalPlayerKey);
                msg.d = new wsData(mType_Enum.GameData_Demand, D.HostPlayer.Key, D.G, D.LocalPlayerKey);
                Send(msg);
            }
            D.A.UpdateUI();
        }
        private void Send_HostSendsGameDataToClients() {
            wsMsg msg = new wsMsg();
            D.G.Players.ForEach(p => {
                if (p.Key != D.LocalPlayerKey) {
                    msg.u.Add(p.Key);
                }
            });
            //msg.d = new wsData(mType_Enum.GameData_Host, D.G.GameId, D.G, D.LocalPlayerKey);
            msg.d = new wsData(mType_Enum.GameData_Host, D.HostPlayer.Key, D.G, D.LocalPlayerKey);
            Send(msg);
        }

        public void Send_Chat(ChatItemData cid) {
            wsMsg msg = new wsMsg();
            msg.d = new wsData(mType_Enum.Chat, cid, D.LocalPlayerKey);
            Send(msg);
        }
        public void LogMessage(string logMsg) {
            DateTime dt = DateTime.Now;
            long t = dt.Ticks;
            LogData log = new LogData(D.LocalPlayerKey, logMsg, t);
            Send_GameLog(log);
        }
        public void LogMessageDummy(string logMsg) {
            DateTime dt = DateTime.Now;
            long t = dt.Ticks;
            LogData log = new LogData(-999, logMsg, t);
            Send_GameLog(log);
        }
        private void Send_GameLog(LogData log) {
            //wsMsg msg = new wsMsg();
            //msg.d = new wsData(mType_Enum.GameLog, D.G.GameId, log, D.LocalPlayerKey);
            //D.LogQueue.Enqueue(log);
            //Send(msg);
        }

        #region send player data
        public void Send_PlayerData() {
            PlayerData playerData = D.LocalPlayer;
            if (D.isHost) {
                Send_HostSendsPlayerDataToClients(playerData);
            } else {
                wsMsg msg = new wsMsg();
                msg.u.Add(D.HostPlayer.Key);
                //msg.d = new wsData(mType_Enum.PlayerData_ToHost, D.G.GameId, playerData, playerData.Key);
                msg.d = new wsData(mType_Enum.PlayerData_ToHost, D.HostPlayer.Key, playerData, playerData.Key);
                Send(msg);
            }
            D.A.UpdateUI();
        }
        public void Send_HostSendsPlayerDataToClients(PlayerData playerData) {
            wsMsg msg = new wsMsg();
            D.G.Players.ForEach(p => {
                if (p.Key != D.LocalPlayerKey) {
                    msg.u.Add(p.Key);
                }
            });
            //msg.d = new wsData(mType_Enum.PlayerData_FromHost, D.G.GameId, playerData, D.LocalPlayerKey);
            msg.d = new wsData(mType_Enum.PlayerData_FromHost, D.HostPlayer.Key, playerData, D.LocalPlayerKey);
            Send(msg);
        }
        #endregion


        #endregion

    }
}
