using System;
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
    }
}
