using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class GameRoundPanel : MonoBehaviour {
        [SerializeField] private Image Background;
        [SerializeField] private GameObject PlayerPhase;

        [SerializeField] private Image[] BackgroundColor;
        [SerializeField] private GameObject StartOfTurn;
        [SerializeField] private GameObject Movement;
        [SerializeField] private GameObject Influence;
        [SerializeField] private GameObject Battle;
        [SerializeField] private GameObject EndOfTurn;
        [SerializeField] private GameObject Resting;
        [SerializeField] private GameObject Exhaustion;
        [SerializeField] private GameObject NotYourTurn;

        private bool setup = false;
        private TurnPhase_Enum turnPhase = TurnPhase_Enum.NotTurn;

        public void SetupUI() {
            setup = true;
            PlayerPhase.SetActive(true);
            Color avatarColor = D.AvatarMetaDataMap[D.LocalPlayer.Avatar].AvatarColor;
            Background.color = avatarColor;
            Color avatarColorLight = avatarColor * 1.2f;
            avatarColorLight.a = 1;
            for (int i = 0; i < BackgroundColor.Length; i++) {
                BackgroundColor[i].color = avatarColorLight;
            }
            UpdateUI_TurnPhase();
        }

        public void UpdateUI() {
            if (!setup) {
                SetupUI();
            }
            if (turnPhase != D.LocalPlayer.PlayerTurnPhase) {
                UpdateUI_TurnPhase();
            }
        }

        private void UpdateUI_TurnPhase() {
            turnPhase = D.LocalPlayer.PlayerTurnPhase;
            StartOfTurn.SetActive(false);
            Movement.SetActive(false);
            Influence.SetActive(false);
            Battle.SetActive(false);
            EndOfTurn.SetActive(false);
            Resting.SetActive(false);
            Exhaustion.SetActive(false);
            NotYourTurn.SetActive(false);
            switch (turnPhase) {
                case TurnPhase_Enum.StartTurn: {
                    StartOfTurn.SetActive(true);
                    break;
                }
                case TurnPhase_Enum.Move: {
                    Movement.SetActive(true);
                    break;
                }
                case TurnPhase_Enum.Influence: {
                    Influence.SetActive(true);
                    break;
                }
                case TurnPhase_Enum.Battle: {
                    Battle.SetActive(true);
                    break;
                }
                case TurnPhase_Enum.AfterBattle: {
                    EndOfTurn.SetActive(true);
                    break;
                }
                case TurnPhase_Enum.Resting: {
                    Resting.SetActive(true);
                    break;
                }
                case TurnPhase_Enum.Exhaustion: {
                    Exhaustion.SetActive(true);
                    break;
                }
                default: {
                    NotYourTurn.SetActive(true);
                    break;
                }
            }
        }

        public void OnClick_Influence() {
            ActionResultVO ar = new ActionResultVO();
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.Push();
        }
    }
}
