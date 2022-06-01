using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class MonsterPrefab : MonoBehaviour {

        [SerializeField] private V2IntVO location;
        [SerializeField] private Vector3 screenLocation;
        [SerializeField] private GameObject[] MonsterContainer;
        [SerializeField] private MonsterContainerPrefab[] Monster_01;
        [SerializeField] private MonsterContainerPrefab[] Monster_02;
        [SerializeField] private MonsterContainerPrefab[] Monster_03;

        public V2IntVO Location { get => location; set => location = value; }
        public Vector3 ScreenLocation { get => screenLocation; set => screenLocation = value; }
        public List<int> Monsters { get => D.G.Monsters.Map[Location].Values; }

        public void UpdateUI() {
            transform.position = ScreenLocation;
            MonsterContainer[0].SetActive(Monsters.Count == 1);
            MonsterContainer[1].SetActive(Monsters.Count == 2);
            MonsterContainer[2].SetActive(Monsters.Count > 2);
            switch (Monsters.Count) {
                case 5: {
                    updateSprite(Monster_03);
                    goto case 4;
                }
                case 4: {
                    updateSprite(Monster_03);
                    goto case 3;
                }
                case 3: {
                    updateSprite(Monster_03);
                    goto case 2;
                }
                case 2: {
                    updateSprite(Monster_02);
                    goto case 1;
                }
                case 1: {
                    updateSprite(Monster_01);
                    break;
                }
            }
        }

        private void updateSprite(MonsterContainerPrefab[] renderers) {
            for (int i = 0; i < renderers.Length; i++) {
                CardVO m = D.Cards[Monsters[i]];
                bool visable = D.LocalPlayer.VisableMonsters.Contains(m.UniqueId);
                renderers[i].SetupUI(m, visable);
            }
        }
    }
}
