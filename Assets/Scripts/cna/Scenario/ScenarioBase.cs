using System;
using System.Collections.Generic;
using System.Linq;
using cna.poo;
using UnityEngine;


namespace cna {
    public abstract class ScenarioBase {
        [SerializeField] protected int maxBoardSize = 0;
        [SerializeField] private List<MapHexId_Enum> mapDeck;
        [SerializeField] private List<int> woundDeck = new List<int>();
        [SerializeField] private List<int> monsterGreenDeck = new List<int>();
        [SerializeField] private List<int> monsterGreyDeck = new List<int>();
        [SerializeField] private List<int> monsterBrownDeck = new List<int>();
        [SerializeField] private List<int> monsterVioletDeck = new List<int>();
        [SerializeField] private List<int> monsterWhiteDeck = new List<int>();
        [SerializeField] private List<int> monsterRedDeck = new List<int>();
        [SerializeField] private List<int> ruinDeck = new List<int>();
        [SerializeField] private List<int> unitRegularDeck = new List<int>();
        [SerializeField] private List<int> unitEliteDeck = new List<int>();
        [SerializeField] private List<int> blueSkillDeck = new List<int>();
        [SerializeField] private List<int> greenSkillDeck = new List<int>();
        [SerializeField] private List<int> redSkillDeck = new List<int>();
        [SerializeField] private List<int> whiteSkillDeck = new List<int>();
        [SerializeField] private List<int> advancedDeck = new List<int>();
        [SerializeField] private List<int> spellDeck = new List<int>();
        [SerializeField] private List<int> artifactDeck = new List<int>();
        [SerializeField] private List<int> tacticsDayDeck = new List<int>();
        [SerializeField] private List<int> tacticsNightDeck = new List<int>();

        private Dictionary<int, Vector3Int> locationMap;
        private Dictionary<int, List<int>> adjBoard;

        public int MaxBoardSize { get => maxBoardSize; }
        public Dictionary<int, List<int>> AdjBoard { get => adjBoard; set => adjBoard = value; }
        public Dictionary<int, Vector3Int> LocationMap { get => locationMap; set => locationMap = value; }
        public List<MapHexId_Enum> MapDeck { get => mapDeck; set => mapDeck = value; }
        public bool isDay { get => D.G.GameRoundCounter % 2 != 0; }
        public List<int> MonsterGreenDeck { get => monsterGreenDeck; set => monsterGreenDeck = value; }
        public List<int> MonsterGreyDeck { get => monsterGreyDeck; set => monsterGreyDeck = value; }
        public List<int> MonsterBrownDeck { get => monsterBrownDeck; set => monsterBrownDeck = value; }
        public List<int> MonsterVioletDeck { get => monsterVioletDeck; set => monsterVioletDeck = value; }
        public List<int> MonsterWhiteDeck { get => monsterWhiteDeck; set => monsterWhiteDeck = value; }
        public List<int> MonsterRedDeck { get => monsterRedDeck; set => monsterRedDeck = value; }
        public List<int> UnitRegularDeck { get => unitRegularDeck; set => unitRegularDeck = value; }
        public List<int> UnitEliteDeck { get => unitEliteDeck; set => unitEliteDeck = value; }
        public List<int> WoundDeck { get => woundDeck; set => woundDeck = value; }
        public List<int> BlueSkillDeck { get => blueSkillDeck; set => blueSkillDeck = value; }
        public List<int> GreenSkillDeck { get => greenSkillDeck; set => greenSkillDeck = value; }
        public List<int> RedSkillDeck { get => redSkillDeck; set => redSkillDeck = value; }
        public List<int> WhiteSkillDeck { get => whiteSkillDeck; set => whiteSkillDeck = value; }
        public List<int> AdvancedDeck { get => advancedDeck; set => advancedDeck = value; }
        public List<int> SpellDeck { get => spellDeck; set => spellDeck = value; }
        public List<int> ArtifactDeck { get => artifactDeck; set => artifactDeck = value; }
        public List<int> RuinDeck { get => ruinDeck; set => ruinDeck = value; }
        public List<int> TacticsDayDeck { get => tacticsDayDeck; set => tacticsDayDeck = value; }
        public List<int> TacticsNightDeck { get => tacticsNightDeck; set => tacticsNightDeck = value; }

