using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class MonsterTilemap : MonoBehaviour {
        [SerializeField] private Grid MainGrid;
        [SerializeField] private MonsterPrefab MonsterPrefab_Prefab;
        [SerializeField] private List<MonsterPrefab> MonsterList = new List<MonsterPrefab>();

        public void UpdateUI() {
            foreach (V2IntVO mhpos in D.G.Monsters.Map.Keys) {
                MonsterPrefab m = MonsterList.Find(m => m.Location.Equals(mhpos));
                if (m == null) {
                    MonsterPrefab monster = Instantiate(MonsterPrefab_Prefab, Vector3.zero, Quaternion.identity);
                    monster.transform.SetParent(transform);
                    monster.transform.localScale = Vector3.one;
                    monster.Location = mhpos;
                    monster.ScreenLocation = MainGrid.CellToWorld(mhpos.Vector3Int);
                    MonsterList.Add(monster);
                    monster.UpdateUI();
                } else {
                    m.UpdateUI();
                }
            }
            foreach (MonsterPrefab m in MonsterList.ToArray()) {
                if (!D.G.Monsters.Map.ContainsKey(m.Location)) {
                    Destroy(m.gameObject);
                    MonsterList.Remove(m);
                }
            }
        }
    }
}
