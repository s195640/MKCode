using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class AttackSelectionPanel : MonoBehaviour {
        private Color BAD_COLOR = new Color32(197, 65, 16, 255);
        private Color GOOD_COLOR = new Color32(163, 255, 0, 255);

        [SerializeField] private TextMeshProUGUI MonsterArmorText;
        [SerializeField] private GameObject[] Fortified;
        [SerializeField] private GameObject ResistFire;
        [SerializeField] private GameObject ResistIce;
        [SerializeField] private GameObject ResistPhysical;
        [SerializeField] private GameObject DamageTypeSiege;
        [SerializeField] private GameObject DamageTypeRange;
        [SerializeField] private GameObject DamageTypeNormal;
        [SerializeField] private GameObject DamageTypeNoAttack;
        [SerializeField] private TextMeshProUGUI TotalDamageText;

        [Header("Range")]
        [SerializeField] private GameObject RangeMonsterPanel;
        [SerializeField] private ButtonContainer rangeKillButton;
        [SerializeField] private ButtonContainer rangeNextButton;
        [Header("Attack")]
        [SerializeField] private GameObject AttackMonsterPanel;
        [SerializeField] private ButtonContainer attackKillButton;
        [SerializeField] private ButtonContainer attackEndBattleButton;

        [SerializeField] private int damage = 0;
        private MonsterDetailsVO combinedMonsterDetails;

        public void UI_Update_Range() {
            Clear();
            if (D.LocalPlayer.Battle.SelectedMonsters.Count > 0) {
                AttackDetails();
                if (combinedMonsterDetails.Fortified) {
                    DamageTypeSiege.SetActive(true);
                    if (combinedMonsterDetails.DoubleFortified) {
                        DamageTypeNoAttack.SetActive(true);
                        TotalDamageText.text = "";
                    } else {
                        V2IntVO d = D.LocalPlayer.Battle.Siege.getDamage(ResistFire.activeSelf, ResistIce.activeSelf, ResistPhysical.activeSelf);
                        damage = d.X + d.Y / 2;
                        TotalDamageText.text = "" + damage;
                    }
                } else {
                    DamageTypeRange.SetActive(true);
                    V2IntVO d = D.LocalPlayer.Battle.Range.getDamage(ResistFire.activeSelf, ResistIce.activeSelf, ResistPhysical.activeSelf) + D.LocalPlayer.Battle.Siege.getDamage(ResistFire.activeSelf, ResistIce.activeSelf, ResistPhysical.activeSelf);
                    damage = d.X + d.Y / 2;
                    TotalDamageText.text = "" + damage;
                }
                if (damage >= combinedMonsterDetails.Armor) {
                    TotalDamageText.color = GOOD_COLOR;
                    rangeKillButton.Active = true;
                } else {
                    TotalDamageText.color = BAD_COLOR;
                    rangeKillButton.Active = false;
                }
            }
            RangeMonsterPanel.SetActive(true);
            rangeNextButton.Active = true;
        }

        public void UI_Update_Attack() {
            Clear();
            if (D.LocalPlayer.Battle.SelectedMonsters.Count > 0) {
                AttackDetails();
                DamageTypeNormal.SetActive(true);
                V2IntVO d = D.LocalPlayer.Battle.Attack.getDamage(ResistFire.activeSelf, ResistIce.activeSelf, ResistPhysical.activeSelf)
                    + D.LocalPlayer.Battle.Range.getDamage(ResistFire.activeSelf, ResistIce.activeSelf, ResistPhysical.activeSelf)
                    + D.LocalPlayer.Battle.Siege.getDamage(ResistFire.activeSelf, ResistIce.activeSelf, ResistPhysical.activeSelf);
                damage = d.X + d.Y / 2;
                TotalDamageText.text = "" + damage;
                if (damage >= combinedMonsterDetails.Armor) {
                    TotalDamageText.color = GOOD_COLOR;
                    attackKillButton.Active = true;
                } else {
                    TotalDamageText.color = BAD_COLOR;
                    attackKillButton.Active = false;
                }
            }
            AttackMonsterPanel.SetActive(true);
            attackEndBattleButton.Active = true;
        }

        private void AttackDetails() {
            D.LocalPlayer.Battle.SelectedMonsters.ForEach(m => {
                combinedMonsterDetails += D.B.MonsterDetails[m];
            });
            ResistFire.SetActive(combinedMonsterDetails.FireResistance);
            ResistIce.SetActive(combinedMonsterDetails.IceResistance);
            ResistPhysical.SetActive(combinedMonsterDetails.PhysicalResistance);
            Fortified[0].SetActive(combinedMonsterDetails.Fortified);
            Fortified[1].SetActive(combinedMonsterDetails.DoubleFortified);
            MonsterArmorText.text = "" + combinedMonsterDetails.Armor;
        }

        public void Clear() {
            MonsterArmorText.text = "";
            Fortified[0].SetActive(false);
            Fortified[1].SetActive(false);
            ResistFire.SetActive(false);

            ResistIce.SetActive(false);
            ResistPhysical.SetActive(false);
            DamageTypeSiege.SetActive(false);
            DamageTypeRange.SetActive(false);
            DamageTypeNormal.SetActive(false);
            DamageTypeNoAttack.SetActive(false);

            TotalDamageText.text = "";
            RangeMonsterPanel.SetActive(false);
            AttackMonsterPanel.SetActive(false);
            rangeKillButton.Active = false;
            attackKillButton.Active = false;
            combinedMonsterDetails = new MonsterDetailsVO();
        }
    }
}
