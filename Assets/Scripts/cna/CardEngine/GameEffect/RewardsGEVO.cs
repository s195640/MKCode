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

        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            rewards = ar.LocalPlayer.GameEffects[GameEffect_Enum.Rewards].Values;
            LevelToken(ar);
            CrystalRewards(ar);
        }

        public void LevelToken(ActionResultVO ar) {
            int newArmor = BasicUtil.GetArmorFromFame(ar.LocalPlayer.TotalFame);
            int newHandLimit = BasicUtil.GetHandLimitFromFame(ar.LocalPlayer.TotalFame);
            int newUnitLimit = BasicUtil.GetUnitHandLimitFromFame(ar.LocalPlayer.TotalFame);
            if (newArmor > ar.LocalPlayer.Armor) {
                ar.change();
                ar.LocalPlayer.Armor = newArmor;
                ar.AddLog("Your Armor has increaed to " + newArmor);
            }
            if (newHandLimit > ar.LocalPlayer.Deck.HandSize.X) {
                ar.change();
                ar.LocalPlayer.Deck.HandSize.X = newHandLimit;
                ar.AddLog("Your Hand limit has increaed to " + newHandLimit);
            }
            if (newUnitLimit > ar.LocalPlayer.Deck.UnitHandLimit) {
                ar.change();
                ar.LocalPlayer.Deck.UnitHandLimit = newUnitLimit;
                ar.AddLog("Your Unit Hand limit has increaed to " + newUnitLimit);
            }
        }

        public void CrystalRewards(ActionResultVO ar) {
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

        public void SpellReward(ActionResultVO ar) {
            if (rewards[8] > 0) {
                List<int> cards = D.G.Board.SpellOffering;
                string title = "Spell Reward";
                string description = "Select the Spell you would like to keep, this card will be added to the TOP of your deck.";
                V2IntVO selectCount = new V2IntVO(1, 1);
                Image_Enum selectionImage = Image_Enum.I_check;
                List<string> buttonText = new List<string>() { "Learn", "None" };
                List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightBlue, CNAColor.ColorLightRed };
                List<Action<ActionResultVO>> buttonCallback = new List<Action<ActionResultVO>>() { SpellRewardCallback, SpellRewardNoneCallback };
                List<bool> buttonForce = new List<bool>() { true, false };
                ar.SelectCards(cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
            } else {
                AdvancedReward(ar);
            }
        }
        public void SpellRewardCallback(ActionResultVO ar) {
            rewards[8]--;
            D.G.Board.SpellOffering.Remove(ar.SelectedCardIds[0]);
            if (D.G.Board.SpellIndex < D.Scenario.SpellDeck.Count) {
                D.G.Board.SpellOffering.Add(D.Scenario.SpellDeck[D.G.Board.SpellIndex]);
                D.G.Board.SpellIndex++;
            }
            ar.AddCardToTopOfDeck(ar.SelectedCardIds[0]);
            SpellReward(ar);
        }

        public void SpellRewardNoneCallback(ActionResultVO ar) {
            ar.AddLog("No Spell selected, Spell Reward Skipped!");
            rewards[8] = 0;
            AdvancedReward(ar);
        }

        public void AdvancedReward(ActionResultVO ar) {
            if (rewards[9] > 0) {
                List<int> cards = D.G.Board.AdvancedOffering;
                string title = "Advanced Reward";
                string description = "Select the Action you would like to keep, this card will be added to the TOP of your deck.";
                V2IntVO selectCount = new V2IntVO(1, 1);
                Image_Enum selectionImage = Image_Enum.I_check;
                List<string> buttonText = new List<string>() { "Train", "None" };
                List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightBlue, CNAColor.ColorLightRed };
                List<Action<ActionResultVO>> buttonCallback = new List<Action<ActionResultVO>>() { AdvancedRewardCallback, AdvancedRewardNoneCallback };
                List<bool> buttonForce = new List<bool>() { true, false };
                ar.SelectCards(cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);
            } else {
                UnitReward(ar);
            }
        }

        public void AdvancedRewardCallback(ActionResultVO ar) {
            rewards[9]--;
            D.G.Board.AdvancedOffering.Remove(ar.SelectedCardIds[0]);
            if (D.G.Board.AdvancedIndex < D.Scenario.AdvancedDeck.Count) {
                D.G.Board.AdvancedOffering.Add(D.Scenario.AdvancedDeck[D.G.Board.AdvancedIndex]);
                D.G.Board.AdvancedIndex++;
            }
            ar.AddCardToTopOfDeck(ar.SelectedCardIds[0]);
            AdvancedReward(ar);
        }

        public void AdvancedRewardNoneCallback(ActionResultVO ar) {
            ar.AddLog("No Action selected, Action Reward Skipped!");
            rewards[9] = 0;
            UnitReward(ar);
        }

        public void UnitReward(ActionResultVO ar) {
            if (rewards[10] > 0) {

                List<int> cards = new List<int>();
                D.G.Board.UnitOffering.ForEach(u => {
                    CardVO card = D.Cards[u];
                    if (card.CardType == CardType_Enum.Unit_Elite || card.CardType == CardType_Enum.Unit_Normal) {
                        cards.Add(u);
                    }
                });
                if (cards.Count > 0) {
                    bool bondsOfLoyaltyUsed = false;
                    ar.LocalPlayer.Deck.State.Keys.ForEach(u => {
                        bondsOfLoyaltyUsed = ar.LocalPlayer.Deck.State[u].ContainsAny(CardState_Enum.Unit_BondsOfLoyalty);
                    });
                    bool bondsOfLoyalty = false;
                    ar.LocalPlayer.Deck.Skill.ForEach(s => {
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
                    List<Action<ActionResultVO>> buttonCallback = new List<Action<ActionResultVO>>() { UnitRewardCallback, UnitRewardNoneCallback };
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

        public void UnitRewardNoneCallback(ActionResultVO ar) {
            ar.AddLog("No Unit selected, Unit Reward Skipped!");
            rewards[10] = 0;
            UnitReward(ar);
        }

        public void UnitRewardCallback(ActionResultVO ar) {
            int unitCount = ar.LocalPlayer.Deck.Unit.Count;
            bool bondsOfLoyaltyUsed = false;
            ar.LocalPlayer.Deck.State.Keys.ForEach(u => {
                bondsOfLoyaltyUsed = ar.LocalPlayer.Deck.State[u].ContainsAny(CardState_Enum.Unit_BondsOfLoyalty);
                if (bondsOfLoyaltyUsed) {
                    unitCount--;
                }
            });
            if (unitCount < ar.LocalPlayer.Deck.UnitHandLimit) {
                D.G.Board.UnitOffering.Remove(ar.SelectedCardIds[0]);
                ar.LocalPlayer.Deck.Unit.Add(ar.SelectedCardIds[0]);
                ar.change();
                rewards[10]--;
            } else {
                D.Msg("You do can not carry any more units.  You must Disband one before adding a new one.");
            }
            UnitReward(ar);
        }

        public void UnitRewardCallback2(ActionResultVO ar) {
            D.G.Board.UnitOffering.Remove(ar.SelectedCardIds[0]);
            ar.LocalPlayer.Deck.Unit.Add(ar.SelectedCardIds[0]);
            ar.AddCardState(ar.SelectedCardIds[0], CardState_Enum.Unit_BondsOfLoyalty);
            ar.change();
            rewards[10]--;
            UnitReward(ar);
        }

        public void ArtifactReward(ActionResultVO ar) {
            if (rewards[7] > 0) {
                List<int> cards = new List<int>();
                for (int i = 0; i <= rewards[7]; i++) {
                    cards.Add(D.Scenario.ArtifactDeck[D.G.Board.ArtifactIndex]);
                    D.G.Board.ArtifactIndex++;
                }
                string title = "Artifact Reward";
                string description = "Select the Artifacts you would like to keep, these card will be added to the TOP of your deck.";
                V2IntVO selectCount = new V2IntVO(cards.Count - 1, cards.Count - 1);
                Image_Enum selectionImage = Image_Enum.I_check;
                List<string> buttonText = new List<string>() { "Gain", "None" };
                List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightBlue, CNAColor.ColorLightRed };
                List<Action<ActionResultVO>> buttonCallback = new List<Action<ActionResultVO>>() { ArtifactRewardCallback, ArtifactRewardNoneCallback };
                List<bool> buttonForce = new List<bool>() { true, false };
                ar.SelectCards(cards, title, description, selectCount, selectionImage, buttonText, buttonColor, buttonCallback, buttonForce);

            } else {
                CalcLevelSkill(ar);
            }
        }

        public void ArtifactRewardCallback(ActionResultVO ar) {
            rewards[7] = 0;
            ar.SelectedCardIds.ForEach(a => ar.AddCardToTopOfDeck(a));
            ArtifactReward(ar);
        }

        public void ArtifactRewardNoneCallback(ActionResultVO ar) {
            ar.AddLog("No Artifact selected, Artifact Reward Skipped!");
            rewards[7] = 0;
            ArtifactReward(ar);
        }

        int totalLevelSkillRewards = 0;
        public void CalcLevelSkill(ActionResultVO ar) {
            totalLevelSkillRewards = 0;
            int oldLevel = BasicUtil.GetPlayerLevel(ar.LocalPlayer.Fame.X);
            int newLevel = BasicUtil.GetPlayerLevel(ar.LocalPlayer.TotalFame);
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

        public void LevelSkill(ActionResultVO ar) {
            if (totalLevelSkillRewards > 0) {
                actionOffering = D.G.Board.AdvancedOffering;
                if (D.Board.DummyPlayer) {
                    int dummySkillCardId = 0;
                    dummySkillCardId = BasicUtil.Draw(D.Scenario.BlueSkillDeck, ref D.Board.skillBlueIndex);
                    switch (D.DummyPlayer.Avatar) {
                        case Image_Enum.A_MEEPLE_BLUE: { dummySkillCardId = BasicUtil.Draw(D.Scenario.BlueSkillDeck, ref D.Board.skillBlueIndex); break; }
                        case Image_Enum.A_MEEPLE_GREEN: { dummySkillCardId = BasicUtil.Draw(D.Scenario.GreenSkillDeck, ref D.Board.skillGreenIndex); break; }
                        case Image_Enum.A_MEEPLE_RED: { dummySkillCardId = BasicUtil.Draw(D.Scenario.RedSkillDeck, ref D.Board.skillRedIndex); break; }
                        case Image_Enum.A_MEEPLE_WHITE: { dummySkillCardId = BasicUtil.Draw(D.Scenario.WhiteSkillDeck, ref D.Board.skillWhiteIndex); break; }
                    }
                    if (dummySkillCardId > 0) {
                        D.Board.SkillOffering.Add(dummySkillCardId);
                    }
                }
                skillOffering.Clear();
                skillOffering.AddRange(
                    D.G.Board.SkillOffering.FindAll(c => {
                        CardVO card = D.Cards[c];
                        return card.Avatar != ar.LocalPlayer.Avatar;
                    }));
                skills.Clear();
                switch (ar.LocalPlayer.Avatar) {
                    case Image_Enum.A_MEEPLE_BLUE: {
                        if (D.G.Board.SkillBlueIndex < D.Scenario.BlueSkillDeck.Count) {
                            skills.Add(D.Scenario.BlueSkillDeck[D.G.Board.SkillBlueIndex]);
                            D.G.Board.SkillBlueIndex++;
                        }
                        if (D.G.Board.SkillBlueIndex < D.Scenario.BlueSkillDeck.Count) {
                            skills.Add(D.Scenario.BlueSkillDeck[D.G.Board.SkillBlueIndex]);
                            D.G.Board.SkillBlueIndex++;
                        }
                        break;
                    }
                    case Image_Enum.A_MEEPLE_GREEN: {
                        if (D.G.Board.SkillGreenIndex < D.Scenario.GreenSkillDeck.Count) {
                            skills.Add(D.Scenario.GreenSkillDeck[D.G.Board.SkillGreenIndex]);
                            D.G.Board.SkillGreenIndex++;
                        }
                        if (D.G.Board.SkillGreenIndex < D.Scenario.GreenSkillDeck.Count) {
                            skills.Add(D.Scenario.GreenSkillDeck[D.G.Board.SkillGreenIndex]);
                            D.G.Board.SkillGreenIndex++;
                        }
                        break;
                    }
                    case Image_Enum.A_MEEPLE_RED: {
                        if (D.G.Board.SkillRedIndex < D.Scenario.RedSkillDeck.Count) {
                            skills.Add(D.Scenario.RedSkillDeck[D.G.Board.SkillRedIndex]);
                            D.G.Board.SkillRedIndex++;
                        }
                        if (D.G.Board.SkillRedIndex < D.Scenario.RedSkillDeck.Count) {
                            skills.Add(D.Scenario.RedSkillDeck[D.G.Board.SkillRedIndex]);
                            D.G.Board.SkillRedIndex++;
                        }
                        break;
                    }
                    case Image_Enum.A_MEEPLE_WHITE: {
                        if (D.G.Board.SkillWhiteIndex < D.Scenario.WhiteSkillDeck.Count) {
                            skills.Add(D.Scenario.WhiteSkillDeck[D.G.Board.SkillWhiteIndex]);
                            D.G.Board.SkillWhiteIndex++;
                        }
                        if (D.G.Board.SkillWhiteIndex < D.Scenario.WhiteSkillDeck.Count) {
                            skills.Add(D.Scenario.WhiteSkillDeck[D.G.Board.SkillWhiteIndex]);
                            D.G.Board.SkillWhiteIndex++;
                        }
                        break;
                    }
                }
                ar.SelectLevelUp(LevelSkillCallback, actionOffering, skillOffering, skills);
            } else {
                EndReward(ar);
            }
        }

        public void LevelSkillCallback(ActionResultVO ar) {
            int actionCard = ar.UniqueCardId;
            int skillCard = ar.SelectedUniqueCardId;
            D.G.Board.SkillOffering.AddRange(skills);
            D.G.Board.SkillOffering.Remove(skillCard);
            D.G.Board.AdvancedOffering.Remove(actionCard);
            if (D.G.Board.AdvancedIndex < D.Scenario.AdvancedDeck.Count) {
                D.G.Board.AdvancedOffering.Add(D.Scenario.AdvancedDeck[D.G.Board.AdvancedIndex]);
                D.G.Board.AdvancedIndex++;
            }
            ar.AddCardToTopOfDeck(actionCard);
            ar.AddSkill(skillCard);
            totalLevelSkillRewards--;
            LevelSkill(ar);
        }



        public void EndReward(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.EndTurn);
            ar.FinishCallback(ar);
        }
    }
}
