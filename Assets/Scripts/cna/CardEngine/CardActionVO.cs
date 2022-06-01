using System.Collections.Generic;
using cna.poo;

namespace cna {
    public abstract class CardActionVO : CardVO {
        public CardActionVO(int uniqueId, string cardTitle, Image_Enum cardImage, CardType_Enum cardType, List<string> actions, List<List<Crystal_Enum>> costs, List<List<TurnPhase_Enum>> allowed, List<List<BattlePhase_Enum>> battleAllowed = null, CardColor_Enum cardColor = CardColor_Enum.NA)
            : base(uniqueId, cardTitle, cardImage, cardType) {
            Actions = actions;
            Costs = costs;
            Allowed = allowed;
            BattleAllowed = battleAllowed;
            CardColor = cardColor;
        }

        public override void OnClick_ActionButton(ActionResultVO ar) {
            if (D.Action.CheckTurnAndUI(ar)) {
                checkAllowedToUse(ar);
                if (ar.Status) {
                    ar.AddCardState();
                    PayForAction(ar);
                    return;
                }
            }
            D.Action.ProcessActionResultVO(ar);
        }


        public virtual ActionResultVO checkAllowedToUse(ActionResultVO ar) {
            CardVO c = D.Cards[ar.UniqueCardId];
            List<TurnPhase_Enum> actionAllowedPhase = new List<TurnPhase_Enum>();
            actionAllowedPhase.AddRange(c.Allowed[ar.ActionIndex]);
            List<BattlePhase_Enum> actionAllowedBattlePhase = new List<BattlePhase_Enum>();
            actionAllowedBattlePhase.AddRange(c.BattleAllowed[ar.ActionIndex]);
            TurnPhase_Enum playerPhase = ar.LocalPlayer.PlayerTurnPhase;
            CNAMap<GameEffect_Enum, WrapList<int>> GameEffects = ar.LocalPlayer.GameEffects;
            GameEffects.Keys.ForEach(ge => {
                switch (ge) {
                    case GameEffect_Enum.AC_Agility01: {
                        if (actionAllowedPhase.Contains(TurnPhase_Enum.Move)) {
                            actionAllowedPhase.Add(TurnPhase_Enum.Battle);
                            actionAllowedBattlePhase.Add(BattlePhase_Enum.Attack);
                        }
                        break;
                    }
                    case GameEffect_Enum.AC_Agility02: {
                        if (actionAllowedPhase.Contains(TurnPhase_Enum.Move)) {
                            actionAllowedPhase.Add(TurnPhase_Enum.Battle);
                            actionAllowedBattlePhase.Add(BattlePhase_Enum.RangeSiege);
                            actionAllowedBattlePhase.Add(BattlePhase_Enum.Attack);
                        }
                        break;
                    }
                    case GameEffect_Enum.AC_Diplomacy01:
                    case GameEffect_Enum.AC_Diplomacy02: {
                        if (actionAllowedPhase.Contains(TurnPhase_Enum.Influence)) {
                            actionAllowedPhase.Add(TurnPhase_Enum.Battle);
                            actionAllowedBattlePhase.Add(BattlePhase_Enum.Block);
                        }
                        break;
                    }
                    case GameEffect_Enum.CS_WingsOfNight: {
                        if (actionAllowedPhase.Contains(TurnPhase_Enum.Move)) {
                            actionAllowedPhase.Add(TurnPhase_Enum.Battle);
                            actionAllowedBattlePhase.Add(BattlePhase_Enum.AssignDamage);
                        }
                        break;
                    }
                }
            });
            switch (playerPhase) {
                case TurnPhase_Enum.StartTurn: {
                    if (actionAllowedPhase.Contains(TurnPhase_Enum.StartTurn) ||
                        actionAllowedPhase.Contains(TurnPhase_Enum.Move) ||
                        actionAllowedPhase.Contains(TurnPhase_Enum.Influence)) { } else {
                        ar.ErrorMsg = "You can not play this during Start of Turn Phase";
                    }
                    break;
                }
                case TurnPhase_Enum.Move: {
                    if (actionAllowedPhase.Contains(TurnPhase_Enum.Move)) { } else {
                        ar.ErrorMsg = "You can not play this during Movement Phase";
                    }
                    break;
                }
                case TurnPhase_Enum.Influence: {
                    if (actionAllowedPhase.Contains(TurnPhase_Enum.Influence)) { } else {
                        ar.ErrorMsg = "You can not play this during Influence Phase";
                    }
                    break;
                }
                case TurnPhase_Enum.Battle: {
                    if (actionAllowedPhase.Contains(TurnPhase_Enum.Battle)) {
                        BattlePhase_Enum playerBattlePhase = ar.LocalPlayer.Battle.BattlePhase;
                        switch (playerBattlePhase) {
                            case BattlePhase_Enum.StartOfBattle: {
                                if (actionAllowedBattlePhase.Contains(BattlePhase_Enum.StartOfBattle)) { } else {
                                    ar.ErrorMsg = "You can not play this during Start Of Battle Phase";
                                }
                                break;
                            }
                            case BattlePhase_Enum.SetupProvoke:
                            case BattlePhase_Enum.Provoke: {
                                if (actionAllowedBattlePhase.Contains(BattlePhase_Enum.Provoke)) { } else {
                                    ar.ErrorMsg = "You can not play this during Provoke Phase";
                                }
                                break;
                            }
                            case BattlePhase_Enum.RangeSiege: {
                                if (actionAllowedBattlePhase.Contains(BattlePhase_Enum.RangeSiege)) { } else {
                                    ar.ErrorMsg = "You can not play this during Range & Siege Phase";
                                }
                                break;
                            }
                            case BattlePhase_Enum.Block: {
                                if (actionAllowedBattlePhase.Contains(BattlePhase_Enum.Block)) { } else {
                                    ar.ErrorMsg = "You can not play this during Block Phase";
                                }
                                break;
                            }
                            case BattlePhase_Enum.AssignDamage: {
                                if (actionAllowedBattlePhase.Contains(BattlePhase_Enum.AssignDamage)) { } else {
                                    ar.ErrorMsg = "You can not play this during Assign Damage Phase";
                                }
                                break;
                            }
                            case BattlePhase_Enum.Attack: {
                                if (actionAllowedBattlePhase.Contains(BattlePhase_Enum.Attack)) { } else {
                                    ar.ErrorMsg = "You can not play this during Attack Phase";
                                }
                                break;
                            }
                            case BattlePhase_Enum.EndOfBattle: {
                                if (actionAllowedBattlePhase.Contains(BattlePhase_Enum.EndOfBattle)) { } else {
                                    ar.ErrorMsg = "You can not play this during End of Battle Phase";
                                }
                                break;
                            }
                        }
                    } else {
                        ar.ErrorMsg = "You can not play this during Battle Phase";
                    }
                    break;
                }
                case TurnPhase_Enum.AfterBattle: {
                    if (actionAllowedPhase.Contains(TurnPhase_Enum.AfterBattle)) { } else {
                        ar.ErrorMsg = "You can not play this during After Battle Phase";
                    }
                    break;
                }
                case TurnPhase_Enum.Resting: {
                    break;
                }
                case TurnPhase_Enum.Exhaustion: {
                    break;
                }
                case TurnPhase_Enum.Reward: {
                    if (actionAllowedPhase.Contains(TurnPhase_Enum.Reward)) { } else { ar.ErrorMsg = "You can not perform this action"; }
                    break;
                }
                default: {
                    ar.ErrorMsg = "You can not perform this action";
                    break;
                }
            }
            return ar;
        }

