using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class BlockSelectionPanel : MonoBehaviour {
        private Color BAD_COLOR = new Color32(197, 65, 16, 255);
        private Color GOOD_COLOR = new Color32(163, 255, 0, 255);

        [SerializeField] GameObject MonsterAttackNormal;
        [SerializeField] GameObject MonsterAttackFire;
        [SerializeField] GameObject MonsterAttackCold;
        [SerializeField] GameObject MonsterAttackColdFire;

        [SerializeField] private TextMeshProUGUI MonsterDamageText;
        [SerializeField] private TextMeshProUGUI TotalBlockText;

        [SerializeField] GameObject brutal;
        [SerializeField] GameObject paralyze;
        [SerializeField] GameObject poison;
        [SerializeField] GameObject swift;

        [SerializeField] GameObject effectiveBlock;
        [SerializeField] int effectiveDamage = 0;

        [SerializeField] private GameObject BlockMonsterPanel;
        [SerializeField] private ButtonContainer block;
        [SerializeField] private ButtonContainer next;

        private MonsterDetailsVO md;
        private PlayerData LocalPlayer;

        public void UI_Update_Block() {
            UpdateUI();
            next.Active = true;
            block.Active = false;
            BlockMonsterPanel.SetActive(true);
            if (LocalPlayer.Battle.SelectedMonsters.Count > 0) {
                effectiveBlock.SetActive(true);
                AttackData mod = new AttackData();
                LocalPlayer.GameEffects.Keys.ForEach(ge => {
                    LocalPlayer.GameEffects[ge].Values.ForEach(m => {
                        if (m == 0) {
                            switch (ge) {
                                case GameEffect_Enum.ColdToughness: {
                                    mod.Cold += md.TotalSymbols;
                                    break;
                                }
                                case GameEffect_Enum.UtemGuardsmen: {
                                    if (md.Swiftness) {
                                        mod.Physical += 4;
                                    }
                                    break;
                                }
                                case GameEffect_Enum.CUE_AltemGuardians01: {
                                    if (md.Swiftness) {
                                        mod.Physical += 8;
                                    }
                                    break;
                                }
                            }
                        }
                    });
                });

                effectiveDamage = LocalPlayer.Battle.Shield.getBlock(md.AttackType, mod);
                int calcMonsterDamage = md.Damage;
                if (md.Swiftness) {
                    calcMonsterDamage *= 2;
                }
                if (effectiveDamage >= calcMonsterDamage) {
                    TotalBlockText.color = GOOD_COLOR;
                    block.Active = true;
                } else {
                    TotalBlockText.color = BAD_COLOR;
                }
                TotalBlockText.text = "" + effectiveDamage;
            } else {
                TotalBlockText.text = "";
            }
        }

        public void UI_Update_Damage() {
            UpdateUI();
        }


        private void UpdateUI() {
            Clear();
            if (LocalPlayer.Battle.SelectedMonsters.Count > 0) {
                int m = LocalPlayer.Battle.SelectedMonsters[0];
                md = D.B.MonsterDetails[m];
                MonsterAttackNormal.SetActive(md.NormalAttack);
                MonsterAttackFire.SetActive(md.FireAttack);
                MonsterAttackCold.SetActive(md.ColdAttack);
                MonsterAttackColdFire.SetActive(md.ColdFireAttack);
                brutal.SetActive(md.Brutal);
                paralyze.SetActive(md.Paralyze);
                poison.SetActive(md.Poison);
                swift.SetActive(md.Swiftness);
                MonsterDamageText.text = "" + md.Damage;
            }
        }

        public void Clear() {
            LocalPlayer = D.LocalPlayer;
            MonsterDamageText.text = "";
            MonsterAttackNormal.SetActive(false);
            MonsterAttackFire.SetActive(false);
            MonsterAttackCold.SetActive(false);
            MonsterAttackColdFire.SetActive(false);
            brutal.SetActive(false);
            paralyze.SetActive(false);
            poison.SetActive(false);
            swift.SetActive(false);
            TotalBlockText.text = "";
            effectiveBlock.SetActive(false);
            BlockMonsterPanel.SetActive(false);
        }
    }
}