        public static ScenarioBase Create() {
            D.A.Clear();
            UnityEngine.Random.InitState(D.GLD.Seed);
            ScenarioBase map = null;
            switch (D.GLD.GameMapLayout) {
                case GameMapLayout_Enum.Wedge: {
                    map = new MapWedge();
                    break;
                }
                case GameMapLayout_Enum.MapFullx3: {
                    map = new MapFullx3();
                    break;
                }
                case GameMapLayout_Enum.MapFullx4: {
                    map = new MapFullx4();
                    break;
                }
                case GameMapLayout_Enum.MapFullx5: {
                    map = new MapFullx5();
                    break;
                }
            }
            map.buildMapDeck(D.GLD.GameMapLayout == GameMapLayout_Enum.Wedge, D.GLD.EasyStart, D.GLD.BasicTiles, D.GLD.CoreTiles, D.GLD.CityTiles);
            D.Cards.ForEach(c => {
                switch (c.CardType) {
                    case CardType_Enum.Monster: {
                        switch (((CardMonsterVO)c).MonsterType) {
                            case MonsterType_Enum.Green: { map.MonsterGreenDeck.Add(c.UniqueId); break; }
                            case MonsterType_Enum.Grey: { map.MonsterGreyDeck.Add(c.UniqueId); break; }
                            case MonsterType_Enum.Brown: { map.MonsterBrownDeck.Add(c.UniqueId); break; }
                            case MonsterType_Enum.Violet: { map.MonsterVioletDeck.Add(c.UniqueId); break; }
                            case MonsterType_Enum.White: { map.MonsterWhiteDeck.Add(c.UniqueId); break; }
                            case MonsterType_Enum.Red: { map.MonsterRedDeck.Add(c.UniqueId); break; }
                        }
                        break;
                    }
                    case CardType_Enum.Unit_Normal: { map.UnitRegularDeck.Add(c.UniqueId); break; }
                    case CardType_Enum.Unit_Elite: { map.UnitEliteDeck.Add(c.UniqueId); break; }
                    case CardType_Enum.Wound: { map.WoundDeck.Add(c.UniqueId); break; }
                    case CardType_Enum.Skill: {
                        switch (c.Avatar) {
                            case Image_Enum.A_MEEPLE_BLUE: { map.BlueSkillDeck.Add(c.UniqueId); break; }
                            case Image_Enum.A_MEEPLE_GREEN: { map.GreenSkillDeck.Add(c.UniqueId); break; }
                            case Image_Enum.A_MEEPLE_RED: { map.RedSkillDeck.Add(c.UniqueId); break; }
                            case Image_Enum.A_MEEPLE_WHITE: { map.WhiteSkillDeck.Add(c.UniqueId); break; }
                        }
                        break;
                    }
                    case CardType_Enum.Advanced: { map.AdvancedDeck.Add(c.UniqueId); break; }
                    case CardType_Enum.Spell: { map.SpellDeck.Add(c.UniqueId); break; }
                    case CardType_Enum.Artifact: { map.ArtifactDeck.Add(c.UniqueId); break; }
                    case CardType_Enum.AncientRuins_Monster:
                    case CardType_Enum.AncientRuins_Alter: { map.RuinDeck.Add(c.UniqueId); break; }
                    case CardType_Enum.Tactics_Day: { map.TacticsDayDeck.Add(c.UniqueId); break; }
                    case CardType_Enum.Tactics_Night: { map.TacticsNightDeck.Add(c.UniqueId); break; }
                }
            });
            map.MonsterGreenDeck.ShuffleDeck();
            map.MonsterGreyDeck.ShuffleDeck();
            map.MonsterBrownDeck.ShuffleDeck();
            map.MonsterVioletDeck.ShuffleDeck();
            map.MonsterWhiteDeck.ShuffleDeck();
            map.MonsterRedDeck.ShuffleDeck();
            map.UnitRegularDeck.ShuffleDeck();
            map.UnitEliteDeck.ShuffleDeck();
            map.BlueSkillDeck.ShuffleDeck();
            map.GreenSkillDeck.ShuffleDeck();
            map.RedSkillDeck.ShuffleDeck();
            map.WhiteSkillDeck.ShuffleDeck();
            map.AdvancedDeck.ShuffleDeck();
            map.SpellDeck.ShuffleDeck();
            map.ArtifactDeck.ShuffleDeck();
            map.RuinDeck.ShuffleDeck();

            UnityEngine.Random.InitState(DateTime.Now.Second * 1000 + DateTime.Now.Millisecond);
            return map;
        }

