using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    public class ManaSearchGEVO : CardGameEffectVO {
        public ManaSearchGEVO(int uniqueId, GameEffect_Enum ge) : base(
                uniqueId, "Mana Search",
                ge == GameEffect_Enum.T_ManaSearch01 ? Image_Enum.I_crystal_yellow : Image_Enum.I_crystal_black,
                CardType_Enum.GameEffect,
                ge,
                GameEffectDuration_Enum.Round,
                CNAColor.ColorLightBlue,
                ge == GameEffect_Enum.T_ManaSearch01 ? "You may re-roll one or two mana die each turn." : "You may re-roll one or two mana die each turn.\n\nUSED",
                true, false, false
            ) {
            GameEffectClickable = ge == GameEffect_Enum.T_ManaSearch01;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.StartTurn, TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.Provoke, BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } };
        }

        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            D.Action.Clear();
            ar.RemoveGameEffect(GameEffect_Enum.T_ManaSearch01);
            ar.AddGameEffect(GameEffect_Enum.T_ManaSearch02);

            string title = "Mana Search";
            string description = "Select one or two mana die to re-roll";
            V2IntVO selectCount = new V2IntVO(1, 2);
            List<string> buttonText = new List<string>() { "Accept" };
            List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightGreen };
            List<Action<ActionResultVO>> buttonActions = new List<Action<ActionResultVO>>() { acceptCallback_00 };
            List<bool> buttonForce = new List<bool>() { true };
            List<Image_Enum> die = new List<Image_Enum>();
            ar.G.Board.ManaPool.ForEach(m => {
                die.Add(BasicUtil.Convert_CrystalToManaDieImageId(m));
            });
            ar.SelectManaDie(die, title, description, selectCount, Image_Enum.I_check, buttonText, buttonColor, buttonActions, buttonForce);
        }

        public void acceptCallback_00(ActionResultVO ar) {
            ar.AddLog("[Mana Search]");
            List<Crystal_Enum> selectedCrystals = new List<Crystal_Enum>();
            ar.SelectedCardIds.ForEach(c => {
                selectedCrystals.Add(ar.G.Board.ManaPool[c]);
            });
            selectedCrystals.ForEach(c => {
                ar.G.Board.ManaPool.Remove(c);
                Crystal_Enum newDie = (Crystal_Enum)UnityEngine.Random.Range(1, 7);
                ar.G.Board.ManaPool.Add(newDie);
                ar.AddLog("ManaDie " + c + " rerolled to " + newDie);
            });

            ar.change();
            ar.FinishCallback(ar);
        }
    }
}
