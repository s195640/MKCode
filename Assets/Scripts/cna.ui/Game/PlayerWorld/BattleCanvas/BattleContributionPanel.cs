using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class BattleContributionPanel : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI[] siege;
        [SerializeField] private TextMeshProUGUI[] range;
        [SerializeField] private TextMeshProUGUI[] normal;
        [SerializeField] private TextMeshProUGUI[] sheild;
        [SerializeField] private TextMeshProUGUI move;
        [SerializeField] private TextMeshProUGUI influence;

        public void UpdateUI() {
            PlayerData p = D.LocalPlayer;
            BattleData battle = p.Battle;
            updateVal(ref siege, battle.Siege);
            updateVal(ref range, battle.Range);
            updateVal(ref normal, battle.Attack);
            updateVal(ref sheild, battle.Shield);
            move.text = "" + p.Movement;
            influence.text = "" + p.Influence;
        }

        private void updateVal(ref TextMeshProUGUI[] val, AttackData attack) {
            val[0].text = "" + attack.Physical;
            val[1].text = "" + attack.Fire;
            val[2].text = "" + attack.Cold;
            val[3].text = "" + attack.ColdFire;
        }
    }
}
