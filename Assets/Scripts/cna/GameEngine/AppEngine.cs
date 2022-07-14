using System;
using System.Collections.Generic;
using cna.connector;
using cna.poo;
using UnityEngine;

namespace cna {
    public abstract class AppEngine : AppBase {

        [SerializeField] internal Data masterGameData = new Data();
        internal ClientState_Enum clientState = ClientState_Enum.NOT_CONNECTED;

        public virtual void Update() {
            if (D.G.GameStatus >= Game_Enum.New_Game) {
                GameEngine();
            }
        }


        private void GameEngine() {
            switch (D.G.GameStatus) {
                case Game_Enum.New_Game: { NewGame(masterGameData); break; }
                case Game_Enum.New_Round: { NewRound(masterGameData); break; }
                case Game_Enum.Tactics: { Tactics(masterGameData); break; }
                case Game_Enum.Tactics_WaitingOnPlayers: { Tactics_WaitingOnPlayers(masterGameData); break; }
                case Game_Enum.SaveGame: { SaveGame(masterGameData); break; }
                case Game_Enum.Player_Turn: { PlayerTurn(masterGameData); break; }
                case Game_Enum.End_Of_Game: { EndOfGame(masterGameData); break; }
            }
        }

        #region NEW GAME
        private void NewGame(Data g) {
            if (D.isHost) {
                g.Clear();
                g.Players.ForEach(p => p.Clear());
                NewGame_BoardSetup(g);
                NewGame_PlayerSetup(g);
            }
        }

        private void NewGame_BoardSetup(Data g) {
            g.Board.TurnCounter = 1;
            g.GameData.Seed = Guid.NewGuid().GetHashCode();
            D.Scenario.buildStartMap(g);
            g.Board.PlayerTurnOrder = new List<int>();
            g.Players.ForEach(p => g.Board.PlayerTurnOrder.Add(p.Key));
            g.Board.PlayerTurnOrder.ShuffleDeck();
            g.Board.PlayerTurnIndex = 0;
            g.GameStatus = Game_Enum.New_Round;
        }

        private void NewGame_PlayerSetup(Data g) {
            NewGame_DummyPlayerSetup(g);
            NewGame_AssignAvatars(g);
            NewGame_PlayerDeckSetup(g);
        }
        private void NewGame_AssignAvatars(Data g) {
            List<Image_Enum> possibleAvatars = new List<Image_Enum>();
            possibleAvatars.AddRange(D.AvatarMetaDataMap.Keys);
            possibleAvatars.Remove(Image_Enum.A_MEEPLE_RANDOM);
            g.Players.ForEach(p => { if (p.Avatar != Image_Enum.A_MEEPLE_RANDOM) { possibleAvatars.Remove(p.Avatar); } });
            g.Players.ForEach(p => {
                if (p.Avatar == Image_Enum.A_MEEPLE_RANDOM) {
                    p.Avatar = possibleAvatars[UnityEngine.Random.Range(0, possibleAvatars.Count)];
                    possibleAvatars.Remove(p.Avatar);
                }
            });
        }

        private void NewGame_PlayerDeckSetup(Data g) {
            D.Cards.ForEach(c => {
                if (c.CardType == CardType_Enum.Basic) {
                    g.Players.ForEach(p => {
                        if (p.Avatar == ((CardActionVO)c).Avatar) {
                            p.Deck.Deck.Add(c.UniqueId);
                        }
                    });
                }
            });
            g.Players.ForEach(p => {
                p.Deck.Deck.ShuffleDeck();
                if (p.DummyPlayer) {
                    switch (p.Avatar) {
                        case Image_Enum.A_meeple_tovak: { p.Crystal.Blue = 2; p.Crystal.Red = 1; break; }
                        case Image_Enum.A_meeple_arythea: { p.Crystal.Red = 2; p.Crystal.White = 1; break; }
                        case Image_Enum.A_meeple_goldyx: { p.Crystal.Green = 2; p.Crystal.Blue = 1; break; }
                        case Image_Enum.A_meeple_norowas: { p.Crystal.White = 2; p.Crystal.Green = 1; break; }
                    }
                }
            });
        }

