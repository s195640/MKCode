using cna.poo;

namespace cna {
    public partial class RestorationVO : CardSpellVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            Image_Enum terrain = getTerrain(ar);
            int heal = 3;
            if (terrain == Image_Enum.TH_Forest) {
                heal = 5;
            }
            ar.Healing(heal);
            return ar;
        }

        private int readyPts = 0;

        public override void ActionPaymentComplete_01(GameAPI ar) {
            Image_Enum terrain = getTerrain(ar);
            int heal = 3;
            if (terrain == Image_Enum.TH_Forest) {
                heal = 5;
            }
            readyPts = heal;
            ar.Healing(heal);
            ar.SelectedUniqueCardId = -1;
            ReadyUnit(ar);
        }

        public void ReadyUnit(GameAPI ar) {
            if (ar.SelectedUniqueCardId != 0 && readyPts > 0 && totalUnitLevelsExhausted(ar) > 0) {
                ar.SelectedUniqueCardId = -1;
                ar.SelectSingleCard(acceptCallback_01, true);
            } else {
                ar.FinishCallback(ar);
            }
        }

        public void acceptCallback_01(GameAPI ar) {
            if (ar.SelectedUniqueCardId != 0) {
                CardVO card = D.Cards[ar.SelectedUniqueCardId];
                readyPts -= card.UnitLevel;
                ar.RemoveCardState(ar.SelectedUniqueCardId, CardState_Enum.Unit_Exhausted);
            }
            ReadyUnit(ar);
        }

        private Image_Enum getTerrain(GameAPI ar) {
            V2IntVO loc = ar.P.CurrentGridLoc;
            int mapIndex = D.Scenario.ConvertWorldToIndex(loc);
            int locIndex = D.Scenario.ConvertWorldToLocIndex(loc);
            MapHexId_Enum mapHexId_Enum = D.G.Board.CurrentMap[mapIndex];
            MapHexVO mapHex = D.HexMap[mapHexId_Enum];
            return mapHex.TerrainList[locIndex];
        }

        private int totalUnitLevelsExhausted(GameAPI ar) {
            int total = 0;
            ar.P.Deck.State.Keys.ForEach(c => {
                if (ar.P.Deck.State[c].Contains(CardState_Enum.Unit_Exhausted)) {
                    CardVO card = D.Cards[c];
                    total += card.UnitLevel;
                }
            });
            return total;
        }

        public override string IsSelectionAllowed(CardVO card, CardHolder_Enum cardHolder, GameAPI ar) {
            string msg = "";
            if (cardHolder != CardHolder_Enum.PlayerUnitHand) {
                return "You must selet a unit from your hand.  You have " + readyPts + " remaining!";
            } else if (card.UnitLevel > readyPts) {
                return "You do not have enough ready points to ready this unit.  You have " + readyPts + " remaining!";
            } else {
                if (ar.P.Deck.State.ContainsKey(card.UniqueId)) {
                    if (!ar.P.Deck.State[card.UniqueId].ContainsAny(CardState_Enum.Unit_Exhausted)) {
                        return "The Unit must be Exhausted to be readied.  You have " + readyPts + " remaining!";
                    }
                } else {
                    return "The Unit must be Exhausted to be readied.  You have " + readyPts + " remaining!";
                }
            }
            return msg;
        }
    }
}
