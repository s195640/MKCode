using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using cna.poo;
using cna;
using System;
using cna.ui;
using System.Threading;
using System.Threading.Tasks;

namespace cna.editor {
    [InitializeOnLoad]
    public class EditorTools {

        static EditorTools() {
            //EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }

        //static void PlayModeStateChanged(PlayModeStateChange state) {
        //    if (state.Equals(PlayModeStateChange.EnteredEditMode)) {
        //        //AppEngineHelper.UpdateSpriteMap();
        //        DisplayAllImages_ToWork();
        //    }
        //}

        [MenuItem("Tools/JSON")]
        static void TryJSON() {
            string json = D.G.ToDataStr();
            Debug.Log(json.Length);
            Debug.Log(json);
        }
        [MenuItem("Tools/Serialize")]
        static void TrySerialize() {
            string data = D.G.Serialize();
            Debug.Log(data.Length);
            Debug.Log(data);
        }
        [MenuItem("Tools/Zip")]
        static void TryZip() {
            string data = CNASerialize.Sz(D.G);
            string json = D.G.ToDataStr();
            string zipData = CNASerialize.Zip(data);
            string zipJson = CNASerialize.Zip(json);
            Debug.Log("DATA :: " + data.Length + " : " + zipData.Length);
            Debug.Log("JSON :: " + json.Length + " : " + zipJson.Length);

        }

        [MenuItem("Tools/SerializeTest")]
        static void TrySerialTest() {
            string data01 = CNASerialize.Sz(D.G);
            Data d;
            CNASerialize.Dz(data01, out d);
            string data02 = CNASerialize.Sz(d);
            CNASerialize.Dz(data02, out d);

            string json01 = D.G.ToDataStr();
            string json02 = d.ToDataStr();
            Debug.Log(json01.Equals(json02));
            Debug.Log(json01);
            Debug.Log(json02);
        }
        [MenuItem("Tools/StopWatch")]
        static void TryStopWatch() {
            Data _obj01;
            Data _obj02;

            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            string json = D.G.ToDataStr();
            string z01 = CNASerialize.Zip(json);
            string z02 = CNASerialize.Unzip(z01);
            _obj01 = JsonUtility.FromJson<Data>(z02);
            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            Debug.Log("JSON = " + timeTaken.ToString(@"m\:ss\.fff"));
            timer.Reset();
            timer.Start();
            string data = D.G.Serialize();
            string z03 = CNASerialize.Zip(json);
            string z04 = CNASerialize.Unzip(z03);
            CNASerialize.Dz(data, out _obj02);
            timer.Stop();
            timeTaken = timer.Elapsed;
            Debug.Log("DATA = " + timeTaken.ToString(@"m\:ss\.fff"));
        }


        [MenuItem("Tools/TEST01")]
        static void TryTest01() {
            string data = D.G.Board.Serialize();
            Debug.Log(data.Length);
            Debug.Log(data);
        }
        [MenuItem("Tools/TEST02")]
        static void TryTest02() {
            string data = D.G.Board.MonsterData.Serialize();
            Debug.Log(data.Length);
            Debug.Log(data);
        }



        [MenuItem("Tools/ButtonsImages")]
        static void Fix_CNA_Buttons() {
            Fix_CNA_Buttons_ToWork();
        }

        async static void Fix_CNA_Buttons_ToWork() {
            if (D.SpriteMap == null || D.SpriteMap.Keys.Count < 300) {
                AppEngineHelper.UpdateSpriteMap();
                await Task.Delay(3000);
            }
            AddressableImage[] addressableImages = Resources.FindObjectsOfTypeAll<AddressableImage>();
            foreach (AddressableImage ai in addressableImages) {
                ai.UpdateUI();
            }

            CNA_Button[] cna_buttons = Resources.FindObjectsOfTypeAll<CNA_Button>();
            foreach (CNA_Button b in cna_buttons) {
                foreach (RectTransform g in b.GetComponentsInChildren<RectTransform>(true)) {
                    if (g.name.Equals("TextAndImage")) {
                        RectTransform i = null;
                        RectTransform t = null;
                        foreach (RectTransform TextAndImage in b.GetComponentsInChildren<RectTransform>(true)) {
                            if (TextAndImage.name.Equals("Image")) {
                                i = TextAndImage;
                            } else if (TextAndImage.name.Equals("TextWithImage")) {
                                t = TextAndImage;

                            }
                        }
                        if (t != null) {
                            float width = t.parent.GetComponent<RectTransform>().rect.width - i.rect.width;
                            t.anchorMin = new Vector2(1, 0);
                            t.anchorMax = new Vector2(1, 1);
                            t.offsetMax = new Vector2(0, 0);
                            t.offsetMin = new Vector2(-1 * width, 0);
                        }
                    }
                }
            }
        }



        [MenuItem("Tools/DisplayImages")]
        static void DisplayAllImages() {
            DisplayAllImages_ToWork();
        }
        async static void DisplayAllImages_ToWork() {
            if (D.SpriteMap == null || D.SpriteMap.Keys.Count < 300) {
                AppEngineHelper.UpdateSpriteMap();
                await Task.Delay(3000);
            }
            AddressableImage[] addressableImages = Resources.FindObjectsOfTypeAll<AddressableImage>();
            foreach (AddressableImage ai in addressableImages) {
                ai.UpdateUI();
            }
        }

        [MenuItem("ExMenu/Use Delay")]
        static void UseDelayAsync() {
            DelayUseAsync();
        }

        async static void DelayUseAsync() {
            Debug.Log("Waiting 1 second...");
            await Task.Delay(1000);
            Debug.Log("After 1 second...");
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