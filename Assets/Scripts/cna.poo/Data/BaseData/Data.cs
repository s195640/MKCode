using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace cna.poo {

    [Serializable]
    public class Data : BaseData {
        public Data() {
            boardGameData = new BoardData();
            //monsters = new MonsterData();
            gameData = new GameData();
        }

        public Data(string gameId, PlayerData host) {
            players = new List<PlayerData>() { host };
            gameData = new GameData(gameId, host.Key);
        }

        [SerializeField] private GameData gameData;
        [SerializeField] private List<PlayerData> players;
        [SerializeField] private BoardData boardGameData;



        [Header("Player Details")]
        [SerializeField] private List<int> playerTurnOrder;
        [SerializeField] private int playerTurnIndex;
        [SerializeField] private bool endOfRound = false;

        [Header("Game Details")]
        [SerializeField] private Game_Enum gameStatus;
        [SerializeField] private int gameRoundCounter;
        [SerializeField] private int turnCounter;

        //[SerializeField] private MonsterData monsters;

        //  StaticData
        public string GameId { get => gameData.GameId; }
        public int HostPlayerKey { get => gameData.HostKey; }



        public GameData GameData { get => gameData; set => gameData = value; }
        public Game_Enum GameStatus { get => gameStatus; set => gameStatus = value; }
        public List<PlayerData> Players { get => players; set => players = value; }
        public BoardData Board { get => boardGameData; set => boardGameData = value; }
        public List<int> PlayerTurnOrder { get => playerTurnOrder; set => playerTurnOrder = value; }
        public int PlayerTurnIndex { get => playerTurnIndex; set { playerTurnIndex = value >= PlayerTurnOrder.Count ? 0 : value; } }
        public int GameRoundCounter { get => gameRoundCounter; set => gameRoundCounter = value; }
        //public MonsterData Monsters { get => monsters; set => monsters = value; }
        public bool EndOfRound { get => endOfRound; set => endOfRound = value; }
        public int TurnCounter { get => turnCounter; set => turnCounter = value; }

        public void UpdateData(Data data) {
            UpdateData_ExcludePlayers(data);
            if (players != null && players.Count == data.players.Count) {
                players.ForEach(p => p.UpdateData(data.players.Find(z => z.Key == p.Key)));
            } else {
                players = data.players;
            }
        }

        public void UpdateData_ExcludePlayers(Data data) {
            if (gameData == null) {
                gameData = data.gameData;
            } else {
                gameData.UpdateData(data.gameData);
            }
            playerTurnOrder = data.playerTurnOrder;
            playerTurnIndex = data.playerTurnIndex;
            endOfRound = data.endOfRound;
            gameStatus = data.gameStatus;
            gameRoundCounter = data.gameRoundCounter;
            turnCounter = data.turnCounter;
            if (boardGameData == null) {
                boardGameData = data.boardGameData;
            } else {
                boardGameData.UpdateData(data.boardGameData);
            }
            //if (monsters == null) {
            //    monsters = data.monsters;
            //} else {
            //    monsters.UpdateData(data.monsters);
            //}
        }

        public Data Clone() {
            Data data = JsonUtility.FromJson<Data>(JsonUtility.ToJson(this));
            return data;
        }

        public void Clear() {
            GameRoundCounter = 0;
            //Monsters.Clear();
            EndOfRound = false;
            TurnCounter = 0;
            GameStatus = Game_Enum.NA;
        }
    }
}
