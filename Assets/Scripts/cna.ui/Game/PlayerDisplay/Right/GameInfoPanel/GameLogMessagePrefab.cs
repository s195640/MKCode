using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class GameLogMessagePrefab : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI PlayerName;
        [SerializeField] private TextMeshProUGUI Time;
        [SerializeField] private TextMeshProUGUI Message;
        [SerializeField] private AddressableImage ShieldImage;

        public void SetupUI(string name, Image_Enum shieldImage, string time, string msg) {
            PlayerName.text = name;
            Time.text = time;
            Message.text = msg;
            ShieldImage.ImageEnum = shieldImage;
        }
    }
}
