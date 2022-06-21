﻿using System;
using System.Collections.Generic;
using System.Reflection;
using cna.poo;
using UnityEngine;

namespace cna {
    public class GameAPI {
        #region quickAccess
        private Data g;
        private PlayerData playerData;
        #endregion


        private PlayerData pdClone;
        private CardVO card;
        private bool status;
        private string errorMsg;
        private List<string> log;
        private int uniqueCardId;
        private bool stale;
        private int actionIndex;
        private List<PaymentVO> payment;
        private int selectedUniqueCardId;
        private CardState_Enum actionTaken;
        private int cardModifier = 0;
        private int selectedButtonIndex;
        private Action<GameAPI> finishCallback;
        private List<int> selectedCardIds;


        public PlayerData P { get => playerData; private set => playerData = value; }
        public Data G { get => g; private set => g = value; }
        public bool Status { get => status; set => status = value; }
        public string ErrorMsg { get => errorMsg; set { errorMsg = value; Status = false; } }
        public int UniqueCardId { get => uniqueCardId; set => uniqueCardId = value; }
        public int ActionIndex { get => actionIndex; set => actionIndex = value; }
        public List<PaymentVO> Payment { get => payment; private set => payment = value; }
        public int SelectedUniqueCardId { get => selectedUniqueCardId; set => selectedUniqueCardId = value; }
        public CardState_Enum ActionTaken { get => actionTaken; private set => actionTaken = value; }
        public int CardModifier { get => cardModifier; set => cardModifier = value; }
        public int SelectedButtonIndex { get => selectedButtonIndex; set => selectedButtonIndex = value; }
        public CardVO Card { get => card; set => card = value; }
        public Action<GameAPI> FinishCallback { get => finishCallback; set => finishCallback = value; }
        public List<int> SelectedCardIds { get => selectedCardIds; set => selectedCardIds = value; }

        public GameAPI(int uniqueCardId, CardState_Enum actionTaken, int actionIndex = -1) : this() {
            status = true;
            stale = false;
            ActionIndex = actionIndex;
            UniqueCardId = uniqueCardId;
            Card = D.Cards[UniqueCardId];
            switch (ActionIndex) {
                case 0: { FinishCallback = Card.ActionFinish_00; break; }
                case 1: { FinishCallback = Card.ActionFinish_01; break; }
                case 2: { FinishCallback = Card.ActionFinish_02; break; }
            }
            ActionTaken = actionTaken;
            if (Card.CardType == CardType_Enum.Tactics_Day || Card.CardType == CardType_Enum.Tactics_Night) {
                AddLog("[" + Card.CardTitle + "]");
            }
        }

        public GameAPI(Data gd, PlayerData localPlayer) {
            G = gd;
            P = localPlayer;
            log = new List<string>();
            selectedCardIds = new List<int>();
            pdClone = P.Clone();
        }

        public GameAPI() : this(D.G, D.LocalPlayer) { }

        #region SendData
        public void UpdateUI() {
            if (stale) {
                D.A.UpdateUI();
            }
        }
        public void Push() {
            if (stale) {
                stale = false;
                //D.C.Send_GameData();
                D.C.Send_PlayerData();
            }
        }
        public void PushForce(bool withLog = true) {
            stale = false;
            if (withLog) {
                D.C.LogMessage(string.Join(", ", log));
                log.Clear();
            }
            D.C.Send_PlayerData();
        }
        public void CompleteAction() {
            //if (Card.CardType == CardType_Enum.Spell) {
            //    P.GameEffects.Keys.ForEach(ge => {
            //        switch (ge) {
            //            case GameEffect_Enum.CT_RubyRing: { if (Card.CardColor == CardColor_Enum.Red) Fame(1); break; }
            //            case GameEffect_Enum.CT_EmeraldRing: { if (Card.CardColor == CardColor_Enum.Green) Fame(1); break; }
            //            case GameEffect_Enum.CT_SapphireRing: { if (Card.CardColor == CardColor_Enum.Blue) Fame(1); break; }
            //            case GameEffect_Enum.CT_DiamondRing: { if (Card.CardColor == CardColor_Enum.White) Fame(1); break; }
            //        }
            //    });
            //}
            D.C.LogMessage(string.Join(", ", log));
            Push();
        }
        public void Rollback() {
            if (stale) {
                D.G.Players.Find(p => p.Key == pdClone.Key).UpdateData(pdClone);
                Push();
            }
        }
        #endregion

        #region Game Updates
        public void change() {
            stale = true;
        }

        public void AddLog(string msg) {
            log.Add(msg);
        }

