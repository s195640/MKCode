using System;
using System.Collections;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class FinalScoreCanvas : UIGameBase {

        [SerializeField] private Transform content;
        [SerializeField] private PlayerScoreContainerPrefab prefab;
        [SerializeField] private List<PlayerScoreContainerPrefab> PlayerScores = new List<PlayerScoreContainerPrefab>();

        public override void SetupUI() {
            foreach (PlayerScoreContainerPrefab n in PlayerScores.ToArray()) {
                Destroy(n.gameObject);
            }
            PlayerScores.Clear();
            D.G.Players.ForEach(p => {
                if (!p.DummyPlayer) {
                    PlayerScoreContainerPrefab score = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                    score.transform.SetParent(content);
                    score.transform.localScale = Vector3.one;
                    score.SetupUI(p.Key);
                    PlayerScores.Add(score);
                }
            });

        }

        public void UpdateUI() {
            CheckSetupUI();
            UpdateUI_Scores();
        }

        public void UpdateUI_Scores() {
            PlayerScores.ForEach(p => p.UpdateUI());
        }
    }
}
