using System;
using cna.poo;

namespace cna.connector {
    public abstract class BaseConnector {
        public BaseConnector() { }
        private Action<wsData> onEvent;
        internal PlayerData player;
        private string pw;
        private string url;
        public Action<wsData> OnEvent { get => onEvent; set => onEvent = value; }
        public string Pw { get => pw; set => pw = value; }
        public string Url { get => url; set => url = value; }
        public PlayerData Player { get => player; set => player = value; }

        public virtual void sendMsg(string msg) { }
        protected virtual void sendMsg(byte[] msg) { }
        public virtual void ping() {
            sendMsg(new byte[] { });
        }
        public virtual void Close() {
            OnEvent = null;
            Pw = "";
            Url = "";
            Player.Name = "";
            Player.Key = -1;
        }
        public virtual void DispatchMessageQueue() { }
    }
}