        public void TurnPhase(TurnPhase_Enum t) {
            if (P.PlayerTurnPhase < t) {
                if (t == TurnPhase_Enum.Influence) {
                    int val = BasicUtil.GetRepForLevel(P.RepLevel);
                    P.Influence += val;
                    log.Add((val > 0 ? "+" : "") + val + " Influence from Reputation Bonus!");
                }
                change();
                P.PlayerTurnPhase = t;
            }
        }
        public void AddCardState(int cardId, CardState_Enum actionTaken) {
            change();
            P.Deck.AddState(cardId, actionTaken);
            string cardTitle = D.Cards[cardId].CardTitle;
            switch (actionTaken) {
                case CardState_Enum.Discard: { log.Add("[Discard] " + cardTitle); break; }
                case CardState_Enum.Trashed: { log.Add("[Trashed] " + cardTitle); break; }
                case CardState_Enum.Basic: { log.Add("[Basic] " + cardTitle); break; }
                case CardState_Enum.Normal: { log.Add("[Normal] " + cardTitle); break; }
                case CardState_Enum.Advanced: { log.Add("[Advanced] " + cardTitle); break; }
                case CardState_Enum.Unit_Wounded: { log.Add("[Wounded] " + cardTitle); break; }
                case CardState_Enum.Unit_Exhausted: { log.Add("[Exhausted] " + cardTitle); break; }
            }
        }
        public void AddCardState() {
            P.ActionTaken = true;
            if (actionTaken != CardState_Enum.NA) {
                if (Card.CardType == CardType_Enum.Artifact) {
                    if (actionIndex == 1) {
                        AddCardState(UniqueCardId, CardState_Enum.Trashed);
                    } else {
                        if (Card.CardImage == Image_Enum.CT_banner_of_courage ||
                            Card.CardImage == Image_Enum.CT_banner_of_fear ||
                            Card.CardImage == Image_Enum.CT_banner_of_glory ||
                            Card.CardImage == Image_Enum.CT_banner_of_protection) {
                            AddCardState(UniqueCardId, CardState_Enum.Trashed);
                        } else {
                            AddCardState(UniqueCardId, actionTaken);
                        }
                    }
                } else {
                    AddCardState(UniqueCardId, actionTaken);
                }
            }
        }
        public void RemoveCardState(int cardId, CardState_Enum cardState) {
            change();
            P.Deck.State[cardId].Values.Remove(cardState);
            string cardTitle = Card.CardTitle;
            switch (cardState) {
                case CardState_Enum.Unit_Exhausted: { log.Add("Unit " + cardTitle + " no longer Exhausted"); break; }
                case CardState_Enum.Unit_Poisoned: { log.Add("Unit " + cardTitle + " no longer Poisoned"); break; }
                case CardState_Enum.Unit_Wounded: { log.Add("Unit " + cardTitle + " no longer Wounded"); break; }
            }
        }

        public void ActionMovement(int val) {
            change();
            P.Movement += val;
            log.Add((val > 0 ? "+" : "") + val + " Movement");
        }
        public void ActionInfluence(int val) {
            change();
            P.Influence += val;
            log.Add((val > 0 ? "+" : "") + val + " Influence");
        }
        public void BattleBlock(AttackData i) {
            change();
            BlockBattleEffect(i);
            P.Battle.Shield.Add(i);
            string msg = "";
            if (i.Physical > 0) {
                msg = "Block +" + i.Physical;
            } else if (i.Fire > 0) {
                msg = "Fire Block +" + i.Fire;
            } else if (i.Cold > 0) {
                msg = "Cold Block +" + i.Cold;
            } else if (i.Fire > 0) {
                msg = "ColdFire Block +" + i.ColdFire;
            }
            log.Add(msg);
        }
        public void BattleAttack(AttackData i) {
            change();
            AttackBattleEffect(i);
            P.Battle.Attack.Add(i);
            string msg = "";
            if (i.Physical > 0) {
                msg = "Attack +" + i.Physical;
            } else if (i.Fire > 0) {
                msg = "Fire Attack +" + i.Fire;
            } else if (i.Cold > 0) {
                msg = "Cold Attack +" + i.Cold;
            } else if (i.Fire > 0) {
                msg = "ColdFire Attack +" + i.ColdFire;
            }
            log.Add(msg);
        }
        public void BattleRange(AttackData i) {
            change();
            RangeBattleEffect(i);
            P.Battle.Range.Add(i);
            string msg = "";
            if (i.Physical > 0) {
                msg = "Range Attack +" + i.Physical;
            } else if (i.Fire > 0) {
                msg = "Fire Range Attack +" + i.Fire;
            } else if (i.Cold > 0) {
                msg = "Cold Range Attack +" + i.Cold;
            } else if (i.Fire > 0) {
                msg = "ColdFire Range Attack +" + i.ColdFire;
            }
            log.Add(msg);
        }
        public void BattleSiege(AttackData i) {
            change();
            SiegeBattleEffect(i);
            P.Battle.Siege.Add(i);
            string msg = "";
            if (i.Physical > 0) {
                msg = "Siege Attack +" + i.Physical;
            } else if (i.Fire > 0) {
                msg = "Fire Siege Attack +" + i.Fire;
            } else if (i.Cold > 0) {
                msg = "Cold Siege Attack +" + i.Cold;
            } else if (i.Fire > 0) {
                msg = "ColdFire Siege Attack +" + i.ColdFire;
            }
            log.Add(msg);
        }
        public void SetPayment(List<PaymentVO> payment) {
            change();
            Payment = payment;
            payment.ForEach(p => {
                switch (p.PaymentType) {
                    case PaymentVO.PaymentType_Enum.Crystal: {
                        P.Crystal.UseCrystal(p.Mana);
                        log.Add(p.Mana + " Crystal was used");
                        break;
                    }
                    case PaymentVO.PaymentType_Enum.Mana: {
                        P.Mana.UseCrystal(p.Mana);
                        log.Add(p.Mana + " Mana was used");
                        break;
                    }
                    case PaymentVO.PaymentType_Enum.ManaPool: {
                        P.ManaPoolAvailable--;
                        P.ManaPool.Find(mp => mp.ManaColor.Equals(p.Mana)).Status = ManaPool_Enum.Used;
                        log.Add(p.Mana + " Mana was used from the mana pool");
                        break;
                    }
                }
            });
        }
        public void Fame(int val) {
            change();
            P.Fame.Y += val;
            log.Add((val > 0 ? "+" : "") + val + " Fame");
            int oldLevel = BasicUtil.GetPlayerLevel(P.Fame.X);
            int newLevel = BasicUtil.GetPlayerLevel(P.TotalFame);
            if (newLevel > oldLevel) {
                Reward_LevelUp(1);
            }
        }


