using UnityEngine;

namespace cna.ui {
    public class Game : MonoBehaviour {

        [SerializeField] GameRoundPanel gameRoundPanel;
        [SerializeField] PlayerWorld playerWorld;
        [SerializeField] GameInfoPanel gameInfoPanel;
        [SerializeField] PlayerCardPanel playerCardPanel;
        [SerializeField] ActionCardSlot actionCardSlot;
        [SerializeField] TopBar TopBar;
        [SerializeField] GameEffectPanel GameEffectPanel;

        //Stopwatch stopWatch = new Stopwatch();

        public void UpdateUI() {
            //stopWatch.Restart();

            gameRoundPanel.UpdateUI();
            playerWorld.gameObject.SetActive(true);
            playerWorld.UpdateUI();
            gameInfoPanel.UpdateUI();
            playerCardPanel.UpdateUI();
            actionCardSlot.UpdateUI();
            TopBar.UpdateUI();
            GameEffectPanel.UpdateUI();

            //stopWatch.Stop();
            //TimeSpan ts = stopWatch.Elapsed;
            //UnityEngine.Debug.Log("RunTime " + string.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds));
        }

        public void Clear() {
            actionCardSlot.SelectedCardSlot = null;
        }
    }
}
