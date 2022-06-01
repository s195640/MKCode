using cna.poo;

namespace cna {
    public partial class BannerofFearVO : CardArtifactVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectSingleCard(acceptCallback_00);
        }

        public void acceptCallback_00(ActionResultVO ar) {
            ar.AddBanner(ar.SelectedUniqueCardId, ar.UniqueCardId);
            ar.FinishCallback(ar);
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, ActionResultVO ar) {
            string msg = "";
            if (cardHolder != CardHolder_Enum.PlayerUnitHand) {
                return "You must selet a unit from your hand!";
            }
            return msg;
        }


        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.LocalPlayer.Battle.Monsters.Keys.ForEach(m => {
                MonsterMetaData md = ar.LocalPlayer.Battle.Monsters[m];
                if (!md.Dead && !md.Blocked && !md.Assigned) {
                    ar.LocalPlayer.Battle.Monsters[m].Blocked = true;
                }
            });
            return ar;
        }


        public override ActionResultVO ActionValid_02(ActionResultVO ar) {
            int monsterId = ar.LocalPlayer.Battle.SelectedMonsters[0];
            ar.LocalPlayer.Battle.Monsters[monsterId].Blocked = true;
            ar.AddCardState(ar.UniqueCardId, CardState_Enum.Unit_Exhausted);
            ar.Fame(1);
            return ar;
        }

        public override ActionResultVO checkAllowedToUse(ActionResultVO ar) {
            if (ar.ActionIndex == 2) {
                if (ar.LocalPlayer.PlayerTurnPhase == TurnPhase_Enum.Battle && ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.AssignDamage) {
                    if (ar.LocalPlayer.Deck.State.ContainsKey(ar.UniqueCardId)) {
                        bool wounded = ar.LocalPlayer.Deck.State[ar.UniqueCardId].Contains(CardState_Enum.Unit_Wounded);
                        bool paralyzed = ar.LocalPlayer.Deck.State[ar.UniqueCardId].Contains(CardState_Enum.Unit_Paralyzed);
                        bool exhausted = ar.LocalPlayer.Deck.State[ar.UniqueCardId].Contains(CardState_Enum.Unit_Exhausted);
                        if (wounded || paralyzed || exhausted) {
                            ar.ErrorMsg = "Unit must be ready and uninjured";
                        }
                    }
                    if (ar.LocalPlayer.Battle.SelectedMonsters.Count != 1) {
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
