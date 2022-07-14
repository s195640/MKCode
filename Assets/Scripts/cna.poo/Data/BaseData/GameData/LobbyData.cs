using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {

    [Serializable]
    public class LobbyData : BaseData {
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

        public override string Serialize() {
            string data = CNASerialize.Sz(gameId) + "%"
                + CNASerialize.Sz(hostPlayer) + "%"
                + CNASerialize.Sz(gameStarted);
            return "[" + data + "]";
        }
        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out gameId);
            CNASerialize.Dz(d[1], out hostPlayer);
        }
    }
}
