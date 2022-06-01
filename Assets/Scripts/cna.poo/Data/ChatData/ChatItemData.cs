using System;
using UnityEngine;

namespace cna.poo {

    [Serializable]
    public class ChatItemData : Data {
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
    }
}