        public void Rep(int val) {
            change();
            P.RepLevel += val;
            log.Add((val > 0 ? "+" : "") + val + " Reputation");
        }
        public void Healing(int val) {
            change();
            P.Healpoints += val;
            log.Add((val > 0 ? "+" : "") + val + " Healing");
        }

        private int cardDraw_LongNight_drawn = 0;
        private int cardDraw_LongNight_owed = 0;
        private Action<GameAPI> cardDraw_LongNight_callback;

        public void DrawCard(int val, Action<GameAPI> callback) {
            change();
            int cardDrawn = 0;
            for (int i = 0; i < val; i++) {
                if (P.Deck.Deck.Count > 0) {
                    P.Deck.Hand.Add(BasicUtil.DrawCard(P.Deck.Deck));
                    cardDrawn++;
                }
            }
            if (cardDrawn < val && P.GameEffects.ContainsKey(GameEffect_Enum.T_LongNight)) {
                cardDraw_LongNight_callback = callback;
                cardDraw_LongNight_drawn = cardDrawn;
                cardDraw_LongNight_owed = val;
                SelectYesNo("Long Night", "Your deck is empty and you still have cards owed to you. Do you want to use Long Night?", DrawCard_LongNight_Yes, DrawCard_LongNight_No);
            } else {
                log.Add("+" + cardDrawn + " Card");
                callback(this);
            }
        }

        public void DrawCard_LongNight_Yes() {
            change();
            RemoveGameEffect(GameEffect_Enum.T_LongNight);
            P.Deck.Discard.ShuffleDeck();
            for (int i = 0; i < 3; i++) {
                P.Deck.Deck.Add(BasicUtil.DrawCard(P.Deck.Discard));
            }
            for (int i = cardDraw_LongNight_drawn; i < cardDraw_LongNight_owed; i++) {
                if (P.Deck.Deck.Count > 0) {
                    P.Deck.Hand.Add(BasicUtil.DrawCard(P.Deck.Deck));
                    cardDraw_LongNight_drawn++;
                }
            }
            log.Add("[Long Night]");
            log.Add("+" + cardDraw_LongNight_drawn + " Card");
            cardDraw_LongNight_callback(this);
        }
        public void DrawCard_LongNight_No() {
            log.Add("+" + cardDraw_LongNight_drawn + " Card");
            cardDraw_LongNight_callback(this);
        }


        public void AddWound(int val) {
            change();
            for (int i = 0; i < val; i++) {
                DrawWoundCard();
            }
            log.Add("+" + val + " Wound(s)");
        }

        public void AddCardToDiscardDeck(int cardid) {
            change();
            CardVO card = D.Cards[cardid];
            P.Deck.Discard.Add(cardid);
            log.Add(card.CardTitle + " added to discard pile!");
        }
        public void AddCardToHandDeck(int cardid) {
            change();
            CardVO card = D.Cards[cardid];
            P.Deck.Hand.Add(cardid);
            log.Add(card.CardTitle + " added your hand!");
        }

        public void AddCardToTopOfDeck(int cardid) {
            change();
            CardVO card = D.Cards[cardid];
            P.Deck.Deck.Insert(0, cardid);
            log.Add(card.CardTitle + " added to top of deck!");
        }
        public void AddSkill(int cardid) {
            change();
            CardVO card = D.Cards[cardid];
            P.Deck.Skill.Add(cardid);
            log.Add(card.CardTitle + " Skill Added!");
        }

        public void AddShieldLocation() {
            AddShieldLocation(P.CurrentGridLoc);
        }
        public void AddShieldLocation(V2IntVO pos) {
            change();
            P.Shields.Add(pos);
        }

