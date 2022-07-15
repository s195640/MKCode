using System;
using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class TriggerBattlePanel : BasePanel {

        public static string STANDARD_BATTLE = "You are about to start a battle with the following monster(s)! Would you like to continue?";
        public static string STANDARD_BATTLE_NO_UNDO = "You are about to start a battle with the following monster(s)! You will NOT be able to UNDO this action, would you like to continue?";
        public static string RUIN_BATTLE = "You are about to explore an ancient ruin which will trigger a battle, You Will NOT be able to UNDO this action, would you like to continue?";


        private HexItemDetail HexItemDetail;
        private Action<HexItemDetail> Yes_Callback;
        [SerializeField] private MonsterCardSlot prefab;
        [SerializeField] private Transform content;
        [SerializeField] private List<MonsterCardSlot> cardSlots = new List<MonsterCardSlot>();
        [SerializeField] private BattleCanvas battleCanvas;
        [SerializeField] private TextMeshProUGUI bodyText;

        public void SetupUI(HexItemDetail hex, Action<HexItemDetail> callback) {
            SetupUI(hex, callback, STANDARD_BATTLE);
        }

        public void SetupUI(HexItemDetail hex, Action<HexItemDetail> callback, string msg) {
            PlayerData pd = D.LocalPlayer;
            gameObject.SetActive(true);
            bodyText.text = msg;
            Yes_Callback = callback;
            HexItemDetail = hex;
            cardSlots.ForEach(c => { Destroy(c.gameObject); });
            cardSlots.Clear();
            hex.Monsters.ForEach(m => add(m, pd));
        }

        private void add(MonsterMetaData m, PlayerData pd) {
            MonsterCardSlot c = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            c.transform.SetParent(content);
            c.transform.localScale = Vector3.one;
            c.SetupUI(pd, m.Uniqueid);
            c.IsButtonInteractable(false);
            cardSlots.Add(c);
        }

        public void CombatConformation_YES() {
            gameObject.SetActive(false);
            GameAPI ar = new GameAPI();
            ar.P.PlayerTurnPhase = TurnPhase_Enum.Battle;
            ar.P.Battle.Monsters.Clear();
            ar.P.Battle.BattlePhase = BattlePhase_Enum.StartOfBattle;
            HexItemDetail.Monsters.ForEach(m => {
                ar.P.Battle.Monsters.Add(m.Uniqueid, m);
                if (!ar.P.VisableMonsters.Contains(m.Uniqueid)) {
                    ar.P.VisableMonsters.Add(m.Uniqueid);
                }
            });
            D.ScreenState = ScreenState_Enum.Combat;
            battleCanvas.SetupUI(ar);
            Yes_Callback(HexItemDetail);
        }

        public void CombatConformation_NO() {
            gameObject.SetActive(false);
        }
    }
}
