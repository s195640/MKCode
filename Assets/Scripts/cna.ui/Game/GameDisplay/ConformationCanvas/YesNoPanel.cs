using System;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class YesNoPanel : BasePanel {

        [SerializeField] private TextMeshProUGUI headText;
        [SerializeField] private TextMeshProUGUI bodyText;
        private Action yes;
        private Action no;

        public void SetupUI(string head, string body, Action yes, Action no) {
            gameObject.SetActive(true);
            headText.text = head;
            bodyText.text = body;
            this.yes = yes;
            this.no = no;
        }

        public void OnClick_Yes() {
            gameObject.SetActive(false);
            yes();
        }

        public void OnClick_No() {
            gameObject.SetActive(false);
            no();
        }
    }
}
