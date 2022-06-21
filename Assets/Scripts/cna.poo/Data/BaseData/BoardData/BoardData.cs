﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class BoardData : BaseData {
        public BoardData() { }

        [SerializeField] public int mapDeckIndex = 0;
        [SerializeField] private List<MapHexId_Enum> currentMap;
        [SerializeField] private CNAMap<V2IntVO, WrapList<int>> monsterData = new CNAMap<V2IntVO, WrapList<int>>();
        [SerializeField] private int greenIndex = 0;
        [SerializeField] private int greyIndex = 0;
        [SerializeField] private int brownIndex = 0;
        [SerializeField] private int violetIndex = 0;
        [SerializeField] private int whiteIndex = 0;
        [SerializeField] private int redIndex = 0;
        [SerializeField] private int ruinIndex = 0;

        public int MapDeckIndex { get => mapDeckIndex; set => mapDeckIndex = value; }
        public List<MapHexId_Enum> CurrentMap { get => currentMap; set => currentMap = value; }
        public CNAMap<V2IntVO, WrapList<int>> MonsterData { get => monsterData; set => monsterData = value; }
        public int GreenIndex { get => greenIndex; set => greenIndex = value; }
        public int GreyIndex { get => greyIndex; set => greyIndex = value; }
        public int BrownIndex { get => brownIndex; set => brownIndex = value; }
        public int VioletIndex { get => violetIndex; set => violetIndex = value; }
        public int WhiteIndex { get => whiteIndex; set => whiteIndex = value; }
        public int RedIndex { get => redIndex; set => redIndex = value; }
        public int RuinIndex { get => ruinIndex; set => ruinIndex = value; }

        public void UpdateData(BoardData b) {
            mapDeckIndex = b.mapDeckIndex;
            currentMap = b.currentMap;
            monsterData.Clear();
            monsterData.Keys.ForEach(k => {
                V2IntVO key = new V2IntVO(k.X, k.Y);
                WrapList<int> value = new WrapList<int>();
                b.monsterData[k].Values.ForEach(v => value.Add(v));
                monsterData.Add(key, value);
            });
            greenIndex = b.greenIndex;
            greyIndex = b.greyIndex;
            brownIndex = b.brownIndex;
            violetIndex = b.violetIndex;
            whiteIndex = b.whiteIndex;
            redIndex = b.redIndex;
            ruinIndex = b.ruinIndex;
        }
    }
}
