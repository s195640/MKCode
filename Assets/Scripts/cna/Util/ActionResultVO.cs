using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    public class ActionResultVO {
        #region quickAccess
        private GameData g;
        private PlayerData localPlayer;
        #endregion


        private GameData gdClone;
        private CardVO card;
        private bool mustRestore;
        private bool status;
        private bool cloneCreated;
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
        private Action<ActionResultVO> finishCallback;
        private List<int> selectedCardIds;


        public PlayerData LocalPlayer { get => localPlayer; private set => localPlayer = value; }
        public GameData G { get => g; private set => g = value; }
        public bool Status { get => status; set => status = value; }
        public string ErrorMsg { get => errorMsg; set { errorMsg = value; Status = false; } }
        public bool MustRestore { get => mustRestore; set => mustRestore = value; }
        public int UniqueCardId { get => uniqueCardId; set => uniqueCardId = value; }
        public int ActionIndex { get => actionIndex; set => actionIndex = value; }
        public List<PaymentVO> Payment { get => payment; private set => payment = value; }
        public int SelectedUniqueCardId { get => selectedUniqueCardId; set => selectedUniqueCardId = value; }
        public CardState_Enum ActionTaken { get => actionTaken; private set => actionTaken = value; }
        public int CardModifier { get => cardModifier; set => cardModifier = value; }
        public int SelectedButtonIndex { get => selectedButtonIndex; set => selectedButtonIndex = value; }
        public CardVO Card { get => card; set => card = value; }
        public Action<ActionResultVO> FinishCallback { get => finishCallback; set => finishCallback = value; }
        public List<int> SelectedCardIds { get => selectedCardIds; set => selectedCardIds = value; }

        public ActionResultVO(int uniqueCardId, CardState_Enum actionTaken, int actionIndex = -1) : this() {
            mustRestore = false;
            status = true;
            stale = false;
            cloneCreated = false;
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

        public ActionResultVO(GameData gd, int uniqueCardId, CardState_Enum actionTaken, int actionIndex) : this(uniqueCardId, actionTaken, actionIndex) {
            gdClone = gd;
            cloneCreated = true;
        }

        public ActionResultVO() {
            G = D.G;
            log = new List<string>();
            selectedCardIds = new List<int>();
            LocalPlayer = D.LocalPlayer;
        }

        #region SendData
        public void Push() {
            if (stale) {
                stale = false;
                D.C.Send_GameData();
            }
        }
        public void CompleteAction() {
            if (Card.CardType == CardType_Enum.Spell) {
                LocalPlayer.GameEffects.Keys.ForEach(ge => {
                    switch (ge) {
                        case GameEffect_Enum.CT_RubyRing: { if (Card.CardColor == CardColor_Enum.Red) Fame(1); break; }
                        case GameEffect_Enum.CT_EmeraldRing: { if (Card.CardColor == CardColor_Enum.Green) Fame(1); break; }
                        case GameEffect_Enum.CT_SapphireRing: { if (Card.CardColor == CardColor_Enum.Blue) Fame(1); break; }
                        case GameEffect_Enum.CT_DiamondRing: { if (Card.CardColor == CardColor_Enum.White) Fame(1); break; }
                    }
                });
            }
            D.C.LogMessage(string.Join(", ", log));
            if (stale) {
                stale = false;
                D.C.Send_GameData();
            }
        }
        public void Rollback() {
            if (cloneCreated && mustRestore) {
                D.G = gdClone;
                D.C.Send_GameData();
            }
        }
        #endregion

        #region Game Updates
        public void change() {
            stale = true;
            mustRestore = true;
        }

        public void AddLog(string msg) {
            log.Add(msg);
        }

        public void TurnPhase(TurnPhase_Enum t) {
            if (LocalPlayer.PlayerTurnPhase < t) {
                if (t == TurnPhase_Enum.Influence) {
                    int val = BasicUtil.GetRepForLevel(LocalPlayer.RepLevel);
                    LocalPlayer.Influence += val;
                    log.Add((val > 0 ? "+" : "") + val + " Influence from Reputation Bonus!");
                }
                change();
                LocalPlayer.PlayerTurnPhase = t;
            }
        }
        public void AddCardState(int cardId, CardState_Enum actionTaken) {
            change();
            LocalPlayer.Deck.AddState(cardId, actionTaken);
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
            LocalPlayer.ActionTaken = true;
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
            LocalPlayer.Deck.State[cardId].Values.Remove(cardState);
            string cardTitle = Card.CardTitle;
            switch (cardState) {
                case CardState_Enum.Unit_Exhausted: { log.Add("Unit " + cardTitle + " no longer Exhausted"); break; }
                case CardState_Enum.Unit_Poisoned: { log.Add("Unit " + cardTitle + " no longer Poisoned"); break; }
                case CardState_Enum.Unit_Wounded: { log.Add("Unit " + cardTitle + " no longer Wounded"); break; }
            }
        }

        public void ActionMovement(int val) {
            change();
            LocalPlayer.Movement += val;
            log.Add((val > 0 ? "+" : "") + val + " Move");
        }
        public void ActionInfluence(int val) {
            change();
            LocalPlayer.Influence += val;
            log.Add((val > 0 ? "+" : "") + val + " Influence");
        }
        public void BattleBlock(AttackData i) {
            change();
            BlockBattleEffect(i);
            LocalPlayer.Battle.Shield.Add(i);
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
            LocalPlayer.Battle.Attack.Add(i);
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
            LocalPlayer.Battle.Range.Add(i);
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
            LocalPlayer.Battle.Siege.Add(i);
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
                        LocalPlayer.Crystal.UseCrystal(p.Mana);
                        log.Add(p.Mana + " Crystal was used");
                        break;
                    }
                    case PaymentVO.PaymentType_Enum.Mana: {
                        LocalPlayer.Mana.UseCrystal(p.Mana);
                        log.Add(p.Mana + " Mana was used");
                        break;
                    }
                    case PaymentVO.PaymentType_Enum.ManaPool: {
                        LocalPlayer.ManaPoolAvailable--;
                        G.Board.ManaPool.Remove(p.Mana);
                        log.Add(p.Mana + " Mana was used from the mana pool");
                        break;
                    }
                }
            });
        }
        public void Fame(int val) {
            change();
            LocalPlayer.Fame.Y += val;
            log.Add((val > 0 ? "+" : "") + val + " Fame");
            int oldLevel = BasicUtil.GetPlayerLevel(LocalPlayer.Fame.X);
            int newLevel = BasicUtil.GetPlayerLevel(LocalPlayer.TotalFame);
            if (newLevel > oldLevel) {
                Reward_LevelUp(1);
            }
        }


        public void Rep(int val) {
            change();
            LocalPlayer.RepLevel += val;
            log.Add((val > 0 ? "+" : "") + val + " Reputation");
        }
        public void Healing(int val) {
            change();
            LocalPlayer.Healpoints += val;
            log.Add((val > 0 ? "+" : "") + val + " Healing");
        }

        private int cardDraw_LongNight_drawn = 0;
        private int cardDraw_LongNight_owed = 0;
        private Action<ActionResultVO> cardDraw_LongNight_callback;

        public void DrawCard(int val, Action<ActionResultVO> callback) {
            change();
            int cardDrawn = 0;
            for (int i = 0; i < val; i++) {
                if (LocalPlayer.Deck.Deck.Count > 0) {
                    LocalPlayer.Deck.Hand.Add(BasicUtil.DrawCard(LocalPlayer.Deck.Deck));
                    cardDrawn++;
                }
            }
            if (cardDrawn < val && LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.T_LongNight)) {
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
            LocalPlayer.Deck.Discard.ShuffleDeck();
            for (int i = 0; i < 3; i++) {
                LocalPlayer.Deck.Deck.Add(BasicUtil.DrawCard(LocalPlayer.Deck.Discard));
            }
            for (int i = cardDraw_LongNight_drawn; i < cardDraw_LongNight_owed; i++) {
                if (LocalPlayer.Deck.Deck.Count > 0) {
                    LocalPlayer.Deck.Hand.Add(BasicUtil.DrawCard(LocalPlayer.Deck.Deck));
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
                LocalPlayer.Deck.Hand.Add(D.Scenario.DrawWound());
            }
            log.Add("+" + val + " Wound(s)");
        }

        public void AddCardToDiscardDeck(int cardid) {
            change();
            CardVO card = D.Cards[cardid];
            LocalPlayer.Deck.Discard.Add(cardid);
            log.Add(card.CardTitle + " added to discard pile!");
        }
        public void AddCardToHandDeck(int cardid) {
            change();
            CardVO card = D.Cards[cardid];
            LocalPlayer.Deck.Hand.Add(cardid);
            log.Add(card.CardTitle + " added your hand!");
        }

        public void AddCardToTopOfDeck(int cardid) {
            change();
            CardVO card = D.Cards[cardid];
            LocalPlayer.Deck.Deck.Insert(0, cardid);
            log.Add(card.CardTitle + " added to top of deck!");
        }
        public void AddSkill(int cardid) {
            change();
            CardVO card = D.Cards[cardid];
            LocalPlayer.Deck.Skill.Add(cardid);
            log.Add(card.CardTitle + " Skill Added!");
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
            LocalPlayer.Crystal.Green += val;
            magicContainerMsg(val, "Green", "Crystal");
        }
        public void CrystalBlue(int val) {
            change();
            LocalPlayer.Crystal.Blue += val;
            magicContainerMsg(val, "Blue", "Crystal");
        }
        public void CrystalRed(int val) {
            change();
            LocalPlayer.Crystal.Red += val;
            magicContainerMsg(val, "Red", "Crystal");
        }
        public void CrystalWhite(int val) {
            change();
            LocalPlayer.Crystal.White += val;
        }

        public void AddGameEffect(GameEffect_Enum ge, params int[] cards) {
            change();
            LocalPlayer.AddGameEffect(ge, cards);
            D.B.UpdateMonsterDetails();
        }
        public void RemoveGameEffect(GameEffect_Enum ge) {
            change();
            LocalPlayer.RemoveGameEffect(ge);
            D.B.UpdateMonsterDetails();
        }
        public void RemoveGameEffect(GameEffect_Enum ge, int card) {
            change();
            LocalPlayer.RemoveGameEffect(ge, card);
            D.B.UpdateMonsterDetails();
        }

        public void AddBanner(int unitId, int bannerId) {
            change();
            if (LocalPlayer.Deck.Banners.ContainsKey(unitId)) {
                LocalPlayer.Deck.Banners.Remove(unitId);
            }
            LocalPlayer.Deck.Banners.Add(unitId, bannerId);
        }


        #endregion
        #region Mana
        public void ManaGreen(int val) {
            change();
            LocalPlayer.Mana.Green += val;
            magicContainerMsg(val, "Green", "Mana");
        }
        public void ManaBlue(int val) {
            change();
            LocalPlayer.Mana.Blue += val;
            magicContainerMsg(val, "Blue", "Mana");
        }
        public void ManaRed(int val) {
            change();
            LocalPlayer.Mana.Red += val;
            magicContainerMsg(val, "Red", "Mana");
        }
        public void ManaWhite(int val) {
            change();
            LocalPlayer.Mana.White += val;
            magicContainerMsg(val, "White", "Mana");
        }
        public void ManaGold(int val) {
            change();
            LocalPlayer.Mana.Gold += val;
            magicContainerMsg(val, "Gold", "Mana");
        }
        public void ManaBlack(int val) {
            change();
            LocalPlayer.Mana.Black += val;
            magicContainerMsg(val, "Black", "Mana");
        }
        #endregion
        #region ManaPool
        public void ManaPool(int val) {
            change();
            LocalPlayer.ManaPoolAvailable += val;
            log.Add((val > 0 ? "+" : "-") + val + " Mana Pool Available");
        }
        #endregion
        private void magicContainerMsg(int val, string color, string magicType) {
            log.Add((val > 0 ? "+" : "") + val + " " + color + " " + magicType);
        }
        #endregion

        #region Panels

        public void PayForAction(List<Crystal_Enum> cost, Action<ActionResultVO> acceptCallback, bool all = true) {
            Push();
            D.Action.PayForAction(this, cost, (ar) => { ar.Rollback(); }, acceptCallback, all);
        }
        public void SelectOptions(Action<ActionResultVO> acceptCallback, Action<ActionResultVO> cancelCallback, params OptionVO[] options) {
            Push();
            D.Action.SelectOptions(this, cancelCallback, acceptCallback, options);
        }
        public void SelectOptions(Action<ActionResultVO> acceptCallback, params OptionVO[] options) {
            Push();
            D.Action.SelectOptions(this, (ar) => { ar.Rollback(); }, acceptCallback, options);
        }

        public void SelectSingleCard(Action<ActionResultVO> acceptCallback, bool allowNone = false) {
            Push();
            D.Action.SelectSingleCard(this, (ar) => { ar.Rollback(); }, acceptCallback, allowNone);
        }

        public void SelectCards(List<int> cards, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<ActionResultVO>> buttonCallback, List<bool> buttonForce) {
            Push();
            D.Action.SelectCards(this, cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
        }
        public void SelectLevelUp(Action<ActionResultVO> callback, List<int> actionOffering, List<int> skillOffering, List<int> skills) {
            Push();
            D.Action.SelectLevelUp(this, callback, actionOffering, skillOffering, skills);
        }

        public void SelectManaDie(List<Image_Enum> die, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<ActionResultVO>> buttonCallback, List<bool> buttonForce) {
            Push();
            D.Action.SelectManaDie(this, die, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
        }

        public void SelectYesNo(string title, string description, Action yes, Action no) {
            Push();
            D.Action.SelectYesNo(title, description, yes, no);
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
            if (LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.CUE_AltemMages01)) {
                i.ColdFire += (i.Physical + i.Cold + i.Fire);
                i.Physical = 0;
                i.Cold = 0;
                i.Fire = 0;
                log.Add("[Altem Mages] Cold Fire!");
            }
        }

        private void CT_SwordOfJustice(AttackData i) {
            if (LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.CT_SwordOfJustice02)) {
                i.Physical *= 2;
                log.Add("[Sword of Justice] doubles attack!");
            }
        }

        private void CT_BannerOfGlory(AttackData i) {
            int count = 0;
            if (LocalPlayer.Deck.Banners.ContainsKey(UniqueCardId)) {
                int bannerUniqueId = LocalPlayer.Deck.Banners[UniqueCardId];
                CardVO banner = D.Cards.Find(c => c.UniqueId == bannerUniqueId);
                if (banner.CardImage == Image_Enum.CT_banner_of_glory) {
                    count++;
                }
            }
            if (LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.CT_BannerOfGlory)) {
                count += LocalPlayer.GameEffects[GameEffect_Enum.CT_BannerOfGlory].Count;
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
            bool normal = LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.AC_Ambush01);
            bool advanced = LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.AC_Ambush02);
            if (normal || advanced) {
                if (Card.CardType == CardType_Enum.BasicAction ||
                     Card.CardType == CardType_Enum.Basic ||
                     Card.CardType == CardType_Enum.Advanced ||
                     Card.CardType == CardType_Enum.Spell ||
                     Card.CardType == CardType_Enum.Artifact
                     ) {
                    int val = 0;
                    switch (LocalPlayer.Battle.BattlePhase) {
                        case BattlePhase_Enum.Block: { val = advanced ? 4 : 2; break; }
                        case BattlePhase_Enum.RangeSiege:
                        case BattlePhase_Enum.Attack: { val = advanced ? 2 : 1; break; }
                    }
                    if (i.Physical > 0) i.Physical += val;
                    if (i.Fire > 0) i.Fire += val;
                    if (i.Cold > 0) i.Cold += val;
                    if (i.ColdFire > 0) i.ColdFire += val;
                    LocalPlayer.RemoveGameEffect(advanced ? GameEffect_Enum.AC_Ambush02 : GameEffect_Enum.AC_Ambush01);
                    log.Add("[Ambush]");
                }
            }
        }

        private void AC_IntoTheHeat(AttackData i) {
            bool normal = LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.AC_IntoTheHeat01);
            bool advanced = LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.AC_IntoTheHeat02);
            if (normal || advanced) {
                if (Card.CardType == CardType_Enum.Unit_Normal || Card.CardType == CardType_Enum.Unit_Elite) {
                    int count;
                    if (normal) {
                        count = LocalPlayer.GameEffects[GameEffect_Enum.AC_IntoTheHeat01].Count;
                    } else {
                        count = LocalPlayer.GameEffects[GameEffect_Enum.AC_IntoTheHeat02].Count;
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
            bool leadership = LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.WHITE_Leadership);
            if (leadership) {
                if (Card.CardType == CardType_Enum.Unit_Normal || Card.CardType == CardType_Enum.Unit_Elite) {
                    int count = LocalPlayer.GameEffects[GameEffect_Enum.WHITE_Leadership].Count;
                    int val = 0;
                    switch (LocalPlayer.Battle.BattlePhase) {
                        case BattlePhase_Enum.Block: { val = 3 * count; break; }
                        case BattlePhase_Enum.RangeSiege: { val = 1 * count; break; }
                        case BattlePhase_Enum.Attack: { val = 2 * count; break; }
                    }
                    if (i.Physical > 0) i.Physical += val;
                    if (i.Fire > 0) i.Fire += val;
                    if (i.Cold > 0) i.Cold += val;
                    if (i.ColdFire > 0) i.ColdFire += val;
                    LocalPlayer.RemoveGameEffect(GameEffect_Enum.WHITE_Leadership);
                    log.Add("[Leadership]");
                }
            }
        }

        #endregion

        #region Reward
        public void Reward_Add(int blue, int red, int green, int white, int random, int artifact, int spell, int advanced, int unit, int level) {
            if (!LocalPlayer.GameEffects.ContainsKey(GameEffect_Enum.Rewards)) {
                AddGameEffect(GameEffect_Enum.Rewards, blue, red, green, white, random, 0, 0, artifact, spell, advanced, unit, level);
            } else {
                List<int> ged = LocalPlayer.GameEffects[GameEffect_Enum.Rewards].Values;
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
    }
}
