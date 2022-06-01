using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {

    [Serializable]
    public class GameLobbyData : Data {
        [SerializeField] private string gameId;
        [SerializeField] private PlayerData host;
        [SerializeField] private List<PlayerData> players;
        [SerializeField] private GameMapLayout_Enum gameMapLayout = GameMapLayout_Enum.MapFullx5;
        [SerializeField] [Range(0, 11)] private int basicTiles = 5;
        [SerializeField] [Range(0, 4)] private int coreTiles = 3;
        [SerializeField] [Range(0, 4)] private int cityTiles = 2;
        [SerializeField] [Range(1, 10)] private int rounds = 6;
        [SerializeField] private bool easyStart = false;
        [SerializeField] private bool dummyPlayer = true;

        public GameLobbyData() { }
        public GameLobbyData(PlayerData p, string gameId) {
            host = p;
            this.gameId = gameId;
            players = new List<PlayerData>() { p };
        }

        public PlayerData Host { get => host; set => host = value; }
        public List<PlayerData> Players { get => players; set => players = value; }
        public string GameId { get => gameId; set => gameId = value; }
        public GameMapLayout_Enum GameMapLayout { get => gameMapLayout; set => gameMapLayout = value; }
        public int BasicTiles { get => basicTiles; set => basicTiles = value; }
        public int CoreTiles { get => coreTiles; set => coreTiles = value; }
        public int CityTiles { get => cityTiles; set => cityTiles = value; }
        public bool EasyStart { get => easyStart; set => easyStart = value; }
        public int Rounds { get => rounds; set => rounds = value; }
        public bool DummyPlayer { get => dummyPlayer; set => dummyPlayer = value; }
    }
}
