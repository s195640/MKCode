using System;
using System.Text;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class wsData : BaseData {
        public mType_Enum type;
        public byte[] byteMsg;
        public string textMsg_01;
        public string textMsg_02;
        public int intMsg;
        public int sender;
        [NonSerialized] private BaseData msg;

        //public I getData<I>() where I : Data {
        //    if (msg == null) {
        //        msg = (I)Activator.CreateInstance(typeof(I));
        //        msg.Deserialize(this);
        //    }
        //    return (I)msg;
        //}

        public I getData<I>() where I : BaseData {
            if (msg == null) {
                msg = (I)Activator.CreateInstance(typeof(I));
                msg.Deserialize(textMsg_01);
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
        public wsData(mType_Enum type, int intMsg, BaseData data, int sender) : this(type, sender) {
            this.intMsg = intMsg;
            this.msg = data;
            textMsg_01 = JsonUtility.ToJson(data);
        }

        public wsData(mType_Enum type, string msg, int sender) : this(type, sender) {
            textMsg_01 = msg;
        }

        public wsData(mType_Enum type, BaseData data, int sender) : this(type, sender) {
            this.msg = data;
            textMsg_01 = JsonUtility.ToJson(data);
        }
        public wsData(mType_Enum type, string msg, BaseData data, int sender) : this(type, sender) {
            this.msg = data;
            textMsg_01 = JsonUtility.ToJson(data);
            textMsg_02 = msg;
        }

        public wsData(byte[] msg) {
            JsonUtility.FromJsonOverwrite(Encoding.UTF8.GetString(msg), this);
        }
    }
}
