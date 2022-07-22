using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace cna.ui {
    public class StartupLobby : MonoBehaviour {
        [Header("GameObjects")]
        [SerializeField] private UIEngine UiEngine;
        [SerializeField] private CNA_Input loginText;
        [SerializeField] private CNA_Input serverText;
        [SerializeField] private CNA_Input passwordText;
        [SerializeField] private Button soloGameButton;
        [SerializeField] private Button multiGameButton;
        [SerializeField] private Button exitButton;

        private void Start() {
            soloGameButton.onClick.AddListener(SoloGameButtonCallback);
            multiGameButton.onClick.AddListener(MultiGameButtonCallback);
            exitButton.onClick.AddListener(Exit_OnClick);
#if UNITY_EDITOR
            setupUnityEditor();
#else
            StartCoroutine(getServerURL());
#endif
        }

        public void UpdateUI() { }

        private void SoloGameButtonCallback() {
            if (loginText.ValidateNotEmpty()) {
                gameObject.SetActive(false);
                D.C.StartNewSoloGame(loginText.InputValue);
            }
        }

        private void MultiGameButtonCallback() {
            if (loginText.ValidateNotEmpty() || serverText.ValidateNotEmpty() || passwordText.ValidateNotEmpty()) {
                gameObject.SetActive(false);
                D.C.ConnectToServer(loginText.InputValue, passwordText.InputValue, serverText.InputValue);
            }
        }

        public void Exit_OnClick() {
            Application.Quit();
        }

        IEnumerator getServerURL() {
            UnityWebRequest www = new UnityWebRequest("https://raw.githubusercontent.com/s195640/MKCode/master/README.md");
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            serverText.InputValue = www.downloadHandler.text.Trim();
        }

        private void setupUnityEditor() {
            serverText.InputValue = "ws://localhost:8080/​";
            loginText.InputValue = "login";
        }

        public void OnClick_UseLocalHost() {
            serverText.InputValue = "ws://localhost:8080/​";
            loginText.InputValue = "login02";
        }

        public void OnClick_Link() {
            string url = serverText.InputValue.Replace("ws", "http").Replace("\u200B", "").Trim();
            Application.OpenURL(url);
        }
    }
}
