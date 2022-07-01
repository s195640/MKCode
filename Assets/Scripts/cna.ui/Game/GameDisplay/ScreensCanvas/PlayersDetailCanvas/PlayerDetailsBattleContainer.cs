using System.Collections;
using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class PlayerDetailsBattleContainer : MonoBehaviour {

        private Vector3 scale = new Vector3(.7f, .7f, 1f);

        [SerializeField] private GameObject NoBattle;
        [SerializeField] private GameObject Battle;

        [SerializeField] private MonsterHandPanel MonsterHandPanel;
        [SerializeField] private BattleEffectPanel BattleEffectPanel;

        [SerializeField] private TextMeshProUGUI BattleTitle;



        public void UpdateUI(PlayerData pd) {
            if (pd.PlayerTurnPhase == TurnPhase_Enum.Battle) {
                Battle.SetActive(true);
                NoBattle.SetActive(false);
                Dictionary<int, MonsterDetailsVO> monsterDetails = new Dictionary<int, MonsterDetailsVO>();
                pd.Battle.Monsters.Keys.ForEach(m => {
                    CardVO monsterCard = D.Cards[m];
                    monsterDetails.Add(monsterCard.UniqueId, new MonsterDetailsVO(monsterCard, pd.GameEffects));
                });
                MonsterHandPanel.UpdateUI(pd, monsterDetails, scale, true);
                BattleEffectPanel.UpdateUI(pd, 105);
                switch (pd.Battle.BattlePhase) {
                    case BattlePhase_Enum.SetupProvoke:
                    case BattlePhase_Enum.Provoke: {
                        BattleTitle.text = "Battle Phase : Provoke";
                        break;
                    }
                    case BattlePhase_Enum.RangeSiege: {
                        BattleTitle.text = "Battle Phase : Range Siege";
                        break;
                    }
                    case BattlePhase_Enum.Block: {
                        BattleTitle.text = "Battle Phase : Block";
                        break;
                    }
                    case BattlePhase_Enum.AssignDamage: {
                        BattleTitle.text = "Battle Phase : Assign Damage";
                        break;
                    }
                    case BattlePhase_Enum.Attack: {
                        BattleTitle.text = "Battle Phase : Attack";
                        break;
                    }
                    case BattlePhase_Enum.EndOfBattle: {
                        BattleTitle.text = "Battle Phase : End of Battle";
                        break;
                    }
                }
            } else {
                Battle.SetActive(false);
                NoBattle.SetActive(true);
            }
        }

    }
}
