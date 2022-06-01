using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace cna.poo {

    [Serializable]
    public class GameData : Data {
        public GameData() {
            board = new BoardData();
            monsters = new MonsterData();
        }

        [SerializeField] private GameLobbyData gld;

        [Header("Player Details")]
        [SerializeField] private int hostId;
        [SerializeField] private List<PlayerData> players;
        [SerializeField] private List<int> playerTurnOrder;
        [SerializeField] private int playerTurnIndex;
        [SerializeField] private int endOfRound = -1;

        [Header("Game Details")]
        [SerializeField] private string gameId;
        [SerializeField] private Game_Enum gameStatus;
        [SerializeField] private int gameRoundCounter;
        [SerializeField] private int turnCounter;
        [SerializeField] private BoardData board;
        [SerializeField] private MonsterData monsters;

        public GameLobbyData Gld { get => gld; set => gld = value; }
        public string GameId { get => gameId; set => gameId = value; }
        public Game_Enum GameStatus { get => gameStatus; set => gameStatus = value; }
        public int HostId { get => hostId; set => hostId = value; }
        public List<PlayerData> Players { get => players; set => players = value; }
        public BoardData Board { get => board; set => board = value; }
        public List<int> PlayerTurnOrder { get => playerTurnOrder; set => playerTurnOrder = value; }
        public int PlayerTurnIndex { get => playerTurnIndex; set { playerTurnIndex = value >= PlayerTurnOrder.Count ? 0 : value; } }
        public int GameRoundCounter { get => gameRoundCounter; set => gameRoundCounter = value; }
        public MonsterData Monsters { get => monsters; set => monsters = value; }
        public int EndOfRound { get => endOfRound; set => endOfRound = value; }
        public int TurnCounter { get => turnCounter; set => turnCounter = value; }

        public void Update(GameData gd) {
            switch (gd.gameStatus) {
                case Game_Enum.NA: { break; }
                case Game_Enum.CHAR_CREATION: {
                    gld = gd.gld;
                    gameStatus = gd.gameStatus;
                    break;
                }
                default: {
                    gameStatus = gd.gameStatus;
                    gameId = gd.gameId;
                    hostId = gd.hostId;
                    players = gd.players;
                    board = gd.board;
                    monsters = gd.Monsters;
                    PlayerTurnOrder = gd.playerTurnOrder;
                    PlayerTurnIndex = gd.playerTurnIndex;
                    EndOfRound = gd.endOfRound;
                    GameRoundCounter = gd.gameRoundCounter;
                    break;
                }
            }
        }

        public GameData Clone() {
            GameData g = JsonUtility.FromJson<GameData>(JsonUtility.ToJson(this));
            return g;
        }

        public void Clear() {
            GameRoundCounter = 0;
            Monsters.Clear();
            EndOfRound = 0;
            TurnCounter = 0;
            GameStatus = Game_Enum.NA;
        }
    }
}
