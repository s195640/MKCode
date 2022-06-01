using UnityEngine;

namespace cna.ui {
    public class PlayerCardPanel : MonoBehaviour {

        [SerializeField] private PlayerDeckPanel playerDeckPanel;
        [SerializeField] private PlayerHandPanel playerHandPanel;
        [SerializeField] private PlayerUnitPanel playerUnitPanel;
        [SerializeField] private PlayerSkillPanel playerSkillPanel;


        //Stopwatch stopWatch = new Stopwatch();

        public void UpdateUI() {
            //stopWatch.Restart();
            playerDeckPanel.UpdateUI();
            playerHandPanel.UpdateUI();
            playerUnitPanel.UpdateUI();
            playerSkillPanel.UpdateUI();
            //stopWatch.Stop();
            //TimeSpan ts = stopWatch.Elapsed;
            //string elapsedTime = string.Format("PlayerCardPanel UPDATE {0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            //UnityEngine.Debug.Log(elapsedTime);
        }
    }
}
