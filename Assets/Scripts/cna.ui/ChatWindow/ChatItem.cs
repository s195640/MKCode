using TMPro;
using UnityEngine;

namespace cna.ui {
    public class ChatItem : UnityEngine.MonoBehaviour {
        [Header("GameObjects")]
        [SerializeField] private TextMeshProUGUI userName;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TextMeshProUGUI time;

        public void UpdateUI(string userName, string text, string time) {
            this.userName.text = userName;
            this.text.text = text;
            this.time.text = time;
        }
    }
}
