using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class MonsterSelectionPanel : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI selectedCount;
        [SerializeField] private AttackSelectionPanel AttackSelectionPanel;
        [SerializeField] private BlockSelectionPanel BlockSelectionPanel;


        public void UpdateUI() {
            selectedCount.text = "Selected " + D.LocalPlayer.Battle.SelectedMonsters.Count;
            switch (D.LocalPlayer.Battle.BattlePhase) {
                case BattlePhase_Enum.RangeSiege: {
                    AttackSelectionPanel.gameObject.SetActive(true);
                    BlockSelectionPanel.gameObject.SetActive(false);
                    AttackSelectionPanel.UI_Update_Range();
                    break;
                }
                case BattlePhase_Enum.Block: {
                    AttackSelectionPanel.gameObject.SetActive(false);
                    BlockSelectionPanel.gameObject.SetActive(true);
                    BlockSelectionPanel.UI_Update_Block();
                    break;
                }
                case BattlePhase_Enum.AssignDamage: {
                    AttackSelectionPanel.gameObject.SetActive(false);
                    BlockSelectionPanel.gameObject.SetActive(true);
                    BlockSelectionPanel.UI_Update_Damage();
                    break;
                }
                case BattlePhase_Enum.Attack: {
                    AttackSelectionPanel.gameObject.SetActive(true);
                    BlockSelectionPanel.gameObject.SetActive(false);
                    AttackSelectionPanel.UI_Update_Attack();
                    break;
                }
            }
        }
    }
}
