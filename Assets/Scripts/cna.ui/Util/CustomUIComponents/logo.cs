using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class logo : MonoBehaviour {
        [SerializeField] TextMeshProUGUI gameVersion;

        public void Start() {
            gameVersion.text = "v " + Application.version;
        }
    }
}
