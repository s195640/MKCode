using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class GoldManaPanel : BasePanel {
        [SerializeField] private CNA_Button[] playerCrystal;
        [SerializeField] private ManaPayPanel ManaPayPanel;
        private PaymentVO.PaymentType_Enum manaType;
        private Crystal_Enum manaStartValue;
        public void SetupUI(List<Crystal_Enum> l, PaymentVO.PaymentType_Enum manaType, Crystal_Enum manaStartValue = Crystal_Enum.Gold) {
            gameObject.SetActive(true);
            this.manaType = manaType;
            this.manaStartValue = manaStartValue;
            playerCrystal[0].gameObject.SetActive(l.Contains(Crystal_Enum.Blue));
            playerCrystal[1].gameObject.SetActive(l.Contains(Crystal_Enum.Red));
            playerCrystal[2].gameObject.SetActive(l.Contains(Crystal_Enum.Green));
            playerCrystal[3].gameObject.SetActive(l.Contains(Crystal_Enum.White));
            playerCrystal[4].gameObject.SetActive(l.Contains(Crystal_Enum.Gold));
            playerCrystal[5].gameObject.SetActive(l.Contains(Crystal_Enum.Black));
        }

        public void OnClick_GoldManaButton(int index) {
            ManaPayPanel.OnClick_GoldManaButton(index, manaType, manaStartValue);
        }
    }
}
