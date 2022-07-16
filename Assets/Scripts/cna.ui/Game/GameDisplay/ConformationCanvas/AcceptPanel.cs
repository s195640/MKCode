using System;
using System.Collections;
using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class AcceptPanel : BasePanel {

        [SerializeField] private Image backgroundImage;
        [SerializeField] private TextMeshProUGUI headText;
        [SerializeField] private TextMeshProUGUI bodyText;
        [SerializeField] private CNA_Button[] buttons;

        private GameAPI ar;
        private List<Action<GameAPI>> callbacks;

        public void SetupUI(GameAPI ar, string head, string body, List<Action<GameAPI>> callbacks, List<string> buttonText, List<Color32> buttonColor, Color32 backgroundColor) {
            Clear();
            this.ar = ar;
            headText.text = head;
            bodyText.text = body;
            this.callbacks = callbacks;
            backgroundImage.color = backgroundColor;
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            for (int i = 0; i < callbacks.Count; i++) {
                buttons[i].gameObject.SetActive(true);
                buttons[i].SetupUI(buttonText[i], buttonColor[i]);
            }
            gameObject.SetActive(true);
        }

        public void OnClick_Button(int index) {
            gameObject.SetActive(false);
            callbacks[index](ar);
        }

    }
}
