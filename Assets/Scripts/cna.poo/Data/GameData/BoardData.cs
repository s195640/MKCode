using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class BoardData : Data {
        public BoardData() { }

        public BoardData(GameMapLayout_Enum gameMapLayout, int basic, int core, int city, bool easyStart, int rounds, bool dummyPlayer) {
            DateTime dt = DateTime.Now;
            seed = dt.Second * 1000 + dt.Millisecond;
            this.gameMapLayout = gameMapLayout;
            this.basic = basic;
            this.core = core;
            this.city = city;
            this.easyStart = easyStart;
            this.rounds = rounds;
            this.dummyPlayer = dummyPlayer;
            mapDeckIndex = 0;
            unitRegularIndex = 0;
            unitOffering = new List<int>();
            manaPool = new List<Crystal_Enum>();
            spellOffering = new List<int>();
            advancedOffering = new List<int>();
            skillOffering = new List<int>();
        }

        [SerializeField] private int seed;
        [SerializeField] GameMapLayout_Enum gameMapLayout = GameMapLayout_Enum.Wedge;
        [SerializeField] [Range(0, 11)] private int basic = 5;
        [SerializeField] [Range(0, 4)] private int core = 3;
        [SerializeField] [Range(0, 4)] private int city = 2;
        [SerializeField] [Range(1, 11)] private int level = 5;
        [SerializeField] private bool easyStart = false;
        [SerializeField] public int mapDeckIndex = 0;
        [SerializeField] public int unitRegularIndex = 0;
        [SerializeField] public int unitEliteIndex = 0;
        [SerializeField] public int woundIndex = 0;
        [SerializeField] public int skillBlueIndex = 0;
        [SerializeField] public int skillGreenIndex = 0;
        [SerializeField] public int skillRedIndex = 0;
        [SerializeField] public int skillWhiteIndex = 0;
        [SerializeField] public int advancedIndex = 0;
        [SerializeField] public int spellIndex = 0;
        [SerializeField] public int artifactIndex = 0;
        [SerializeField] private List<MapHexId_Enum> currentMap;
        [SerializeField] private List<int> unitOffering;
        [SerializeField] private List<Crystal_Enum> manaPool;
        [SerializeField] private List<int> spellOffering;
        [SerializeField] private List<int> advancedOffering;
        [SerializeField] private List<int> skillOffering;
        [SerializeField] private int monasteryCount = 0;
        [SerializeField] private int rounds = 0;
        [SerializeField] private bool dummyPlayer;



        public int Seed { get => seed; set => seed = value; }
        public GameMapLayout_Enum GameMapLayout { get => gameMapLayout; set => gameMapLayout = value; }
        public int Basic { get => basic; set => basic = value; }
        public int Core { get => core; set => core = value; }
        public int City { get => city; set => city = value; }
        public int MapDeckIndex { get => mapDeckIndex; set => mapDeckIndex = value; }
        public List<MapHexId_Enum> CurrentMap { get => currentMap; set => currentMap = value; }
        public bool EasyStart { get => easyStart; set => easyStart = value; }
        public int Level { get => level; set => level = value; }
        public int UnitRegularIndex { get => unitRegularIndex; set => unitRegularIndex = value; }
        public List<int> UnitOffering { get => unitOffering; set => unitOffering = value; }
        public int UnitEliteIndex { get => unitEliteIndex; set => unitEliteIndex = value; }
        public int WoundIndex { get => woundIndex; set => woundIndex = value; }
        public List<Crystal_Enum> ManaPool { get => manaPool; set => manaPool = value; }
        public int SkillBlueIndex { get => skillBlueIndex; set => skillBlueIndex = value; }
        public int SkillGreenIndex { get => skillGreenIndex; set => skillGreenIndex = value; }
        public int SkillRedIndex { get => skillRedIndex; set => skillRedIndex = value; }
        public int SkillWhiteIndex { get => skillWhiteIndex; set => skillWhiteIndex = value; }
        public int AdvancedIndex { get => advancedIndex; set => advancedIndex = value; }
        public int SpellIndex { get => spellIndex; set => spellIndex = value; }
        public int ArtifactIndex { get => artifactIndex; set => artifactIndex = value; }
        public List<int> SpellOffering { get => spellOffering; set => spellOffering = value; }
        public List<int> AdvancedOffering { get => advancedOffering; set => advancedOffering = value; }
        public List<int> SkillOffering { get => skillOffering; set => skillOffering = value; }
        public int MonasteryCount { get => monasteryCount; set => monasteryCount = value; }
        public int Rounds { get => rounds; set => rounds = value; }
        public bool DummyPlayer { get => dummyPlayer; set => dummyPlayer = value; }
    }
}