        public ScenarioBase() {
            setupLocationMap();
            setupAdjBoard();
        }

        protected abstract void setupLocationMap();
        protected abstract void setupAdjBoard();

        public int ConvertWorldToLocIndex(V2IntVO loc) {
            /*
                 0 1
                2 3 4
                 5 6
            */
            int mapIndex = D.Scenario.ConvertWorldToIndex(loc);
            Vector3Int center = D.Scenario.ConvertIndexToWorld(mapIndex);
            int locIndex = 3;
            if (center.y == loc.Y) {
                if (center.x == loc.X) {
                    locIndex = 3;
                } else if (center.x > loc.X) {
                    locIndex = 2;
                } else {
                    locIndex = 4;
                }
            } else if (center.y > loc.Y) {
                if (center.x > loc.X) {
                    locIndex = 5;
                } else if (center.x > loc.X) {
                    locIndex = 6;
                } else {
                    V2IntVO testLoc = new V2IntVO(loc.X + 1, loc.Y);
                    int testMapIndex = D.Scenario.ConvertWorldToIndex(testLoc);
                    if (mapIndex == testMapIndex) {
                        locIndex = 5;
                    } else {
                        locIndex = 6;
                    }
                }
            } else {
                if (center.x > loc.X) {
                    locIndex = 0;
                } else if (center.x > loc.X) {
                    locIndex = 1;
                } else {
                    V2IntVO testLoc = new V2IntVO(loc.X + 1, loc.Y);
                    int testMapIndex = D.Scenario.ConvertWorldToIndex(testLoc);
                    if (mapIndex == testMapIndex) {
                        locIndex = 0;
                    } else {
                        locIndex = 1;
                    }
                }
            }
            return locIndex;
        }

        public Vector3Int ConvertIndexToWorld(int index) {
            if (LocationMap.ContainsKey(index)) {
                return LocationMap[index];
            } else {
                return new Vector3Int(0, 0, -50);
            }
        }
        public Vector3Int ConvertIndexToWorld(int index, int loc) {
            Vector3Int v = ConvertIndexToWorld(index);
            int offset_0 = 0;
            int offset_1 = 1;
            int offset_5 = 0;
            int offset_6 = 1;
            if (2 * (v.y / 2) == v.y) {
                offset_0 = -1;
                offset_1 = 0;
                offset_5 = -1;
                offset_6 = 0;
            }
            switch (loc) {
                case 0: {
                    v.y += 1;
                    v.x += offset_0;
                    break;
                }
                case 1: {
                    v.y += 1;
                    v.x += offset_1;
                    break;
                }
                case 2: {
                    v.x -= 1;
                    break;
                }
                case 3: {
                    break;
                }
                case 4: {
                    v.x += 1;
                    break;
                }
                case 5: {
                    v.y -= 1;
                    v.x += offset_5;
                    break;
                }
                case 6: {
                    v.y -= 1;
                    v.x += offset_6;
                    break;
                }
            }
            return v;
        }
        public int ConvertGridLocToIndex(V2IntVO gridPos) {
            List<V2IntVO> pts = BasicUtil.GetAdjacentPoints(gridPos);
            pts.Add(gridPos);
            foreach (V2IntVO p in pts) {
                if (LocationMap.ContainsValue(p.Vector3Int)) {
                    return LocationMap.First(x => x.Value.Equals(p)).Key;
                }
            }
            return -1;
        }
        public int ConvertWorldToIndex(V2IntVO loc) {
            for (int i = 0; i < MaxBoardSize; i++) {
                for (int j = 0; j < 7; j++) {
                    if (loc.Equals(ConvertIndexToWorld(i, j))) {
                        return i;
                    }
                }
            }
            return 0;
        }



