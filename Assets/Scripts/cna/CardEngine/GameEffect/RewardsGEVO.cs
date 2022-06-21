using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    public class RewardsGEVO : CardGameEffectVO {
        public RewardsGEVO(int uniqueId) : base(
            uniqueId, "Rewards", Image_Enum.I_trophy,
                CardType_Enum.GameEffect,
                GameEffect_Enum.Rewards,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "During your turn you have received rewards.  All rewards are received at the end of your turn!",
                true, false, false
            ) {
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.Reward } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.NA } };
        }

        public override string GameEffectDescription {
            get {
                string desc = "All rewards are received at the end of your turn!\n";
                List<int> data = D.LocalPlayer.GameEffects[GameEffect_Enum.Rewards].Values;
                if (data[0] > 0 || data[1] > 0 || data[2] > 0 || data[3] > 0 || data[4] > 0 || data[5] > 0 || data[6] > 0) {
                    bool commaflag = false;
                    desc += "\nCrystals {";
                    if (data[0] > 0) {
                        desc += (commaflag ? "," : "") + " Blue +" + data[0];
                        commaflag = true;
                    }
                    if (data[1] > 0) {
                        desc += (commaflag ? "," : "") + " Red +" + data[1];
                        commaflag = true;
                    }
                    if (data[2] > 0) {
                        desc += (commaflag ? "," : "") + " Green +" + data[2];
                        commaflag = true;
                    }
                    if (data[3] > 0) {
                        desc += (commaflag ? "," : "") + " White +" + data[3];
                        commaflag = true;
                    }
                    if (data[4] > 0) {
                        desc += (commaflag ? "," : "") + " Random +" + data[4];
                        commaflag = true;
                    }
                    if (data[5] > 0) {
                        desc += (commaflag ? "," : "") + " Gold (you pick) +" + data[5];
                        commaflag = true;
                    }
                    if (data[6] > 0) {
                        desc += (commaflag ? "," : "") + " Black (fame) +" + data[6];
                    }
                    desc += "}";
                }
                if (data[7] > 0) {
                    desc += "\nArtifacts +" + data[7];
                }
                if (data[8] > 0) {
                    desc += "\nSpells +" + data[8];
                }
                if (data[9] > 0) {
                    desc += "\nAdvanced +" + data[9];
                }
                if (data[10] > 0) {
                    desc += "\nUnits +" + data[10];
                }
                if (data[11] > 0) {
                    desc += "\nLevel Up";
                }
                return desc;
            }
        }

        private List<int> rewards = new List<int>();

        public override void ActionPaymentComplete_00(GameAPI ar) {
            rewards = ar.P.GameEffects[GameEffect_Enum.Rewards].Values;
            LevelToken(ar);
            CrystalRewards(ar);
        }

        public void LevelToken(GameAPI ar) {
            int newArmor = BasicUtil.GetArmorFromFame(ar.P.TotalFame);
            int newHandLimit = BasicUtil.GetHandLimitFromFame(ar.P.TotalFame);
            int newUnitLimit = BasicUtil.GetUnitHandLimitFromFame(ar.P.TotalFame);
            if (newArmor > ar.P.Armor) {
                ar.change();
                ar.P.Armor = newArmor;
                ar.AddLog("Your Armor has increaed to " + newArmor);
            }
            if (newHandLimit > ar.P.Deck.HandSize.X) {
                ar.change();
                ar.P.Deck.HandSize.X = newHandLimit;
                ar.AddLog("Your Hand limit has increaed to " + newHandLimit);
            }
            if (newUnitLimit > ar.P.Deck.UnitHandLimit) {
                ar.change();
                ar.P.Deck.UnitHandLimit = newUnitLimit;
                ar.AddLog("Your Unit Hand limit has increaed to " + newUnitLimit);
            }
        }

        public void CrystalRewards(GameAPI ar) {
            if (rewards[0] > 0 || rewards[1] > 0 || rewards[2] > 0 || rewards[3] > 0 || rewards[4] > 0) {
                ar.AddLog("Crystal Rewards");
                if (rewards[0] > 0)
                    ar.CrystalBlue(rewards[0]);
                if (rewards[1] > 0)
                    ar.CrystalRed(rewards[1]);
                if (rewards[2] > 0)
                    ar.CrystalGreen(rewards[2]);
                if (rewards[3] > 0)
                    ar.CrystalWhite(rewards[3]);
                for (int i = 0; i < rewards[4]; i++) {
                    ar.AddCrystal((Crystal_Enum)UnityEngine.Random.Range(2, 6));
                }
            }
            SpellReward(ar);
        }

        public void SpellReward(GameAPI ar) {
            if (rewards[8] > 0) {
                List<int> cards = ar.P.Board.SpellOffering;
                string title = "Spell Reward";
                string description = "Select the Spell you would like to keep, this card will be added to the TOP of your deck.";
                V2IntVO selectCount = new V2IntVO(1, 1);
                Image_Enum selectionImage = Image_Enum.I_check;
                List<string> buttonText = new List<string>() { "Learn", "None" };
                List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightBlue, CNAColor.ColorLightRed };
                List<Action<GameAPI>> buttonCallback = new List<Action<GameAPI>>() { SpellRewardCallback, SpellRewardNoneCallback };
                List<bool> buttonForce = new List<bool>() { true, false };
                ar.SelectCards(cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
            } else {
                AdvancedReward(ar);
            }
        }
        public void SpellRewardCallback(GameAPI ar) {
            rewards[8]--;
            ar.P.Board.SpellOffering.Remove(ar.SelectedCardIds[0]);
            ar.drawSpellToOffering();
            ar.AddCardToTopOfDeck(ar.SelectedCardIds[0]);
            SpellReward(ar);
        }

        public void SpellRewardNoneCallback(GameAPI ar) {
            ar.AddLog("No Spell selected, Spell Reward Skipped!");
            rewards[8] = 0;
            AdvancedReward(ar);
        }

        public void AdvancedReward(GameAPI ar) {
            if (rewards[9] > 0) {
                List<int> cards = ar.P.Board.AdvancedOffering;
                string title = "Advanced Reward";
                string description = "Select the Action you would like to keep, this card will be added to the TOP of your deck.";
                V2IntVO selectCount = new V2IntVO(1, 1);
                Image_Enum selectionImage = Image_Enum.I_check;
                List<string> buttonText = new List<string>() { "Train", "None" };
                List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightBlue, CNAColor.ColorLightRed };
                List<Action<GameAPI>> buttonCallback = new List<Action<GameAPI>>() { AdvancedRewardCallback, AdvancedRewardNoneCallback };
                List<bool> buttonForce = new List<bool>() { true, false };
                ar.SelectCards(cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
            } else {
                UnitReward(ar);
            }
        }

        public void AdvancedRewardCallback(GameAPI ar) {
            rewards[9]--;
            ar.P.Board.AdvancedOffering.Remove(ar.SelectedCardIds[0]);
            ar.drawAdvancedToOffering();
            ar.AddCardToTopOfDeck(ar.SelectedCardIds[0]);
            AdvancedReward(ar);
        }

        public void AdvancedRewardNoneCallback(GameAPI ar) {
            ar.AddLog("No Action selected, Action Reward Skipped!");
            rewards[9] = 0;
            UnitReward(ar);
        }

        public void UnitReward(GameAPI ar) {
            if (rewards[10] > 0) {

                List<int> cards = new List<int>();
                ar.P.Board.UnitOffering.ForEach(u => {
                    CardVO card = D.Cards[u];
                    if (card.CardType == CardType_Enum.Unit_Elite || card.CardType == CardType_Enum.Unit_Normal) {
                        cards.Add(u);
                    }
                });
                if (cards.Count > 0) {
                    bool bondsOfLoyaltyUsed = false;
                    ar.P.Deck.State.Keys.ForEach(u => {
                        bondsOfLoyaltyUsed = ar.P.Deck.State[u].ContainsAny(CardState_Enum.Unit_BondsOfLoyalty);
                    });
                    bool bondsOfLoyalty = false;
                    ar.P.Deck.Skill.ForEach(s => {
                        if (D.Cards[s].CardImage == Image_Enum.SKW_bonds_of_loyalty) {
                            bondsOfLoyalty = true;
                        }
                    });

                    string title = "Unit Reward";
                    string description = "Select the Unit you would like to keep.  Or Select None!";
                    V2IntVO selectCount = new V2IntVO(1, 1);
                    Image_Enum selectionImage = Image_Enum.I_check;
                    List<string> buttonText = new List<string>() { "Recruit", "None" };
                    List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightBlue, CNAColor.ColorLightRed };
                    List<Action<GameAPI>> buttonCallback = new List<Action<GameAPI>>() { UnitRewardCallback, UnitRewardNoneCallback };
                    List<bool> buttonForce = new List<bool>() { true, false };
                    if (bondsOfLoyalty && !bondsOfLoyaltyUsed) {
                        buttonText.Add("Bond of Loyalty");
                        buttonColor.Add(CNAColor.ColorLightBlue);
                        buttonCallback.Add(UnitRewardCallback2);
                        buttonForce.Add(true);
                    }
                    ar.SelectCards(cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
                } else {
                    ar.AddLog("No units available, Unit reward skipped!");
                }
            } else {
                ArtifactReward(ar);
            }
        }

        public void UnitRewardNoneCallback(GameAPI ar) {
            ar.AddLog("No Unit selected, Unit Reward Skipped!");
            rewards[10] = 0;
            UnitReward(ar);
        }

        public void UnitRewardCallback(GameAPI ar) {
            int unitCount = ar.P.Deck.Unit.Count;
            bool bondsOfLoyaltyUsed = false;
            ar.P.Deck.State.Keys.ForEach(u => {
                bondsOfLoyaltyUsed = ar.P.Deck.State[u].ContainsAny(CardState_Enum.Unit_BondsOfLoyalty);
                if (bondsOfLoyaltyUsed) {
                    unitCount--;
                }
            });
            if (unitCount < ar.P.Deck.UnitHandLimit) {
                ar.P.Board.UnitOffering.Remove(ar.SelectedCardIds[0]);
                ar.P.Deck.Unit.Add(ar.SelectedCardIds[0]);
                ar.change();
                rewards[10]--;
            } else {
                D.Msg("You do can not carry any more units.  You must Disband one before adding a new one.");
            }
            UnitReward(ar);
        }

        public void UnitRewardCallback2(GameAPI ar) {
            ar.P.Board.UnitOffering.Remove(ar.SelectedCardIds[0]);
            ar.P.Deck.Unit.Add(ar.SelectedCardIds[0]);
            ar.AddCardState(ar.SelectedCardIds[0], CardState_Enum.Unit_BondsOfLoyalty);
            ar.change();
            rewards[10]--;
            UnitReward(ar);
        }

        public void ArtifactReward(GameAPI ar) {
            if (rewards[7] > 0) {
                List<int> cards = new List<int>();
                for (int i = 0; i <= rewards[7]; i++) {
                    cards.Add(D.Scenario.ArtifactDeck[ar.P.Board.ArtifactIndex]);
                    ar.P.Board.ArtifactIndex++;
                }
                string title = "Artifact Reward";
                string description = "Select the Artifacts you would like to keep, these card will be added to the TOP of your deck.";
                V2IntVO selectCount = new V2IntVO(cards.Count - 1, cards.Count - 1);
                Image_Enum selectionImage = Image_Enum.I_check;
                List<string> buttonText = new List<string>() { "Gain", "None" };
                List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightBlue, CNAColor.ColorLightRed };
                List<Action<GameAPI>> buttonCallback = new List<Action<GameAPI>>() { ArtifactRewardCallback, ArtifactRewardNoneCallback };
                List<bool> buttonForce = new List<bool>() { true, false };
                ar.SelectCards(cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);

            } else {
                CalcLevelSkill(ar);
            }
        }

        public void ArtifactRewardCallback(GameAPI ar) {
            rewards[7] = 0;
            ar.SelectedCardIds.ForEach(a => ar.AddCardToTopOfDeck(a));
            ArtifactReward(ar);
        }

        public void ArtifactRewardNoneCallback(GameAPI ar) {
            ar.AddLog("No Artifact selected, Artifact Reward Skipped!");
            rewards[7] = 0;
            ArtifactReward(ar);
        }

        int totalLevelSkillRewards = 0;
        public void CalcLevelSkill(GameAPI ar) {
            totalLevelSkillRewards = 0;
            int oldLevel = BasicUtil.GetPlayerLevel(ar.P.Fame.X);
            int newLevel = BasicUtil.GetPlayerLevel(ar.P.TotalFame);
            if (newLevel > oldLevel) {
                if (oldLevel % 2 == 1) {
                    totalLevelSkillRewards = (int)Math.Ceiling((newLevel - oldLevel) / 2.0);
                } else {
                    totalLevelSkillRewards = (int)Math.Ceiling((newLevel - oldLevel - 1) / 2.0);
                }
            }
            LevelSkill(ar);
        }

        List<int> actionOffering = new List<int>();
        List<int> skillOffering = new List<int>();
        List<int> skills = new List<int>();

        public void LevelSkill(GameAPI ar) {
            if (totalLevelSkillRewards > 0) {
                actionOffering = ar.P.Board.AdvancedOffering;
                if (D.GLD.DummyPlayer) {
                    int dummySkillCardId = 0;
                    switch (D.DummyPlayer.Avatar) {
                        case Image_Enum.A_MEEPLE_BLUE: { dummySkillCardId = ar.DrawBlueSkillCard(); break; }
                        case Image_Enum.A_MEEPLE_GREEN: { dummySkillCardId = ar.DrawGreenSkillCard(); break; }
                        case Image_Enum.A_MEEPLE_RED: { dummySkillCardId = ar.DrawRedSkillCard(); break; }
                        case Image_Enum.A_MEEPLE_WHITE: { dummySkillCardId = ar.DrawWhiteSkillCard(); break; }
                    }
                    if (dummySkillCardId > 0) {
                        ar.P.Board.SkillOffering.Add(dummySkillCardId);
                    }
                }
                skillOffering.Clear();
                skillOffering.AddRange(
                    ar.P.Board.SkillOffering.FindAll(c => {
                        CardVO card = D.Cards[c];
                        return card.Avatar != ar.P.Avatar;
                    }));
                skills.Clear();
                switch (ar.P.Avatar) {
                    case Image_Enum.A_MEEPLE_BLUE: {
                        if (ar.P.Board.SkillBlueIndex < D.Scenario.BlueSkillDeck.Count) {
                            skills.Add(D.Scenario.BlueSkillDeck[ar.P.Board.SkillBlueIndex]);
                            ar.P.Board.SkillBlueIndex++;
                        }
                        if (ar.P.Board.SkillBlueIndex < D.Scenario.BlueSkillDeck.Count) {
                            skills.Add(D.Scenario.BlueSkillDeck[ar.P.Board.SkillBlueIndex]);
                            ar.P.Board.SkillBlueIndex++;
                        }
                        break;
                    }
                    case Image_Enum.A_MEEPLE_GREEN: {
                        if (ar.P.Board.SkillGreenIndex < D.Scenario.GreenSkillDeck.Count) {
                            skills.Add(D.Scenario.GreenSkillDeck[ar.P.Board.SkillGreenIndex]);
                            ar.P.Board.SkillGreenIndex++;
                        }
                        if (ar.P.Board.SkillGreenIndex < D.Scenario.GreenSkillDeck.Count) {
                            skills.Add(D.Scenario.GreenSkillDeck[ar.P.Board.SkillGreenIndex]);
                            ar.P.Board.SkillGreenIndex++;
                        }
                        break;
                    }
                    case Image_Enum.A_MEEPLE_RED: {
                        if (ar.P.Board.SkillRedIndex < D.Scenario.RedSkillDeck.Count) {
                            skills.Add(D.Scenario.RedSkillDeck[ar.P.Board.SkillRedIndex]);
                            ar.P.Board.SkillRedIndex++;
                        }
                        if (ar.P.Board.SkillRedIndex < D.Scenario.RedSkillDeck.Count) {
                            skills.Add(D.Scenario.RedSkillDeck[ar.P.Board.SkillRedIndex]);
                            ar.P.Board.SkillRedIndex++;
                        }
                        break;
                    }
                    case Image_Enum.A_MEEPLE_WHITE: {
                        if (ar.P.Board.SkillWhiteIndex < D.Scenario.WhiteSkillDeck.Count) {
                            skills.Add(D.Scenario.WhiteSkillDeck[ar.P.Board.SkillWhiteIndex]);
                            ar.P.Board.SkillWhiteIndex++;
                        }
                        if (ar.P.Board.SkillWhiteIndex < D.Scenario.WhiteSkillDeck.Count) {
                            skills.Add(D.Scenario.WhiteSkillDeck[ar.P.Board.SkillWhiteIndex]);
                            ar.P.Board.SkillWhiteIndex++;
                        }
                        break;
                    }
                }
                ar.SelectLevelUp(LevelSkillCallback, actionOffering, skillOffering, skills);
            } else {
                EndReward(ar);
            }
        }

        public void LevelSkillCallback(GameAPI ar) {
            int actionCard = ar.UniqueCardId;
            int skillCard = ar.SelectedUniqueCardId;
            ar.P.Board.SkillOffering.AddRange(skills);
            ar.P.Board.SkillOffering.Remove(skillCard);
            ar.P.Board.AdvancedOffering.Remove(actionCard);
            if (ar.P.Board.AdvancedIndex < D.Scenario.AdvancedDeck.Count) {
                ar.P.Board.AdvancedOffering.Add(D.Scenario.AdvancedDeck[ar.P.Board.AdvancedIndex]);
                ar.P.Board.AdvancedIndex++;
            }
            ar.AddCardToTopOfDeck(actionCard);
            ar.AddSkill(skillCard);
            totalLevelSkillRewards--;
            LevelSkill(ar);
        }



        public void EndReward(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.EndTurn);
            ar.FinishCallback(ar);
        }
    }
}
