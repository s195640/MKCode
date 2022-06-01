using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class PlayerOfferingCanvas : MonoBehaviour {

        [SerializeField] private ManaPoolContainer ManaPoolContainer;
        [SerializeField] private UnitOfferingContainer UnitOfferingContainer;
        [SerializeField] private SkillOfferingContainer SkillOfferingContainer;

        [Header("Map Hex")]
        [SerializeField] private TextMeshProUGUI BasicTileNumber;
        [SerializeField] private TextMeshProUGUI CoreTileNumber;
        [SerializeField] private TextMeshProUGUI ScenarioDetailText;

        [Header("Spells")]
        [SerializeField] private NormalCardSlot[] spellSlots;

        [Header("Advanced")]
        [SerializeField] private NormalCardSlot[] advancedSlots;

        [Header("Tactics")]
        [SerializeField] private NormalCardSlot[] tacticsSlots;





        public void UpdateUI() {
            ManaPoolContainer.UpdateUI();
            UnitOfferingContainer.UpdateUI();
            SkillOfferingContainer.UpdateUI();
            UpdateUI_Spell();
            UpdateUI_Advanced();
            UpdateUI_Tactics();
            UpdateUI_Scenario();
        }

        public void UpdateUI_Spell() {
            List<int> spellIds = D.G.Board.SpellOffering;
            for (int i = 0; i < 3; i++) {
                if (spellSlots[i].UniqueCardId != spellIds[i]) {
                    spellSlots[i].SetupUI(spellIds[i], CardHolder_Enum.SpellOffering);
                }
            }
        }

        public void UpdateUI_Advanced() {
            List<int> advancedIds = D.G.Board.AdvancedOffering;
            for (int i = 0; i < 3; i++) {
                if (advancedSlots[i].UniqueCardId != advancedIds[i]) {
                    advancedSlots[i].SetupUI(advancedIds[i], CardHolder_Enum.AdvancedOffering);
                }
            }
        }

        public void UpdateUI_Tactics() {
            for (int i = 0; i < 4; i++) {
                if (i >= D.G.Players.Count) {
                    tacticsSlots[i].gameObject.SetActive(false);
                } else {
                    if (tacticsSlots[i].UniqueCardId != D.G.Players[i].Deck.TacticsCardId) {
                        tacticsSlots[i].SetupUI(D.G.Players[i].Deck.TacticsCardId, CardHolder_Enum.TacticsBoard);
                    }
                }
            }
        }

        public void UpdateUI_Scenario() {
            string msg = string.Format("Rounds {0}\nBasic Tiles {1}\nCore Tiles {2}\nCity Tiles {3}\nCity Levels {4}", D.G.Board.Rounds, D.G.Board.Basic, D.G.Board.Core, D.G.Board.City, D.G.Board.Level);
            ScenarioDetailText.text = msg;
        }

    }
}
