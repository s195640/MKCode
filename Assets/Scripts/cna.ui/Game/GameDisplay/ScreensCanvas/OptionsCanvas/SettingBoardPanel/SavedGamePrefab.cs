using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class SavedGamePrefab : MonoBehaviour {
        [SerializeField] private string gameId;
        [SerializeField] private TextMeshProUGUI gameName;
        [SerializeField] private Button button;

        public string GameId { get => gameId; set => gameId = value; }

        public void SetupUI(DirectoryInfo di, Action<SavedGamePrefab> callback) {
            gameId = di.Name;
            gameName.text = di.CreationTime.ToString("yyyy-MM-dd HH:mm:ss");
            button.onClick.AddListener(() => callback(this));
        }

        public void Selected(bool flag) {
            GetComponent<Outline>().enabled = flag;
        }
    }
}
