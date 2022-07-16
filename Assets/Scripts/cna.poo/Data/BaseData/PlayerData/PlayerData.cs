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
        [SerializeField] private CNAMap<GameEffect_Enum, CNAList<int>> gameEffects = new CNAMap<GameEffect_Enum, CNAList<int>>();
        [SerializeField] private List<V2IntVO> shields = new List<V2IntVO>();
        [SerializeField] private List<ManaPoolData> manaPool = new List<ManaPoolData>();
        [SerializeField] private PlayerBoardData board = new PlayerBoardData();
        [SerializeField] private bool waitOnServer = false;
        [SerializeField] private bool undoLock = false;
        [SerializeField] private long time01 = 0;
        [SerializeField] private long time02 = 0;

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
        //public int TotalFame {
        //    get {
        //        return fame.X + fame.Y;
        //    }
        //}
        public V2IntVO Fame { get => fame; set => fame = value; }
        public int RepLevel { get => repLevel; set { repLevel = value > 7 ? 7 : value < -7 ? -7 : value; } }
        public bool ActionTaken { get => actionTaken; set => actionTaken = value; }
        public CrystalData Crystal { get => crystal; set => crystal = value; }
        public CrystalData Mana { get => mana; set => mana = value; }
        public int Armor { get => armor; set => armor = value; }
        public int Healpoints { get => healpoints; set => healpoints = value; }
        public int ManaPoolAvailable { get => manaPoolAvailable; set => manaPoolAvailable = value; }
        public CNAMap<GameEffect_Enum, CNAList<int>> GameEffects { get => gameEffects; set => gameEffects = value; }
        public bool DummyPlayer { get => dummyPlayer; set => dummyPlayer = value; }
        public List<V2IntVO> Shields { get => shields; set => shields = value; }
        public List<ManaPoolData> ManaPoolFull { get => manaPool; set => manaPool = value; }
        public List<ManaPoolData> ManaPool { get => manaPool.FindAll(mp => mp.Status != ManaPool_Enum.NA && mp.Status != ManaPool_Enum.Used && mp.Status != ManaPool_Enum.ManaSteal); }
        public PlayerBoardData Board { get => board; set => board = value; }
        public bool WaitOnServer { get => waitOnServer; set => waitOnServer = value; }
        public bool UndoLock { get => undoLock; set => undoLock = value; }
        public long Time01 { get => time01; set => time01 = value; }
        public long Time02 { get => time02; set => time02 = value; }

        public void AddGameEffect(GameEffect_Enum ge, params int[] cards) {
            if (cards.Length == 0) cards = new int[] { 0 };
            if (!gameEffects.ContainsKey(ge)) {
                gameEffects.Add(ge, new CNAList<int>());
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
            UndoLock = false;
            time01 = 0;
            time02 = 0;
        }

        public void ClearEndTurn() {
            ManaPoolAvailable = 1;
            Movement = 0;
            Influence = 0;
            ActionTaken = false;
            Healpoints = 0;
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
            playerDeckData.UpdateData(p.playerDeckData);
            playerMovementCount = p.playerMovementCount;
            playerInfluenceCount = p.playerInfluenceCount;
            gridLocationHistory.Clear();
            p.gridLocationHistory.ForEach(pos => gridLocationHistory.Add(pos.Clone()));
            visableMonsters.Clear();
            visableMonsters.AddRange(p.visableMonsters);
            if (battle == null) {
                battle = p.battle;
            } else {
                battle.UpdateData(p.battle);
            }
            fame = p.fame.Clone();
            repLevel = p.repLevel;
            actionTaken = p.actionTaken;
            crystal.UpdateData(p.crystal);
            mana.UpdateData(p.mana);
            armor = p.armor;
            healpoints = p.healpoints;
            manaPoolAvailable = p.manaPoolAvailable;
            dummyPlayer = p.dummyPlayer;
            gameEffects.Clear();
            p.gameEffects.Keys.ForEach(key => {
                CNAList<int> value = new CNAList<int>();
                p.gameEffects[key].Values.ForEach(v => value.Add(v));
                gameEffects.Add(key, value);
            });
            shields.Clear();
            p.shields.ForEach(pos => shields.Add(pos.Clone()));
            manaPool.Clear();
            p.manaPool.ForEach(m => manaPool.Add(m.Clone()));
            board.UpdateData(p.board);
            waitOnServer = p.waitOnServer;
            undoLock = p.undoLock;
            time01 = p.time01;
            time02 = p.time02;
        }

        public PlayerData Clone() {
            PlayerData pd = JsonUtility.FromJson<PlayerData>(JsonUtility.ToJson(this));
            return pd;
        }

        public void UpdateTime() {
            if (Time02 > 0) {
                Time01 += (long)(DateTime.Now - new DateTime(Time02)).TotalSeconds;
                Time02 = 0;
            }
        }
        public void SetTime() {
            Time02 = DateTime.Now.Ticks;
        }

        public string GetTime() {
            long totalSec = Time01;
            if (Time02 > 0) {
                DateTime playerTurnStart = new DateTime(Time02);
                DateTime currentTime = DateTime.Now;
                totalSec += (long)(currentTime - playerTurnStart).TotalSeconds;
            }
            long hour = totalSec / 3600;
            long min = (totalSec - (3600 * hour)) / 60;
            long sec = totalSec - (3600 * hour) - (60 * min);
            return string.Format("{0}:{1}:{2}", ("" + hour).PadLeft(1, '0'), ("" + min).PadLeft(2, '0'), ("" + sec).PadLeft(2, '0'));
        }

        public override string Serialize() {
            string data = CNASerialize.Sz(playerName) + "%"
                + CNASerialize.Sz(playerKey) + "%"
                + CNASerialize.Sz(avatar) + "%"
                + CNASerialize.Sz(playerReady) + "%"
                + CNASerialize.Sz(playerTurnPhase) + "%"
                + CNASerialize.Sz(playerDeckData) + "%"
                + CNASerialize.Sz(playerMovementCount) + "%"
                + CNASerialize.Sz(playerInfluenceCount) + "%"
                + CNASerialize.Sz(gridLocationHistory) + "%"
                + CNASerialize.Sz(visableMonsters) + "%"
                + CNASerialize.Sz(battle) + "%"
                + CNASerialize.Sz(fame) + "%"
                + CNASerialize.Sz(repLevel) + "%"
                + CNASerialize.Sz(actionTaken) + "%"
                + CNASerialize.Sz(crystal) + "%"
                + CNASerialize.Sz(mana) + "%"
                + CNASerialize.Sz(armor) + "%"
                + CNASerialize.Sz(healpoints) + "%"
                + CNASerialize.Sz(manaPoolAvailable) + "%"
                + CNASerialize.Sz(dummyPlayer) + "%"
                + CNASerialize.Sz(gameEffects) + "%"
                + CNASerialize.Sz(shields) + "%"
                + CNASerialize.Sz(manaPool) + "%"
                + CNASerialize.Sz(board) + "%"
                + CNASerialize.Sz(waitOnServer) + "%"
                + CNASerialize.Sz(undoLock) + "%"
                + CNASerialize.Sz(time01) + "%"
                + CNASerialize.Sz(time02);
            return "[" + data + "]";
        }

        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out playerName);
            CNASerialize.Dz(d[1], out playerKey);
            CNASerialize.Dz(d[2], out avatar);
            CNASerialize.Dz(d[3], out playerReady);
            CNASerialize.Dz(d[4], out playerTurnPhase);
            CNASerialize.Dz(d[5], out playerDeckData);
            CNASerialize.Dz(d[6], out playerMovementCount);
            CNASerialize.Dz(d[7], out playerInfluenceCount);
            CNASerialize.Dz(d[8], out gridLocationHistory);
            CNASerialize.Dz(d[9], out visableMonsters);
            CNASerialize.Dz(d[10], out battle);
            CNASerialize.Dz(d[11], out fame);
            CNASerialize.Dz(d[12], out repLevel);
            CNASerialize.Dz(d[13], out actionTaken);
            CNASerialize.Dz(d[14], out crystal);
            CNASerialize.Dz(d[15], out mana);
            CNASerialize.Dz(d[16], out armor);
            CNASerialize.Dz(d[17], out healpoints);
            CNASerialize.Dz(d[18], out manaPoolAvailable);
            CNASerialize.Dz(d[19], out dummyPlayer);
            CNASerialize.Dz(d[20], out gameEffects);
            CNASerialize.Dz(d[21], out shields);
            CNASerialize.Dz(d[22], out manaPool);
            CNASerialize.Dz(d[23], out board);
            CNASerialize.Dz(d[24], out waitOnServer);
            CNASerialize.Dz(d[25], out undoLock);
            CNASerialize.Dz(d[26], out time01);
            CNASerialize.Dz(d[27], out time02);

        }
    }
}