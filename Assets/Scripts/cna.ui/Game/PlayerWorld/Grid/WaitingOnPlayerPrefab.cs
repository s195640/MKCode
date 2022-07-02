using System;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class WaitingOnPlayerPrefab : MonoBehaviour {
        [SerializeField] private AddressableImage AvatarShield;
        [SerializeField] private TextMeshProUGUI AvatarName;
        [SerializeField] private TextMeshProUGUI AvatarTotalTime;
        [SerializeField] private TextMeshProUGUI AvatarTime;
        private DateTime activateTime;
        PlayerData playerData = null;

        public bool SetupUI(int index) {
            if (index >= D.G.Players.Count) {
                gameObject.SetActive(false);
                return false;
            } else {
                playerData = D.G.Players[index];
                AvatarShield.ImageEnum = D.AvatarMetaDataMap[playerData.Avatar].AvatarShieldId;
                AvatarName.text = playerData.Name;
                TurnPhase_Enum tp = playerData.PlayerTurnPhase;
                bool turnOn = (tp > TurnPhase_Enum.TacticsNotTurn && tp < TurnPhase_Enum.TacticsEnd)
                    || (tp > TurnPhase_Enum.PlayerNotTurn && tp < TurnPhase_Enum.EndTurn);
                if (!gameObject.activeSelf && turnOn) {
                    activateTime = DateTime.Now;
                }
                gameObject.SetActive(turnOn);
                return turnOn;
            }
        }

        private void Update() {
            int hour = (int)(DateTime.Now - activateTime).TotalHours;
            int min = (int)(DateTime.Now - activateTime).TotalMinutes - hour * 60;
            int sec = (int)(DateTime.Now - activateTime).TotalSeconds - hour * 60 - min * 60;
            AvatarTime.text = string.Format("{0}:{1}:{2}", ("" + hour).PadLeft(2, '0'), ("" + min).PadLeft(2, '0'), ("" + sec).PadLeft(2, '0'));
            if (playerData != null) {
                AvatarTotalTime.gameObject.SetActive(true);
                AvatarTotalTime.text = "0" + playerData.GetTime();
            } else {
                AvatarTotalTime.gameObject.SetActive(false);
            }
        }
    }
}