        public void NewGame_DummyPlayerSetup(Data g) {
            if (g.GameData.DummyPlayer) {
                if (g.Players.Count == 4) {
                    g.GameData.DummyPlayer = false;
                } else {
                    PlayerData pd = new PlayerData("DUMMY", -999);
                    pd.Avatar = Image_Enum.A_MEEPLE_RANDOM;
                    pd.DummyPlayer = true;
                    g.Players.Add(pd);
                    g.Board.PlayerTurnOrder.Add(pd.Key);
                }
            }
        }

        #endregion

        #region NEW ROUND
        private void NewRound(Data g) {
            if (D.isHost) {
                NewRound_BoardSetup(g);
                NewRound_PlayerSetup(g);
                //NewRound_TESTING();
                D.C.Send_GameData();
            }
        }


        private void NewRound_BoardSetup(Data g) {
            g.Board.EndOfRound = false;
            g.GameStatus = Game_Enum.Tactics;
            g.Board.GameRoundCounter++;
            g.Board.PlayerTurnIndex = 0;
        }

        private void NewRound_TESTING() {
            PlayerData pd = D.CurrentTurn;
            if (!pd.DummyPlayer) {
                pd.Movement = 49;
                pd.Influence = 49;
                //pd.Battle.Siege.Physical += 49;

                pd.Crystal.Blue += 1;
                pd.Crystal.Red += 1;
                pd.Crystal.Green += 1;
                pd.Crystal.White += 1;

                pd.Mana.Blue += 1;
                pd.Mana.Red += 1;
                pd.Mana.Green += 1;
                pd.Mana.White += 1;
                pd.Mana.Gold += 1;
                pd.Mana.Black += 1;

                //  Basic Cards
                //pd.Deck.Deck.Clear();
                //pd.Deck.Hand.Clear();
                //pd.Deck.Hand.AddRange(D.Cards.FindAll(c => c.CardType == CardType_Enum.Basic && c.Avatar == pd.Avatar).ConvertAll(c => c.UniqueId));
                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardType == CardType_Enum.Wound).UniqueId);

                //pd.Deck.Skill.AddRange(D.Cards.FindAll(c => c.CardType == CardType_Enum.Skill && c.Avatar == pd.Avatar).ConvertAll(c => c.UniqueId));

