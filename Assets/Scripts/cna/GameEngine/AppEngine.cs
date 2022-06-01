using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    public abstract class AppEngine : AppBase {
        [SerializeField] internal GameData gd_StartOfTurn = new GameData();
        [SerializeField] private bool gd_StartOfTurnFlag = false;
        [SerializeField] internal ScreenState_Enum screenState = ScreenState_Enum.Map;
        [SerializeField] internal GameData gameData = new GameData();
        [SerializeField] internal ClientState_Enum clientState = ClientState_Enum.NOT_CONNECTED;
        [SerializeField] internal ScenarioBase scenario;

        public bool Gd_StartOfTurnFlag { get => gd_StartOfTurnFlag; set => gd_StartOfTurnFlag = value; }

        private void Update() {
            if (D.G.GameStatus >= Game_Enum.New_Game) {
                GameEngine();
            }
        }

        private void GameEngine() {
            switch (D.G.GameStatus) {
                case Game_Enum.New_Game: { NewGame(); break; }
                case Game_Enum.New_Round: { NewRound(); break; }
                case Game_Enum.Tactics: { Tactics(); break; }
                case Game_Enum.Player_Turn: { PlayerTurn(); break; }
            }
        }

        #region NEW GAME
        private void NewGame() {
            Clear();
            if (D.isHost) {
                D.G.Clear();
                D.G.Players.ForEach(p => p.Clear());
                NewGame_BoardSetup();
                NewGame_PlayerSetup();
            }
        }

        private void NewGame_BoardSetup() {
            D.G.TurnCounter = 1;
            D.G.Board = new BoardData(D.G.Gld.GameMapLayout, D.G.Gld.BasicTiles, D.G.Gld.CoreTiles, D.G.Gld.CityTiles, D.G.Gld.EasyStart, D.G.Gld.Rounds, D.G.Gld.DummyPlayer);
            D.Scenario.buildStartMap();
            D.G.PlayerTurnOrder = new List<int>();
            D.G.Players.ForEach(p => D.G.PlayerTurnOrder.Add(p.Key));
            D.G.PlayerTurnOrder.ShuffleDeck();
            D.G.PlayerTurnIndex = 0;
            D.G.GameStatus = Game_Enum.New_Round;
        }

        private void NewGame_PlayerSetup() {
            NewGame_DummyPlayerSetup();
            NewGame_AssignAvatars();
            NewGame_PlayerDeckSetup();
        }
        private void NewGame_AssignAvatars() {
            List<Image_Enum> possibleAvatars = new List<Image_Enum>();
            possibleAvatars.AddRange(D.AvatarMetaDataMap.Keys);
            possibleAvatars.Remove(Image_Enum.A_MEEPLE_RANDOM);
            D.G.Players.ForEach(p => { if (p.Avatar != Image_Enum.A_MEEPLE_RANDOM) { possibleAvatars.Remove(p.Avatar); } });
            D.G.Players.ForEach(p => {
                if (p.Avatar == Image_Enum.A_MEEPLE_RANDOM) {
                    p.Avatar = possibleAvatars[UnityEngine.Random.Range(0, possibleAvatars.Count)];
                    possibleAvatars.Remove(p.Avatar);
                }
            });
        }

        private void NewGame_PlayerDeckSetup() {
            D.Cards.ForEach(c => {
                if (c.CardType == CardType_Enum.Basic) {
                    D.G.Players.ForEach(p => {
                        if (p.Avatar == ((CardActionVO)c).Avatar) {
                            p.Deck.Deck.Add(c.UniqueId);
                        }
                    });
                }
            });
            D.G.Players.ForEach(p => {
                p.Deck.Deck.ShuffleDeck();
                if (p.DummyPlayer) {
                    switch (p.Avatar) {
                        case Image_Enum.A_MEEPLE_BLUE: { p.Crystal.Blue = 2; p.Crystal.Red = 1; break; }
                        case Image_Enum.A_MEEPLE_RED: { p.Crystal.Red = 2; p.Crystal.White = 1; break; }
                        case Image_Enum.A_MEEPLE_GREEN: { p.Crystal.Green = 2; p.Crystal.Blue = 1; break; }
                        case Image_Enum.A_MEEPLE_WHITE: { p.Crystal.White = 2; p.Crystal.Green = 1; break; }
                    }
                }
            });
        }

        public void NewGame_DummyPlayerSetup() {
            if (D.G.Board.DummyPlayer) {
                if (D.G.Players.Count == 4) {
                    D.G.Board.DummyPlayer = false;
                } else {
                    PlayerData pd = new PlayerData("DUMMY", -999);
                    pd.Avatar = Image_Enum.A_MEEPLE_RANDOM;
                    pd.DummyPlayer = true;
                    D.G.Players.Add(pd);
                    D.G.PlayerTurnOrder.Add(pd.Key);
                }
            }
        }

        #endregion

        #region NEW ROUND
        private void NewRound() {
            if (D.isHost) {
                NewRound_BoardSetup();
                //D.G.GameRoundCounter++;
                NewRound_PlayerSetup();
                //NewRound_TESTING();
                D.C.Send_GameData();
            }
        }

        private void NewRound_PlayerSetup() {
            D.G.Players.ForEach(p => {
                p.PlayerTurnPhase = TurnPhase_Enum.NotTurn;
                p.Deck.ClearNewRound();
                foreach (GameEffect_Enum ge in p.GameEffects.Keys.ToArray()) {
                    CardVO c = D.GetGameEffectCard(ge);
                    if (c.GameEffectDurationId != GameEffectDuration_Enum.Game) {
                        p.RemoveGameEffect(ge);
                    }
                }
                p.PlayerTurnPhase = TurnPhase_Enum.NotTurn;
                p.Deck.Deck.ShuffleDeck();
                if (!p.DummyPlayer) {
                    p.ClearEndTurn();
                    p.AddGameEffect(D.Scenario.isDay ? GameEffect_Enum.Day : GameEffect_Enum.Night);
                    drawHand(p.Deck);
                    if (D.Scenario.isDay) {
                        D.G.Monsters.Map.Keys.ForEach(pos => {
                            D.G.Monsters.Map[pos].Values.ForEach(r => {
                                if (D.Cards[r].CardType != CardType_Enum.Monster) {
                                    p.VisableMonsters.Add(r);
                                }
                            });
                        });
                    }
                }
            });
            D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.TacticsSelect;
        }

        private void NewRound_BoardSetup() {
            D.G.EndOfRound = -1;
            //D.G.GameStatus = Game_Enum.Player_Turn;
            D.G.GameStatus = Game_Enum.Tactics;
            D.Scenario.BuildUnitOfferingDeck();
            D.Scenario.BuildManaOfferingDeck();
            D.Scenario.BuildSpellOfferingDeck();
            D.Scenario.BuildAdvancedOfferingDeck();
            D.G.GameRoundCounter++;
            D.G.PlayerTurnIndex = 0;
        }

        private void NewRound_TESTING() {
            PlayerData pd = D.CurrentTurn;
            if (!pd.DummyPlayer) {
                //pd.Deck.HandSize.X = 10;
                pd.Movement = 49;
                pd.Battle.Siege.Physical += 49;
                pd.Crystal.Blue += 3;
                pd.Crystal.Red += 3;
                pd.Crystal.Green += 3;
                pd.Crystal.White += 3;


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

                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardImage == Image_Enum.CA_learning).UniqueId);
                //pd.Deck.Unit.Add(D.Cards.Find(c => c.CardImage == Image_Enum.CUE_altem_guardians_x3).UniqueId);
                //pd.Deck.Hand.Add(D.Cards.Find(c => c.CardImage == Image_Enum.CS_fireball).UniqueId);
                //D.G.Board.ManaPool.Add(Crystal_Enum.Gold);
                //D.G.Board.ManaPool.Add(Crystal_Enum.Black);
            }
        }

        #endregion

        #region TACTICS

        private void Tactics() {
            if (D.isHost) {
                switch (D.CurrentTurn.PlayerTurnPhase) {
                    case TurnPhase_Enum.TacticsHost: {
                        GameData gd = D.G;
                        D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.TacticsEnd;
                        bool tacticsDone = true;
                        gd.Players.ForEach(p => { tacticsDone = tacticsDone && p.PlayerTurnPhase == TurnPhase_Enum.TacticsEnd; });
                        if (tacticsDone) {
                            //  reorder turns
                            bool swapped = true;
                            while (swapped) {
                                swapped = false;
                                for (int i = 0; i < gd.PlayerTurnOrder.Count - 1; i++) {
                                    int a = D.GetPlayerByKey(gd.PlayerTurnOrder[i]).Deck.TacticsCardId;
                                    int b = D.GetPlayerByKey(gd.PlayerTurnOrder[i + 1]).Deck.TacticsCardId;
                                    if (a > b) {
                                        int temp = gd.PlayerTurnOrder[i];
                                        gd.PlayerTurnOrder[i] = gd.PlayerTurnOrder[i + 1];
                                        gd.PlayerTurnOrder[i + 1] = temp;
                                        swapped = true;
                                        break;
                                    }
                                }
                            }
                            gd.GameStatus = Game_Enum.Player_Turn;
                            gd.PlayerTurnIndex = 0;
                            D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.HostSaveGame;
                        } else {
                            gd.PlayerTurnIndex++;
                            D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.TacticsSelect;
                        }
                        break;
                    }
                    case TurnPhase_Enum.TacticsSelect: {
                        if (D.CurrentTurn.DummyPlayer) {
                            List<int> cards = new List<int>();
                            cards.AddRange(D.Scenario.isDay ? D.Scenario.TacticsDayDeck : D.Scenario.TacticsNightDeck);
                            D.G.Players.ForEach(p => cards.Remove(p.Deck.TacticsCardId));
                            D.CurrentTurn.Deck.TacticsCardId = cards.Random();
                            D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.TacticsHost;
                        }
                        break;
                    }
                }
                D.C.Send_GameData();
            }
        }


        #endregion

        #region PLAYER TURN
        private void PlayerTurn() {
            switch (D.CurrentTurn.PlayerTurnPhase) {
                case TurnPhase_Enum.HostSaveGame: { PlayerTurn_HostSaveGame(); break; }
                case TurnPhase_Enum.SetupTurn: { PlayerTurn_SetupTurn(); break; }
                case TurnPhase_Enum.EndTurn: { PlayerTurn_EndTurn(); break; }
            }
        }

        private void PlayerTurn_HostSaveGame() {
            if (D.isHost) {
                PlayerData currentPlayer = D.CurrentTurn;
                if (currentPlayer.DummyPlayer) {
                    currentPlayer.PlayerTurnPhase = TurnPhase_Enum.EndTurn;
                } else {
                    currentPlayer.PlayerTurnPhase = TurnPhase_Enum.SetupTurn;
                    BasicUtil.SaveGameToFile(D.G);
                }
                D.C.Send_GameData();
            }
        }

        private void PlayerTurn_EndTurn() {
            if (D.isHost) {
                PlayerData currentPlayer = D.CurrentTurn;
                if (currentPlayer.DummyPlayer) {
                    if (currentPlayer.Deck.Deck.Count == 0) {
                        D.G.EndOfRound = currentPlayer.Key;
                    } else {
                        CardColor_Enum cardColor = CardColor_Enum.NA;
                        int totalCards = 0;
                        int cardId = 0;
                        for (int i = 0; i < 3; i++) {
                            if (currentPlayer.Deck.Deck.Count > 0) {
                                cardId = BasicUtil.DrawCard(currentPlayer.Deck.Deck);
                                currentPlayer.Deck.Discard.Add(cardId);
                                totalCards++;
                            }
                        }
                        if (currentPlayer.Deck.Deck.Count > 0) {
                            int extra = 0;
                            cardColor = D.Cards[cardId].CardColor;
                            switch (cardColor) {
                                case CardColor_Enum.Blue: { extra = currentPlayer.Crystal.Blue; break; }
                                case CardColor_Enum.Red: { extra = currentPlayer.Crystal.Red; break; }
                                case CardColor_Enum.Green: { extra = currentPlayer.Crystal.Green; break; }
                                case CardColor_Enum.White: { extra = currentPlayer.Crystal.White; break; }
                            }
                            for (int i = 0; i < extra; i++) {
                                if (currentPlayer.Deck.Deck.Count > 0) {
                                    cardId = BasicUtil.DrawCard(currentPlayer.Deck.Deck);
                                    currentPlayer.Deck.Discard.Add(cardId);
                                    totalCards++;
                                }
                            }
                        }
                        string msg = "Total Cards " + totalCards;
                        if (cardColor != CardColor_Enum.NA) {
                            msg += " (" + cardColor + ")";
                        }
                        D.C.LogMessageDummy(msg);
                    }

                    D.G.PlayerTurnIndex++;
                    if (D.G.EndOfRound == D.CurrentTurn.Key) {
                        D.C.LogMessageDummy("End of Round Declared!");
                        D.G.GameStatus = Game_Enum.New_Round;
                    } else {
                        D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.HostSaveGame;
                    }
                } else {
                    if (currentPlayer.GameEffects.ContainsKey(GameEffect_Enum.CS_TimeBending)) {
                        TimeBend(currentPlayer);
                    } else {
                        bool theRightMoment = currentPlayer.GameEffects.ContainsKey(GameEffect_Enum.T_TheRightMoment02);
                        clearHand(currentPlayer.Deck, currentPlayer.GameEffects);
                        resetUnit(currentPlayer.Deck);
                        resetSkill(currentPlayer.Deck, false);
                        int cardsLeftInHand = currentPlayer.Deck.Hand.Count;
                        drawHand(currentPlayer.Deck);
                        resetManaPool();
                        foreach (GameEffect_Enum ge in currentPlayer.GameEffects.Keys.ToArray()) {
                            CardVO c = D.GetGameEffectCard(ge);
                            switch (ge) {
                                case GameEffect_Enum.AC_CrystalMastery: {
                                    CrystalData crystal = currentPlayer.Crystal;
                                    crystal.Blue += crystal.SpentBlue;
                                    crystal.Green += crystal.SpentGreen;
                                    crystal.Red += crystal.SpentRed;
                                    crystal.White += crystal.SpentWhite;
                                    break;
                                }
                                case GameEffect_Enum.SH_CrystalMines_Blue: {
                                    D.C.LogMessage("[Blue Crystal Mine] +1 Blue Crystal");
                                    currentPlayer.Crystal.Blue++;
                                    break;
                                }
                                case GameEffect_Enum.SH_CrystalMines_Red: {
                                    D.C.LogMessage("[Red Crystal Mine] +1 Red Crystal");
                                    currentPlayer.Crystal.Red++;
                                    break;
                                }
                                case GameEffect_Enum.SH_CrystalMines_Green: {
                                    D.C.LogMessage("[Green Crystal Mine] +1 Green Crystal");
                                    currentPlayer.Crystal.Green++;
                                    break;
                                }
                                case GameEffect_Enum.SH_CrystalMines_White: {
                                    D.C.LogMessage("[White Crystal Mine] +1 White Crystal");
                                    currentPlayer.Crystal.White++;
                                    break;
                                }
                                case GameEffect_Enum.T_Planning: {
                                    if (cardsLeftInHand >= 2) {
                                        D.C.LogMessage("[Planning] +1 Card");
                                        if (currentPlayer.Deck.Deck.Count > 0) {
                                            currentPlayer.Deck.Hand.Add(BasicUtil.DrawCard(currentPlayer.Deck.Deck));
                                        }
                                    }
                                    break;
                                }
                                case GameEffect_Enum.T_ManaSearch02: {
                                    currentPlayer.RemoveGameEffect(GameEffect_Enum.T_ManaSearch02);
                                    currentPlayer.AddGameEffect(GameEffect_Enum.T_ManaSearch01);
                                    break;
                                }
                            }
                            if (c.GameEffectDurationId == GameEffectDuration_Enum.Turn) {
                                currentPlayer.RemoveGameEffect(ge);
                            }
                        }
                        currentPlayer.ClearEndTurn();
                        if (theRightMoment) {
                            D.C.LogMessage("[The Right Moment] Taking another turn!");
                        } else {
                            D.G.PlayerTurnIndex++;
                        }
                        D.G.TurnCounter++;
                        if (D.G.EndOfRound == D.CurrentTurn.Key) {
                            D.G.GameStatus = Game_Enum.New_Round;
                        } else {
                            D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.HostSaveGame;
                        }
                    }
                }
                D.C.Send_GameData();
            }
        }

        private void TimeBend(PlayerData pd) {
            PlayerDeckData pDeck = pd.Deck;
            pd.ActionTaken = false;
            foreach (int c in pDeck.Hand.ToArray()) {
                if (pDeck.State.ContainsKey(c)) {
                    if (pDeck.StateContainsAny(c, CardState_Enum.Discard, CardState_Enum.Trashed)) {
                        pDeck.Hand.Remove(c);
                        if (!pDeck.StateContains(c, CardState_Enum.Trashed)) {
                            pDeck.Discard.Add(c);
                        }
                        pDeck.State.Remove(c);
                    } else if (pDeck.StateContainsAny(c, CardState_Enum.Basic, CardState_Enum.Normal, CardState_Enum.Advanced)) {
                        pDeck.State.Remove(c);
                    }
                }
            }
            resetUnit(pd.Deck);
            resetSkill(pd.Deck, false);
            resetManaPool();
            foreach (GameEffect_Enum ge in pd.GameEffects.Keys.ToArray()) {
                CardVO c = D.GetGameEffectCard(ge);
                switch (ge) {
                    case GameEffect_Enum.AC_CrystalMastery: {
                        CrystalData crystal = pd.Crystal;
                        crystal.Blue += crystal.SpentBlue;
                        crystal.Green += crystal.SpentGreen;
                        crystal.Red += crystal.SpentRed;
                        crystal.White += crystal.SpentWhite;
                        break;
                    }
                    case GameEffect_Enum.SH_CrystalMines_Blue: {
                        D.C.LogMessage("[Blue Crystal Mine] +1 Blue Crystal");
                        pd.Crystal.Blue++;
                        break;
                    }
                    case GameEffect_Enum.SH_CrystalMines_Red: {
                        D.C.LogMessage("[Red Crystal Mine] +1 Red Crystal");
                        pd.Crystal.Red++;
                        break;
                    }
                    case GameEffect_Enum.SH_CrystalMines_Green: {
                        D.C.LogMessage("[Green Crystal Mine] +1 Green Crystal");
                        pd.Crystal.Green++;
                        break;
                    }
                    case GameEffect_Enum.SH_CrystalMines_White: {
                        D.C.LogMessage("[White Crystal Mine] +1 White Crystal");
                        pd.Crystal.White++;
                        break;
                    }
                    case GameEffect_Enum.T_ManaSearch02: {
                        pd.RemoveGameEffect(GameEffect_Enum.T_ManaSearch02);
                        pd.AddGameEffect(GameEffect_Enum.T_ManaSearch01);
                        break;
                    }
                }
                if (c.GameEffectDurationId == GameEffectDuration_Enum.Turn) {
                    pd.RemoveGameEffect(ge);
                }
            }
            D.G.TurnCounter++;
            pd.ClearEndTurn();
            pd.PlayerTurnPhase = TurnPhase_Enum.HostSaveGame;
        }

        private void PlayerTurn_SetupTurn() {
            PlayerData localPlayer = D.LocalPlayer;
            if (localPlayer.Equals(D.CurrentTurn)) {
                Clear();
                BasicUtil.UpdateMovementGameEffects(localPlayer);
                localPlayer.PlayerTurnPhase = TurnPhase_Enum.NotifyTurn;
                localPlayer.GameEffects.Keys.ForEach(ge => {
                    switch (ge) {
                        case GameEffect_Enum.SH_MagicGlade: {
                            if (D.Scenario.isDay) {
                                D.C.LogMessage("[Magical Glade] +1 Gold Mana");
                                localPlayer.Mana.Gold++;
                            } else {
                                D.C.LogMessage("[Magical Glade] +1 Black Mana");
                                localPlayer.Mana.Black++;
                            }
                            break;
                        }
                        case GameEffect_Enum.SH_City_Red_Own:
                        case GameEffect_Enum.SH_City_Green_Own:
                        case GameEffect_Enum.SH_City_White_Own:
                        case GameEffect_Enum.SH_City_Blue_Own: {
                            if (D.G.Monsters.Shield.ContainsKey(localPlayer.CurrentGridLoc)) {
                                Image_Enum AvatarShieldId = D.AvatarMetaDataMap[localPlayer.Avatar].AvatarShieldId;
                                D.G.Monsters.Shield[localPlayer.CurrentGridLoc].Values.ForEach(s => { if (s.Equals(AvatarShieldId)) { localPlayer.Influence++; } });
                            }
                            break;
                        }
                    }
                });
                D.C.Send_GameData();
                PlayerTurn_SetupTurn_SparingPower(localPlayer);
            }
        }

        private void PlayerTurn_SetupTurn_SparingPower(PlayerData localPlayer) {
            bool sparingPower = localPlayer.GameEffects.ContainsKey(GameEffect_Enum.T_SparingPower);
            if (sparingPower) {
                int powerDeckSize = localPlayer.GameEffects[GameEffect_Enum.T_SparingPower].Count - 1;
                int deckSize = localPlayer.Deck.Deck.Count;
                if (powerDeckSize == 0) {
                    int topCardFromDeck = BasicUtil.DrawCard(localPlayer.Deck.Deck);
                    localPlayer.AddGameEffect(GameEffect_Enum.T_SparingPower, topCardFromDeck);
                    D.C.LogMessage("[Sparing Power] No cards in Sparing Power Deck, +1 Card to Sparing Power Deck!");
                } else if (deckSize == 0) {
                    D.C.LogMessage("[Sparing Power] No cards in Deck, Sparing Power Used!");
                    localPlayer.GameEffects[GameEffect_Enum.T_SparingPower].Values.ForEach(c => {
                        if (c != 0) {
                            localPlayer.Deck.Hand.Add(c);
                        }
                    });
                    localPlayer.RemoveGameEffect(GameEffect_Enum.T_SparingPower);
                } else {
                    ActionResultVO ar = new ActionResultVO(D.GetGameEffectCard(GameEffect_Enum.T_SparingPower).UniqueId, CardState_Enum.NA);
                    ar.ActionIndex = 0;
                    D.Action.SelectOptions(ar, null, PlayerTurn_SetupTurn_SparingPower_options, new OptionVO("Use Deck", Image_Enum.I_check), new OptionVO("+1 to Deck", Image_Enum.I_cardBackRounded));
                    return;
                }
                D.C.Send_GameData();
            }
            PlayerTurn_SetupTurn_Notification(localPlayer);
        }

        private void PlayerTurn_SetupTurn_SparingPower_options(ActionResultVO ar) {
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.AddLog("Sparing Power Used!");
                    ar.LocalPlayer.GameEffects[GameEffect_Enum.T_SparingPower].Values.ForEach(c => {
                        if (c != 0) {
                            ar.LocalPlayer.Deck.Hand.Add(c);
                        }
                    });
                    ar.LocalPlayer.RemoveGameEffect(GameEffect_Enum.T_SparingPower);
                    ar.change();
                    ar.CompleteAction();
                    break;
                }
                case 1: {
                    ar.AddLog("+1 Card to Sparing Power Deck!");
                    int topCardFromDeck = BasicUtil.DrawCard(ar.LocalPlayer.Deck.Deck);
                    ar.LocalPlayer.AddGameEffect(GameEffect_Enum.T_SparingPower, topCardFromDeck);
                    ar.change();
                    ar.CompleteAction();
                    break;
                }
            }
            PlayerTurn_SetupTurn_Notification(ar.LocalPlayer);
        }


        private void PlayerTurn_SetupTurn_Notification(PlayerData localPlayer) {
            bool forceDeclareEndOfRound = localPlayer.Deck.Deck.Count == 0 && localPlayer.Deck.Hand.Count == 0;
            bool isExhausted = true;
            localPlayer.Deck.Hand.ForEach(c => isExhausted = isExhausted && D.Cards[c].CardType == CardType_Enum.Wound);
            bool endOfRoundDeclared = D.G.EndOfRound != -1;
            StartOfTurnNotification(forceDeclareEndOfRound, isExhausted, endOfRoundDeclared);
        }

        #endregion

        #region PLAYER REWARDS

        public void PlayerRewards() {
            PlayerData localPlayer = D.LocalPlayer;
            localPlayer.PlayerTurnPhase = TurnPhase_Enum.Reward;
            CardVO Card = D.GetGameEffectCard(GameEffect_Enum.Rewards);
            ActionResultVO ar = new ActionResultVO(D.G.Clone(), Card.UniqueId, CardState_Enum.NA, 0);
            Card.OnClick_ActionButton(ar);
        }

        #endregion

        private void drawHand(PlayerDeckData p) {
            int handLimit = p.TotalHandSize;
            while (p.Deck.Count > 0 && p.Hand.Count < handLimit) {
                p.Hand.Add(BasicUtil.DrawCard(p.Deck));
            }
        }
        private void clearHand(PlayerDeckData p, CNAMap<GameEffect_Enum, WrapList<int>> gameEffects) {
            foreach (int c in p.Hand.ToArray()) {
                if (p.State.ContainsKey(c)) {
                    if (p.StateContainsAny(c, CardState_Enum.Discard, CardState_Enum.Basic, CardState_Enum.Normal, CardState_Enum.Advanced, CardState_Enum.Trashed)) {
                        p.Hand.Remove(c);
                        if (!p.StateContains(c, CardState_Enum.Trashed)) {
                            bool steadyTempNormal = gameEffects.ContainsKey(GameEffect_Enum.AC_SteadyTempo01);
                            bool steadyTempAdvanced = gameEffects.ContainsKey(GameEffect_Enum.AC_SteadyTempo02);
                            if (p.Deck.Count > 0 && (steadyTempNormal || steadyTempAdvanced)) {
                                if (steadyTempNormal) {
                                    p.Deck.Add(c);
                                } else {
                                    p.Deck.Insert(0, c);
                                }
                            } else {
                                p.Discard.Add(c);
                            }
                        }
                        p.State.Remove(c);
                    }
                }
            }
        }
        private void resetSkill(PlayerDeckData p, bool round) {
            foreach (int c in p.Skill.ToArray()) {
                if (p.State.ContainsKey(c)) {
                    SkillRefresh_Enum refresh = D.Cards[c].SkillRefresh;
                    if (round || refresh == SkillRefresh_Enum.Turn) {
                        p.State.Remove(c);
                    }
                }
            }
        }
        private void resetUnit(PlayerDeckData p) {
            foreach (int c in p.Unit.ToArray()) {
                if (p.State.ContainsKey(c)) {
                    if (p.State[c].Values.Contains(CardState_Enum.Unit_Paralyzed)) {
                        p.State.Remove(c);
                        p.Unit.Remove(c);
                    }
                    p.State[c].Values.Remove(CardState_Enum.Unit_UsedInBattle);
                    if (p.State[c].Count == 0) {
                        p.State.Remove(c);
                    }
                }
            }
        }
        private void resetManaPool() {
            bool manaSteal = false;
            D.G.Players.ForEach(p => {
                manaSteal |= p.GameEffects.ContainsKey(GameEffect_Enum.T_ManaSteal);
            });
            int totalDie = D.G.Players.Count + 2;
            if (D.Board.DummyPlayer) {
                totalDie--;
            }
            if (manaSteal) {
                totalDie--;
            }
            int currentManaTotal = D.G.Board.ManaPool.Count;
            for (int i = currentManaTotal; i < totalDie; i++) {
                D.G.Board.ManaPool.Add((Crystal_Enum)UnityEngine.Random.Range(1, 7));
            }
        }
        public void OnClick_StartofTurnOkay(bool forceDeclareEndOfRound, bool isExhausted, bool endOfRoundDeclared) {
            if (forceDeclareEndOfRound) {
                if (D.G.EndOfRound == -1) {
                    D.G.EndOfRound = D.CurrentTurn.Key;
                }
                D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.EndTurn;
            } else {
                if (isExhausted) {
                    D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.Exhaustion;
                    int cardid = D.LocalPlayer.Deck.Hand.Find(c => D.Cards[c].CardType == CardType_Enum.Wound);
                    if (cardid > 0) {
                        D.LocalPlayer.Deck.AddState(cardid, CardState_Enum.Discard);
                    }
                } else {
                    D.CurrentTurn.PlayerTurnPhase = TurnPhase_Enum.StartTurn;
                }
                gd_StartOfTurn = D.G.Clone();
            }
            D.C.Send_GameData();
        }

        #region GameWorld Buttons

        public void OnClick_Undo() {
            D.C.LogMessage("[Undo]");
            gameData = gd_StartOfTurn.Clone();
            Clear();
            D.C.Send_GameData();
        }
        #endregion

        public abstract void Clear();
        public abstract void StartOfTurnNotification(bool forceDeclareEndOfRound, bool isExhausted, bool endOfRoundDeclared);
        public abstract void StartTacticsPanel();
    }
}
