using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {

    [Serializable]
    public class GameData : BaseData {
        [SerializeField] private string gameId;
        [SerializeField] private long gameStartTime;
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
            gameStartTime = DateTime.Now.Ticks;
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
        public long GameStartTime { get => gameStartTime; set => gameStartTime = value; }

        public void UpdateData(GameData gameData) {
            gameId = gameData.gameId;
            gameStartTime = gameData.gameStartTime;
            hostKey = gameData.hostKey;
            seed = gameData.seed;
            gameMapLayout = gameData.gameMapLayout;
            basicTiles = gameData.basicTiles;
            coreTiles = gameData.coreTiles;
            cityTiles = gameData.cityTiles;
            rounds = gameData.rounds;
            level = gameData.level;
            easyStart = gameData.easyStart;
            dummyPlayer = gameData.dummyPlayer;
        }

        public override string Serialize() {
            string data = CNASerialize.Sz(gameId) + "%"
                + CNASerialize.Sz(gameStartTime) + "%"
                + CNASerialize.Sz(hostKey) + "%"
                + CNASerialize.Sz(seed) + "%"
                + CNASerialize.Sz(gameMapLayout) + "%"
                + CNASerialize.Sz(basicTiles) + "%"
                + CNASerialize.Sz(coreTiles) + "%"
                + CNASerialize.Sz(cityTiles) + "%"
                + CNASerialize.Sz(rounds) + "%"
                + CNASerialize.Sz(level) + "%"
                + CNASerialize.Sz(easyStart) + "%"
                + CNASerialize.Sz(dummyPlayer) + "%";
            return "[" + data + "]";
        }

        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out gameId);
            CNASerialize.Dz(d[1], out gameStartTime);
            CNASerialize.Dz(d[2], out hostKey);
            CNASerialize.Dz(d[3], out seed);
            CNASerialize.Dz(d[4], out gameMapLayout);
            CNASerialize.Dz(d[5], out basicTiles);
            CNASerialize.Dz(d[6], out coreTiles);
            CNASerialize.Dz(d[7], out cityTiles);
            CNASerialize.Dz(d[8], out rounds);
            CNASerialize.Dz(d[9], out level);
            CNASerialize.Dz(d[10], out easyStart);
            CNASerialize.Dz(d[11], out dummyPlayer);
        }
    }
}