                //  Specific Cards
                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardType == CardType_Enum.Wound && !pd.Deck.Hand.Contains(c.UniqueId)).UniqueId);
                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardType == CardType_Enum.Wound && !pd.Deck.Hand.Contains(c.UniqueId)).UniqueId);
                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardType == CardType_Enum.Wound && !pd.Deck.Hand.Contains(c.UniqueId)).UniqueId);
                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardType == CardType_Enum.Wound && !pd.Deck.Hand.Contains(c.UniqueId)).UniqueId);
                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardType == CardType_Enum.Wound && !pd.Deck.Hand.Contains(c.UniqueId)).UniqueId);
                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardType == CardType_Enum.Wound && !pd.Deck.Hand.Contains(c.UniqueId)).UniqueId);
                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardImage == Image_Enum.CT_golden_grail).UniqueId);

                //WrapList<Image_Enum> shields = new WrapList<Image_Enum>();
                //shields.Add(Image_Enum.AVATAR_GREEN_SHIELD);
                //shields.Add(Image_Enum.AVATAR_BLUE_SHIELD);

                //D.G.Monsters.Shield.Add(new V2IntVO(2, 2), shields);

                //pd.AddGameEffect(GameEffect_Enum.Rewards, 1, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 0);

                //pd.Deck.Skill.Add(D.Cards.Find(c => c.CardImage == Image_Enum.SKB_bonds_of_loyalty).UniqueId);
                //pd.Fame.X = 3;
                //pd.Fame.Y = 12;

                //D.G.Board.SkillOffering.Add(D.Cards.Find(c => c.CardImage == Image_Enum.SKB_forward_march).UniqueId);
                //D.G.Board.SkillOffering.Add(D.Cards.Find(c => c.CardImage == Image_Enum.SKR_dark_paths).UniqueId);

                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardImage == Image_Enum.CS_underground_travel).UniqueId);
                //pd.Deck.Unit.Add(D.Cards.Find(c => c.CardImage == Image_Enum.CUE_altem_guardians_x3).UniqueId);
                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardImage == Image_Enum.CS_fireball).UniqueId);
                //D.G.Board.ManaPool.Add(Crystal_Enum.Gold);
                //D.G.Board.ManaPool.Add(Crystal_Enum.Black);
            }
        }
        private void NewRound_PlayerSetup(Data g) {
            PlayerData hostPlayerData = g.Players.Find(p => p.Key == g.HostPlayerKey);
            D.Scenario.BuildManaOfferingDeck(g, hostPlayerData);
            D.Scenario.BuildAdvancedOfferingDeck(hostPlayerData);
            D.Scenario.BuildSpellOfferingDeck(hostPlayerData);
            hostPlayerData.Board.PlayerMap.Clear();
            hostPlayerData.Board.PlayerMap.AddRange(g.Board.CurrentMap);
            D.Scenario.rebuildCurrentMap(hostPlayerData);
            hostPlayerData.Board.MonsterData.Clear();
            g.Board.MonsterData.Keys.ForEach(k => {
                V2IntVO key = new V2IntVO(k.X, k.Y);
                CNAList<int> value = new CNAList<int>();
                g.Board.MonsterData[k].Values.ForEach(v => value.Add(v));
                hostPlayerData.Board.MonsterData.Add(key, value);
            });
            D.Scenario.BuildUnitOfferingDeck(g, hostPlayerData);
            g.Players.ForEach(p => {
                GameAPI ar = new GameAPI(g, p);
                ar.PlayerNewRoundSetup();
                if (!p.DummyPlayer && p.Key != g.HostPlayerKey) {
                    ar.P.ManaPoolFull.Clear();
                    ar.P.Board.UnitOffering.Clear();
                    ar.P.Board.AdvancedOffering.Clear();
                    ar.P.Board.SpellOffering.Clear();
                    hostPlayerData.ManaPoolFull.ForEach(m => ar.P.ManaPoolFull.Add(new ManaPoolData(m.ManaColor)));
                    ar.P.Board.UnitOffering.AddRange(hostPlayerData.Board.UnitOffering);
                    ar.P.Board.AdvancedOffering.AddRange(hostPlayerData.Board.AdvancedOffering);
                    ar.P.Board.SpellOffering.AddRange(hostPlayerData.Board.SpellOffering);
                    ar.P.Board.UnitEliteIndex = hostPlayerData.Board.UnitEliteIndex;
                    ar.P.Board.UnitRegularIndex = hostPlayerData.Board.UnitRegularIndex;
                    ar.P.Board.SpellIndex = hostPlayerData.Board.SpellIndex;
                    ar.P.Board.AdvancedIndex = hostPlayerData.Board.AdvancedIndex;
                    ar.P.Board.AdvancedUnitIndex = hostPlayerData.Board.AdvancedUnitIndex;
                    ar.P.Board.PlayerMap.Clear();
                    ar.P.Board.PlayerMap.AddRange(hostPlayerData.Board.PlayerMap);
                    ar.P.Board.MonsterData.Clear();
                    hostPlayerData.Board.MonsterData.Keys.ForEach(k => {
                        V2IntVO key = new V2IntVO(k.X, k.Y);
                        CNAList<int> value = new CNAList<int>();
                        g.Board.MonsterData[k].Values.ForEach(v => value.Add(v));
                        ar.P.Board.MonsterData.Add(key, value);
                    });
                }
                //  Rebuild VisableMonsters
                if (!p.DummyPlayer) {
                    ar.PlayerRebuildVisableMonsters();
                }
            });
            D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.TacticsSelect;
            D.CurrentTurn.SetTime();
        }

        #endregion

        #region TACTICS

        private void Tactics(Data g) {
            if (D.isHost) {
                switch (D.CurrentTurn.PlayerTurnPhase) {
                    case TurnPhase_Enum.TacticsEnd:
                    case TurnPhase_Enum.TacticsAction: {
                        if (D.CurrentTurn.PlayerTurnPhase == TurnPhase_Enum.TacticsEnd) {
                            D.CurrentTurn.UpdateTime();
                        }
                        if (g.Players.TrueForAll(p => p.PlayerTurnPhase == TurnPhase_Enum.TacticsEnd || p.PlayerTurnPhase == TurnPhase_Enum.TacticsAction)) {
                            //  reorder turns
                            bool swapped = true;
                            while (swapped) {
                                swapped = false;
                                for (int i = 0; i < g.Board.PlayerTurnOrder.Count - 1; i++) {
                                    int a = D.GetPlayerByKey(g.Board.PlayerTurnOrder[i]).Deck.TacticsCardId;
                                    int b = D.GetPlayerByKey(g.Board.PlayerTurnOrder[i + 1]).Deck.TacticsCardId;
                                    if (a > b) {
                                        int temp = g.Board.PlayerTurnOrder[i];
                                        g.Board.PlayerTurnOrder[i] = g.Board.PlayerTurnOrder[i + 1];
                                        g.Board.PlayerTurnOrder[i + 1] = temp;
                                        swapped = true;
                                        break;
                                    }
                                }
                            }
                            g.GameStatus = Game_Enum.Tactics_WaitingOnPlayers;
                            g.Board.PlayerTurnIndex = 0;
                        } else {
                            g.Board.PlayerTurnIndex++;
                            D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.TacticsSelect;
                            D.CurrentTurn.SetTime();
                        }
                        D.C.Send_GameData();
                        break;
                    }
                    case TurnPhase_Enum.TacticsSelect: {
                        if (D.CurrentTurn.DummyPlayer) {
                            List<int> cards = new List<int>();
                            cards.AddRange(D.Scenario.isDay ? D.Scenario.TacticsDayDeck : D.Scenario.TacticsNightDeck);
                            g.Players.ForEach(p => cards.Remove(p.Deck.TacticsCardId));
                            D.CurrentTurn.Deck.TacticsCardId = cards.Random();
                            D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.TacticsEnd;
                            D.C.Send_GameData();
                        }
                        break;
                    }
                }
            }
        }

        #endregion

        #region TACTICS WAITING ON PLAYERS
        private void Tactics_WaitingOnPlayers(Data g) {
            if (D.isHost) {
                if (g.Players.TrueForAll(p => p.PlayerTurnPhase == TurnPhase_Enum.TacticsEnd)) {
                    g.Players.ForEach(p => { p.PlayerTurnPhase = TurnPhase_Enum.PlayerNotTurn; p.UpdateTime(); });
                    g.GameStatus = Game_Enum.SaveGame;
                    D.C.Send_GameData();
                }
            }
        }

        #endregion

        #region SAVE GAME
        public void SaveGame(Data g) {
            if (D.isHost) {
                g.GameStatus = Game_Enum.Player_Turn;
                g.Players.ForEach(p => { if (p.PlayerTurnPhase != TurnPhase_Enum.EndOfRound) { p.PlayerTurnPhase = TurnPhase_Enum.SetupTurn; p.SetTime(); } });
                SaveLoadUtil.SaveGameToFile(D.G);
                D.C.Send_GameData();
            }
        }
        #endregion

        #region PLAYER TURN
        public void PlayerTurn(Data g) {
            if (D.isHost) {
                PlayerData waitingOnServer = g.Players.Find(p => p.WaitOnServer);
                if (waitingOnServer != null) {
                    GameAPI ar = new GameAPI(g, waitingOnServer);
                    int index = ar.P.Board.PlayerMap.FindIndex(h => h == MapHexId_Enum.Explore);
                    MapHexId_Enum currentMapHex = g.Board.CurrentMap[index];
                    if (currentMapHex == MapHexId_Enum.Basic_Back ||
                        currentMapHex == MapHexId_Enum.Core_Back ||
                        currentMapHex == MapHexId_Enum.Invalid
                        ) {
                        currentMapHex = D.Scenario.DrawGameHex(index, g);
                    }
                    ar.P.Board.PlayerMap[index] = currentMapHex;
                    D.Scenario.rebuildCurrentMap(ar.P);
                    V2IntVO centerPos = new V2IntVO(D.Scenario.ConvertIndexToWorld(index));
                    List<V2IntVO> pts = BasicUtil.GetAdjacentPoints(centerPos);
                    pts.Add(centerPos);
                    pts.ForEach(pos => {
                        if (g.Board.MonsterData.ContainsKey(pos)) {
                            CNAList<int> value = new CNAList<int>();
                            g.Board.MonsterData[pos].Values.ForEach(v => value.Add(v));
                            ar.P.Board.MonsterData.Add(pos, value);
                        }
                        if (BasicUtil.GetStructureAtLoc(pos) == Image_Enum.SH_Monastery) {
                            if (ar.P.Board.AdvancedIndexTotal < D.Scenario.AdvancedDeck.Count) {
                                ar.P.Board.UnitOffering.Add(D.Scenario.AdvancedDeck[D.Scenario.AdvancedDeck.Count - ar.P.Board.AdvancedUnitIndex - 1]);
                                ar.P.Board.AdvancedUnitIndex++;
                            }
                        }
                    });
                    ar.P.WaitOnServer = false;
                    ar.PlayerRebuildVisableMonsters();
                    D.C.Send_GameData();
                    return;
                } else {
                    if (g.GameData.DummyPlayer && D.DummyPlayer.PlayerTurnPhase == TurnPhase_Enum.SetupTurn) {
                        GameAPI ar = new GameAPI(g, D.DummyPlayer);
                        ar.Dummy();
                        return;
                    }
                    if (g.Players.TrueForAll(p => p.PlayerTurnPhase >= TurnPhase_Enum.EndTurn && p.PlayerTurnPhase <= TurnPhase_Enum.EndOfRound)) {
                        int spellIndex = -1;
                        int skillBlueIndex = -1;
                        int skillGreenIndex = -1;
                        int skillRedIndex = -1;
                        int skillWhiteIndex = -1;
                        int advancedIndex = -1;
                        int advancedUnitIndex = -1;
                        int unitRegularIndex = -1;
                        int unitEliteIndex = -1;
                        int woundIndex = -1;
                        int artifactIndex = -1;
                        List<int> spellOffering = new List<int>();
                        List<int> skillOffering = new List<int>();
                        List<int> advancedOffering = new List<int>();
                        List<int> unitOffering = new List<int>();
                        List<MapHexId_Enum> playerMap = new List<MapHexId_Enum>();
                        for (int i = 0; i < g.Board.CurrentMap.Count; i++) {
                            playerMap.Add(MapHexId_Enum.Invalid);
                        }
                        g.Players.ForEach(p => {
                            if (!p.DummyPlayer) {
                                for (int i = 0; i < p.Board.PlayerMap.Count; i++) {
                                    if (p.Board.PlayerMap[i] >= MapHexId_Enum.Basic_01) {
                                        V2IntVO centerPos = new V2IntVO(D.Scenario.ConvertIndexToWorld(i));
                                        List<V2IntVO> pts = BasicUtil.GetAdjacentPoints(centerPos);
                                        pts.Add(centerPos);
                                        pts.ForEach(pos => {
                                            if (g.Board.MonsterData.ContainsKey(pos)) {
                                                if (p.Board.MonsterData.ContainsKey(pos)) {
                                                    foreach (int m in g.Board.MonsterData[pos].Values.ToArray()) {
                                                        if (!p.Board.MonsterData[pos].Contains(m)) {
                                                            g.Board.MonsterData[pos].Remove(m);
                                                        }
                                                    }
                                                } else {
                                                    g.Board.MonsterData.Remove(pos);
                                                }
                                            }
                                        });
                                    }
                                }
                                for (int i = 0; i < p.Board.PlayerMap.Count; i++) {
                                    MapHexId_Enum m = p.Board.PlayerMap[i];
                                    if (m >= MapHexId_Enum.Start_A) {
                                        playerMap[i] = m;
                                    }
                                }
                                if (p.Board.SpellIndex > spellIndex) {
                                    spellIndex = p.Board.SpellIndex;
                                    spellOffering.Clear();
                                    spellOffering.AddRange(p.Board.SpellOffering);
                                }
                                if (p.Board.AdvancedUnitIndex > advancedUnitIndex) {
                                    advancedUnitIndex = p.Board.AdvancedUnitIndex;
                                }
                                if (p.Board.AdvancedIndex > advancedIndex) {
                                    advancedIndex = p.Board.AdvancedIndex;
                                    advancedOffering.Clear();
                                    advancedOffering.AddRange(p.Board.AdvancedOffering);
                                }
                                if (p.Board.UnitRegularIndex > unitRegularIndex) {
                                    unitRegularIndex = p.Board.UnitRegularIndex;
                                }
                                if (p.Board.UnitEliteIndex > unitEliteIndex) {
                                    unitEliteIndex = p.Board.UnitEliteIndex;
                                }
                                if (p.Board.WoundIndex > woundIndex) {
                                    woundIndex = p.Board.WoundIndex;
                                }
                                if (p.Board.ArtifactIndex > artifactIndex) {
                                    artifactIndex = p.Board.ArtifactIndex;
                                }
                                if (p.Board.SkillBlueIndex > skillBlueIndex) {
                                    skillBlueIndex = p.Board.SkillBlueIndex;
                                }
                                if (p.Board.SkillGreenIndex > skillGreenIndex) {
                                    skillGreenIndex = p.Board.SkillGreenIndex;
                                }
                                if (p.Board.SkillRedIndex > skillRedIndex) {
                                    skillRedIndex = p.Board.SkillRedIndex;
                                }
                                if (p.Board.SkillWhiteIndex > skillWhiteIndex) {
                                    skillWhiteIndex = p.Board.SkillWhiteIndex;
                                }
                                p.Board.SkillOffering.ForEach(s => {
                                    if (!skillOffering.Contains(s)) {
                                        skillOffering.Add(s);
                                    }
                                });
                                p.Board.UnitOffering.ForEach(u => {
                                    if (!unitOffering.Contains(u)) {
                                        unitOffering.Add(u);
                                    }
                                });
                            }
                        });
                        g.Board.MonsterData.Keys.ForEach(pos => {
                            if (BasicUtil.GetStructureAtLoc(pos) == Image_Enum.SH_SpawningGround) {
                                if (g.Board.MonsterData[pos].Count == 1) {
                                    g.Board.MonsterData[pos].Add(D.Scenario.DrawMonster(MonsterType_Enum.Brown));
                                }
                            }
                        });
                        g.Players.ForEach(p => {
                            if (!p.DummyPlayer) {
                                p.Deck.Skill.ForEach(s => skillOffering.Remove(s));
                                p.Deck.Deck.ForEach(c => { spellOffering.Remove(c); advancedOffering.Remove(c); unitOffering.Remove(c); });
                                p.Deck.Discard.ForEach(c => { spellOffering.Remove(c); advancedOffering.Remove(c); unitOffering.Remove(c); });
                                p.Deck.Hand.ForEach(c => { spellOffering.Remove(c); advancedOffering.Remove(c); unitOffering.Remove(c); });
                                p.Deck.Unit.ForEach(c => { unitOffering.Remove(c); });
                            }
                        });
                        while (advancedOffering.Count < 3 && advancedIndex < D.Scenario.AdvancedDeck.Count) {
                            advancedOffering.Add(D.Scenario.AdvancedDeck[advancedIndex]);
                            advancedIndex++;
                        }
                        while (spellOffering.Count < 3 && spellIndex < D.Scenario.SpellDeck.Count) {
                            spellOffering.Add(D.Scenario.SpellDeck[spellIndex]);
                            spellIndex++;
                        }
                        g.Players.ForEach(p => {
                            if (!p.DummyPlayer) {
                                GameAPI ar = new GameAPI(g, p);
                                ar.P.Board.UnitOffering.Clear();
                                ar.P.Board.AdvancedOffering.Clear();
                                ar.P.Board.SpellOffering.Clear();
                                ar.P.Board.SkillOffering.Clear();
                                ar.P.Board.UnitOffering.AddRange(unitOffering);
                                ar.P.Board.AdvancedOffering.AddRange(advancedOffering);
                                ar.P.Board.SpellOffering.AddRange(spellOffering);
                                ar.P.Board.SkillOffering.AddRange(skillOffering);
                                ar.P.Board.UnitEliteIndex = unitEliteIndex;
                                ar.P.Board.UnitRegularIndex = unitRegularIndex;
                                ar.P.Board.SpellIndex = spellIndex;
                                ar.P.Board.AdvancedIndex = advancedIndex;
                                ar.P.Board.AdvancedUnitIndex = advancedUnitIndex;
                                ar.P.Board.ArtifactIndex = artifactIndex;
                                ar.P.Board.WoundIndex = woundIndex;
                                ar.P.Board.SkillBlueIndex = skillBlueIndex;
                                ar.P.Board.SkillGreenIndex = skillGreenIndex;
                                ar.P.Board.SkillRedIndex = skillRedIndex;
                                ar.P.Board.SkillWhiteIndex = skillWhiteIndex;
                                ar.P.Board.MonsterData.Clear();
                                ar.P.Board.PlayerMap.Clear();
                                ar.P.Board.PlayerMap.AddRange(playerMap);
                                D.Scenario.rebuildCurrentMap(ar.P);
                                for (int i = 0; i < playerMap.Count; i++) {
                                    if ((int)playerMap[i] >= 10) {
                                        V2IntVO centerPos = new V2IntVO(D.Scenario.ConvertIndexToWorld(i));
                                        List<V2IntVO> pts = BasicUtil.GetAdjacentPoints(centerPos);
                                        pts.Add(centerPos);
                                        pts.ForEach(pos => {
                                            if (g.Board.MonsterData.ContainsKey(pos)) {
                                                CNAList<int> value = new CNAList<int>();
                                                g.Board.MonsterData[pos].Values.ForEach(v => value.Add(v));
                                                ar.P.Board.MonsterData.Add(pos, value);
                                            }
                                        });
                                    }
                                }
                                ar.PlayerRebuildVisableMonsters();
                            }
                        });
                        bool allCitiesConquered = BasicUtil.AllCitiesConquered(g);
                        if (g.Board.EndOfRound) {
                            if (allCitiesConquered || g.Board.GameRoundCounter + 1 >= g.GameData.Rounds) {
                                g.GameStatus = Game_Enum.End_Of_Game;
                            } else {
                                g.GameStatus = Game_Enum.New_Round;
                            }
                        } else {
                            g.GameStatus = Game_Enum.SaveGame;
                            g.Board.EndOfRound = !g.Players.TrueForAll(p => p.PlayerTurnPhase == TurnPhase_Enum.EndTurn) || allCitiesConquered;
                            List<ManaPoolData> rerollmana = new List<ManaPoolData>();
                            g.Players.ForEach(p => {
                                GameAPI ar = new GameAPI(g, p);
                                ar.PlayerEndOfTurn();
                                if (!p.DummyPlayer) {
                                    List<ManaPoolData> m = p.ManaPoolFull;
                                    for (int i = 0; i < p.ManaPoolFull.Count; i++) {
                                        Crystal_Enum c;
                                        ManaPool_Enum status = ManaPool_Enum.Reroll;
                                        if (m[i].Status == ManaPool_Enum.Used) {
                                            c = (Crystal_Enum)BasicUtil.RandomRange(1, 7);
                                        } else {
                                            c = m[i].ManaColor;
                                            status = m[i].Status;
                                        }
                                        if (rerollmana.Count <= i) {
                                            rerollmana.Add(new ManaPoolData(c, status));
                                        } else {
                                            if (rerollmana[i].Status != ManaPool_Enum.Reroll) {
                                                rerollmana[i].ManaColor = c;
                                                rerollmana[i].Status = status;
                                            }
                                        }
                                    }
                                }
                            });
                            rerollmana.ForEach(m => {
                                if (m.Status != ManaPool_Enum.ManaSteal) {
                                    m.Status = ManaPool_Enum.None;
                                }
                            });
                            g.Players.ForEach(p => {
                                if (!p.DummyPlayer) {
                                    p.ManaPoolFull.Clear();
                                    rerollmana.ForEach(m => p.ManaPoolFull.Add(new ManaPoolData(m.ManaColor, m.Status)));
                                }
                            });
                        }
                        g.Board.TurnCounter++;
                        D.C.Send_GameData();
                        return;
                    }
                }
            }


            PlayerData localPlayer = D.LocalPlayer;
            switch (localPlayer.PlayerTurnPhase) {
                case TurnPhase_Enum.SetupTurn: {
                    PlayerTurn_SetupTurn(g, localPlayer);
                    break;
                }
                case TurnPhase_Enum.Move: {
                    if (localPlayer.UndoLock) {
                        localPlayer.UndoLock = false;
                        pd_StartOfTurn = localPlayer.Clone();
                        D.C.Send_PlayerData();
                    }
                    break;
                }
                case TurnPhase_Enum.EndTurn_TheRightMoment: {
                    GameAPI ar = new GameAPI(g, localPlayer);
                    ar.PlayerEndOfTurn();
                    ar.P.PlayerTurnPhase = TurnPhase_Enum.SetupTurn;
                    ar.PushForce();
                    break;
                }
            }
        }

        public void PlayerTurn_SetupTurn(Data g, PlayerData l) {
            GameAPI ar = new GameAPI(g, l);
            ar.StartOfTurnPanel(PlayerTurn_SetupTurnCallback);
        }

        public void PlayerTurn_SetupTurnCallback(GameAPI ar) {
            pd_StartOfTurn = ar.P.Clone();
            ar.Push();
        }

        #endregion

        #region END OF GAME
        public void EndOfGame(Data g) {
            GameAPI ar = new GameAPI(g, D.LocalPlayer);
            ar.EndOfGame();
        }
        #endregion

        public virtual void New() {
            screenState = ScreenState_Enum.Map;
            masterGameData = new Data();
        }


        //  MIGHT RETHING LOCATION OF THESE
        internal ScenarioBase scenario;
        internal ScreenState_Enum screenState = ScreenState_Enum.Map;
        public virtual void StartTacticsPanel() { }
        private bool gd_StartOfTurnFlag = false;
        public bool Gd_StartOfTurnFlag { get => gd_StartOfTurnFlag; set => gd_StartOfTurnFlag = value; }
        public PlayerData pd_StartOfTurn = new PlayerData();

        public void OnClick_Undo() {
            D.C.LogMessage("[Undo]");
            int localPlayerKey = D.LocalPlayerKey;
            masterGameData.Players.Find(p => p.Key == localPlayerKey).UpdateData(pd_StartOfTurn.Clone());
            D.C.Send_PlayerData();
        }
    }
}
