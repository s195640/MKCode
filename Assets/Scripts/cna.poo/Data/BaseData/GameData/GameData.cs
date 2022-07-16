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
        [SerializeField] private Scenario_Enum scenario = Scenario_Enum.FullConquest;
        [SerializeField] private Difficulty_Enum difficulty = Difficulty_Enum.Normal;
        [SerializeField] private GameMapLayout_Enum gameMapLayout = GameMapLayout_Enum.Wedge;
        [SerializeField] [Range(0, 11)] private int basicTiles = 5;
        [SerializeField] [Range(0, 4)] private int coreTiles = 2;
        [SerializeField] [Range(0, 4)] private int cityTiles = 2;
        [SerializeField] [Range(1, 10)] private int rounds = 6;
        [SerializeField] [Range(1, 11)] private int level = 6;
        [SerializeField] private bool easyStart = false;
        [SerializeField] private bool dummyPlayer = true;

        [SerializeField] [Range(0, 2)] private int famePerLevel = 0;
        [SerializeField] [Range(-7, 7)] private int startRep = 0;
        [SerializeField] [Range(1, 8)] private int manaDie = 3;
        [SerializeField] [Range(0, 8)] private int unitOffer = 3;

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
        public Scenario_Enum Scenario { get => scenario; set => scenario = value; }
        public Difficulty_Enum Difficulty { get => difficulty; set => difficulty = value; }
        public int FamePerLevel { get => famePerLevel; set => famePerLevel = value; }
        public int StartRep { get => startRep; set => startRep = value; }
        public int ManaDie { get => manaDie; set => manaDie = value; }
        public int UnitOffer { get => unitOffer; set => unitOffer = value; }

        public void CharCreateUpdate(int players) {
            if (scenario != Scenario_Enum.Custom) {
                if (difficulty == Difficulty_Enum.Custom) {
                    difficulty = Difficulty_Enum.Normal;
                }
                easyStart = false;
                dummyPlayer = players == 1;
                switch (scenario) {
                    case Scenario_Enum.Blitz: {
                        rounds = 4;
                        level = 3;
                        famePerLevel = 1;
                        startRep = 2;
                        manaDie = players + 3;
                        unitOffer = players + 3;
                        switch (players) {
                            case 1: {
                                gameMapLayout = GameMapLayout_Enum.Wedge;
                                basicTiles = 5;
                                coreTiles = 3;
                                cityTiles = 2;
                                break;
                            }
                            case 2: {
                                gameMapLayout = GameMapLayout_Enum.Wedge;
                                basicTiles = 6;
                                coreTiles = 2;
                                cityTiles = 2;
                                break;
                            }
                            case 3: {
                                gameMapLayout = GameMapLayout_Enum.Wedge;
                                basicTiles = 7;
                                coreTiles = 3;
                                cityTiles = 3;
                                break;
                            }
                            case 4: {
                                gameMapLayout = GameMapLayout_Enum.MapFullx4;
                                basicTiles = 9;
                                coreTiles = 4;
                                cityTiles = 4;
                                break;
                            }
                        }
                        break;
                    }
                    case Scenario_Enum.FullConquest: {
                        rounds = 6;
                        level = 4;
                        famePerLevel = 0;
                        startRep = 0;
                        manaDie = players + 2;
                        unitOffer = players + 2;
                        switch (players) {
                            case 1: {
                                gameMapLayout = GameMapLayout_Enum.Wedge;
                                basicTiles = 7;
                                coreTiles = 2;
                                cityTiles = 2;
                                level = 6;
                                break;
                            }
                            case 2: {
                                gameMapLayout = GameMapLayout_Enum.Wedge;
                                basicTiles = 8;
                                coreTiles = 1;
                                cityTiles = 2;
                                break;
                            }
                            case 3: {
                                gameMapLayout = GameMapLayout_Enum.Wedge;
                                basicTiles = 9;
                                coreTiles = 2;
                                cityTiles = 3;
                                break;
                            }
                            case 4: {
                                gameMapLayout = GameMapLayout_Enum.MapFullx5;
                                basicTiles = 11;
                                coreTiles = 3;
                                cityTiles = 4;
                                break;
                            }
                        }
                        break;
                    }
                }
                basicTiles -= (gameMapLayout == GameMapLayout_Enum.Wedge) ? 2 : 3;
                switch (difficulty) {
                    case Difficulty_Enum.VeryEasy: {
                        level -= 2;
                        manaDie++;
                        startRep++;
                        unitOffer++;
                        famePerLevel++;
                        easyStart = true;
                        break;
                    }
                    case Difficulty_Enum.Easy: {
                        level--;
                        manaDie++;
                        unitOffer++;
                        break;
                    }
                    case Difficulty_Enum.Normal: {
                        break;
                    }
                    case Difficulty_Enum.Hard: {
                        level += 2;
                        break;
                    }
                    case Difficulty_Enum.VeryHard: {
                        level += 3;
                        manaDie--;
                        unitOffer--;
                        break;
                    }
                    case Difficulty_Enum.Impossible: {
                        level = 11;
                        manaDie -= 2;
                        unitOffer -= 2;
                        break;
                    }
                }
            }
        }


        public void UpdateData(GameData gameData) {
            gameId = gameData.gameId;
            gameStartTime = gameData.gameStartTime;
            hostKey = gameData.hostKey;
            seed = gameData.seed;
            scenario = gameData.scenario;
            difficulty = gameData.difficulty;
            gameMapLayout = gameData.gameMapLayout;
            basicTiles = gameData.basicTiles;
            coreTiles = gameData.coreTiles;
            cityTiles = gameData.cityTiles;
            rounds = gameData.rounds;
            level = gameData.level;
            easyStart = gameData.easyStart;
            dummyPlayer = gameData.dummyPlayer;
            famePerLevel = gameData.famePerLevel;
            startRep = gameData.startRep;
            manaDie = gameData.manaDie;
            unitOffer = gameData.unitOffer;
        }

        public override string Serialize() {
            string data = CNASerialize.Sz(gameId) + "%"
                + CNASerialize.Sz(gameStartTime) + "%"
                + CNASerialize.Sz(hostKey) + "%"
                + CNASerialize.Sz(seed) + "%"
                + CNASerialize.Sz(scenario) + "%"
                + CNASerialize.Sz(difficulty) + "%"
                + CNASerialize.Sz(gameMapLayout) + "%"
                + CNASerialize.Sz(basicTiles) + "%"
                + CNASerialize.Sz(coreTiles) + "%"
                + CNASerialize.Sz(cityTiles) + "%"
                + CNASerialize.Sz(rounds) + "%"
                + CNASerialize.Sz(level) + "%"
                + CNASerialize.Sz(easyStart) + "%"
                + CNASerialize.Sz(dummyPlayer) + "%"
                + CNASerialize.Sz(famePerLevel) + "%"
                + CNASerialize.Sz(startRep) + "%"
                + CNASerialize.Sz(manaDie) + "%"
                + CNASerialize.Sz(unitOffer) + "%";
            return "[" + data + "]";
        }

        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out gameId);
            CNASerialize.Dz(d[1], out gameStartTime);
            CNASerialize.Dz(d[2], out hostKey);
            CNASerialize.Dz(d[3], out seed);
            CNASerialize.Dz(d[4], out scenario);
            CNASerialize.Dz(d[5], out difficulty);
            CNASerialize.Dz(d[6], out gameMapLayout);
            CNASerialize.Dz(d[7], out basicTiles);
            CNASerialize.Dz(d[8], out coreTiles);
            CNASerialize.Dz(d[9], out cityTiles);
            CNASerialize.Dz(d[10], out rounds);
            CNASerialize.Dz(d[11], out level);
            CNASerialize.Dz(d[12], out easyStart);
            CNASerialize.Dz(d[13], out dummyPlayer);
            CNASerialize.Dz(d[14], out famePerLevel);
            CNASerialize.Dz(d[15], out startRep);
            CNASerialize.Dz(d[16], out manaDie);
            CNASerialize.Dz(d[17], out unitOffer);
        }
    }
}
