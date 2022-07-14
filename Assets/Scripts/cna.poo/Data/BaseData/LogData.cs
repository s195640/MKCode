using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {

    [Serializable]
    public class LogData : BaseData {
        [SerializeField] private int i;
        [SerializeField] private string m;
        [SerializeField] private long t;

        public int PlayerId { get => i; set => i = value; }
        public string Message { get => m; set => m = value; }
        public long Time { get => t; set => t = value; }

        public LogData() { }

        public LogData(int i, string m, long t) {
            this.i = i;
            this.m = m;
            this.t = t;
        }
        public override string Serialize() {
            string data = CNASerialize.Sz(i) + "%"
                + CNASerialize.Sz(m) + "%"
                + CNASerialize.Sz(t);
            return "[" + data + "]";
        }
        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out i);
            CNASerialize.Dz(d[1], out m);
            CNASerialize.Dz(d[2], out t);
        }
    }
}
