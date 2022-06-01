using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class ShieldTilemap : MonoBehaviour {
        [SerializeField] private Grid MainGrid;
        [SerializeField] private PlayerShieldTokenPrefab PlayerShieldTokenPrefab;
        [SerializeField] private List<PlayerShieldTokenPrefab> ShieldList = new List<PlayerShieldTokenPrefab>();

        public void UpdateUI() {
            foreach (V2IntVO pos in D.G.Monsters.Shield.Keys) {
                PlayerShieldTokenPrefab t = ShieldList.Find(s => s.Location.Equals(pos));
                if (t == null) {
                    PlayerShieldTokenPrefab prefab = Instantiate(PlayerShieldTokenPrefab, Vector3.zero, Quaternion.identity);
                    prefab.transform.SetParent(transform);
                    prefab.transform.localScale = Vector3.one;
                    ShieldList.Add(prefab);
                    prefab.SetupUI(MainGrid, pos, D.G.Monsters.Shield[pos].Values);
                } else {
                    t.UpdateUI(D.G.Monsters.Shield[pos].Values);
                }
            }
            foreach (PlayerShieldTokenPrefab t in ShieldList.ToArray()) {
                if (!D.G.Monsters.Shield.ContainsKey(t.Location)) {
                    Destroy(t.gameObject);
                    ShieldList.Remove(t);
                }
            }
        }
    }
}
