using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class App : AppEngine {

        [SerializeField] GameObject appCanvas;
        [SerializeField] GameSetup gameSetup;
        [SerializeField] Game game;
        [SerializeField] PlayerWorld playerWorld;
        [SerializeField] GameObject chatWindow;
        [SerializeField] StartOfTurnPanel StartOfTurnPanel;
        [SerializeField] TacticsPanel TacticsPanel;

        public override void UpdateUI() {
            switch (D.G.GameStatus) {
                case Game_Enum.NA:
                case Game_Enum.CHAR_CREATION: {
                    gameSetup.gameObject.SetActive(true);
                    playerWorld.gameObject.SetActive(false);
                    game.gameObject.SetActive(false);
                    gameSetup.UpdateUI();
                    break;
                }
                default: {
                    gameSetup.gameObject.SetActive(false);
                    game.gameObject.SetActive(true);
                    game.UpdateUI();
                    break;
                }
            }
            appCanvas.SetActive(true);
            chatWindow.SetActive(D.ClientState > ClientState_Enum.MULTI_PLAYER);
        }

        public override void Clear() {
            D.ScreenState = ScreenState_Enum.Map;
            game.Clear();
        }

        public override void StartTacticsPanel() {
            TacticsPanel.SetupUI(D.LocalPlayer);
        }
    }
}
