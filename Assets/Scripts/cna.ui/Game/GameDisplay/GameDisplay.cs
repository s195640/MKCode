using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cna.ui {
    public class GameDisplay : UIGameBase {

        [SerializeField] private OverlayCanvas OverlayCanvas;
        [SerializeField] private ScreensCanvas ScreensCanvas;
        [SerializeField] private ConformationCanvas ConformationCanvas;
        [SerializeField] private GameObject NotificationCanvas;

        public override void SetupUI() {
            Clear();
        }

        public void UpdateUI() {
            CheckSetupUI();
            OverlayCanvas.UpdateUI();
            ScreensCanvas.UpdateUI();
            ConformationCanvas.UpdateUI();
        }

        public override void Clear() {
            OverlayCanvas.gameObject.SetActive(true);
            ScreensCanvas.gameObject.SetActive(true);
            ConformationCanvas.gameObject.SetActive(true);
            ConformationCanvas.Clean();
            NotificationCanvas.SetActive(true);
        }
    }
}
