using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class BookmarkGamePrefab : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI time;
        [SerializeField] private TextMeshProUGUI turn;
        [SerializeField] private TextMeshProUGUI player;
        [SerializeField] private Button button;

        public string fileName = "";
        public string fullFileName = "";

        public void SetupUI(FileInfo fi, Action<BookmarkGamePrefab> callback) {
            time.text = fi.CreationTime.ToString("HH:mm:ss");
            turn.text = fi.Name.Split("_")[0];
            player.text = fi.Name.Split("_")[1];
            button.onClick.AddListener(() => callback(this));
            fileName = fi.Name;
            fullFileName = fi.FullName;
        }
        public void Selected(bool flag) {
            GetComponent<Outline>().enabled = flag;
        }
    }
}
