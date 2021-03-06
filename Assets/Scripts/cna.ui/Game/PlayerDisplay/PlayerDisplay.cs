using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cna.ui {
    public class PlayerDisplay : UIGameBase {

        [SerializeField] TopBar TopBar;
        [SerializeField] GameRoundPanel GameRoundPanel;
        [SerializeField] GameEffectPanel GameEffectPanel;
        [SerializeField] ActionCardSlot ActionCardSlot;
        [SerializeField] GameInfoPanel GameInfoPanel;
        [SerializeField] PlayerCardPanel PlayerCardPanel;

        public override void SetupUI() {
            _Clear();
        }

        public void UpdateUI() {
            CheckSetupUI();
            TopBar.UpdateUI();
            GameRoundPanel.UpdateUI();
            GameEffectPanel.UpdateUI();
            ActionCardSlot.UpdateUI();
            GameInfoPanel.UpdateUI();
            PlayerCardPanel.UpdateUI();
        }

        public override void Clear() {
            base.Clear();
            _Clear();
        }

        private void _Clear() {
            TopBar.Clear();
            GameRoundPanel.Clear();
            GameEffectPanel.Clear();
            ActionCardSlot.Clear();
            GameInfoPanel.Clear();
            PlayerCardPanel.Clear();
        }
    }
}
