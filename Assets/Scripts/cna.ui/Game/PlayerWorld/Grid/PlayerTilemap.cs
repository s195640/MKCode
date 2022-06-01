using System.Collections.Generic;
using UnityEngine;

namespace cna.ui {
    public class PlayerTilemap : MonoBehaviour {
        private string gameid = "";
        [SerializeField] private Grid mainGrid;
        [SerializeField] private PlayerAvatarPrefab PlayerAvatarPrefab_Prefab;
        [SerializeField] private List<PlayerAvatarPrefab> PlayerAvatarList = new List<PlayerAvatarPrefab>();

        public void UpdateUI() {
            if (!D.G.GameId.Equals(gameid)) {
                gameid = D.G.GameId;
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
