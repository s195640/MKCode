using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cna.ui {
    public class GameDisplay : UIGameBase {

        [SerializeField] private OverlayCanvas OverlayCanvas;
        [SerializeField] private ScreensCanvas ScreensCanvas;
        [SerializeField] private ConformationCanvas ConformationCanvas;
        [SerializeField] private GameObject NotificationCanvas;
        [SerializeField] private GoodsCanvas GoodsCanvas;

        public override void SetupUI() {
            _Clear();
        }

        public void UpdateUI() {
            CheckSetupUI();
            OverlayCanvas.UpdateUI();
            ScreensCanvas.UpdateUI();
            ConformationCanvas.UpdateUI();
            GoodsCanvas.UpdateUI();
        }

        public override void Clear() {
            base.Clear();
            _Clear();
        }

        private void _Clear() {
            OverlayCanvas.gameObject.SetActive(true);
            ScreensCanvas.gameObject.SetActive(true);
            ConformationCanvas.gameObject.SetActive(true);
            ConformationCanvas.Clear();
            NotificationCanvas.SetActive(true);
            ScreensCanvas.Clear();
            GoodsCanvas.Clear();
        }
    }
}
