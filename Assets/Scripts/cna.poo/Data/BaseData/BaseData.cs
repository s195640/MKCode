using System;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public abstract class BaseData {
        public BaseData() { }
        public override string ToString() {
            return JsonUtility.ToJson(this, true);
        }
        public virtual string ToDataStr() {
            return CNASerialize.Zip(CNASerialize.Sz(Serialize()));
        }

        public abstract string Serialize();

        public abstract void Deserialize(string data);
    }
}
