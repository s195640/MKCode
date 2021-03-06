using System;
using UnityEngine;
using cna.poo;
using UnityEngine.Networking;
using System.Collections;
using System.Threading.Tasks;

namespace cna.connector {
    public class MultiConnector : BaseConnector {
        private WebSocket ws;
        private bool waitingForReconnect = false;

        private void OnOpen() {
            if (waitingForReconnect) {
                waitingForReconnect = false;
                OnReconnect();
            }
        }
        private void OnReconnect() {
            OnEvent(new wsData(mType_Enum.OnReconnect, 0));
        }
        private void OnMessage(byte[] msg) {
            OnEvent(new wsData(msg));
        }
        private void OnError(string e) {
            Debug.Log("OnError");
            Debug.Log(e);
        }
        private void OnClose(WebSocketCloseCode code) {
            OnEvent(new wsData(mType_Enum.OnServerDisconnect, ((int)code), 0));
        }

        public MultiConnector(string un, string pw, string url, Action<wsData> onEvent) {
            waitingForReconnect = false;
            OnEvent = onEvent;
            player = new PlayerData(un, -1);
            Pw = pw;
            Url = url.Replace("\u200B", "").Trim();
            Connect();
            //PingThenConnect();
        }


        public async void Reconnect() {
            waitingForReconnect = true;
            ws = WebSocketFactory.CreateInstance(Url + "?u=" + player.Name + "&p=" + Pw);
            ws.OnOpen += OnOpen;
            ws.OnMessage += OnMessage;
            ws.OnError += OnError;
            ws.OnClose += OnClose;
            await ws.Connect();
        }

        public async void Connect() {
            ws = WebSocketFactory.CreateInstance(Url + "?u=" + player.Name + "&p=" + Pw);
            ws.OnOpen += OnOpen;
            ws.OnMessage += OnMessage;
            ws.OnError += OnError;
            ws.OnClose += OnClose;
            await ws.Connect();
        }


#if !UNITY_WEBGL || UNITY_EDITOR
        override public void DispatchMessageQueue() {
            if (ws != null) {
                ws.DispatchMessageQueue();
            }
        }
#endif

        override protected void sendMsg(byte[] msg) {
            ws.Send(msg);
        }

        override public void sendMsg(string msg) {
            ws.SendText(msg);
        }

        public async override void Close() {
            base.Close();
            ws.OnOpen -= OnOpen;
            ws.OnMessage -= OnMessage;
            ws.OnError -= OnError;
            ws.OnClose -= OnClose;
            await ws.Close();
        }

        //public async void PingThenConnect() {
        //    string url = Url.Replace("ws", "http");
        //    Debug.Log("ping :: " + url);
        //    UnityWebRequest www = new UnityWebRequest(url);
        //    www.SetRequestHeader("ngrok-skip-browser-warning", "true");
        //    www.downloadHandler = new DownloadHandlerBuffer();
        //    await www.SendWebRequest();
        //    string ping = www.downloadHandler.text.Trim();
        //    Debug.Log(ping);
        //    if (ping.Equals("PING")) {
        //        Connect();
        //    } else {
        //        OnError("Failed to ping server!");
        //        OnClose(WebSocketCloseCode.ServerError);
        //    }
        //}
    }
}
