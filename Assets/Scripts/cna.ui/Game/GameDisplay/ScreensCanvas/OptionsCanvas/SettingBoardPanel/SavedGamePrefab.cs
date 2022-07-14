using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class SavedGamePrefab : MonoBehaviour {
        [SerializeField] private long time;
        [SerializeField] private TextMeshProUGUI gameName;
        [SerializeField] private Button button;

        public long Time { get => time; set => time = value; }


        public void SetupUI(long time, LoadGameVO first, Action<SavedGamePrefab> callback) {
            this.time = time;
            gameName.text = first.getDescriptionName();
            button.onClick.AddListener(() => callback(this));
        }


        public void Selected(bool flag) {
            GetComponent<Outline>().enabled = flag;
        }
    }
}
