using System;
using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class SelectManaPanel : BasePanel {
        [SerializeField] ActionCardSlot ActionCard;
        [SerializeField] private TextMeshProUGUI TitleText;
        [SerializeField] private TextMeshProUGUI DescText;
        [SerializeField] private CNA_Button button_Prefab;
        [SerializeField] private Transform buttonGroup;

        [SerializeField] private CNA_Button[] manaDie;

        private ActionResultVO ar;
        private string title;
        private V2IntVO selectCount;
        private List<CNA_Button> buttonSlots = new List<CNA_Button>();
        private List<Action<ActionResultVO>> buttonCallback;
        private List<bool> buttonForce;
        private List<int> selectedIndexes;
        private List<Image_Enum> die;

        public void SetupUI(ActionResultVO ar, List<Image_Enum> die, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<ActionResultVO>> buttonCallback, List<bool> buttonForce) {
            gameObject.SetActive(true);
            selectedIndexes = new List<int>();
            this.ar = ar;
            this.title = title;
            this.selectCount = selectCount;
            this.buttonCallback = buttonCallback;
            this.buttonForce = buttonForce;
            this.die = die;
            DescText.text = description;
            UpdateUI_CardTitle();

            for (int i = 0; i < 10; i++) {
                manaDie[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < die.Count; i++) {
                manaDie[i].gameObject.SetActive(true);
                manaDie[i].SetupUI("", CNAColor.BlueSelect, die[i]);
                manaDie[i].Selected.ImageEnum = selectionImage;
            }

            buttonSlots.ForEach(c => Destroy(c.gameObject));
            buttonSlots.Clear();
            for (int i = 0; i < buttonText.Count; i++) {
                CNA_Button buttonSlot = Instantiate(button_Prefab, Vector3.zero, Quaternion.identity);
                buttonSlot.transform.SetParent(buttonGroup);
                buttonSlot.transform.localScale = Vector3.one;
                buttonSlot.SetupUI(buttonText[i], buttonColor[i]);
                buttonSlot.addButtonClick(i, OnClick_Button);
                buttonSlots.Add(buttonSlot);
            }
        }

        private void UpdateUI_CardTitle() {
            TitleText.text = title + " (" + selectedIndexes.Count + " of " + selectCount.Y + ")";
        }

        public void OnClick_Button(int i) {
            if (buttonForce[i]) {
                if (selectedIndexes.Count >= selectCount.X) {
                    ar.SelectedCardIds = selectedIndexes;
                    gameObject.SetActive(false);
                    buttonCallback[i](ar);
                } else {
                    ActionCard.Msg("You must select at least " + selectCount.X + " cards!");
                    buttonSlots[i].ShakeButton();
                }
            } else {
                ar.SelectedCardIds = selectedIndexes;
                gameObject.SetActive(false);
                buttonCallback[i](ar);
            }
        }

        public void OnClick_ManaPool(int i) {
            if (selectedIndexes.Contains(i)) {
                selectedIndexes.Remove(i);
                manaDie[i].Selected.gameObject.SetActive(false);
            } else {
                if (selectedIndexes.Count < selectCount.Y) {
                    selectedIndexes.Add(i);
                    manaDie[i].Selected.gameObject.SetActive(true);
                } else {
                    ActionCard.Msg("You have already selected the max number of die!");
                }
            }
            UpdateUI_CardTitle();
        }
    }
}
