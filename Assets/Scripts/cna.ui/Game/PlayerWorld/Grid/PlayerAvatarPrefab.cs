using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class PlayerAvatarPrefab : MonoBehaviour {
        [SerializeField] private int playerKey;
        [SerializeField] private AddressableSprite avatarSpriteRenderer;
        [SerializeField] private Vector3 screenLocation;
        [SerializeField] private V2IntVO location;

        public int PlayerKey { get => playerKey; set => playerKey = value; }
        public Image_Enum AvatarImage { get => avatarSpriteRenderer.ImageEnum; set => avatarSpriteRenderer.ImageEnum = value; }

        public void UpdateUI(Grid mainGrid) {
            location = D.G.Players.Find(p => p.Key == playerKey).CurrentGridLoc;
            screenLocation = mainGrid.CellToWorld(location.Vector3Int);
            transform.position = screenLocation;
        }
    }
}
