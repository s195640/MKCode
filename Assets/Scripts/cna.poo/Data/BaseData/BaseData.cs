using System;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public abstract class BaseData {
        public BaseData() { }
        public override string ToString() {
            return JsonUtility.ToJson(this, true);
        }
        public string ToJson() {
            return JsonUtility.ToJson(this);
        }

        public void Deserialize(string text) {
            JsonUtility.FromJsonOverwrite(text, this);
        }
    }
}
