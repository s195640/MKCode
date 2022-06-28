using System.Collections.Generic;
using UnityEngine;

namespace cna.ui {
    public class PlayerTilemap : MonoBehaviour {
        private int seed = 0;
        [SerializeField] private Grid mainGrid;
        [SerializeField] private PlayerAvatarPrefab PlayerAvatarPrefab_Prefab;
        [SerializeField] private List<PlayerAvatarPrefab> PlayerAvatarList = new List<PlayerAvatarPrefab>();

        public void UpdateUI() {
            if (seed != D.GLD.Seed) {
                seed = D.GLD.Seed;
                PlayerAvatarList.ForEach(p => Destroy(p.gameObject));
                PlayerAvatarList.Clear();
                D.G.Players.ForEach(p => {
                    if (!p.DummyPlayer) {
                        PlayerAvatarPrefab a = Instantiate(PlayerAvatarPrefab_Prefab, Vector3.zero, Quaternion.identity);
                        a.transform.SetParent(transform);
                        a.transform.localScale = Vector3.one;
                        a.PlayerKey = p.Key;
                        a.AvatarImage = p.Avatar;
                        PlayerAvatarList.Add(a);
                    }
                });
            }
            PlayerAvatarList.ForEach(p => p.UpdateUI(mainGrid));
        }
    }
}
