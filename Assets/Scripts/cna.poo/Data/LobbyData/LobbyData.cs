using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {

    [Serializable]
    public class LobbyData : Data {
        [SerializeField] private string gameId;
        [SerializeField] private PlayerData hostPlayer;
        [SerializeField] private bool gameStarted = false;

        public bool GameStarted { get => gameStarted; set => gameStarted = value; }
        public PlayerData HostPlayer { get => hostPlayer; set => hostPlayer = value; }
        public string GameId { get => gameId; set => gameId = value; }

        public LobbyData() { }

        public LobbyData(string hostName, int hostKey, string gameId) {
            hostPlayer = new PlayerData(hostName, hostKey);
            this.GameId = gameId;
        }

        public override bool Equals(object obj) {
            return obj is LobbyData data &&
                   gameId == data.gameId;
        }

        public override int GetHashCode() {
            return HashCode.Combine(gameId);
        }
    }
}
