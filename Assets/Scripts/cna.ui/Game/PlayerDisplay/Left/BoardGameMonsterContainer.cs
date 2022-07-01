using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class BoardGameMonsterContainer : MonoBehaviour {
        [SerializeField] private MonsterCardSlot prefab;
        [SerializeField] private List<MonsterCardSlot> cardSlots = new List<MonsterCardSlot>();
        public void UpdateUI(PlayerData pd, HexItemDetail hex) {
            cardSlots.ForEach(c => c.gameObject.SetActive(false));
            List<int> monsters = hex.LocalPlayer.Board.MonsterData[hex.GridPosition].Values;
            for (int i = 0; i < monsters.Count; i++) {
                RectTransform t = cardSlots[i].GetComponent<RectTransform>();
                setTransform(t, i, monsters.Count);
                cardSlots[i].SetupUI(pd, monsters[i]);
                cardSlots[i].gameObject.SetActive(true);
            }
        }


        private void setTransform(RectTransform t, int index, int total) {
            t.pivot = new Vector2(0, 0);
            t.anchorMin = new Vector2(0, 0);
            t.anchorMax = new Vector2(0, 0);
            float scale = 1f;
            switch (total) {
                case 1: {
                    scale = 1.3f;
                    t.anchoredPosition = new Vector3(10f, 0, 0);
                    break;
                }
                case 2: {
                    scale = .75f;
                    switch (index) {
                        case 0: {
                            t.anchoredPosition = new Vector3(0f, 130f, 1f);
                            break;
                        }
                        case 1: {
                            t.anchoredPosition = new Vector3(130f, 0f, 1f);
                            break;
                        }
                    }
                    break;
                }
                case 3: {
                    scale = .70f;
                    switch (index) {
                        case 0: {
                            t.anchoredPosition = new Vector3(75f, 140f, 1f);
                            break;
                        }
                        case 1: {
                            t.anchoredPosition = new Vector3(0, 0f, 1f);
                            break;
                        }
                        case 2: {
                            t.anchoredPosition = new Vector3(140f, 0f, 1f);
                            break;
                        }
                    }
                    break;
                }
                case 4: {
                    scale = .70f;
                    switch (index) {
                        case 0: {
                            t.anchoredPosition = new Vector3(0f, 140f, 1f);
                            break;
                        }
                        case 1: {
                            t.anchoredPosition = new Vector3(140f, 140f, 1f);
                            break;
                        }
                        case 2: {
                            t.anchoredPosition = new Vector3(70f, 80f, 1f);
                            break;
                        }
                        case 3: {
                            t.anchoredPosition = new Vector3(210f, 80f, 1f);
                            break;
                        }
                    }
                    break;
                }
                case 5: {
                    scale = .55f;
                    switch (index) {
                        case 0: {
                            t.anchoredPosition = new Vector3(0f, 175f, 1f);
                            break;
                        }
                        case 1: {
                            t.anchoredPosition = new Vector3(170f, 175f, 1f);
                            break;
                        }
                        case 2: {
                            t.anchoredPosition = new Vector3(55f, 63f, 1f);
                            break;
                        }
                        case 3: {
                            t.anchoredPosition = new Vector3(225f, 63f, 1f);
                            break;
                        }
                        case 4: {
                            t.anchoredPosition = new Vector3(145f, 155f, 1f);
                            break;
                        }
                    }
                    break;
                }
            }
            t.localScale = new Vector3(scale, scale, 1f);
        }
    }
}
