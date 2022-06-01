using UnityEngine;
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
        [SerializeField] private GameObject disableScreen;

        private void Start() {
            soloGameButton.onClick.AddListener(SoloGameButtonCallback);
            multiGameButton.onClick.AddListener(MultiGameButtonCallback);
            exitButton.onClick.AddListener(Exit_OnClick);
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
    }
}
