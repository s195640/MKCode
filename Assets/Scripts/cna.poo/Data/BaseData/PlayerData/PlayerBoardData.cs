using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class PlayerBoardData : BaseData {
        public PlayerBoardData() { }

        [SerializeField] private List<int> unitOffering = new List<int>();
        [SerializeField] private List<int> spellOffering = new List<int>();
        [SerializeField] private List<int> advancedOffering = new List<int>();
        [SerializeField] private List<int> skillOffering = new List<int>();
        [SerializeField] private List<MapHexId_Enum> playerMap = new List<MapHexId_Enum>();
        [SerializeField] private CNAMap<V2IntVO, WrapList<int>> monsterData = new CNAMap<V2IntVO, WrapList<int>>();
        [SerializeField] private int monasteryCount = 0;
        [SerializeField] private int unitRegularIndex = 0;
        [SerializeField] private int unitEliteIndex = 0;
        [SerializeField] private int woundIndex = 0;
        [SerializeField] private int skillBlueIndex = 0;
        [SerializeField] private int skillGreenIndex = 0;
        [SerializeField] private int skillRedIndex = 0;
        [SerializeField] private int skillWhiteIndex = 0;
        [SerializeField] private int advancedIndex = 0;
        [SerializeField] private int spellIndex = 0;
        [SerializeField] private int artifactIndex = 0;


        public List<int> UnitOffering { get => unitOffering; set => unitOffering = value; }
        public List<int> SpellOffering { get => spellOffering; set => spellOffering = value; }
        public List<int> AdvancedOffering { get => advancedOffering; set => advancedOffering = value; }
        public List<int> SkillOffering { get => skillOffering; set => skillOffering = value; }
        public int MonasteryCount { get => monasteryCount; set => monasteryCount = value; }
        public int UnitRegularIndex { get => unitRegularIndex; set => unitRegularIndex = value; }
        public int UnitEliteIndex { get => unitEliteIndex; set => unitEliteIndex = value; }
        public int WoundIndex { get => woundIndex; set => woundIndex = value; }
        public int SkillBlueIndex { get => skillBlueIndex; set => skillBlueIndex = value; }
        public int SkillGreenIndex { get => skillGreenIndex; set => skillGreenIndex = value; }
        public int SkillRedIndex { get => skillRedIndex; set => skillRedIndex = value; }
        public int SkillWhiteIndex { get => skillWhiteIndex; set => skillWhiteIndex = value; }
        public int AdvancedIndex { get => advancedIndex; set => advancedIndex = value; }
        public int SpellIndex { get => spellIndex; set => spellIndex = value; }
        public int ArtifactIndex { get => artifactIndex; set => artifactIndex = value; }
        public List<MapHexId_Enum> PlayerMap { get => playerMap; set => playerMap = value; }
        public CNAMap<V2IntVO, WrapList<int>> MonsterData { get => monsterData; set => monsterData = value; }

        public void UpdateData(PlayerBoardData pbd) {

            monasteryCount = pbd.monasteryCount;
            unitRegularIndex = pbd.unitRegularIndex;
            unitEliteIndex = pbd.unitEliteIndex;
            woundIndex = pbd.woundIndex;
            skillBlueIndex = pbd.skillBlueIndex;
            skillGreenIndex = pbd.skillGreenIndex;
            skillRedIndex = pbd.skillRedIndex;
            skillWhiteIndex = pbd.skillWhiteIndex;
            advancedIndex = pbd.advancedIndex;
            spellIndex = pbd.spellIndex;
            artifactIndex = pbd.artifactIndex;

            unitOffering.Clear();
            unitOffering.AddRange(pbd.unitOffering);
            spellOffering.Clear();
            spellOffering.AddRange(pbd.spellOffering);
            advancedOffering.Clear();
            advancedOffering.AddRange(pbd.advancedOffering);
            skillOffering.Clear();
            skillOffering.AddRange(pbd.skillOffering);
            playerMap.Clear();
            playerMap.AddRange(pbd.playerMap);


            monsterData.Clear();
            pbd.monsterData.Keys.ForEach(k => {
                V2IntVO key = new V2IntVO(k.X, k.Y);
                WrapList<int> value = new WrapList<int>();
                pbd.monsterData[k].Values.ForEach(v => value.Add(v));
                monsterData.Add(key, value);
            });
        }

        public void Clear() {
            unitOffering.Clear();
            spellOffering.Clear();
            advancedOffering.Clear();
            skillOffering.Clear();
            monasteryCount = 0;
            unitRegularIndex = 0;
            unitEliteIndex = 0;
            woundIndex = 0;
            skillBlueIndex = 0;
            skillGreenIndex = 0;
            skillRedIndex = 0;
            skillWhiteIndex = 0;
            advancedIndex = 0;
            spellIndex = 0;
            artifactIndex = 0;
            playerMap.Clear();
            monsterData.Clear();
        }
    }
}
