using System;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class ServerMessage : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI headText;
        [SerializeField] private TextMeshProUGUI bodyText;
        [SerializeField] private AddressableImage image;
        [SerializeField] private TextMeshProUGUI buttonText;

        Action callback;

        public void LoginFailed(Action callback) {
            headText.text = "Connection Failed";
            bodyText.text = "Check UserName name and password.";
            buttonText.text = "OK";
            image.ImageEnum = Image_Enum.I_warning;
            this.callback = callback;
            gameObject.SetActive(true);
        }

        public void JoinGame(Action callback) {
            headText.text = "Joining Game";
            bodyText.text = "";
            buttonText.text = "Cancel";
            image.ImageEnum = Image_Enum.I_info;
            this.callback = callback;
            gameObject.SetActive(true);
        }

        public void CreateGame(Action callback) {
            headText.text = "Creating Game";
            bodyText.text = "";
            buttonText.text = "Cancel";
            image.ImageEnum = Image_Enum.I_info;
            this.callback = callback;
            gameObject.SetActive(true);
        }


        public void OK_OnClick() {
            gameObject.SetActive(false);
            callback();
        }
    }
}
