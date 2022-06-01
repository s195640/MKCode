using System;
using System.Collections.Generic;
using cna.connector;
using cna.poo;
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
                    Debug.Log(m.ToString());
                    switch (m.type) {
                        case mType_Enum.OnConnect: {
                            D.LocalPlayer.Key = m.intMsg;
                            D.ClientState = ClientState_Enum.CONNECTED;
                            Send_RequestGameList();
                            D.A.UpdateUI();
                            break;
                        }
                        case mType_Enum.OnDisconnect: {
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
                                    if (D.G.Gld.Players.RemoveAll(p => p.Key == m.intMsg) > 0) {
                                        Send_HostSendsGameDataToClients();
                                        D.A.UpdateUI();
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                        case mType_Enum.OnServerDisconnect: {

                            Debug.Log("OnServerDisconnect");
                            CloseConnection();
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
                            if (D.ClientState == ClientState_Enum.CONNECTED_HOST && m.textMsg_02.Equals(D.G.GameId)) {
                                if (D.G.GameStatus == Game_Enum.New_Game) {
                                    PlayerData playerToAdd = m.getData<PlayerData>();
                                    if (!D.G.Gld.Players.Exists(p => p.Key == playerToAdd.Key)) {
                                        Send_JoinGameRejected(m.sender, D.G.GameId);
                                    } else {
                                        Send_HostSendsGameDataToClients();
                                    }
                                } else {
                                    PlayerData playerToAdd = m.getData<PlayerData>();
                                    if (!D.G.Gld.Players.Exists(p => p.Key == playerToAdd.Key)) {
                                        D.G.Gld.Players.Add(playerToAdd);
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
                            if (D.ClientState == ClientState_Enum.CONNECTED_HOST && m.textMsg_02.Equals(D.G.GameId)) {
                                //  TODO - Check Request
                                D.G.Update(m.getData<GameData>());
                                Send_HostSendsGameDataToClients();
                                D.A.UpdateUI();
                            }
                            break;
                        }
                        case mType_Enum.GameData_Demand: {
                            if (D.ClientState == ClientState_Enum.CONNECTED_HOST && m.textMsg_02.Equals(D.G.GameId)) {
                                GameData g = m.getData<GameData>();
                                D.G.Update(g);
                                Send_HostSendsGameDataToClients();
                                D.A.UpdateUI();
                            }
                            break;
                        }
                        case mType_Enum.GameData_Host: {
                            if (m.textMsg_02.Equals(D.G.GameId)) {
                                if (D.ClientState == ClientState_Enum.CONNECTED_JOINING_GAME) {
                                    D.ClientState = ClientState_Enum.CONNECTED_PLAYER;
                                }
                                D.G.Update(m.getData<GameData>());
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
            Clean();
            D.G.GameId = Guid.NewGuid().ToString();
            D.G.Gld = new GameLobbyData(D.LocalPlayer, D.G.GameId);
            D.G.GameStatus = Game_Enum.CHAR_CREATION;
        }

        public void CloseConnection() {
            Clean();
            D.ClientState = ClientState_Enum.NOT_CONNECTED;
            D.Connector.Close();
            D.A.UpdateUI();
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
                    D.G.Gld.Players.RemoveAll(p => p.Key == D.LocalPlayer.Key);
                    D.C.Send_GameData();
                    D.C.Send_RequestGameList();
                    break;
                }
            }
            D.A.UpdateUI();
        }
        public void Clean() {
            D.A.gameData = new GameData();
            lobbyDataList = new List<LobbyData>();
        }

        #region Messages
        private void Send(wsMsg msg) {
            latestMsgSent = msg;
            D.Send(msg);
        }
        public void Send_RequestGameList() {
            wsMsg msg = new wsMsg();
            msg.d = new wsData(mType_Enum.RequestGameList, D.LocalPlayer.Key);
            Send(msg);
        }
        public void Send_OpenLobbyGame(int requesterKey) {
            wsMsg msg = new wsMsg();
            msg.u.Add(requesterKey);
            msg.d = new wsData(mType_Enum.LobbyGame, new LobbyData(D.LocalPlayer.Name, D.LocalPlayer.Key, D.G.GameId), D.LocalPlayer.Key);
            Send(msg);
        }
        public void Send_DestroyGame() {
            wsMsg msg = new wsMsg();
            msg.d = new wsData(mType_Enum.GameData_Destroy, D.LocalPlayer.Key);
            Send(msg);
        }
        public void Send_CreateNewGame() {
            wsMsg msg = new wsMsg();
            msg.d = new wsData(mType_Enum.LobbyGame, new LobbyData(D.LocalPlayer.Name, D.LocalPlayer.Key, D.G.GameId), D.LocalPlayer.Key);
            Send(msg);
        }
        public void Send_JoinGame(int gameHostKey, string gameid) {
            wsMsg msg = new wsMsg();
            msg.u.Add(gameHostKey);
            msg.d = new wsData(mType_Enum.RequestJoinGame, gameid, new PlayerData(D.LocalPlayer.Name, D.LocalPlayer.Key), D.LocalPlayer.Key);
            Send(msg);
        }
        public void Send_JoinGameRejected(int requesterKey, string gameid) {
            wsMsg msg = new wsMsg();
            msg.u.Add(requesterKey);
            msg.d = new wsData(mType_Enum.RequestJoinGameRejected, gameid, D.LocalPlayer.Key);
            Send(msg);
        }
        public void Send_GameDataRequest(GameData gdClone, string gameid) {
            if (D.isHost) {
                D.G.Update(gdClone);
                Send_HostSendsGameDataToClients();
                D.A.UpdateUI();
            } else {
                wsMsg msg = new wsMsg();
                msg.u.Add(gdClone.Gld.Host.Key);
                msg.d = new wsData(mType_Enum.GameData_Request, gameid, gdClone, D.LocalPlayer.Key);
                Send(msg);
            }
        }
        public void Send_GameData() {
            if (D.isHost) {
                Send_HostSendsGameDataToClients();
            } else {
                wsMsg msg = new wsMsg();
                msg.u.Add(D.HostPlayer.Key);
                msg.d = new wsData(mType_Enum.GameData_Demand, D.G.GameId, D.G, D.LocalPlayer.Key);
                Send(msg);
            }
            D.A.UpdateUI();
        }
        private void Send_HostSendsGameDataToClients() {
            wsMsg msg = new wsMsg();
            D.G.Gld.Players.ForEach(p => {
                if (p.Key != D.LocalPlayer.Key) {
                    msg.u.Add(p.Key);
                }
            });
            msg.d = new wsData(mType_Enum.GameData_Host, D.G.GameId, D.G, D.LocalPlayer.Key);
            Send(msg);
        }

        public void Send_Chat(ChatItemData cid) {
            wsMsg msg = new wsMsg();
            msg.d = new wsData(mType_Enum.Chat, cid, D.LocalPlayer.Key);
            Send(msg);
        }
        public void LogMessage(string logMsg) {
            DateTime dt = DateTime.Now;
            long t = dt.Ticks;
            LogData log = new LogData(D.LocalPlayer.Key, logMsg, t);
            Send_GameLog(log);
        }
        public void LogMessageDummy(string logMsg) {
            DateTime dt = DateTime.Now;
            long t = dt.Ticks;
            LogData log = new LogData(-999, logMsg, t);
            Send_GameLog(log);
        }
        private void Send_GameLog(LogData log) {
            wsMsg msg = new wsMsg();
            msg.d = new wsData(mType_Enum.GameLog, D.G.GameId, log, D.LocalPlayer.Key);
            D.LogQueue.Enqueue(log);
            Send(msg);
        }
        #endregion

    }
}
