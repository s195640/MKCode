using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class ScoreCellPrefab : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI scoreValueText;
        [SerializeField] private Image bkColor;
        [SerializeField] public Transform tokens;
        [SerializeField] public int scoreValue;


        public void SetupUI(int scoreValue) {
            this.scoreValue = scoreValue;
            scoreValueText.text = "" + scoreValue;
            bkColor.color = scoreValue % 2 == 0 ? CNAColor.ScoreEvenColor : CNAColor.ScoreOddColor;
        }
    }
}
