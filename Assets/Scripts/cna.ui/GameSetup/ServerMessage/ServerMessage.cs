using System;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class ServerMessage : UnityEngine.MonoBehaviour {
        [SerializeField] private TextMeshProUGUI systemText;
        [SerializeField] private GameObject disableScreen;

        public void DisplaySystemMessage(int code) {
            disableScreen.SetActive(false);
            gameObject.SetActive(true);
            string usrMsg = "";
            //if (code == 1002) {
            //    usrMsg = "username/password may be incorrect";
            //} else if (code == 1006) {
            //    usrMsg = "the server may be down or invalid";
            //}
            systemText.text = string.Format("Code : {0} Message : {1}{2}", code, Environment.NewLine, usrMsg);
        }

        public void OK_OnClick() {
            //gameObject.SetActive(false);
            //GE.Clean();
            //GE.UpdateUI();
        }
    }
}
