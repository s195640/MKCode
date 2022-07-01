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
            PlayerData pd = D.LocalPlayer;
            ManaPoolContainer.UpdateUI();
            UnitOfferingContainer.UpdateUI(pd);
            SkillOfferingContainer.UpdateUI(pd);
            UpdateUI_Spell(pd);
            UpdateUI_Advanced(pd);
            UpdateUI_Tactics();
            UpdateUI_Scenario();
        }

        public void UpdateUI_Spell(PlayerData pd) {
            List<int> spellIds = pd.Board.SpellOffering;
            for (int i = 0; i < 3; i++) {
                if (spellIds.Count > i) {
                    spellSlots[i].gameObject.SetActive(true);
                    if (spellSlots[i].UniqueCardId != spellIds[i]) {
                        spellSlots[i].SetupUI(pd, spellIds[i], CardHolder_Enum.SpellOffering);
                    }
                } else {
                    spellSlots[i].gameObject.SetActive(false);
                }

            }
        }

        public void UpdateUI_Advanced(PlayerData pd) {
            List<int> advancedIds = pd.Board.AdvancedOffering;
            for (int i = 0; i < 3; i++) {
                advancedSlots[i].gameObject.SetActive(true);
                if (advancedIds.Count > i) {
                    if (advancedSlots[i].UniqueCardId != advancedIds[i]) {
                        advancedSlots[i].SetupUI(pd, advancedIds[i], CardHolder_Enum.AdvancedOffering);
                    }
                } else {
                    advancedSlots[i].gameObject.SetActive(false);
                }
            }
        }

        public void UpdateUI_Tactics() {
            for (int i = 0; i < 4; i++) {
                if (i >= D.G.Players.Count) {
                    tacticsSlots[i].gameObject.SetActive(false);
                } else {
                    if (tacticsSlots[i].UniqueCardId != D.G.Players[i].Deck.TacticsCardId) {
                        tacticsSlots[i].SetupUI(D.G.Players[i], D.G.Players[i].Deck.TacticsCardId, CardHolder_Enum.TacticsBoard);
                    }
                }
            }
        }

        public void UpdateUI_Scenario() {
            string msg = string.Format("Rounds {0}\nBasic Tiles {1}\nCore Tiles {2}\nCity Tiles {3}\nCity Levels {4}", D.GLD.Rounds, D.GLD.BasicTiles, D.GLD.CoreTiles, D.GLD.CityTiles, D.GLD.Level);
            ScenarioDetailText.text = msg;
        }

    }
}
