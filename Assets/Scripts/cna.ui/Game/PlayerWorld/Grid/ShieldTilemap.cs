using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class ShieldTilemap : MonoBehaviour {
        [SerializeField] private Grid MainGrid;
        [SerializeField] private PlayerShieldTokenPrefab PlayerShieldTokenPrefab;
        [SerializeField] private List<PlayerShieldTokenPrefab> ShieldList = new List<PlayerShieldTokenPrefab>();

        public void UpdateUI() {
            Dictionary<V2IntVO, List<Image_Enum>> shields = BasicUtil.getAllShields(D.G);
            foreach (V2IntVO pos in shields.Keys) {
                PlayerShieldTokenPrefab t = ShieldList.Find(s => s.Location.Equals(pos));
                if (t == null) {
                    PlayerShieldTokenPrefab prefab = Instantiate(PlayerShieldTokenPrefab, Vector3.zero, Quaternion.identity);
                    prefab.transform.SetParent(transform);
                    prefab.transform.localScale = Vector3.one;
                    ShieldList.Add(prefab);
                    prefab.SetupUI(MainGrid, pos, shields[pos]);
                } else {
                    t.UpdateUI(shields[pos]);
                }
            }
            foreach (PlayerShieldTokenPrefab t in ShieldList.ToArray()) {
                if (!shields.ContainsKey(t.Location)) {
                    Destroy(t.gameObject);
                    ShieldList.Remove(t);
                }
            }
        }
    }
}
