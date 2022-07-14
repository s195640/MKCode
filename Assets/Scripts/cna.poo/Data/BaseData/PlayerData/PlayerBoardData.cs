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
        [SerializeField] private CNAMap<V2IntVO, CNAList<int>> monsterData = new CNAMap<V2IntVO, CNAList<int>>();
        [SerializeField] private int unitRegularIndex = 0;
        [SerializeField] private int unitEliteIndex = 0;
        [SerializeField] private int woundIndex = 0;
        [SerializeField] private int skillBlueIndex = 0;
        [SerializeField] private int skillGreenIndex = 0;
        [SerializeField] private int skillRedIndex = 0;
        [SerializeField] private int skillWhiteIndex = 0;
        [SerializeField] private int advancedIndex = 0;
        [SerializeField] private int advancedUnitIndex = 0;
        [SerializeField] private int spellIndex = 0;
        [SerializeField] private int artifactIndex = 0;


        public List<int> UnitOffering { get => unitOffering; set => unitOffering = value; }
        public List<int> SpellOffering { get => spellOffering; set => spellOffering = value; }
        public List<int> AdvancedOffering { get => advancedOffering; set => advancedOffering = value; }
        public List<int> SkillOffering { get => skillOffering; set => skillOffering = value; }
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
        public CNAMap<V2IntVO, CNAList<int>> MonsterData { get => monsterData; set => monsterData = value; }
        public int AdvancedUnitIndex { get => advancedUnitIndex; set => advancedUnitIndex = value; }
        public int AdvancedIndexTotal { get => advancedIndex + advancedUnitIndex; }


        public void UpdateData(PlayerBoardData pbd) {

            unitRegularIndex = pbd.unitRegularIndex;
            unitEliteIndex = pbd.unitEliteIndex;
            woundIndex = pbd.woundIndex;
            skillBlueIndex = pbd.skillBlueIndex;
            skillGreenIndex = pbd.skillGreenIndex;
            skillRedIndex = pbd.skillRedIndex;
            skillWhiteIndex = pbd.skillWhiteIndex;
            advancedIndex = pbd.advancedIndex;
            advancedUnitIndex = pbd.advancedUnitIndex;
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
                CNAList<int> value = new CNAList<int>();
                pbd.monsterData[k].Values.ForEach(v => value.Add(v));
                monsterData.Add(key, value);
            });
        }

        public void Clear() {
            unitOffering.Clear();
            spellOffering.Clear();
            advancedOffering.Clear();
            skillOffering.Clear();
            unitRegularIndex = 0;
            unitEliteIndex = 0;
            woundIndex = 0;
            skillBlueIndex = 0;
            skillGreenIndex = 0;
            skillRedIndex = 0;
            skillWhiteIndex = 0;
            advancedIndex = 0;
            advancedUnitIndex = 0;
            spellIndex = 0;
            artifactIndex = 0;
            playerMap.Clear();
            monsterData.Clear();
        }

        public override string Serialize() {
            string data = CNASerialize.Sz(unitOffering) + "%"
                + CNASerialize.Sz(spellOffering) + "%"
                + CNASerialize.Sz(advancedOffering) + "%"
                + CNASerialize.Sz(skillOffering) + "%"
                + CNASerialize.Sz(playerMap) + "%"
                + CNASerialize.Sz(monsterData) + "%"
                + CNASerialize.Sz(unitRegularIndex) + "%"
                + CNASerialize.Sz(unitEliteIndex) + "%"
                + CNASerialize.Sz(woundIndex) + "%"
                + CNASerialize.Sz(skillBlueIndex) + "%"
                + CNASerialize.Sz(skillGreenIndex) + "%"
                + CNASerialize.Sz(skillRedIndex) + "%"
                + CNASerialize.Sz(skillWhiteIndex) + "%"
                + CNASerialize.Sz(advancedIndex) + "%"
                + CNASerialize.Sz(advancedUnitIndex) + "%"
                + CNASerialize.Sz(spellIndex) + "%"
                + CNASerialize.Sz(artifactIndex);
            return "[" + data + "]";
        }

        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out unitOffering);
            CNASerialize.Dz(d[1], out spellOffering);
            CNASerialize.Dz(d[2], out advancedOffering);
            CNASerialize.Dz(d[3], out skillOffering);
            CNASerialize.Dz(d[4], out playerMap);
            CNASerialize.Dz(d[5], out monsterData);
            CNASerialize.Dz(d[6], out unitRegularIndex);
            CNASerialize.Dz(d[7], out unitEliteIndex);
            CNASerialize.Dz(d[8], out woundIndex);
            CNASerialize.Dz(d[9], out skillBlueIndex);
            CNASerialize.Dz(d[10], out skillGreenIndex);
            CNASerialize.Dz(d[11], out skillRedIndex);
            CNASerialize.Dz(d[12], out skillWhiteIndex);
            CNASerialize.Dz(d[13], out advancedIndex);
            CNASerialize.Dz(d[14], out advancedUnitIndex);
            CNASerialize.Dz(d[15], out spellIndex);
            CNASerialize.Dz(d[16], out artifactIndex);
        }
    }
}
