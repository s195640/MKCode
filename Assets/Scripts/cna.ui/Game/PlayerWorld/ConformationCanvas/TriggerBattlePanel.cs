using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class TriggerBattlePanel : BasePanel {

        private HexItemDetail HexItemDetail;
        private Action<HexItemDetail> Yes_Callback;
        [SerializeField] private MonsterCardSlot prefab;
        [SerializeField] private Transform content;
        [SerializeField] private List<MonsterCardSlot> cardSlots = new List<MonsterCardSlot>();
        [SerializeField] private BattleCanvas battleCanvas;


        public void SetupUI(HexItemDetail hex, Action<HexItemDetail> callback) {
            gameObject.SetActive(true);
            Yes_Callback = callback;
            HexItemDetail = hex;
            cardSlots.ForEach(c => { Destroy(c.gameObject); });
            cardSlots.Clear();
            hex.Monsters.ForEach(m => add(m));
        }

        private void add(MonsterMetaData m) {
            MonsterCardSlot c = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            c.transform.SetParent(content);
            c.transform.localScale = Vector3.one;
            c.SetupUI(m.Uniqueid);
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
