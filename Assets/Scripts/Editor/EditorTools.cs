using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using cna.poo;
using cna;
using System;

namespace cna.editor {
    [InitializeOnLoad]
    public class EditorTools {

        static EditorTools() {
            EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }

        static void PlayModeStateChanged(PlayModeStateChange state) {
            if (state.Equals(PlayModeStateChange.EnteredEditMode)) {
                AppEngineHelper.UpdateSpriteMap();
            }
        }



        [MenuItem("Tools/Rebuild")]
        static void REBUILD() {
            if (D.SpriteMap != null) {
                D.SpriteMap.Clear();
                D.SpriteMap = null;
            }
            AppEngineHelper.UpdateSpriteMap();
        }

        #region LobbyGame
        [MenuItem("Tools/WS/LobbyGame/100")]
        static void LobbyGame_100() {
            LobbyGame(100);
        }
        [MenuItem("Tools/WS/LobbyGame/101")]
        static void LobbyGame_101() {
            LobbyGame(101);
        }
        [MenuItem("Tools/WS/LobbyGame/102")]
        static void LobbyGame_102() {
            LobbyGame(102);
        }
        [MenuItem("Tools/WS/LobbyGame/103")]
        static void LobbyGame_103() {
            LobbyGame(103);
        }
        static void LobbyGame(int hostId) {
            wsMsg msg = new wsMsg();
            msg.u.AddRange(new int[] { 0, 1, 2, 3, 4, 5, 6 });
            msg.d = new wsData(mType_Enum.LobbyGame, new LobbyData("Test " + hostId, hostId, "" + hostId), hostId);
            D.Send(msg);
        }
        #endregion

        #region Disconnect

        [MenuItem("Tools/WS/Disconnect/100")]
        static void Disconnect_100() {
            Disconnect(100);
        }
        [MenuItem("Tools/WS/Disconnect/101")]
        static void Disconnect_101() {
            Disconnect(101);
        }
        [MenuItem("Tools/WS/Disconnect/102")]
        static void Disconnect_102() {
            Disconnect(102);
        }
        [MenuItem("Tools/WS/Disconnect/103")]
        static void Disconnect_103() {
            Disconnect(103);
        }

        static void Disconnect(int hostId) {
            wsMsg msg = new wsMsg();
            msg.u.AddRange(new int[] { 0, 1, 2, 3, 4, 5, 6 });
            msg.d = new wsData(mType_Enum.OnDisconnect, hostId);
            D.Send(msg);
        }

        #endregion

        #region DestroyGame
        [MenuItem("Tools/WS/DestroyGame/100")]
        static void DestroyGame_100() {
            DestroyGame(100);
        }
        [MenuItem("Tools/WS/DestroyGame/101")]
        static void DestroyGame_101() {
            DestroyGame(101);
        }
        [MenuItem("Tools/WS/DestroyGame/102")]
        static void DestroyGame_102() {
            DestroyGame(102);
        }
        [MenuItem("Tools/WS/DestroyGame/103")]
        static void DestroyGame_103() {
            DestroyGame(103);
        }
        static void DestroyGame(int hostId) {
            wsMsg msg = new wsMsg();
            msg.u.AddRange(new int[] { 0, 1, 2, 3, 4, 5, 6 });
            msg.d = new wsData(mType_Enum.GameData_Destroy, hostId);
            D.Send(msg);
        }
        #endregion

        #region RequestJoinGameRejected
        [MenuItem("Tools/WS/RequestJoinGameRejected/6")]
        static void RequestJoinGameRejected_6() {
            RequestJoinGameRejected(6);
        }
        static void RequestJoinGameRejected(int hostId) {
            wsMsg msg = new wsMsg();
            msg.u.AddRange(new int[] { hostId });
            msg.d = new wsData(mType_Enum.RequestJoinGameRejected, 0);
            D.Send(msg);
        }
        #endregion

        #region Chat
        [MenuItem("Tools/WS/Chat/100")]
        static void Chat_100() {
            Chat(100);
        }
        [MenuItem("Tools/WS/Chat/101")]
        static void Chat_101() {
            Chat(101);
        }
        static void Chat(int hostId) {
            wsMsg msg = new wsMsg();
            msg.u.AddRange(new int[] { 0, 1, 2, 3, 4, 5, 6 });
            DateTime dt = DateTime.Now;
            long t = dt.Ticks;
            string chatMsg = "Test message from " + hostId;
            ChatItemData cid = new ChatItemData("TEST_" + hostId, chatMsg, t);
            msg.d = new wsData(mType_Enum.Chat, cid, hostId);
            D.Send(msg);
        }
        #endregion

        //[MenuItem("Tools/WS/Lobby")]
        //static void Lobby() {

        //    //LobbyData ld = new LobbyData();
        //    //ld.LobbyGames = new List<LobbyGameData>();
        //    //LobbyGameData lgd = new LobbyGameData();
        //    //lgd.HostPlayer = "HOST_PLAYER";
        //    //lgd.GameStarted = true;
        //    //lgd.Gameid = 324;
        //    //ld.LobbyGames.Add(lgd);

        //    //Debug.Log(ld.LobbyGames.ToString());
        //    //GE.SendMsg(ld.LobbyGames.ToString());
        //}
        //[MenuItem("Tools/UpdateGameData")]
        //static void UpdateGameData() {
        //    //GameDataDebug gdd = GameObject.Find("GameDataDebug").GetComponent<GameDataDebug>();
        //    //gdd.gameData = GE.GameData;
        //    //gdd.localData = GE.Local;
        //}
        //[MenuItem("Tools/WS/Ping")]
        //static void Ping() {
        //    //GE.Ping();
        //}
    }
}