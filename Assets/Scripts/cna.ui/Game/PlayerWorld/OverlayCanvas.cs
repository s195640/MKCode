using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class OverlayCanvas : MonoBehaviour {
        [SerializeField] private GameObject DayImage;
        [SerializeField] private GameObject NightImage;
        [SerializeField] private TextMeshProUGUI DayNightCount;
        [SerializeField] private GameObject LastTurn;
        [SerializeField] private Camera WorldCamera;
        [SerializeField] private GameObject WaitingOn;
        [SerializeField] private WaitingOnPlayerPrefab[] WaitingOnPlayerPrefab;

        private bool isDay = true;
        private int gameTurnCounter = -1;

        public void UpdateUI() {
            UpdateUI_Day();
            UpdateUI_Count();
            UpdateUI_LastTurn();
            UpdateUI_WaitingOn();
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
            LastTurn.SetActive(D.G.EndOfRound);
        }

        private void UpdateUI_WaitingOn() {
            bool turnOn = false;
            if (D.G.GameStatus == Game_Enum.Tactics_WaitingOnPlayers
                || D.G.GameStatus == Game_Enum.Tactics
                || D.G.GameStatus == Game_Enum.Player_Turn) {
                TurnPhase_Enum tp = D.LocalPlayer.PlayerTurnPhase;
                if (tp == TurnPhase_Enum.TacticsEnd
                    || tp == TurnPhase_Enum.EndTurn
                    || tp == TurnPhase_Enum.EndOfRound) {
                    for (int i = 0; i < 4; i++) {
                        turnOn |= WaitingOnPlayerPrefab[i].SetupUI(i);
                    }
                }
            }
            WaitingOn.SetActive(turnOn);
        }
    }
}
