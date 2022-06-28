using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {

    [Serializable]
    public class Data : BaseData {
        public Data() {
            boardGameData = new BoardData();
            gameData = new GameData();
        }

        public Data(string gameId, PlayerData host) {
            players = new List<PlayerData>() { host };
            gameData = new GameData(gameId, host.Key);
        }

        [SerializeField] private GameData gameData = new GameData();
        [SerializeField] private List<PlayerData> players = new List<PlayerData>();
        [SerializeField] private BoardData boardGameData = new BoardData();
        [SerializeField] private Game_Enum gameStatus = Game_Enum.NA;




        public string GameId { get => gameData.GameId; }
        public int HostPlayerKey { get => gameData.HostKey; }

        public GameData GameData { get => gameData; set => gameData = value; }
        public Game_Enum GameStatus { get => gameStatus; set => gameStatus = value; }
        public List<PlayerData> Players { get => players; set => players = value; }
        public BoardData Board { get => boardGameData; set => boardGameData = value; }
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
            gameStatus = data.gameStatus;
            if (boardGameData == null) {
                boardGameData = data.boardGameData;
            } else {
                boardGameData.UpdateData(data.boardGameData);
            }
        }

        public Data Clone() {
            Data data = JsonUtility.FromJson<Data>(JsonUtility.ToJson(this));
            return data;
        }

        public void Clear() {
            players.ForEach(p => p.Clear());
            boardGameData.Clear();
            GameStatus = Game_Enum.NA;
        }
    }
}