        #region Crystal
        public void AddCrystal(Crystal_Enum crystal) {
            switch (crystal) {
                case Crystal_Enum.Green: { CrystalGreen(1); break; }
                case Crystal_Enum.Blue: { CrystalBlue(1); break; }
                case Crystal_Enum.Red: { CrystalRed(1); break; }
                case Crystal_Enum.White: { CrystalWhite(1); break; }
            }
        }
        public void AddMana(Crystal_Enum crystal) {
            switch (crystal) {
                case Crystal_Enum.Green: { ManaGreen(1); break; }
                case Crystal_Enum.Blue: { ManaBlue(1); break; }
                case Crystal_Enum.Red: { ManaRed(1); break; }
                case Crystal_Enum.White: { ManaWhite(1); break; }
                case Crystal_Enum.Gold: { ManaGold(1); break; }
                case Crystal_Enum.Black: { ManaBlack(1); break; }
            }
        }
        public void CrystalGreen(int val) {
            change();
            P.Crystal.Green += val;
            magicContainerMsg(val, "Green", "Crystal");
        }
        public void CrystalBlue(int val) {
            change();
            P.Crystal.Blue += val;
            magicContainerMsg(val, "Blue", "Crystal");
        }
        public void CrystalRed(int val) {
            change();
            P.Crystal.Red += val;
            magicContainerMsg(val, "Red", "Crystal");
        }
        public void CrystalWhite(int val) {
            change();
            P.Crystal.White += val;
        }

        public void AddGameEffect(GameEffect_Enum ge, params int[] cards) {
            change();
            P.AddGameEffect(ge, cards);
            D.B.UpdateMonsterDetails();
        }
        public void RemoveGameEffect(GameEffect_Enum ge) {
            change();
            P.RemoveGameEffect(ge);
            D.B.UpdateMonsterDetails();
        }
        public void RemoveGameEffect(GameEffect_Enum ge, int card) {
            change();
            P.RemoveGameEffect(ge, card);
            D.B.UpdateMonsterDetails();
        }

        public void AddBanner(int unitId, int bannerId) {
            change();
            if (P.Deck.Banners.ContainsKey(unitId)) {
                P.Deck.Banners.Remove(unitId);
            }
            P.Deck.Banners.Add(unitId, bannerId);
        }


        #endregion

        #region Mana
        public void ManaGreen(int val) {
            change();
            P.Mana.Green += val;
            magicContainerMsg(val, "Green", "Mana");
        }
        public void ManaBlue(int val) {
            change();
            P.Mana.Blue += val;
            magicContainerMsg(val, "Blue", "Mana");
        }
        public void ManaRed(int val) {
            change();
            P.Mana.Red += val;
            magicContainerMsg(val, "Red", "Mana");
        }
        public void ManaWhite(int val) {
            change();
            P.Mana.White += val;
            magicContainerMsg(val, "White", "Mana");
        }
        public void ManaGold(int val) {
            change();
            P.Mana.Gold += val;
            magicContainerMsg(val, "Gold", "Mana");
        }
        public void ManaBlack(int val) {
            change();
            P.Mana.Black += val;
            magicContainerMsg(val, "Black", "Mana");
        }
        #endregion

        #region ManaPool
        public void ManaPool(int val) {
            change();
            P.ManaPoolAvailable += val;
            log.Add((val > 0 ? "+" : "-") + val + " Mana Pool Available");
        }
        #endregion

        private void magicContainerMsg(int val, string color, string magicType) {
            log.Add((val > 0 ? "+" : "") + val + " " + color + " " + magicType);
        }

        public void SetCurrentLocation(V2IntVO pos) {
            change();
            P.GridLocationHistory.Insert(0, pos);
        }
        #endregion

        #region Panels

        public void PayForAction(List<Crystal_Enum> cost, Action<GameAPI> acceptCallback, bool all = true) {
            UpdateUI();
            D.Action.PayForAction(this, cost, (ar) => { ar.Rollback(); }, acceptCallback, all);
        }
        public void SelectOptions(Action<GameAPI> acceptCallback, Action<GameAPI> cancelCallback, params OptionVO[] options) {
            UpdateUI();
            D.Action.SelectOptions(this, cancelCallback, acceptCallback, options);
        }
        public void SelectOptions(Action<GameAPI> acceptCallback, params OptionVO[] options) {
            UpdateUI();
            D.Action.SelectOptions(this, (ar) => { ar.Rollback(); }, acceptCallback, options);
        }

        public void SelectSingleCard(Action<GameAPI> acceptCallback, bool allowNone = false) {
            UpdateUI();
            D.Action.SelectSingleCard(this, (ar) => { ar.Rollback(); }, acceptCallback, allowNone);
        }

