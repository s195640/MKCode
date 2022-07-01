using System.Collections.Generic;
using UnityEngine;

namespace cna.ui {
    public class PlayerTilemap : UIGameBase {
        [SerializeField] private Grid mainGrid;
        [SerializeField] private PlayerAvatarPrefab PlayerAvatarPrefab_Prefab;
        [SerializeField] private List<PlayerAvatarPrefab> PlayerAvatarList = new List<PlayerAvatarPrefab>();

        public override void SetupUI() {
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

        public void UpdateUI() {
            CheckSetupUI();
            PlayerAvatarList.ForEach(p => p.UpdateUI(mainGrid));
        }
    }
}
