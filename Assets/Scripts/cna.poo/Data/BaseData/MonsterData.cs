using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class MonsterData : BaseData {
        public MonsterData() { }

        [SerializeField] private CNAMap<V2IntVO, CNAList<int>> map = new CNAMap<V2IntVO, CNAList<int>>();
        [SerializeField] private int greenIndex = 0;
        [SerializeField] private int greyIndex = 0;
        [SerializeField] private int brownIndex = 0;
        [SerializeField] private int violetIndex = 0;
        [SerializeField] private int whiteIndex = 0;
        [SerializeField] private int redIndex = 0;
        [SerializeField] private int ruinIndex = 0;

        public int GreenIndex { get => greenIndex; set => greenIndex = value; }
        public int GreyIndex { get => greyIndex; set => greyIndex = value; }
        public int BrownIndex { get => brownIndex; set => brownIndex = value; }
        public int VioletIndex { get => violetIndex; set => violetIndex = value; }
        public int WhiteIndex { get => whiteIndex; set => whiteIndex = value; }
        public int RedIndex { get => redIndex; set => redIndex = value; }
        public CNAMap<V2IntVO, CNAList<int>> Map { get => map; }
        public int RuinIndex { get => ruinIndex; set => ruinIndex = value; }

        public void Clear() {
            GreenIndex = 0;
            GreyIndex = 0;
            BrownIndex = 0;
            VioletIndex = 0;
            WhiteIndex = 0;
            RedIndex = 0;
            ruinIndex = 0;
            Map.Clear();
        }

        public void UpdateData(MonsterData m) {
            map.Clear();
            m.map.Keys.ForEach(k => {
                V2IntVO key = new V2IntVO(k.X, k.Y);
                CNAList<int> value = new CNAList<int>();
                m.map[k].Values.ForEach(v => value.Add(v));
                map.Add(key, value);
            });
            greenIndex = m.greenIndex;
            greyIndex = m.greyIndex;
            brownIndex = m.brownIndex;
            violetIndex = m.violetIndex;
            whiteIndex = m.whiteIndex;
            redIndex = m.redIndex;
            ruinIndex = m.ruinIndex;
        }

        public override string Serialize() {
            string data = CNASerialize.Sz(greenIndex) + "%"
                + CNASerialize.Sz(greyIndex) + "%"
                + CNASerialize.Sz(brownIndex) + "%"
                + CNASerialize.Sz(violetIndex) + "%"
                + CNASerialize.Sz(whiteIndex) + "%"
                + CNASerialize.Sz(redIndex) + "%"
                + CNASerialize.Sz(ruinIndex);
            return "[" + data + "]";
        }
        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out greenIndex);
            CNASerialize.Dz(d[1], out greyIndex);
            CNASerialize.Dz(d[2], out brownIndex);
            CNASerialize.Dz(d[3], out violetIndex);
            CNASerialize.Dz(d[4], out whiteIndex);
            CNASerialize.Dz(d[5], out redIndex);
            CNASerialize.Dz(d[6], out ruinIndex);
        }
    }
}
