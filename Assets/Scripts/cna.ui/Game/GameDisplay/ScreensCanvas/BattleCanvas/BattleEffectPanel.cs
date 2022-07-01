using System.Collections.Generic;
using System.Linq;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class BattleEffectPanel : MonoBehaviour {

        [SerializeField] private ActionCardSlot ActionCard;
        [SerializeField] private List<CNA_Button> slots = new List<CNA_Button>();
        [SerializeField] private CNA_Button prefab;
        [SerializeField] private Transform content;
        [SerializeField] private List<GameEffect_Enum> slotData = new List<GameEffect_Enum>();


        public void UpdateUI(PlayerData pd, int beSize = 200) {
            UpdateUI_BattleEffects(pd, beSize);
        }

        private void UpdateUI_BattleEffects(PlayerData pd, int beSize) {
            CNAMap<GameEffect_Enum, WrapList<int>> gameEffects = pd.GameEffects;
            List<GameEffect_Enum> data = new List<GameEffect_Enum>();
            gameEffects.Keys.ForEach(ge => {
                CardVO geCard = D.GetGameEffectCard(ge);
                if (geCard.GameEffectBattle) {
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
                    slot.SetupUI_SetSize(beSize);
                    slot.addButtonClick(slots.Count, OnClick_GameEffect);
                    slots.Add(slot);
                });
            }
        }

        public void OnClick_GameEffect(int ge) {
            CardVO cardGameEffect = D.GetGameEffectCard(slotData[ge]);
            ActionCard.SetupUI(cardGameEffect.UniqueId, CardHolder_Enum.GameEffect);
        }
    }
}
