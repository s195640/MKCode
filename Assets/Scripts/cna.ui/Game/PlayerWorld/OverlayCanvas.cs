using TMPro;
using UnityEngine;

namespace cna.ui {
    public class OverlayCanvas : MonoBehaviour {
        [SerializeField] private GameObject DayImage;
        [SerializeField] private GameObject NightImage;
        [SerializeField] private TextMeshProUGUI DayNightCount;
        [SerializeField] private GameObject LastTurn;
        [SerializeField] private Camera WorldCamera;

        private bool isDay = true;
        private int gameTurnCounter = -1;

        public void UpdateUI() {
            UpdateUI_Day();
            UpdateUI_Count();
            UpdateUI_LastTurn();
        }
        private void UpdateUI_Day() {
            isDay = D.Scenario.isDay;
            DayImage.SetActive(isDay);
            NightImage.SetActive(!isDay);
            WorldCamera.backgroundColor = isDay ? CNAColor.Day : CNAColor.Night;
        }

        private void UpdateUI_Count() {
            gameTurnCounter = D.G.GameRoundCounter;
            string val;
            if (gameTurnCounter < 1) {
                val = "Day 00";
            } else {
                int dayNightVal = gameTurnCounter / 2 + gameTurnCounter % 2;
                if (isDay) {
                    val = "Day " + dayNightVal.ToString().PadLeft(2, '0');
                } else {
                    val = "Night " + dayNightVal.ToString().PadLeft(2, '0');
                }
            }
            DayNightCount.text = val;
        }

        private void UpdateUI_LastTurn() {
            LastTurn.SetActive(D.G.EndOfRound != -1);
        }
    }
}
