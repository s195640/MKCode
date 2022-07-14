using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class ManaPoolData : BaseData {

        public ManaPoolData() { }
        public ManaPoolData(Crystal_Enum manaColor, ManaPool_Enum status = ManaPool_Enum.None) {
            this.manaColor = manaColor;
            this.status = status;
        }

        [SerializeField] private Crystal_Enum manaColor;
        [SerializeField] private ManaPool_Enum status;

        public Crystal_Enum ManaColor { get => manaColor; set => manaColor = value; }
        public ManaPool_Enum Status { get => status; set => status = value; }

        public ManaPoolData Clone() {
            return new ManaPoolData(manaColor, status);
        }

        public override string Serialize() {
            string data = CNASerialize.Sz(manaColor) + "%"
                + CNASerialize.Sz(status) + "%";
            return "[" + data + "]";
        }

        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out manaColor);
            CNASerialize.Dz(d[1], out status);
        }
    }
}
