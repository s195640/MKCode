using System.Collections;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class NotificationCanvas : MonoBehaviour {
        [SerializeField] private GameObject NotificationPanel;
        [SerializeField] private TextMeshProUGUI NotificationText;

        private bool running = false;
        private IEnumerator co;

        public void Msg(string message) {
            NotificationText.text = message;
            if (running && co != null) {
                StopCoroutine(co);
                running = false;
            }
            co = TurnOffMsg();
            StartCoroutine(co);
        }

        IEnumerator TurnOffMsg() {
            running = true;
            NotificationPanel.SetActive(true);
            yield return new WaitForSeconds(5);
            NotificationPanel.SetActive(false);
            running = false;
        }
    }
}
