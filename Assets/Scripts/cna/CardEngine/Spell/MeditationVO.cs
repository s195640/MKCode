using System.Collections.Generic;
using cna.poo;
namespace cna {
    public partial class MeditationVO : CardSpellVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("Back of Deck", Image_Enum.I_cardBackRounded),
                new OptionVO("Top of Deck", Image_Enum.I_cardBackRounded)
                );
        }

        public void acceptCallback_00(ActionResultVO ar) {
            int totalCards = ar.LocalPlayer.Deck.Discard.Count;
            ar.LocalPlayer.Deck.HandSize.Y += 2;
            ar.AddGameEffect(GameEffect_Enum.CS_Meditation);
            ar.AddLog("Meditation activated +2 to hand limit at end of turn.");
            if (totalCards != 0) {
                List<int> playerDiscardCopy = new List<int>();
                playerDiscardCopy.AddRange(ar.LocalPlayer.Deck.Discard);
                playerDiscardCopy.ShuffleDeck();
                List<int> cards = new List<int>();
                if (totalCards == 1) {
                    int card = playerDiscardCopy[0];
                    cards.Add(card);
                    ar.LocalPlayer.Deck.Discard.Remove(card);
                } else {
                    int card00 = playerDiscardCopy[0];
                    cards.Add(card00);
                    int card01 = playerDiscardCopy[1];
                    cards.Add(card01);
                    ar.LocalPlayer.Deck.Discard.Remove(card00);
                    ar.LocalPlayer.Deck.Discard.Remove(card01);
                }
                switch (ar.SelectedButtonIndex) {
                    case 0: {
                        ar.LocalPlayer.Deck.Deck.AddRange(cards);
                        break;
                    }
                    case 1: {
                        ar.LocalPlayer.Deck.Deck.InsertRange(0, playerDiscardCopy);
                        break;
                    }
                }
            }
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("Back of Deck", Image_Enum.I_cardBackRounded),
                new OptionVO("Top of Deck", Image_Enum.I_cardBackRounded)
                );
        }

        public void acceptCallback_01(ActionResultVO ar) {
            int totalCards = ar.LocalPlayer.Deck.Discard.Count;
            ar.LocalPlayer.Deck.HandSize.Y += 4;
            ar.AddGameEffect(GameEffect_Enum.CS_Trance);
            ar.AddLog("Meditation activated +4 to hand limit at end of turn.");
            if (totalCards != 0) {
                List<int> playerDiscardCopy = new List<int>();
                playerDiscardCopy.AddRange(ar.LocalPlayer.Deck.Discard);
                playerDiscardCopy.ShuffleDeck();
                List<int> cards = new List<int>();
                for (int i = 0; i < 4 && playerDiscardCopy.Count > 0; i++) {
                    int card = playerDiscardCopy[0];
                    playerDiscardCopy.Remove(0);
                    cards.Add(card);
                    ar.LocalPlayer.Deck.Discard.Remove(card);
                }
                switch (ar.SelectedButtonIndex) {
                    case 0: {
                        ar.LocalPlayer.Deck.Deck.AddRange(cards);
                        break;
                    }
                    case 1: {
                        ar.LocalPlayer.Deck.Deck.InsertRange(0, playerDiscardCopy);
                        break;
                    }
                }
            }
            ar.FinishCallback(ar);
        }
    }
}