        public MapHexId_Enum DrawGameHex(int index) {
            MapHexId_Enum mapHex = MapDeck[D.Board.MapDeckIndex];
            D.Board.MapDeckIndex++;
            D.Board.CurrentMap[index] = mapHex;
            MapHexVO vo = D.HexMap[mapHex];
            for (int loc = 0; loc < 7; loc++) {
                List<MonsterType_Enum> monstersToAdd = new List<MonsterType_Enum>();
                bool isMonsterVisable = true;
                switch (vo.StructureList[loc]) {
                    case Image_Enum.SH_MaraudingOrcs: {
                        monstersToAdd.Add(MonsterType_Enum.Green);
                        break;
                    }
                    case Image_Enum.SH_Draconum: {
                        monstersToAdd.Add(MonsterType_Enum.Red);
                        break;
                    }
                    case Image_Enum.SH_Keep: {
                        monstersToAdd.Add(MonsterType_Enum.Grey);
                        isMonsterVisable = false;
                        break;
                    }
                    case Image_Enum.SH_MageTower: {
                        monstersToAdd.Add(MonsterType_Enum.Violet);
                        isMonsterVisable = false;
                        break;
                    }
                    case Image_Enum.SH_City_Blue: {
                        monstersToAdd.AddRange(getCityDefenders(Image_Enum.SH_City_Blue, D.GLD.Level));
                        break;
                    }
                    case Image_Enum.SH_City_Green: {
                        monstersToAdd.AddRange(getCityDefenders(Image_Enum.SH_City_Green, D.GLD.Level));
                        break;
                    }
                    case Image_Enum.SH_City_Red: {
                        monstersToAdd.AddRange(getCityDefenders(Image_Enum.SH_City_Red, D.GLD.Level));
                        break;
                    }
                    case Image_Enum.SH_City_White: {
                        monstersToAdd.AddRange(getCityDefenders(Image_Enum.SH_City_White, D.GLD.Level));
                        break;
                    }
                    case Image_Enum.SH_SpawningGround: {
                        monstersToAdd.Add(MonsterType_Enum.Brown);
                        monstersToAdd.Add(MonsterType_Enum.Brown);
                        isMonsterVisable = false;
                        break;
                    }
                    case Image_Enum.SH_MonsterDen: {
                        monstersToAdd.Add(MonsterType_Enum.Brown);
                        isMonsterVisable = false;
                        break;
                    }
                    case Image_Enum.SH_Monastery: {
                        //D.G.Board.MonasteryCount++;
                        //if (D.G.GameStatus != Game_Enum.Player_Turn) {
                        //    if (D.G.Board.AdvancedIndex < AdvancedDeck.Count) {
                        //        D.G.Board.UnitOffering.Add(AdvancedDeck[D.G.Board.AdvancedIndex]);
                        //        D.G.Board.AdvancedIndex++;
                        //    }
                        //}
                        break;
                    }
                    case Image_Enum.SH_AncientRuins: {
                        monstersToAdd.Add(MonsterType_Enum.Yellow);
                        if (!D.Scenario.isDay) {
                            isMonsterVisable = false;
                        }
                        break;
                    }
                    default: {
                        break;
                    }
                }
                if (monstersToAdd.Count > 0) {
                    AddMonster(monstersToAdd, new V2IntVO(ConvertIndexToWorld(index, loc)), isMonsterVisable);
                }
            }
            //rebuildCurrentMap();
            return mapHex;
        }
        public void AddMonster(List<MonsterType_Enum> monsterTypes, V2IntVO pos, bool visable) {
            List<int> monsterCards = new List<int>();

            if (!D.G.Board.MonsterData.ContainsKey(pos)) {
                D.G.Board.MonsterData.Add(pos, new WrapList<int>());
            }
            foreach (MonsterType_Enum monsterType in monsterTypes) {
                monsterCards.Add(DrawMonster(monsterType));
            }
            D.G.Board.MonsterData[pos].AddRange(monsterCards);
            if (visable) {
                D.G.Players.ForEach(p => {
                    if (!p.DummyPlayer) {
                        p.VisableMonsters.AddRange(monsterCards);
                    }
                });
            }
        }
        //public int DrawWound() {
        //    int woundId = WoundDeck[D.G.Board.WoundIndex];
        //    D.G.Board.WoundIndex++;
        //    return woundId;
        //}
        public int DrawMonster(MonsterType_Enum monsterType) {
            int monsterId;
            switch (monsterType) {
                case MonsterType_Enum.Green: {
                    monsterId = MonsterGreenDeck[D.G.Board.GreenIndex];
                    D.G.Board.GreenIndex++;
                    break;
                }
                case MonsterType_Enum.Grey: {
                    monsterId = MonsterGreyDeck[D.G.Board.GreyIndex];
                    D.G.Board.GreyIndex++;
                    break;
                }
                case MonsterType_Enum.Violet: {
                    monsterId = MonsterVioletDeck[D.G.Board.VioletIndex];
                    D.G.Board.VioletIndex++;
                    break;
                }
                case MonsterType_Enum.Brown: {
                    monsterId = MonsterBrownDeck[D.G.Board.BrownIndex];
                    D.G.Board.BrownIndex++;
                    break;
                }
                case MonsterType_Enum.White: {
                    monsterId = MonsterWhiteDeck[D.G.Board.WhiteIndex];
                    D.G.Board.WhiteIndex++;
                    break;
                }
                case MonsterType_Enum.Red: {
                    monsterId = MonsterRedDeck[D.G.Board.RedIndex];
                    D.G.Board.RedIndex++;
                    break;
                }
                case MonsterType_Enum.Yellow: {
                    monsterId = RuinDeck[D.G.Board.RuinIndex];
                    D.G.Board.RuinIndex++;
                    break;
                }
                default: {
                    monsterId = 0;
                    break;
                }
            }
            return monsterId;
        }

