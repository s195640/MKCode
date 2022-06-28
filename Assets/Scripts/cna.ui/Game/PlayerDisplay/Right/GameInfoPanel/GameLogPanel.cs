using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class GameLogPanel : MonoBehaviour {
        [SerializeField] private Transform GameLogContent;
        [SerializeField] private GameLogMessagePrefab GameLogMessagePrefabObj;
        [SerializeField] private Transform GameLogScroll;
        private List<GameLogMessagePrefab> gameLogList = new List<GameLogMessagePrefab>();

        private void Start() {
            int totalPlayers = D.G.Players.Count;
            float height = 1080 - 184 * totalPlayers;
            GameLogScroll.GetComponent<RectTransform>().sizeDelta = new Vector2(266.44f, height);
        }

        public void Update() {
            if (D.LogQueue.Count > 0) {
                AddLog(D.LogQueue.Dequeue());
            }
        }

        private void addUI(int playerId, string msg, long time) {
            DateTime dt = new DateTime(time);
            string timeStr = dt.ToString("hh:mm");
            PlayerData p = D.G.Players.Find(p => p.Key == playerId);
            string playerName = p.Name;
            AvatarMetaData a = D.AvatarMetaDataMap[p.Avatar];
            Image_Enum shieldImage = a.AvatarShieldId;
            GameLogMessagePrefab msgPrefab = Instantiate(GameLogMessagePrefabObj, Vector3.zero, Quaternion.identity);
            msgPrefab.transform.SetParent(GameLogContent);
            msgPrefab.transform.SetSiblingIndex(0);
            msgPrefab.transform.localScale = Vector3.one;
            msgPrefab.transform.localPosition = Vector3.zero;
            msgPrefab.SetupUI(playerName, shieldImage, timeStr, msg);
            gameLogList.Add(msgPrefab);
        }
        private void AddLog(LogData log) {
            addUI(log.PlayerId, log.Message, log.Time);
        }

        public void Clear() {
            foreach (GameLogMessagePrefab m in gameLogList.ToArray()) {
                Destroy(m.gameObject);
            }
            gameLogList.Clear();
        }
    }
}
