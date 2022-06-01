using System.Collections.Generic;
using System.Linq;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class GameEffectPanel : MonoBehaviour {
        [SerializeField] private ActionCardSlot ActionCard;
        [SerializeField] private TextMeshProUGUI effectCountText;
        [SerializeField] private List<CNA_Button> slots = new List<CNA_Button>();
        [SerializeField] private CNA_Button prefab;
        [SerializeField] private Transform content;
        [SerializeField] private List<GameEffect_Enum> slotData = new List<GameEffect_Enum>();


        public void UpdateUI() {
            UpdateUI_GameEffects();
        }

        private void UpdateUI_GameEffects() {
            CNAMap<GameEffect_Enum, WrapList<int>> gameEffects = D.LocalPlayer.GameEffects;
            List<GameEffect_Enum> data = new List<GameEffect_Enum>();
            gameEffects.Keys.ForEach(ge => {
                CardVO geCard = D.GetGameEffectCard(ge);
                if (geCard.GameEffectWorld) {
                    if (geCard.GameEffectDisplayMulti) {
                        gameEffects[ge].Values.ForEach(card => { data.Add(ge); });
                    } else {
                        data.Add(ge);
                    }
                }
            });
            if (!Enumerable.SequenceEqual(data, slotData)) {
                slots.ForEach(s => Destroy(s.gameObject));
                slots.Clear();
                slotData.Clear();
                slotData = data;
                slotData.ForEach(ge => {
                    CNA_Button slot = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                    slot.transform.SetParent(content);
                    slot.transform.localScale = Vector3.one;
                    CardVO cardGameEffect = D.GetGameEffectCard(ge);
                    Image_Enum image = cardGameEffect.CardImage;
                    string text = cardGameEffect.CardTitle;
                    Color color = cardGameEffect.GameEffectColor;
                    slot.SetupUI(text, color, image, true);
                    slot.UpdateUI_TextWidthFromLeft(98);
                    slot.addButtonClick(slots.Count, OnClick_GameEffect);
                    slots.Add(slot);
                });
                effectCountText.text = "Game Effects (" + slotData.Count + ")";
            }
        }

        public void OnClick_GameEffect(int ge) {
            CardVO cardGameEffect = D.GetGameEffectCard(slotData[ge]);
            ActionCard.SetupUI(cardGameEffect.UniqueId, CardHolder_Enum.GameEffect);
        }
    }
}