        public virtual void PayForAction(ActionResultVO ar) {
            List<Crystal_Enum> cost = calculateCost(ar.ActionIndex);
            if (cost.Count == 0) {
                OnClick_PaymentAccept(ar);
            } else {
                ar.Push();
                ar.PayForAction(cost, OnClick_PaymentAccept);
            }
        }

        protected virtual List<Crystal_Enum> calculateCost(int actionId) {
            List<Crystal_Enum> c = new List<Crystal_Enum>();
            if (Costs.Count > actionId && Costs[actionId][0] != Crystal_Enum.NA) {
                c.AddRange(Costs[actionId]);
            }
            return c;
        }

        public virtual void OnClick_PaymentAccept(ActionResultVO ar) {
            switch (ar.ActionIndex) {
                case 0: { ActionPaymentComplete_00(ar); break; }
                case 1: { ActionPaymentComplete_01(ar); break; }
                case 2: { ActionPaymentComplete_02(ar); break; }
            }
        }

        public virtual void ActionPaymentComplete_00(ActionResultVO ar) { ar.FinishCallback(ActionValid_00(ar)); }
        public virtual void ActionPaymentComplete_01(ActionResultVO ar) { ar.FinishCallback(ActionValid_01(ar)); }
        public virtual void ActionPaymentComplete_02(ActionResultVO ar) { ar.FinishCallback(ActionValid_02(ar)); }

        public virtual ActionResultVO ActionValid_00(ActionResultVO ar) { return ar; }
        public virtual ActionResultVO ActionValid_01(ActionResultVO ar) { return ar; }
        public virtual ActionResultVO ActionValid_02(ActionResultVO ar) { return ar; }

        public override void ActionFinish_00(ActionResultVO ar) { ActionFinish(ar); }
        public override void ActionFinish_01(ActionResultVO ar) { ActionFinish(ar); }
        public override void ActionFinish_02(ActionResultVO ar) { ActionFinish(ar); }

        public void ActionFinish(ActionResultVO ar) {
            D.Action.ProcessActionResultVO(ar);
        }

        public virtual string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, ActionResultVO ar) {
            string msg = "";
            if (cardHolder != CardHolder_Enum.PlayerHand) {
                return "You must select a card from your hand!";
            } else if (UniqueId == card.UniqueId) {
                return "You can not select the card being used!";
            } else if (D.LocalPlayer.Deck.State.ContainsKey(card.UniqueId)) {
                return card.CardTitle + " has already been used!";
            }
            return msg;
        }

        //public void AddGameEffect(GameEffect_Enum ge, params int[] cards) {
        //    D.LocalPlayer.AddGameEffect(ge, cards);
        //    D.B.UpdateMonsterDetails();
        //}
    }
}
