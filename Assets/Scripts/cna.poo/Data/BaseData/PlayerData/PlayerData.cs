using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class PlayerData : BaseData {
        [SerializeField] protected string playerName;
        [SerializeField] protected int playerKey;
        [SerializeField] private Image_Enum avatar = Image_Enum.A_MEEPLE_RANDOM;
        [SerializeField] private bool playerReady = false;
        [SerializeField] private TurnPhase_Enum playerTurnPhase;
        [SerializeField] private PlayerDeckData playerDeckData = new PlayerDeckData();
        [SerializeField] private int playerMovementCount;
        [SerializeField] private int playerInfluenceCount;
        [SerializeField] private List<V2IntVO> gridLocationHistory = new List<V2IntVO>();
        [SerializeField] private List<int> visableMonsters = new List<int>();
        [SerializeField] private BattleData battle = new BattleData();
        [SerializeField] private V2IntVO fame = V2IntVO.zero;
        [SerializeField] private int repLevel;
        [SerializeField] private bool actionTaken;
        [SerializeField] private CrystalData crystal = new CrystalData();
        [SerializeField] private CrystalData mana = new CrystalData();
        [SerializeField] private int armor;
        [SerializeField] private int healpoints;
        [SerializeField] private int manaPoolAvailable;
        [SerializeField] private bool dummyPlayer;
        [SerializeField] private CNAMap<GameEffect_Enum, WrapList<int>> gameEffects = new CNAMap<GameEffect_Enum, WrapList<int>>();
        [SerializeField] private List<V2IntVO> shields = new List<V2IntVO>();
        [SerializeField] private List<ManaPoolData> manaPool = new List<ManaPoolData>();
        [SerializeField] private PlayerBoardData board = new PlayerBoardData();
        [SerializeField] private bool waitOnServer = false;

        public string Name { get => playerName; set => playerName = value; }
        public int Key { get => playerKey; set => playerKey = value; }
        public Image_Enum Avatar { get => avatar; set => avatar = value; }
        public bool PlayerReady { get => playerReady; set => playerReady = value; }
        public TurnPhase_Enum PlayerTurnPhase { get => playerTurnPhase; set => playerTurnPhase = value; }
        public PlayerDeckData Deck { get => playerDeckData; set => playerDeckData = value; }
        public int Movement { get => playerMovementCount; set => playerMovementCount = value; }
        public int Influence { get => playerInfluenceCount; set => playerInfluenceCount = value; }
        public List<V2IntVO> GridLocationHistory { get => gridLocationHistory; set => gridLocationHistory = value; }
        public V2IntVO CurrentGridLoc { get { return gridLocationHistory[0]; } }
        public List<int> VisableMonsters { get => visableMonsters; set => visableMonsters = value; }
        public BattleData Battle { get => battle; set => battle = value; }
        public int TotalFame { get => fame.X + fame.Y; }
        public V2IntVO Fame { get => fame; set => fame = value; }
        public int RepLevel { get => repLevel; set { repLevel = value > 7 ? 7 : value < -7 ? -7 : value; } }
        public bool ActionTaken { get => actionTaken; set => actionTaken = value; }
        public CrystalData Crystal { get => crystal; set => crystal = value; }
        public CrystalData Mana { get => mana; set => mana = value; }
        public int Armor { get => armor; set => armor = value; }
        public int Healpoints { get => healpoints; set => healpoints = value; }
        public int ManaPoolAvailable { get => manaPoolAvailable; set => manaPoolAvailable = value; }
        public CNAMap<GameEffect_Enum, WrapList<int>> GameEffects { get => gameEffects; set => gameEffects = value; }
        public bool DummyPlayer { get => dummyPlayer; set => dummyPlayer = value; }
        public List<V2IntVO> Shields { get => shields; set => shields = value; }
        public List<ManaPoolData> ManaPoolFull { get => manaPool; set => manaPool = value; }
        public List<ManaPoolData> ManaPool { get => manaPool.FindAll(mp => mp.Status != ManaPool_Enum.NA && mp.Status != ManaPool_Enum.Used && mp.Status != ManaPool_Enum.ManaSteal); }
        public PlayerBoardData Board { get => board; set => board = value; }
        public bool WaitOnServer { get => waitOnServer; set => waitOnServer = value; }

        public void AddGameEffect(GameEffect_Enum ge, params int[] cards) {
            if (cards.Length == 0) cards = new int[] { 0 };
            if (!gameEffects.ContainsKey(ge)) {
                gameEffects.Add(ge, new WrapList<int>());
            }
            gameEffects[ge].AddRange(cards);
        }
        public void RemoveGameEffect(GameEffect_Enum ge, int card) {
            if (gameEffects.ContainsKey(ge)) {
                gameEffects[ge].Remove(card);
                if (gameEffects[ge].Count == 0) {
                    gameEffects.Remove(ge);
                }
            }
        }
        public void RemoveGameEffect(GameEffect_Enum ge) {
            gameEffects.Remove(ge);
        }


        public PlayerData() { }
        public PlayerData(string name, int key) {
            Name = name;
            Key = key;
            Clear();
            //Deck = new PlayerDeckData();
            //Battle = new BattleData();
            //Movement = 0;
            //Influence = 0;
            //fame = V2IntVO.zero;
            //repLevel = 0;
            //crystal = new CrystalData(); ;
            //mana = new CrystalData();
            //armor = 2;
            //healpoints = 0;
            //dummyPlayer = false;
            //shields = new List<V2IntVO>();
            //manaPool = new List<ManaPoolData>();
            //board = new PlayerBoardData();
        }

        public override bool Equals(object obj) {
            return obj is PlayerData @base &&
                   playerKey == @base.playerKey;
        }
        public override int GetHashCode() {
            return HashCode.Combine(playerKey);
        }

        public void Clear() {
            Deck.Clear();
            Battle.Clear();
            Crystal.Clear();
            Mana.Clear();
            PlayerTurnPhase = TurnPhase_Enum.NA;
            Movement = 0;
            Influence = 0;
            GridLocationHistory.Clear();
            GridLocationHistory.Add(V2IntVO.zero);
            Fame = V2IntVO.zero;
            RepLevel = 0;
            ActionTaken = false;
            Armor = 2;
            Healpoints = 0;
            ManaPoolAvailable = 0;
            GameEffects.Clear();
            shields.Clear();
            manaPool.Clear();
            board.Clear();
            WaitOnServer = false;
        }

        public void ClearEndTurn() {
            ManaPoolAvailable = 1;
            Movement = 0;
            Influence = 0;
            ActionTaken = false;
            Healpoints = 0;
            Fame.X += Fame.Y;
            Fame.Y = 0;
            Battle.Clear();
            Crystal.ClearSpent();
            Crystal.RemoveExtraCrystals(3);
            Mana.ClearSpent();
            Mana.Clear();
        }

        public void UpdateData(PlayerData p) {
            playerName = p.playerName;
            playerKey = p.playerKey;
            avatar = p.avatar;
            playerReady = p.playerReady;
            playerTurnPhase = p.playerTurnPhase;
            playerDeckData = p.playerDeckData;
            playerMovementCount = p.playerMovementCount;
            playerInfluenceCount = p.playerInfluenceCount;
            gridLocationHistory = p.gridLocationHistory;
            visableMonsters = p.visableMonsters;
            if (battle == null) {
                battle = p.battle;
            } else {
                battle.UpdateData(p.battle);
            }
            fame = p.fame;
            repLevel = p.repLevel;
            actionTaken = p.actionTaken;
            crystal = p.crystal;
            mana = p.mana;
            armor = p.armor;
            healpoints = p.healpoints;
            manaPoolAvailable = p.manaPoolAvailable;
            dummyPlayer = p.dummyPlayer;
            gameEffects = p.gameEffects;
            shields = p.shields;
            manaPool = p.manaPool;
            board.UpdateData(p.board);
            waitOnServer = p.waitOnServer;
        }

        public PlayerData Clone() {
            PlayerData pd = JsonUtility.FromJson<PlayerData>(JsonUtility.ToJson(this));
            return pd;
        }
    }
}
