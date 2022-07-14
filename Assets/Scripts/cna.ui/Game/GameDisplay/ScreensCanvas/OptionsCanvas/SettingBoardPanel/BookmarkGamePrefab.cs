using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class BookmarkGamePrefab : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI turn;
        [SerializeField] private Button button;

        public LoadGameVO LG;
        public string FileName {
            get {
                return LG.fileName;
            }
        }

        public void SetupUI(LoadGameVO lg, Action<BookmarkGamePrefab> callback) {
            LG = lg;
            turn.text = lg.turn;
            button.onClick.AddListener(() => callback(this));
        }
        public void Selected(bool flag) {
            GetComponent<Outline>().enabled = flag;
        }
    }
}
