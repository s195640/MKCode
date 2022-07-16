using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using cna.poo;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace cna {
    public static class BasicUtil {

        public static byte[] toByteArray<T>(T obj) {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream()) {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T byteArrayToData<T>(byte[] data) {
            using (var memStream = new MemoryStream()) {
                var binForm = new BinaryFormatter();
                memStream.Write(data, 0, data.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = (T)binForm.Deserialize(memStream);
                return obj;
            }
        }

        public static bool IsAdjacent(V2IntVO pt1, V2IntVO pt2) {
            return GetAdjacentPoints(pt1).Contains(pt2);
        }

        public static int Distance(V2IntVO start, V2IntVO dest) {
            int d1 = Math.Abs(dest.Y - start.Y);
            int d2 = Math.Abs((int)Math.Ceiling((float)dest.Y / -2) + dest.X - (int)Math.Ceiling((float)start.Y / -2) - start.X);
            int d3 = Math.Abs(-dest.Y - (int)Math.Ceiling((float)dest.Y / -2) - dest.X + start.Y + (int)Math.Ceiling((float)start.Y / -2) + start.X);
            int distance = Math.Max(Math.Max(d1, d2), d3);
            return distance;
        }

        public static List<V2IntVO> GetAdjacentPoints(V2IntVO pt) {
            List<V2IntVO> pts = new List<V2IntVO>();
            bool left = pt.Y % 2 == 0;
            //  TOP     [{pt.x-1, pt.x, pt.x+1}]
            if (left) pts.Add(new V2IntVO(pt.X + -1, pt.Y + 1));
            pts.Add(new V2IntVO(pt.X, pt.Y + 1));
            if (!left) pts.Add(new V2IntVO(pt.X + +1, pt.Y + 1));
            //  CENTER  [{pt.x-1, pt.x, pt.x+1}]
            pts.Add(new V2IntVO(pt.X + -1, pt.Y));
            pts.Add(new V2IntVO(pt.X + +1, pt.Y));
            //  BOTTOM  [{pt.x-1, pt.x, pt.x+1}]
            if (left) pts.Add(new V2IntVO(pt.X + -1, pt.Y - 1));
            pts.Add(new V2IntVO(pt.X, pt.Y - 1));
            if (!left) pts.Add(new V2IntVO(pt.X + +1, pt.Y - 1));
            return pts;
        }

        public static void ShuffleDeck<T>(this IList<T> deck) {
            int n = deck.Count;
            while (n > 1) {
                n--;
                int k = RandomRange(0, n + 1);
                T value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }
        }

        public static T Random<T>(this IList<T> deck) {
            return deck[RandomRange(0, deck.Count)];
        }

        public static T DrawCard<T>(this IList<T> deck) {
            if (deck.Count > 0) {
                T _card = deck[0];
                deck.RemoveAt(0);
                return _card;
            }
            return default(T);
        }


        public static Image_Enum GetTilemapId(V2IntVO gameGridPos, Tilemap map) {
            Image_Enum id = Image_Enum.NA;
            CNATile tile = (CNATile)map.GetTile(gameGridPos.Vector3Int);
            if (tile != null) {
                id = tile.TileId;
            }
            return id;
        }

        public static int GetPlayerTotalFame(V2IntVO fame, int levelMod) {
            return _getPlayerTotalFame(fame.X, fame.X + fame.Y, levelMod);
        }

        private static int _getPlayerTotalFame(int o, int n, int mod) {
            int currentLevel = GetPlayerLevel(o);
            int newLevel = GetPlayerLevel(n);
            if (currentLevel == newLevel) {
                return n;
            } else {
                return _getPlayerTotalFame(n, n + (mod * (newLevel - currentLevel)), mod);
            }
        }

        public static int GetPlayerLevel(int fame) {
            int lvl = 0;
            if (fame < 3)
                lvl = 1;
            else if (fame < 8)
                lvl = 2;
            else if (fame < 15)
                lvl = 3;
            else if (fame < 24)
                lvl = 4;
            else if (fame < 35)
                lvl = 5;
            else if (fame < 48)
                lvl = 6;
            else if (fame < 63)
                lvl = 7;
            else if (fame < 80)
                lvl = 8;
            else if (fame < 99)
                lvl = 9;
            else if (fame < 120)
                lvl = 10;
            else
                lvl = 11;
            return lvl;
        }
        public static int GetFameForLevel(int level) {
            int fame = 0;
            if (level == 1) fame = 0;
            else if (level == 2) fame = 3;
            else if (level == 3) fame = 8;
            else if (level == 4) fame = 15;
            else if (level == 5) fame = 24;
            else if (level == 6) fame = 35;
            else if (level == 7) fame = 48;
            else if (level == 8) fame = 63;
            else if (level == 9) fame = 80;
            else if (level == 10) fame = 99;
            else if (level == 11) fame = 120;
            return fame;
        }

        public static int GetRepForLevel(int level) {
            switch (level) {
                case -7: return -99;
                case -6: return -5;
                case -5: return -3;
                case -4: return -2;
                case -3: return -1;
                case -2: return -1;
                case -1: return 0;
                case 0: return 0;
                case 1: return 0;
                case 2: return 1;
                case 3: return 1;
                case 4: return 2;
                case 5: return 2;
                case 6: return 3;
                case 7: return 5;
            }
            return 0;
        }

        public static int GetArmorFromFame(int fame) {
            if (fame < 8) {
                return 2;
            } else if (fame < 48) {
                return 3;
            } else {
                return 4;
            }
        }

        public static int GetHandLimitFromFame(int fame) {
            if (fame < 24) {
                return 5;
            } else if (fame < 80) {
                return 6;
            } else {
                return 7;
            }
        }

        public static int GetUnitHandLimitFromFame(int fame) {
            if (fame < 8) {
                return 1;
            } else if (fame < 24) {
                return 2;
            } else if (fame < 48) {
                return 3;
            } else if (fame < 80) {
                return 4;
            } else {
                return 5;
            }
        }

        public static Image_Enum Convert_CrystalToCrystalImageId(Crystal_Enum index) {
            switch (index) {
                case Crystal_Enum.Black: { return Image_Enum.I_crystal_black; }
                case Crystal_Enum.Gold: { return Image_Enum.I_crystal_yellow; }
                case Crystal_Enum.Blue: { return Image_Enum.I_crystal_blue; }
                case Crystal_Enum.Red: { return Image_Enum.I_crystal_red; }
                case Crystal_Enum.Green: { return Image_Enum.I_crystal_green; }
                case Crystal_Enum.White: { return Image_Enum.I_crystal_white; }
            }
            return Image_Enum.NA;
        }
        public static Image_Enum Convert_CrystalToManaImageId(Crystal_Enum index) {
            switch (index) {
                case Crystal_Enum.Black: { return Image_Enum.I_mana_black; }
                case Crystal_Enum.Gold: { return Image_Enum.I_mana_gold; }
                case Crystal_Enum.Blue: { return Image_Enum.I_mana_blue; }
                case Crystal_Enum.Red: { return Image_Enum.I_mana_red; }
                case Crystal_Enum.Green: { return Image_Enum.I_mana_green; }
                case Crystal_Enum.White: { return Image_Enum.I_mana_white; }
            }
            return Image_Enum.NA;
        }
        public static Image_Enum Convert_CrystalToManaDieImageId(Crystal_Enum index) {
            switch (index) {
                case Crystal_Enum.Black: { return Image_Enum.I_die_black; }
                case Crystal_Enum.Gold: { return Image_Enum.I_die_gold; }
                case Crystal_Enum.Blue: { return Image_Enum.I_die_blue; }
                case Crystal_Enum.Red: { return Image_Enum.I_die_red; }
                case Crystal_Enum.Green: { return Image_Enum.I_die_green; }
                case Crystal_Enum.White: { return Image_Enum.I_die_white; }
            }
            return Image_Enum.NA;
        }

        public static Crystal_Enum Convert_ManaDieToCrystalId(Image_Enum index) {
            switch (index) {
                case Image_Enum.I_die_black: { return Crystal_Enum.Black; }
                case Image_Enum.I_die_gold: { return Crystal_Enum.Gold; }
                case Image_Enum.I_die_blue: { return Crystal_Enum.Blue; }
                case Image_Enum.I_die_red: { return Crystal_Enum.Red; }
                case Image_Enum.I_die_green: { return Crystal_Enum.Green; }
                case Image_Enum.I_die_white: { return Crystal_Enum.White; }
            }
            return Crystal_Enum.NA;
        }

        public static Crystal_Enum Convert_ColorIdToCrystalId(CardColor_Enum index) {
            switch (index) {
                case CardColor_Enum.Blue: { return Crystal_Enum.Blue; }
                case CardColor_Enum.Green: { return Crystal_Enum.Green; }
                case CardColor_Enum.Red: { return Crystal_Enum.Red; }
                case CardColor_Enum.White: { return Crystal_Enum.White; }
            }
            return Crystal_Enum.NA;
        }


        public static Image_Enum GetStructureAtLoc(V2IntVO loc) {
            int mapIndex = D.Scenario.ConvertWorldToIndex(loc);
            int locIndex = D.Scenario.ConvertWorldToLocIndex(loc);
            MapHexId_Enum mapHexId_Enum = D.G.Board.CurrentMap[mapIndex];
            MapHexVO mapHex = D.HexMap[mapHexId_Enum];
            return mapHex.StructureList[locIndex];
        }

        public static void ClearAllMovementGameEffects(PlayerData pd) {
            pd.GameEffects.Remove(GameEffect_Enum.SH_CrystalMines_Blue);
            pd.GameEffects.Remove(GameEffect_Enum.SH_CrystalMines_Red);
            pd.GameEffects.Remove(GameEffect_Enum.SH_CrystalMines_Green);
            pd.GameEffects.Remove(GameEffect_Enum.SH_CrystalMines_White);
            pd.GameEffects.Remove(GameEffect_Enum.SH_MagicGlade);
            pd.GameEffects.Remove(GameEffect_Enum.SH_MageTower);
            pd.GameEffects.Remove(GameEffect_Enum.SH_Keep);
            pd.GameEffects.Remove(GameEffect_Enum.SH_City_Red);
            pd.GameEffects.Remove(GameEffect_Enum.SH_City_Green);
            pd.GameEffects.Remove(GameEffect_Enum.SH_City_White);
            pd.GameEffects.Remove(GameEffect_Enum.SH_City_Blue);
            pd.GameEffects.Remove(GameEffect_Enum.SH_Keep_Own);
            pd.GameEffects.Remove(GameEffect_Enum.SH_City_Blue_Own);
            pd.GameEffects.Remove(GameEffect_Enum.SH_City_Red_Own);
            pd.GameEffects.Remove(GameEffect_Enum.SH_City_Green_Own);
            pd.GameEffects.Remove(GameEffect_Enum.SH_City_White_Own);
            pd.GameEffects.Remove(GameEffect_Enum.SH_City_Own);
        }
        public static void AddMovementGameEffects(PlayerData pd) {
            ClearAllMovementGameEffects(pd);
            Image_Enum structure = GetStructureAtLoc(pd.CurrentGridLoc);
            List<V2IntVO> adj = GetAdjacentPoints(pd.CurrentGridLoc);
            adj.ForEach(pos => {
                if (!pd.Board.MonsterData.ContainsKey(pos)) {
                    if (pd.Shields.Contains(pos)) {
                        Image_Enum structure = GetStructureAtLoc(pos);
                        switch (structure) {
                            case Image_Enum.SH_Keep: { pd.AddGameEffect(GameEffect_Enum.SH_Keep_Own); break; }
                            case Image_Enum.SH_City_Blue:
                            case Image_Enum.SH_City_Red:
                            case Image_Enum.SH_City_Green:
                            case Image_Enum.SH_City_White: { pd.AddGameEffect(GameEffect_Enum.SH_City_Own); break; }
                        }
                    }
                }
            });
            switch (structure) {
                case Image_Enum.SH_CrystalMines_Blue: { pd.AddGameEffect(GameEffect_Enum.SH_CrystalMines_Blue); break; }
                case Image_Enum.SH_CrystalMines_Red: { pd.AddGameEffect(GameEffect_Enum.SH_CrystalMines_Red); break; }
                case Image_Enum.SH_CrystalMines_Green: { pd.AddGameEffect(GameEffect_Enum.SH_CrystalMines_Green); break; }
                case Image_Enum.SH_CrystalMines_White: { pd.AddGameEffect(GameEffect_Enum.SH_CrystalMines_White); break; }
                case Image_Enum.SH_MagicGlade: { pd.AddGameEffect(GameEffect_Enum.SH_MagicGlade); break; }
                case Image_Enum.SH_Keep: {
                    if (pd.Shields.Contains(pd.CurrentGridLoc)) { pd.AddGameEffect(GameEffect_Enum.SH_Keep_Own); }
                    break;
                }
                case Image_Enum.SH_City_Blue: {
                    if (pd.Shields.Contains(pd.CurrentGridLoc)) { pd.AddGameEffect(GameEffect_Enum.SH_City_Blue_Own); }
                    break;
                }
                case Image_Enum.SH_City_Red: {
                    if (pd.Shields.Contains(pd.CurrentGridLoc)) { pd.AddGameEffect(GameEffect_Enum.SH_City_Red_Own); }
                    break;
                }
                case Image_Enum.SH_City_Green: {
                    if (pd.Shields.Contains(pd.CurrentGridLoc)) { pd.AddGameEffect(GameEffect_Enum.SH_City_Green_Own); }
                    break;
                }
                case Image_Enum.SH_City_White: {
                    if (pd.Shields.Contains(pd.CurrentGridLoc)) { pd.AddGameEffect(GameEffect_Enum.SH_City_White_Own); }
                    break;
                }
            }
        }

        public static void AddBattleGameEffects(PlayerData pd, List<MonsterMetaData> monsters) {
            monsters.ForEach(m => {
                GameEffect_Enum be = GameEffect_Enum.NA;
                switch (m.Structure) {
                    case Image_Enum.SH_MageTower: {
                        be = GameEffect_Enum.SH_MageTower;
                        break;
                    }
                    case Image_Enum.SH_Keep: {
                        be = GameEffect_Enum.SH_Keep;
                        break;
                    }
                    case Image_Enum.SH_City_Blue: {
                        be = GameEffect_Enum.SH_City_Blue;
                        break;
                    }
                    case Image_Enum.SH_City_Green: {
                        be = GameEffect_Enum.SH_City_Green;
                        break;
                    }
                    case Image_Enum.SH_City_Red: {
                        be = GameEffect_Enum.SH_City_Red;
                        break;
                    }
                    case Image_Enum.SH_City_White: {
                        be = GameEffect_Enum.SH_City_White;
                        break;
                    }
                }
                if (be != GameEffect_Enum.NA) {
                    pd.AddGameEffect(be, m.Uniqueid);
                }
            });
        }


        public static void UpdateMovementGameEffects(PlayerData pd) {
            Image_Enum AvatarShieldId = D.AvatarMetaDataMap[pd.Avatar].AvatarShieldId;
            Image_Enum Structure = GetStructureAtLoc(pd.CurrentGridLoc);
            List<V2IntVO> adj = GetAdjacentPoints(pd.CurrentGridLoc);
            //  Check to reveal mage/keep
            bool amuletOfSun = pd.GameEffects.ContainsKey(GameEffect_Enum.CT_AmuletOfTheSun);
            if (D.Scenario.isDay || amuletOfSun) {
                adj.ForEach(pos => {
                    if (pd.Board.MonsterData.ContainsKey(pos)) {
                        List<int> monsters = pd.Board.MonsterData[pos].Values;
                        if (monsters.Count > 0) {
                            Image_Enum structure = GetStructureAtLoc(pos);
                            if (structure == Image_Enum.SH_Keep || structure == Image_Enum.SH_MageTower) {
                                monsters.ForEach(m => {
                                    if (!pd.VisableMonsters.Contains(m)) {
                                        pd.VisableMonsters.Add(m);
                                    }
                                });
                            }
                        }
                    }
                });
            }
            //  Check to reveal ruins
            if (!D.Scenario.isDay && Structure == Image_Enum.SH_AncientRuins) {
                if (pd.Board.MonsterData.ContainsKey(pd.CurrentGridLoc)) {
                    List<int> monsters = pd.Board.MonsterData[pd.CurrentGridLoc].Values;
                    if (monsters.Count > 0) {
                        monsters.ForEach(m => {
                            if (D.Cards[m].CardType != CardType_Enum.Monster) {
                                if (!pd.VisableMonsters.Contains(m)) {
                                    pd.VisableMonsters.Add(m);
                                }
                            }
                        });
                    }
                }
            }
            AddMovementGameEffects(pd);
            ////  Mines & Glade
            //pd.GameEffects.Remove(GameEffect_Enum.SH_CrystalMines_Blue);
            //pd.GameEffects.Remove(GameEffect_Enum.SH_CrystalMines_Red);
            //pd.GameEffects.Remove(GameEffect_Enum.SH_CrystalMines_Green);
            //pd.GameEffects.Remove(GameEffect_Enum.SH_CrystalMines_White);
            //pd.GameEffects.Remove(GameEffect_Enum.SH_MagicGlade);
            //pd.GameEffects.Remove(GameEffect_Enum.SH_Keep_Own);
            //pd.GameEffects.Remove(GameEffect_Enum.SH_City_Blue_Own);
            //pd.GameEffects.Remove(GameEffect_Enum.SH_City_Red_Own);
            //pd.GameEffects.Remove(GameEffect_Enum.SH_City_Green_Own);
            //pd.GameEffects.Remove(GameEffect_Enum.SH_City_White_Own);
            //pd.GameEffects.Remove(GameEffect_Enum.SH_City_Own);
            //adj.ForEach(pos => {
            //    if (!pd.Board.MonsterData.ContainsKey(pos)) {
            //        if (pd.Shields.Contains(pos)) {
            //            Image_Enum structure = GetStructureAtLoc(pos);
            //            switch (structure) {
            //                case Image_Enum.SH_Keep: { pd.AddGameEffect(GameEffect_Enum.SH_Keep_Own); break; }
            //                case Image_Enum.SH_City_Blue:
            //                case Image_Enum.SH_City_Red:
            //                case Image_Enum.SH_City_Green:
            //                case Image_Enum.SH_City_White: { pd.AddGameEffect(GameEffect_Enum.SH_City_Own); break; }
            //            }
            //        }
            //    }
            //});

            //switch (Structure) {
            //    case Image_Enum.SH_CrystalMines_Blue: { pd.AddGameEffect(GameEffect_Enum.SH_CrystalMines_Blue); break; }
            //    case Image_Enum.SH_CrystalMines_Red: { pd.AddGameEffect(GameEffect_Enum.SH_CrystalMines_Red); break; }
            //    case Image_Enum.SH_CrystalMines_Green: { pd.AddGameEffect(GameEffect_Enum.SH_CrystalMines_Green); break; }
            //    case Image_Enum.SH_CrystalMines_White: { pd.AddGameEffect(GameEffect_Enum.SH_CrystalMines_White); break; }
            //    case Image_Enum.SH_MagicGlade: { pd.AddGameEffect(GameEffect_Enum.SH_MagicGlade); break; }
            //    case Image_Enum.SH_Keep: {
            //        if (pd.Shields.Contains(pd.CurrentGridLoc)) { pd.AddGameEffect(GameEffect_Enum.SH_Keep_Own); }
            //        break;
            //    }
            //    case Image_Enum.SH_City_Blue: {
            //        if (pd.Shields.Contains(pd.CurrentGridLoc)) { pd.AddGameEffect(GameEffect_Enum.SH_City_Blue_Own); }
            //        break;
            //    }
            //    case Image_Enum.SH_City_Red: {
            //        if (pd.Shields.Contains(pd.CurrentGridLoc)) { pd.AddGameEffect(GameEffect_Enum.SH_City_Red_Own); }
            //        break;
            //    }
            //    case Image_Enum.SH_City_Green: {
            //        if (pd.Shields.Contains(pd.CurrentGridLoc)) { pd.AddGameEffect(GameEffect_Enum.SH_City_Green_Own); }
            //        break;
            //    }
            //    case Image_Enum.SH_City_White: {
            //        if (pd.Shields.Contains(pd.CurrentGridLoc)) { pd.AddGameEffect(GameEffect_Enum.SH_City_White_Own); }
            //        break;
            //    }
            //}
            CalculatePlayerHandBonus(pd);
        }
        public static void CalculatePlayerHandBonus(PlayerData pd) {
            Image_Enum AvatarShieldId = D.AvatarMetaDataMap[pd.Avatar].AvatarShieldId;
            int keepBonus = KeepBonus(pd);
            int cityBonus = CityBonus(pd);
            int totalBonus = Math.Max(keepBonus, cityBonus);
            if (pd.GameEffects.ContainsKey(GameEffect_Enum.CS_Meditation)) {
                totalBonus += 2;
            }
            if (pd.GameEffects.ContainsKey(GameEffect_Enum.CS_Trance)) {
                totalBonus += 4;
            }
            pd.Deck.HandSize.Y = totalBonus;
        }

        private static int KeepBonus(PlayerData pd) {
            int keepBonus = 0;
            if (pd.GameEffects.ContainsKey(GameEffect_Enum.SH_Keep_Own)) {
                pd.Shields.ForEach(pos => {
                    Image_Enum structure = GetStructureAtLoc(pos);
                    if (structure == Image_Enum.SH_Keep) {
                        keepBonus++;
                    }
                });
            }
            return keepBonus;
        }

        private static int CityBonus(PlayerData pd) {
            //  due to everyone playing at once, tie is considered the leader
            int cityBonus = 0;
            if (pd.GameEffects.ContainsKeyAny(GameEffect_Enum.SH_City_Own, GameEffect_Enum.SH_City_Blue_Own, GameEffect_Enum.SH_City_Red_Own, GameEffect_Enum.SH_City_Green_Own, GameEffect_Enum.SH_City_White_Own)) {
                V2IntVO cityLoc = V2IntVO.zero;
                if (pd.GameEffects.ContainsKey(GameEffect_Enum.SH_Keep_Own)) {
                    List<V2IntVO> adj = GetAdjacentPoints(pd.CurrentGridLoc);
                    adj.ForEach(pos => {
                        Image_Enum structure = GetStructureAtLoc(pos);
                        if (structure == Image_Enum.SH_City_Blue || structure == Image_Enum.SH_City_Red || structure == Image_Enum.SH_City_Green || structure == Image_Enum.SH_City_White) {
                            cityLoc = pos;
                        }
                    });
                } else {
                    cityLoc = pd.CurrentGridLoc;
                }
                int currentPlayerTotal = pd.Shields.FindAll(pos => pos.Equals(cityLoc)).Count;
                if (currentPlayerTotal > 0) {
                    cityBonus = 1;
                    if (D.G.Players.Find(p => p.Shields.FindAll(pos => pos.Equals(cityLoc)).Count > currentPlayerTotal) == null) {
                        cityBonus = 2;
                    }
                }
            }
            return cityBonus;
        }

        //public static int Draw(List<int> deck, ref int index) {
        //    int card = 0;
        //    if (index < deck.Count) {
        //        card = deck[index];
        //        index++;
        //    }
        //    return card;
        //}

        public static List<Image_Enum> getAllShieldsAtPos(Data gd, V2IntVO pos) {
            List<Image_Enum> shields = new List<Image_Enum>();
            gd.Players.ForEach(pd => {
                int totalShields = pd.Shields.FindAll(z => z.Equals(pos)).Count;
                if (totalShields > 0) {
                    Image_Enum shieldImage = D.AvatarMetaDataMap[pd.Avatar].AvatarShieldId;
                    for (int i = 0; i < totalShields; i++) {
                        shields.Add(shieldImage);
                    }
                }
            });
            return shields;
        }

        public static Dictionary<V2IntVO, List<Image_Enum>> getAllShields(Data gd) {
            Dictionary<V2IntVO, List<Image_Enum>> shields = new Dictionary<V2IntVO, List<Image_Enum>>();
            gd.Players.ForEach(pd => {
                Image_Enum shieldImage = D.AvatarMetaDataMap[pd.Avatar].AvatarShieldId;
                pd.Shields.ForEach(pos => {
                    if (!shields.ContainsKey(pos)) {
                        shields.Add(pos, new List<Image_Enum>());
                    }
                    shields[pos].Add(shieldImage);
                });
            });
            return shields;
        }

        public static int RandomRange(int a, int b) {
            return UnityEngine.Random.Range(a, b);
        }

        public static bool AllCitiesConquered(Data g) {
            int totalCitiesConquered = 0;
            for (int i = 0; i < g.Board.CurrentMap.Count; i++) {
                if (g.Board.CurrentMap[i] >= MapHexId_Enum.City_Green) {
                    V2IntVO pos = new V2IntVO(D.Scenario.ConvertIndexToWorld(i));
                    if (!g.Board.MonsterData.ContainsKey(pos)) {
                        totalCitiesConquered++;
                    }
                }
            }
            return totalCitiesConquered >= g.GameData.CityTiles;
        }
    }
}