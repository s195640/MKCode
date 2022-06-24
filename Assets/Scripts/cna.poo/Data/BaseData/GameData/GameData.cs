using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {

    [Serializable]
    public class GameData : BaseData {
        [SerializeField] private string gameId;
        [SerializeField] private int hostKey;
        [SerializeField] private int seed;
        [SerializeField] private GameMapLayout_Enum gameMapLayout = GameMapLayout_Enum.MapFullx5;
        [SerializeField] [Range(0, 11)] private int basicTiles = 5;
        [SerializeField] [Range(0, 4)] private int coreTiles = 3;
        [SerializeField] [Range(0, 4)] private int cityTiles = 2;
        [SerializeField] [Range(1, 10)] private int rounds = 6;
        [SerializeField] [Range(1, 11)] private int level = 5;
        [SerializeField] private bool easyStart = false;
        [SerializeField] private bool dummyPlayer = true;

        public GameData() { }
        public GameData(string gameId, int hostKey) {
            this.hostKey = hostKey;
            this.gameId = gameId;
        }

        public string GameId { get => gameId; set => gameId = value; }
        public GameMapLayout_Enum GameMapLayout { get => gameMapLayout; set => gameMapLayout = value; }
        public int BasicTiles { get => basicTiles; set => basicTiles = value; }
        public int CoreTiles { get => coreTiles; set => coreTiles = value; }
        public int CityTiles { get => cityTiles; set => cityTiles = value; }
        public bool EasyStart { get => easyStart; set => easyStart = value; }
        public int Rounds { get => rounds; set => rounds = value; }
        public bool DummyPlayer { get => dummyPlayer; set => dummyPlayer = value; }
        public int Seed { get => seed; set => seed = value; }
        public int Level { get => level; set => level = value; }
        public int HostKey { get => hostKey; set => hostKey = value; }

        public void UpdateData(GameData gameData) {
            gameId = gameData.gameId;
            gameMapLayout = gameData.gameMapLayout;
            basicTiles = gameData.basicTiles;
            coreTiles = gameData.coreTiles;
            cityTiles = gameData.cityTiles;
            rounds = gameData.rounds;
            easyStart = gameData.easyStart;
            dummyPlayer = gameData.dummyPlayer;
            hostKey = gameData.hostKey;
        }
    }
}
