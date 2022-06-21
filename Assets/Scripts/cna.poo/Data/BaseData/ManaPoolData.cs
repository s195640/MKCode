using System;
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
    }
}
