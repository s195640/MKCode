using cna.poo;

namespace cna {
    public partial class BannerofFearVO : CardArtifactVO {
        public override void ActionPaymentComplete_00(GameAPI ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }

        public void acceptCallback_00(GameAPI ar) {
            ar.AddBanner(ar.SelectedUniqueCardId, ar.UniqueCardId);
            ar.FinishCallback(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, GameAPI ar) {
            string msg = "";
            if (cardHolder != CardHolder_Enum.PlayerUnitHand) {
                return "You must selet a unit from your hand!";
            }
            return msg;
        }


        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.P.Battle.Monsters.Keys.ForEach(m => {
                MonsterMetaData md = ar.P.Battle.Monsters[m];
                if (!md.Dead && !md.Blocked && !md.Assigned) {
                    ar.P.Battle.Monsters[m].Blocked = true;
                }
            });
            return ar;
        }


        public override GameAPI ActionValid_02(GameAPI ar) {
            int monsterId = ar.P.Battle.SelectedMonsters[0];
            ar.P.Battle.Monsters[monsterId].Blocked = true;
            ar.AddCardState(ar.UniqueCardId, CardState_Enum.Unit_Exhausted);
            ar.Fame(1);
            return ar;
        }

        public override GameAPI checkAllowedToUse(GameAPI ar) {
            if (ar.ActionIndex == 2) {
                if (ar.P.PlayerTurnPhase == TurnPhase_Enum.Battle && ar.P.Battle.BattlePhase == BattlePhase_Enum.AssignDamage) {
                    if (ar.P.Deck.State.ContainsKey(ar.UniqueCardId)) {
                        bool wounded = ar.P.Deck.State[ar.UniqueCardId].Contains(CardState_Enum.Unit_Wounded);
                        bool paralyzed = ar.P.Deck.State[ar.UniqueCardId].Contains(CardState_Enum.Unit_Paralyzed);
                        bool exhausted = ar.P.Deck.State[ar.UniqueCardId].Contains(CardState_Enum.Unit_Exhausted);
                        if (wounded || paralyzed || exhausted) {
                            ar.ErrorMsg = "Unit must be ready and uninjured";
                        }
                    }
                    if (ar.P.Battle.SelectedMonsters.Count != 1) {
                        ar.ErrorMsg = "You must select ONE monster to use this effect on!";
                    }
                } else {
                    ar.ErrorMsg = "You can only play this banner during the assign damage phase of a battle.";
                }
                return ar;
            } else {
                return base.checkAllowedToUse(ar);
            }
        }
    }
}