        private List<MonsterType_Enum> getCityDefenders(Image_Enum city, int difficulty) {
            List<MonsterType_Enum> l = new List<MonsterType_Enum>();
            switch (city) {
                case Image_Enum.SH_City_Blue: {
                    switch (difficulty) {
                        case 1: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Violet };
                            break;
                        }
                        case 2: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Violet, MonsterType_Enum.Violet };
                            break;
                        }
                        case 3: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Violet, MonsterType_Enum.White };
                            break;
                        }
                        case 4: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Violet, MonsterType_Enum.White };
                            break;
                        }
                        case 5: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Violet, MonsterType_Enum.Violet, MonsterType_Enum.White };
                            break;
                        }
                        case 6: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Violet, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 7: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Violet, MonsterType_Enum.Violet, MonsterType_Enum.White };
                            break;
                        }
                        case 8: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Violet, MonsterType_Enum.Violet, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 9: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Violet, MonsterType_Enum.White, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 10: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Violet, MonsterType_Enum.Violet, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 11: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Violet, MonsterType_Enum.Violet, MonsterType_Enum.White, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                    }
                    break;
                }
                case Image_Enum.SH_City_Green: {
                    switch (difficulty) {
                        case 1: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Brown };
                            break;
                        }
                        case 2: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Brown };
                            break;
                        }
                        case 3: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Grey, MonsterType_Enum.Brown };
                            break;
                        }
                        case 4: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Brown, MonsterType_Enum.White };
                            break;
                        }
                        case 5: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Brown, MonsterType_Enum.White };
                            break;
                        }
                        case 6: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Grey, MonsterType_Enum.Brown, MonsterType_Enum.White };
                            break;
                        }
                        case 7: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Brown, MonsterType_Enum.Brown, MonsterType_Enum.White };
                            break;
                        }
                        case 8: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Brown, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 9: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Brown, MonsterType_Enum.Brown, MonsterType_Enum.Brown, MonsterType_Enum.White };
                            break;
                        }
                        case 10: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Brown, MonsterType_Enum.Brown, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 11: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Brown, MonsterType_Enum.White, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                    }
                    break;
                }
                case Image_Enum.SH_City_White: {
                    switch (difficulty) {
                        case 1: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.White };
                            break;
                        }
                        case 2: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.White };
                            break;
                        }
                        case 3: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 4: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Grey, MonsterType_Enum.White };
                            break;
                        }
                        case 5: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 6: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Grey, MonsterType_Enum.Grey, MonsterType_Enum.White };
                            break;
                        }
                        case 7: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Grey, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 8: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.White, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 9: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Grey, MonsterType_Enum.Grey, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 10: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.Grey, MonsterType_Enum.White, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 11: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Grey, MonsterType_Enum.White, MonsterType_Enum.White, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                    }
                    break;
                }
                case Image_Enum.SH_City_Red: {
                    switch (difficulty) {
                        case 1: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.White };
                            break;
                        }
                        case 2: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Violet };
                            break;
                        }
                        case 3: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.White };
                            break;
                        }
                        case 4: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Violet, MonsterType_Enum.Violet };
                            break;
                        }
                        case 5: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Violet, MonsterType_Enum.White };
                            break;
                        }
                        case 6: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Brown, MonsterType_Enum.Violet, MonsterType_Enum.Violet };
                            break;
                        }
                        case 7: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Violet, MonsterType_Enum.Violet, MonsterType_Enum.White };
                            break;
                        }
                        case 8: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Violet, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 9: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Brown, MonsterType_Enum.Violet, MonsterType_Enum.Violet, MonsterType_Enum.White };
                            break;
                        }
                        case 10: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.Brown, MonsterType_Enum.Violet, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                        case 11: {
                            l = new List<MonsterType_Enum>() { MonsterType_Enum.Brown, MonsterType_Enum.White, MonsterType_Enum.Violet, MonsterType_Enum.White, MonsterType_Enum.White };
                            break;
                        }
                    }
                    break;
                }
            }
            return l;
        }

        public int getTerrainMovementCost(Image_Enum terrainHex) {
            int movement = -1;
            switch (terrainHex) {
                case Image_Enum.TH_Plains: {
                    movement = 2;
                    break;
                }
                case Image_Enum.TH_Desert: {
                    if (isDay) {
                        movement = 5;
                    } else {
                        movement = 3;
                    }
                    break;
                }
                case Image_Enum.TH_Hill: {
                    movement = 3;
                    break;
                }
                case Image_Enum.TH_Swamp: {
                    movement = 5;
                    break;
                }
                case Image_Enum.TH_Wasteland: {
                    movement = 4;
                    break;
                }
                case Image_Enum.TH_Forest: {
                    if (isDay) {
                        movement = 3;
                    } else {
                        movement = 5;
                    }
                    break;
                }
                case Image_Enum.TH_Lake: {
                    movement = -1;
                    break;
                }
                case Image_Enum.TH_Mountain: {
                    movement = -1;
                    break;
                }
            }
            return movement;
        }

        #region Create Scenario

        public void buildMapDeck(bool isWedge, bool easyStart, int basic, int core, int city) {
            MapDeck = new List<MapHexId_Enum>();

            List<MapHexId_Enum> basicIds = new List<MapHexId_Enum>();
            List<MapHexId_Enum> coreIds = new List<MapHexId_Enum>();
            List<MapHexId_Enum> citeIds = new List<MapHexId_Enum>();
            foreach (MapHexId_Enum id in Enum.GetValues(typeof(MapHexId_Enum))) {
                if (id >= MapHexId_Enum.City_Green) {
                    citeIds.Add(id);
                } else if (id >= MapHexId_Enum.Core_01) {
                    coreIds.Add(id);
                } else if (id >= MapHexId_Enum.Basic_01) {
                    basicIds.Add(id);
                }
            }

            int b = 0;
            MapDeck.Add(isWedge ? MapHexId_Enum.Start_A : MapHexId_Enum.Start_B);
            if (easyStart) {
                basicIds.Remove(MapHexId_Enum.Basic_01);
                MapDeck.Add(MapHexId_Enum.Basic_01);
                MapDeck.Add(MapHexId_Enum.Basic_02);
                basicIds.Remove(MapHexId_Enum.Basic_02);
                b += 2;
                if (!isWedge) {
                    MapDeck.Add(MapHexId_Enum.Basic_03);
                    basicIds.Remove(MapHexId_Enum.Basic_03);
                    b++;
                }
            }
            for (; b < basic; b++) {
                int rand = UnityEngine.Random.Range(0, basicIds.Count);
                MapDeck.Add(basicIds[rand]);
                basicIds.RemoveAt(rand);
            }
            List<MapHexId_Enum> advancedIds = new List<MapHexId_Enum>();
            for (int i = 0; i < city; i++) {
                int rand = UnityEngine.Random.Range(0, citeIds.Count);
                advancedIds.Add(citeIds[rand]);
                citeIds.RemoveAt(rand);
            }
            for (int i = 0; i < core; i++) {
                int rand = UnityEngine.Random.Range(0, coreIds.Count);
                advancedIds.Add(coreIds[rand]);
                coreIds.RemoveAt(rand);
            }
            int total = advancedIds.Count;
            for (int i = 0; i < total; i++) {
                int rand = UnityEngine.Random.Range(0, advancedIds.Count);
                MapDeck.Add(advancedIds[rand]);
                advancedIds.RemoveAt(rand);
            }
            total = basicIds.Count;
            for (int i = 0; i < total; i++) {
                int rand = UnityEngine.Random.Range(0, basicIds.Count);
                MapDeck.Add(basicIds[rand]);
                basicIds.RemoveAt(rand);
            }
            total = coreIds.Count;
            for (int i = 0; i < total; i++) {
                int rand = UnityEngine.Random.Range(0, coreIds.Count);
                MapDeck.Add(coreIds[rand]);
                coreIds.RemoveAt(rand);
            }
        }
        public void buildStartMap() {
            D.Board.CurrentMap = new List<MapHexId_Enum>();
            for (int i = 0; i < MaxBoardSize; i++) {
                D.Board.CurrentMap.Add(MapHexId_Enum.Invalid);
            }
            D.Board.MapDeckIndex = 0;
            int row_1 = D.GLD.GameMapLayout == GameMapLayout_Enum.Wedge ? 3 : 4;
            for (int i = 0; i < row_1; i++) {
                DrawGameHex(i);
            }
        }
        public void rebuildCurrentMap(PlayerData pd) {
            int totalTilesLeft = MapDeck.Count - D.Board.MapDeckIndex;
            int gameTilesLeft = 1 + (D.GLD.BasicTiles + D.GLD.CoreTiles + D.GLD.CityTiles) - D.Board.MapDeckIndex;
            bool nextTileBasic = D.Board.MapDeckIndex <= D.GLD.BasicTiles;
            for (int i = 0; i < pd.Board.PlayerMap.Count; i++) {
                if (pd.Board.PlayerMap[i] <= MapHexId_Enum.Core_Back) {
                    if (recalculateHex(i, pd.Board.PlayerMap, totalTilesLeft, gameTilesLeft, nextTileBasic)) {
                        pd.Board.PlayerMap[i] = MapDeck[D.Board.MapDeckIndex] < MapHexId_Enum.Core_01 ? MapHexId_Enum.Basic_Back : MapHexId_Enum.Core_Back;
                    } else {
                        pd.Board.PlayerMap[i] = MapHexId_Enum.Invalid;
                    }
                }
            }
        }
        public bool recalculateHex(int index, List<MapHexId_Enum> currentMap, int totalTilesLeft, int gameTilesLeft, bool nextTileBasic) {
            if (totalTilesLeft > 0) {
                List<int> adj = AdjBoard[index].FindAll(i => currentMap.Count > i && currentMap[i] > MapHexId_Enum.Core_Back);
                if (gameTilesLeft > 0) {
                    if (adj.Count > 1) {
                        return true;
                    } else if (adj.Count > 0 && nextTileBasic) {
                        foreach (int j in adj) {
                            List<int> adj2 = AdjBoard[j];
                            int totalAdj2 = adj2.FindAll(i => currentMap.Count > i && currentMap[i] > MapHexId_Enum.Core_Back).Count;
                            if (totalAdj2 > 1) {
                                return true;
                            }
                        }
                    }
                } else if (adj.Count > 2) {
                    return true;
                }
            }
            return false;
        }
        #endregion


        #region NewRound Setup
        public void BuildUnitOfferingDeck(Data g, PlayerData pd) {
            pd.Board.UnitOffering.Clear();
            int totalCards = g.Players.Count + 2 - (g.GameData.DummyPlayer ? 1 : 0);
            for (int i = 0; i < totalCards; i++) {
                if (i % 2 == 0) {
                    pd.Board.UnitOffering.Add(UnitRegularDeck[pd.Board.UnitRegularIndex]);
                    pd.Board.UnitRegularIndex++;
                } else {
                    if (D.hasCoreBeenDrawn) {
                        pd.Board.UnitOffering.Add(UnitEliteDeck[pd.Board.UnitRegularIndex]);
                        pd.Board.UnitEliteIndex++;
                    } else {
                        pd.Board.UnitOffering.Add(UnitRegularDeck[pd.Board.UnitRegularIndex]);
                        pd.Board.UnitRegularIndex++;
                    }
                }
            }
            for (int i = 0; i < pd.Board.MonasteryCount; i++) {
                if (pd.Board.AdvancedIndex < AdvancedDeck.Count) {
                    pd.Board.UnitOffering.Add(AdvancedDeck[pd.Board.AdvancedIndex]);
                    pd.Board.AdvancedIndex++;
                }
            }
        }

        public void BuildManaOfferingDeck(Data g, PlayerData pd) {
            pd.ManaPoolFull.Clear();
            int totalDie = g.Players.Count + 2 - (g.GameData.DummyPlayer ? 1 : 0);
            for (int i = 0; i < totalDie / 2; i++) {
                Crystal_Enum crystal_Enum = (Crystal_Enum)UnityEngine.Random.Range(1, 7);
                pd.ManaPoolFull.Add(new ManaPoolData(crystal_Enum));
            }
            for (int i = 0; i < totalDie - (totalDie / 2); i++) {
                Crystal_Enum crystal_Enum = (Crystal_Enum)UnityEngine.Random.Range(2, 6);
                pd.ManaPoolFull.Add(new ManaPoolData(crystal_Enum));
            }
        }

        public void BuildSpellOfferingDeck(PlayerData pd) {
            //  Lowest = 0 <-- Bottom
            if (pd.Board.SpellOffering.Count > 0) {
                pd.Board.SpellOffering.RemoveAt(0);
            }
            while (pd.Board.SpellOffering.Count < 3 && pd.Board.SpellIndex < SpellDeck.Count) {
                pd.Board.SpellOffering.Add(SpellDeck[pd.Board.SpellIndex]);
                pd.Board.SpellIndex++;
            }
        }

        public void BuildAdvancedOfferingDeck(PlayerData pd) {
            //Lowest = 0 < --Bottom
            if (pd.Board.AdvancedOffering.Count > 0) {
                if (D.GLD.DummyPlayer) {
                    int cardid = pd.Board.AdvancedOffering[0];
                    PlayerData dummy = D.GetPlayerByKey(-999);
                    Crystal_Enum crystal = BasicUtil.Convert_ColorIdToCrystalId(D.Cards[cardid].CardColor);
                    dummy.Crystal.AddCrystal(crystal);
                    dummy.Deck.Deck.Add(cardid);
                    D.C.LogMessageDummy("+1 " + crystal + " Crystal");
                }
                pd.Board.AdvancedOffering.RemoveAt(0);
            }
            while (pd.Board.AdvancedOffering.Count < 3 && pd.Board.AdvancedIndex < AdvancedDeck.Count) {
                pd.Board.AdvancedOffering.Add(AdvancedDeck[pd.Board.AdvancedIndex]);
                pd.Board.AdvancedIndex++;
            }
        }

        #endregion

        public void Check() { }
    }
}
