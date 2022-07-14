using System.Collections;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class ScreensCanvas : BaseCanvas {

        [SerializeField] private PlayerOfferingCanvas PlayerOfferingCanvas;
        [SerializeField] private BattleCanvas BattleCanvas;
        [SerializeField] private PlayersDetailCanvas PlayersDetailCanvas;
        [SerializeField] private LegendMonsterCanvas LegendMonsterCanvas;
        [SerializeField] private LegendRuinsCanvas LegendRuinsCanvas;
        [SerializeField] private LegendIconsCanvas LegendIconsCanvas;
        [SerializeField] private ScoreBoardCanvas ScoreBoardCanvas;
        [SerializeField] private SettingsCanvas SettingsCanvas;
        [SerializeField] private FinalScoreCanvas FinalScoreCanvas;


        public override void SetupUI() {
            D.ScreenState = ScreenState_Enum.Map;
        }

        public void UpdateUI() {
            CheckSetupUI();
            UpdateUI_SetActive();
            UpdateUI_Screens();
        }

        private void UpdateUI_Screens() {
            PlayerOfferingCanvas.UpdateUI();
            BattleCanvas.UpdateUI();
            PlayersDetailCanvas.UpdateUI();
            LegendMonsterCanvas.UpdateUI();
            LegendRuinsCanvas.UpdateUI();
            LegendIconsCanvas.UpdateUI();
            ScoreBoardCanvas.UpdateUI();
            SettingsCanvas.UpdateUI();
            FinalScoreCanvas.UpdateUI();
        }

        private void UpdateUI_SetActive() {
            PlayerOfferingCanvas.gameObject.SetActive(D.ScreenState == ScreenState_Enum.Offering);
            BattleCanvas.gameObject.SetActive(D.ScreenState == ScreenState_Enum.Combat);
            PlayersDetailCanvas.gameObject.SetActive(D.ScreenState == ScreenState_Enum.MultiCombat);
            LegendMonsterCanvas.gameObject.SetActive(D.ScreenState == ScreenState_Enum.Legend_Monsters);
            LegendRuinsCanvas.gameObject.SetActive(D.ScreenState == ScreenState_Enum.Legend_Ruins);
            LegendIconsCanvas.gameObject.SetActive(D.ScreenState == ScreenState_Enum.Legend_Icons);
            ScoreBoardCanvas.gameObject.SetActive(D.ScreenState == ScreenState_Enum.Score);
            SettingsCanvas.gameObject.SetActive(D.ScreenState == ScreenState_Enum.Settings);
            FinalScoreCanvas.gameObject.SetActive(D.ScreenState == ScreenState_Enum.FinalScore);
        }

        public override void Clear() {
            base.Clear();
            PlayersDetailCanvas.Clear();
            LegendMonsterCanvas.Clear();
            LegendRuinsCanvas.Clear();
            LegendIconsCanvas.Clear();
            ScoreBoardCanvas.Clear();
            SettingsCanvas.Clear();
            FinalScoreCanvas.Clear();
        }
    }
}
