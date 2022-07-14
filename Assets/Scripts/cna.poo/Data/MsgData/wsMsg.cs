using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class wsMsg : BaseData {
        public List<int> u = new List<int>();
        public wsData d;

        public override string ToDataStr() {
            return JsonUtility.ToJson(this);
        }

        public override string Serialize() {
            throw new NotImplementedException();
            //string data = CNASerialize.Sz(u) + "%"
            //    + CNASerialize.Sz(d) + "%";
            //return "[" + data + "]";
        }

        public override void Deserialize(string data) {
            throw new NotImplementedException();
            //    List<string> t = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            //    CNASerialize.Dz(t[0], out u);
            //    CNASerialize.Dz(t[1], out d);
        }
    }
}