        public void SelectCards(List<int> cards, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<GameAPI>> buttonCallback, List<bool> buttonForce) {
            UpdateUI();
            D.Action.SelectCards(this, cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
        }
        public void SelectLevelUp(Action<GameAPI> callback, List<int> actionOffering, List<int> skillOffering, List<int> skills) {
            UpdateUI();
            D.Action.SelectLevelUp(this, callback, actionOffering, skillOffering, skills);
        }

        public void SelectManaDie(List<Image_Enum> die, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<GameAPI>> buttonCallback, List<bool> buttonForce) {
            UpdateUI();
            D.Action.SelectManaDie(this, die, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
        }

        public void SelectYesNo(string title, string description, Action yes, Action no) {
            UpdateUI();
            D.Action.SelectYesNo(title, description, yes, no);
        }

        public void AcceptPanel(string head, string body, List<Action<GameAPI>> callbacks, List<string> buttonText, List<Color32> buttonColor, Color32 backgroundColor) {
            UpdateUI();
            D.Action.SelectAcceptPanel(this, head, body, callbacks, buttonText, buttonColor, backgroundColor);
        }

        public void StartOfTurnPanel(Action<GameAPI> callback) {
            BasicUtil.UpdateMovementGameEffects(P);
            List<string> startUpMsg = new List<string>();
            P.GameEffects.Keys.ForEach(ge => {
                switch (ge) {
                    case GameEffect_Enum.SH_MagicGlade: {
                        if (D.Scenario.isDay) {
                            startUpMsg.Add("[Magical Glade] +1 Gold Mana");
                            ManaGold(1);
                        } else {
                            startUpMsg.Add("[Magical Glade] +1 Black Mana");
                            ManaBlack(1);
                        }
                        break;
                    }
                    case GameEffect_Enum.SH_City_Red_Own:
                    case GameEffect_Enum.SH_City_Green_Own:
                    case GameEffect_Enum.SH_City_White_Own:
                    case GameEffect_Enum.SH_City_Blue_Own: {
                        if (P.Shields.Contains(P.CurrentGridLoc)) {
                            int influenceAdded = P.Shields.FindAll(pos => pos.Equals(P.CurrentGridLoc)).Count;
                            if (influenceAdded > 0) {
                                startUpMsg.Add("[City Influence] +" + influenceAdded);
                                ActionInfluence(influenceAdded);
                            }
                        }
                        break;
                    }
                }
            });
            bool forceDeclareEndOfRound = playerData.Deck.Deck.Count == 0 && playerData.Deck.Hand.Count == 0;
            bool isExhausted = true;
            playerData.Deck.Hand.ForEach(c => isExhausted = isExhausted && D.Cards[c].CardType == CardType_Enum.Wound);
            bool endOfRoundDeclared = D.G.EndOfRound;

            if (forceDeclareEndOfRound) {
                TurnPhase(TurnPhase_Enum.EndOfRound);
            } else {
                if (isExhausted) {
                    TurnPhase(TurnPhase_Enum.Exhaustion);
                    int cardid = P.Deck.Hand.Find(c => D.Cards[c].CardType == CardType_Enum.Wound);
                    if (cardid > 0) {
                        AddCardState(cardid, CardState_Enum.Discard);
                    }
                } else {
                    TurnPhase(TurnPhase_Enum.StartTurn);
                }
            }
            if (forceDeclareEndOfRound) {
                startUpMsg.Add("Both your deed deck and your hand are empty.  Your turn is forfeit and End of Round has been declared.");
            } else {
                if (endOfRoundDeclared) {
                    startUpMsg.Add("End of Round has been declared.  This will be your last turn.");
                }
                if (isExhausted) {
                    startUpMsg.Add("You are Exhausted, you started your hand without any NON Wound cards in your hand.  You will not be able to take any Move, Influence, or Battle actions this turn, if possible one wound card will be discarded.");
                }
            }
            string head = "Start of Turn";
            string body = "It is your turn!\n";
            startUpMsg.ForEach(s => body += ("\n" + s));
            List<Action<GameAPI>> callbacks = new List<Action<GameAPI>>() { callback };
            List<string> buttonText = new List<string>() { "Okay" };
            List<Color32> buttonColor = new List<Color32>() { CNAColor.ColorLightGreen };
            Color32 backgroundColor = CNAColor.BlueBackgroundColor;

            AcceptPanel(head, body, callbacks, buttonText, buttonColor, backgroundColor);
        }

        public void WaitOnServerPanel() {
            UpdateUI();
            D.Action.WaitOnServerPanel(this);
        }
        #endregion

        #region BattleEffects
        public void BlockBattleEffect(AttackData i) {
            AC_Ambush01(i);
            AC_IntoTheHeat(i);
            BLUE_Leadership(i);
            CT_BannerOfGlory(i);
        }
        public void RangeBattleEffect(AttackData i) {
            AC_Ambush01(i);
            AC_IntoTheHeat(i);
            BLUE_Leadership(i);
            CT_BannerOfGlory(i);
            CUE_AltemMages01(i);
        }
        public void SiegeBattleEffect(AttackData i) {
            AC_Ambush01(i);
            AC_IntoTheHeat(i);
            CT_BannerOfGlory(i);
            CUE_AltemMages01(i);
        }
        public void AttackBattleEffect(AttackData i) {
            AC_Ambush01(i);
            AC_IntoTheHeat(i);
            BLUE_Leadership(i);
            CT_BannerOfGlory(i);
            CT_SwordOfJustice(i);
            CUE_AltemMages01(i);
        }

        private void CUE_AltemMages01(AttackData i) {
            if (P.GameEffects.ContainsKey(GameEffect_Enum.CUE_AltemMages01)) {
                i.ColdFire += (i.Physical + i.Cold + i.Fire);
                i.Physical = 0;
                i.Cold = 0;
                i.Fire = 0;
                log.Add("[Altem Mages] Cold Fire!");
            }
        }

        private void CT_SwordOfJustice(AttackData i) {
            if (P.GameEffects.ContainsKey(GameEffect_Enum.CT_SwordOfJustice02)) {
                i.Physical *= 2;
                log.Add("[Sword of Justice] doubles attack!");
            }
        }

        private void CT_BannerOfGlory(AttackData i) {
            int count = 0;
            if (P.Deck.Banners.ContainsKey(UniqueCardId)) {
                int bannerUniqueId = P.Deck.Banners[UniqueCardId];
                CardVO banner = D.Cards.Find(c => c.UniqueId == bannerUniqueId);
                if (banner.CardImage == Image_Enum.CT_banner_of_glory) {
                    count++;
                }
            }
            if (P.GameEffects.ContainsKey(GameEffect_Enum.CT_BannerOfGlory)) {
                count += P.GameEffects[GameEffect_Enum.CT_BannerOfGlory].Count;
            }
            if (count > 0) {
                if (i.Physical > 0) i.Physical += count;
                if (i.Fire > 0) i.Fire += count;
                if (i.Cold > 0) i.Cold += count;
                if (i.ColdFire > 0) i.ColdFire += count;
                log.Add("[Glory] +" + count + " attack/block");
                Fame(count);
            }
        }

        private void AC_Ambush01(AttackData i) {
            bool normal = P.GameEffects.ContainsKey(GameEffect_Enum.AC_Ambush01);
            bool advanced = P.GameEffects.ContainsKey(GameEffect_Enum.AC_Ambush02);
            if (normal || advanced) {
                if (Card.CardType == CardType_Enum.BasicAction ||
                     Card.CardType == CardType_Enum.Basic ||
                     Card.CardType == CardType_Enum.Advanced ||
                     Card.CardType == CardType_Enum.Spell ||
                     Card.CardType == CardType_Enum.Artifact
                     ) {
                    int val = 0;
                    switch (P.Battle.BattlePhase) {
                        case BattlePhase_Enum.Block: { val = advanced ? 4 : 2; break; }
                        case BattlePhase_Enum.RangeSiege:
                        case BattlePhase_Enum.Attack: { val = advanced ? 2 : 1; break; }
                    }
                    if (i.Physical > 0) i.Physical += val;
                    if (i.Fire > 0) i.Fire += val;
                    if (i.Cold > 0) i.Cold += val;
                    if (i.ColdFire > 0) i.ColdFire += val;
                    P.RemoveGameEffect(advanced ? GameEffect_Enum.AC_Ambush02 : GameEffect_Enum.AC_Ambush01);
                    log.Add("[Ambush]");
                }
            }
        }

        private void AC_IntoTheHeat(AttackData i) {
            bool normal = P.GameEffects.ContainsKey(GameEffect_Enum.AC_IntoTheHeat01);
            bool advanced = P.GameEffects.ContainsKey(GameEffect_Enum.AC_IntoTheHeat02);
            if (normal || advanced) {
                if (Card.CardType == CardType_Enum.Unit_Normal || Card.CardType == CardType_Enum.Unit_Elite) {
                    int count;
                    if (normal) {
                        count = P.GameEffects[GameEffect_Enum.AC_IntoTheHeat01].Count;
                    } else {
                        count = P.GameEffects[GameEffect_Enum.AC_IntoTheHeat02].Count;
                    }
                    int val = normal ? (count * 2) : (count * 3);
                    if (i.Physical > 0) i.Physical += val;
                    if (i.Fire > 0) i.Fire += val;
                    if (i.Cold > 0) i.Cold += val;
                    if (i.ColdFire > 0) i.ColdFire += val;
                    log.Add("[Into The Heat]");
                }
            }
        }

        private void BLUE_Leadership(AttackData i) {
            bool leadership = P.GameEffects.ContainsKey(GameEffect_Enum.WHITE_Leadership);
            if (leadership) {
                if (Card.CardType == CardType_Enum.Unit_Normal || Card.CardType == CardType_Enum.Unit_Elite) {
                    int count = P.GameEffects[GameEffect_Enum.WHITE_Leadership].Count;
                    int val = 0;
                    switch (P.Battle.BattlePhase) {
                        case BattlePhase_Enum.Block: { val = 3 * count; break; }
                        case BattlePhase_Enum.RangeSiege: { val = 1 * count; break; }
                        case BattlePhase_Enum.Attack: { val = 2 * count; break; }
                    }
                    if (i.Physical > 0) i.Physical += val;
                    if (i.Fire > 0) i.Fire += val;
                    if (i.Cold > 0) i.Cold += val;
                    if (i.ColdFire > 0) i.ColdFire += val;
                    P.RemoveGameEffect(GameEffect_Enum.WHITE_Leadership);
                    log.Add("[Leadership]");
                }
            }
        }

        #endregion

        #region Reward
        public void Reward_Add(int blue, int red, int green, int white, int random, int artifact, int spell, int advanced, int unit, int level) {
            if (!P.GameEffects.ContainsKey(GameEffect_Enum.Rewards)) {
                AddGameEffect(GameEffect_Enum.Rewards, blue, red, green, white, random, 0, 0, artifact, spell, advanced, unit, level);
            } else {
                List<int> ged = P.GameEffects[GameEffect_Enum.Rewards].Values;
                ged[0] += blue;
                ged[1] += red;
                ged[2] += green;
                ged[3] += white;
                ged[4] += random;
                ged[7] += artifact;
                ged[8] += spell;
                ged[9] += advanced;
                ged[10] += unit;
                ged[11] += level;
            }
        }

        public void Reward_Blue(int val) {
            Reward_Add(val, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }
        public void Reward_Red(int val) {
            Reward_Add(0, val, 0, 0, 0, 0, 0, 0, 0, 0);
        }
        public void Reward_Green(int val) {
            Reward_Add(0, 0, val, 0, 0, 0, 0, 0, 0, 0);
        }
        public void Reward_White(int val) {
            Reward_Add(0, 0, 0, val, 0, 0, 0, 0, 0, 0);
        }
        public void Reward_Random(int val) {
            Reward_Add(0, 0, 0, 0, val, 0, 0, 0, 0, 0);
        }
        public void Reward_Artifact(int val) {
            Reward_Add(0, 0, 0, 0, 0, val, 0, 0, 0, 0);
        }
        public void Reward_Spell(int val) {
            Reward_Add(0, 0, 0, 0, 0, 0, val, 0, 0, 0);
        }
        public void Reward_Advanced(int val) {
            Reward_Add(0, 0, 0, 0, 0, 0, 0, val, 0, 0);
        }
        public void Reward_Unit(int val) {
            Reward_Add(0, 0, 0, 0, 0, 0, 0, 0, val, 0);
        }
        public void Reward_LevelUp(int val) {
            Reward_Add(0, 0, 0, 0, 0, 0, 0, 0, 0, val);
        }
        #endregion

        #region Dummy
        public void Dummy() {
            string msg;
            if (P.Deck.Deck.Count == 0) {
                msg = "Dummy Player Declared End of Round";
                TurnPhase(TurnPhase_Enum.EndOfRound);
            } else {
                TurnPhase(TurnPhase_Enum.EndTurn);
                CardColor_Enum cardColor = CardColor_Enum.NA;
                int totalCards = 0;
                int cardId = 0;
                for (int i = 0; i < 3; i++) {
                    if (P.Deck.Deck.Count > 0) {
                        cardId = BasicUtil.DrawCard(P.Deck.Deck);
                        P.Deck.Discard.Add(cardId);
                        totalCards++;
                    }
                }
                if (P.Deck.Deck.Count > 0) {
                    int extra = 0;
                    cardColor = D.Cards[cardId].CardColor;
                    switch (cardColor) {
                        case CardColor_Enum.Blue: { extra = P.Crystal.Blue; break; }
                        case CardColor_Enum.Red: { extra = P.Crystal.Red; break; }
                        case CardColor_Enum.Green: { extra = P.Crystal.Green; break; }
                        case CardColor_Enum.White: { extra = P.Crystal.White; break; }
                    }
                    for (int i = 0; i < extra; i++) {
                        if (P.Deck.Deck.Count > 0) {
                            cardId = BasicUtil.DrawCard(P.Deck.Deck);
                            P.Deck.Discard.Add(cardId);
                            totalCards++;
                        }
                    }
                }
                msg = "Total Cards " + totalCards;
                if (cardColor != CardColor_Enum.NA) {
                    msg += " (" + cardColor + ")";
                }
            }
            D.C.LogMessageDummy(msg);
            D.C.Send_HostSendsPlayerDataToClients(P);
            D.A.UpdateUI();
        }
        #endregion

        #region PlayerWorkflow
        public void PlayerNewRoundSetup() {
            P.Deck.ClearNewRound();
            foreach (GameEffect_Enum ge in P.GameEffects.Keys.ToArray()) {
                CardVO c = D.GetGameEffectCard(ge);
                if (c.GameEffectDurationId != GameEffectDuration_Enum.Game) {
                    P.RemoveGameEffect(ge);
                }
            }
            P.PlayerTurnPhase = TurnPhase_Enum.TacticsNotTurn;
            P.Deck.Deck.ShuffleDeck();
            if (!P.DummyPlayer) {
                P.ClearEndTurn();
                P.AddGameEffect(D.Scenario.isDay ? GameEffect_Enum.Day : GameEffect_Enum.Night);
                drawHand(P.Deck);
                if (D.Scenario.isDay) {
                    P.Board.MonsterData.Keys.ForEach(pos => {
                        P.Board.MonsterData[pos].Values.ForEach(r => {
                            if (D.Cards[r].CardType != CardType_Enum.Monster) {
                                P.VisableMonsters.Add(r);
                            }
                        });
                    });
                }
            }
        }
        public void PlayerEndOfTurn() {
            if (P.PlayerTurnPhase == TurnPhase_Enum.EndTurn) {
                P.PlayerTurnPhase = TurnPhase_Enum.NotTurn;
                if (!P.DummyPlayer) {
                    clearHand(P.Deck, P.GameEffects);
                    resetUnit(P.Deck);
                    resetSkill(P.Deck, false);
                    int cardsLeftInHand = P.Deck.Hand.Count;
                    drawHand(P.Deck);
                    foreach (GameEffect_Enum ge in P.GameEffects.Keys.ToArray()) {
                        CardVO c = D.GetGameEffectCard(ge);
                        switch (ge) {
                            case GameEffect_Enum.AC_CrystalMastery: {
                                CrystalData crystal = P.Crystal;
                                crystal.Blue += crystal.SpentBlue;
                                crystal.Green += crystal.SpentGreen;
                                crystal.Red += crystal.SpentRed;
                                crystal.White += crystal.SpentWhite;
                                break;
                            }
                            case GameEffect_Enum.SH_CrystalMines_Blue: {
                                D.C.LogMessage("[Blue Crystal Mine] +1 Blue Crystal");
                                P.Crystal.Blue++;
                                break;
                            }
                            case GameEffect_Enum.SH_CrystalMines_Red: {
                                D.C.LogMessage("[Red Crystal Mine] +1 Red Crystal");
                                P.Crystal.Red++;
                                break;
                            }
                            case GameEffect_Enum.SH_CrystalMines_Green: {
                                D.C.LogMessage("[Green Crystal Mine] +1 Green Crystal");
                                P.Crystal.Green++;
                                break;
                            }
                            case GameEffect_Enum.SH_CrystalMines_White: {
                                D.C.LogMessage("[White Crystal Mine] +1 White Crystal");
                                P.Crystal.White++;
                                break;
                            }
                            case GameEffect_Enum.T_Planning: {
                                if (cardsLeftInHand >= 2) {
                                    D.C.LogMessage("[Planning] +1 Card");
                                    if (P.Deck.Deck.Count > 0) {
                                        P.Deck.Hand.Add(BasicUtil.DrawCard(P.Deck.Deck));
                                    }
                                }
                                break;
                            }
                            case GameEffect_Enum.T_ManaSearch02: {
                                P.RemoveGameEffect(GameEffect_Enum.T_ManaSearch02);
                                P.AddGameEffect(GameEffect_Enum.T_ManaSearch01);
                                break;
                            }
                        }
                        if (c.GameEffectDurationId == GameEffectDuration_Enum.Turn) {
                            P.RemoveGameEffect(ge);
                        }
                    }
                    P.ClearEndTurn();
                }
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
        private void drawHand(PlayerDeckData p) {
            int handLimit = p.TotalHandSize;
            while (p.Deck.Count > 0 && p.Hand.Count < handLimit) {
                p.Hand.Add(BasicUtil.DrawCard(p.Deck));
            }
        }
        #endregion

        #region ManageCardDecks

        public void removeFromSpellOffering(int cardid) {
            P.Board.SpellOffering.Remove(cardid);
        }
        public void drawSpellToOffering() {
            if (P.Board.SpellIndex < D.Scenario.SpellDeck.Count) {
                P.Board.SpellOffering.Add(D.Scenario.SpellDeck[P.Board.SpellIndex]);
                P.Board.SpellIndex++;
            }
        }
        public void removeFromAdvancedOffering(int cardid) {
            P.Board.AdvancedOffering.Remove(cardid);
        }
        public void drawAdvancedToOffering() {
            if (P.Board.AdvancedIndex < D.Scenario.AdvancedDeck.Count) {
                P.Board.AdvancedOffering.Add(D.Scenario.AdvancedDeck[P.Board.AdvancedIndex]);
                P.Board.AdvancedIndex++;
            }
        }
        public void removeFromUnitOffering(int cardid) {
            P.Board.UnitOffering.Remove(cardid);
        }
        public int drawEliteUnitToOffering() {
            int eliteCardId = 0;
            if (P.Board.UnitEliteIndex < D.Scenario.UnitEliteDeck.Count) {
                eliteCardId = D.Scenario.UnitEliteDeck[P.Board.UnitEliteIndex];
                P.Board.UnitOffering.Add(eliteCardId);
                P.Board.UnitEliteIndex++;
            }
            return eliteCardId;
        }
        public int DrawBlueSkillCard() {
            int card = 0;
            if (P.Board.SkillBlueIndex < D.Scenario.BlueSkillDeck.Count) {
                card = D.Scenario.BlueSkillDeck[P.Board.SkillBlueIndex];
                P.Board.SkillBlueIndex++;
            }
            return card;
        }
        public int DrawGreenSkillCard() {
            int card = 0;
            if (P.Board.SkillGreenIndex < D.Scenario.GreenSkillDeck.Count) {
                card = D.Scenario.GreenSkillDeck[P.Board.SkillGreenIndex];
                P.Board.SkillGreenIndex++;
            }
            return card;
        }
        public int DrawRedSkillCard() {
            int card = 0;
            if (P.Board.SkillRedIndex < D.Scenario.RedSkillDeck.Count) {
                card = D.Scenario.RedSkillDeck[P.Board.SkillRedIndex];
                P.Board.SkillRedIndex++;
            }
            return card;
        }
        public int DrawWhiteSkillCard() {
            int card = 0;
            if (P.Board.SkillWhiteIndex < D.Scenario.WhiteSkillDeck.Count) {
                card = D.Scenario.WhiteSkillDeck[P.Board.SkillWhiteIndex];
                P.Board.SkillWhiteIndex++;
            }
            return card;
        }
        public int DrawWoundCard() {
            int woundId = D.Scenario.WoundDeck[P.Board.WoundIndex];
            P.Board.WoundIndex++;
            return woundId;
        }
        #endregion
    }
}
