using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {

    [Serializable]
    public class ChatItemData : BaseData {
        [SerializeField] private string n;
        [SerializeField] private string m;
        [SerializeField] private long t;

        public string UserName { get => n; set => n = value; }
        public string Message { get => m; set => m = value; }
        public long Time { get => t; set => t = value; }

        public ChatItemData() { }

        public ChatItemData(string n, string m, long t) {
            this.n = n;
            this.m = m;
            this.t = t;
        }

        public override string Serialize() {
            string data = CNASerialize.Sz(n) + "%"
                + CNASerialize.Sz(m) + "%"
                + CNASerialize.Sz(t);
            return "[" + data + "]";
        }
        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out n);
            CNASerialize.Dz(d[1], out m);
            CNASerialize.Dz(d[2], out t);
        }
    }
}
