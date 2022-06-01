using System;
using System.Collections.Generic;
using cna.connector;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class TESTPanelStuff : MonoBehaviour {

        [SerializeField] private ManaPayPanel ManaPayPanel;
        [SerializeField] private SelectCardsPanel SelectCardsPanel;
        [SerializeField] private SelectManaPanel SelectManaPanel;

        public void Start() {
            TEST_BUILD_GAME_DATA();
            //Debug.Log("START");
            //CrystalData crystal = new CrystalData(2, 2, 2, 2, 0, 0);
            //CrystalData mana = new CrystalData(2, 2, 2, 2, 2, 2);
            ////int manaPoolAvailable = 2;
            //List<Crystal_Enum> manaPool = new List<Crystal_Enum> { Crystal_Enum.Blue, Crystal_Enum.Blue, Crystal_Enum.White, Crystal_Enum.Green, Crystal_Enum.Gold, Crystal_Enum.Black, Crystal_Enum.Red };
            ////bool isDayRules = true;
            //List<Crystal_Enum> cost = new List<Crystal_Enum> { Crystal_Enum.Green, Crystal_Enum.Blue, Crystal_Enum.Blue, Crystal_Enum.Blue };

            //ManaPayPanel.SetupUI(crystal, mana, manaPoolAvailable, manaPool, isDayRules, cost);
            //TEST_SelectCardsPanel();
            TEST_SelectManaPanel();
        }

        public void TEST_BUILD_GAME_DATA() {
            string playerName = "TEST USER";
            D.Connector = new SoloConnector(playerName, (wsData e) => { });
            D.G = new GameData();
            D.G.HostId = 0;
            D.G.GameId = "NEWGAMEID";
            D.G.PlayerTurnOrder = new List<int>() { 0 };
            D.G.PlayerTurnIndex = 0;
            D.G.Players = new List<PlayerData>();
            D.G.Players.Add(new PlayerData(playerName, 0));
            D.G.GameStatus = Game_Enum.TESTING;

        }

        public void TEST_SelectManaPanel() {
            string title = "Mana Search";
            string description = "Select one or two mana die to re-roll";
            V2IntVO selectCount = new V2IntVO(1, 2);
            List<string> buttonText = new List<string>() { "Accept" };
            List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightGreen };
            List<Action<ActionResultVO>> buttonActions = new List<Action<ActionResultVO>>() { OnClick_Button01 };
            List<bool> buttonForce = new List<bool>() { true };
            ActionResultVO ar = new ActionResultVO(0, CardState_Enum.NA);
            List<Image_Enum> die = new List<Image_Enum>() { Image_Enum.I_die_blue, Image_Enum.I_die_red, Image_Enum.I_die_green };
            SelectManaPanel.SetupUI(ar, die, title, description, selectCount, Image_Enum.I_check, buttonText, buttonColor, buttonActions, buttonForce);
        }




        public void TEST_SelectCardsPanel() {
            List<int> cards = new List<int>();
            for (int i = 0; i < 5; i++) {
                cards.Add(D.Cards.Find(c => c.CardType == CardType_Enum.Basic && !cards.Contains(c.UniqueId)).UniqueId);
            }
            string title = "Title if Card";
            string description = "Select upto 3 cards to discard, you will then draw that many cards back into your hand.";
            V2IntVO selectCount = new V2IntVO(1, 2);
            List<string> buttonText = new List<string>() { "Accept", "None" };
            List<Color> buttonColor = new List<Color>() { CNAColor.ColorLightGreen, CNAColor.ColorLightRed };
            List<Action<ActionResultVO>> buttonActions = new List<Action<ActionResultVO>>() { OnClick_Button01, OnClick_Button02 };
            List<bool> buttonForce = new List<bool>() { true, false };
            ActionResultVO ar = new ActionResultVO(0, CardState_Enum.NA);
            SelectCardsPanel.SetupUI(ar, cards, title, description, selectCount, Image_Enum.I_disable, buttonText, buttonColor, buttonActions, buttonForce);
        }

        public void OnClick_Button01(ActionResultVO ar) {
            Debug.Log("Button Clicked 01");
        }

        public void OnClick_Button02(ActionResultVO ar) {
            Debug.Log("Button Clicked 02");
        }
    }
}
