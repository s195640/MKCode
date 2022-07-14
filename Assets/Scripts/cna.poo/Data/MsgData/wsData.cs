using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class wsData : BaseData {
        public mType_Enum type;
        public byte[] byteMsg;
        public string textMsg_01;
        //public string textMsg_02;
        public int gameHostKey;
        public int intMsg;
        public int sender;
        [NonSerialized] private BaseData msg;
        public wsData() { }

        public I getData<I>() where I : BaseData {
            if (msg == null) {
                msg = (I)Activator.CreateInstance(typeof(I));
                string unzip = CNASerialize.Unzip(textMsg_01);
                msg.Deserialize(unzip);
            }
            return (I)msg;
        }

        public wsData(mType_Enum type, int sender) {
            this.type = type;
            this.sender = sender;
        }

        public wsData(mType_Enum type, int msg, int sender) : this(type, sender) {
            intMsg = msg;
        }

        public wsData(mType_Enum type, string msg, int sender) : this(type, sender) {
            textMsg_01 = msg;
        }

        public wsData(mType_Enum type, BaseData data, int sender) : this(type, sender) {
            this.msg = data;
            textMsg_01 = data.ToDataStr();
        }
        //public wsData(mType_Enum type, string msg, BaseData data, int sender) : this(type, sender) {
        //    this.msg = data;
        //    textMsg_01 = data.ToDataStr();
        //    //textMsg_02 = msg;
        //}
        public wsData(mType_Enum type, int gameHostKey, BaseData data, int sender) : this(type, sender) {
            this.msg = data;
            textMsg_01 = data.ToDataStr();
            this.gameHostKey = gameHostKey;
        }

        public wsData(byte[] msg) {
            JsonUtility.FromJsonOverwrite(Encoding.UTF8.GetString(msg), this);
        }

        public override string Serialize() {
            string data = CNASerialize.Sz(type) + "%"
                + CNASerialize.Sz(byteMsg) + "%"
                + CNASerialize.Sz(textMsg_01) + "%"
                //+ CNASerialize.Sz(textMsg_02) + "%"
                + CNASerialize.Sz(gameHostKey) + "%"
                + CNASerialize.Sz(intMsg) + "%"
                + CNASerialize.Sz(sender) + "%";
            return "[" + data + "]";
        }

        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out type);
            CNASerialize.Dz(d[1], out byteMsg);
            CNASerialize.Dz(d[2], out textMsg_01);
            //CNASerialize.Dz(d[3], out textMsg_02);
            CNASerialize.Dz(d[2], out gameHostKey);
            CNASerialize.Dz(d[4], out intMsg);
            CNASerialize.Dz(d[5], out sender);
        }
    }
}
