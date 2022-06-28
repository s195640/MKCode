using System;
using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class ManaPayPanel : BasePanel {
        [SerializeField] private GameObject sun;
        [SerializeField] private GameObject moon;
        [SerializeField] private TextMeshProUGUI topText;
        [SerializeField] private CNA_Button[] playerCrystal;
        [SerializeField] private CNA_Button[] playerMana;
        [SerializeField] private TextMeshProUGUI manaPoolAvailable;
        [SerializeField] private CNA_Button[] manaPool;
        [SerializeField] private CNA_Button manaSteal;
        [SerializeField] private CNA_Button[] paymentNeeded;
        [SerializeField] private CNA_Button acceptButton;
        [SerializeField] private TextMeshProUGUI paymentNeededText;

        [SerializeField] private GoldManaPanel GoldManaPanel;
        [SerializeField] private NotificationCanvas NotificationCanvas;

        [Header("StartingData")]
        [SerializeField] private int[] playerCrystalData;
        [SerializeField] private int[] playerManaData;
        [SerializeField] private int manaPoolAvailableData;
        [SerializeField] private List<Crystal_Enum> manaPoolData;
        [SerializeField] private bool dayRules;
        [SerializeField] private int[] cost;

        [Header("Payment")]
        private List<PaymentVO> payment;
        private string[] color = { "Blue", "Red", "Green", "White", "Gold", "Black" };
        private Crystal_Enum[] crystalEnum = { Crystal_Enum.Blue, Crystal_Enum.Red, Crystal_Enum.Green, Crystal_Enum.White, Crystal_Enum.Gold, Crystal_Enum.Black };
        private Action<GameAPI> cancelCallback;
        private Action<GameAPI> acceptCallback;
        private GameAPI ar;
        private bool payAll;
        public string cardTitle;

        public bool DayRules { get { return dayRules; } }

        public void SetupUI(string cardTitle, CrystalData crystal, CrystalData mana, int manaPoolAvailable, List<Crystal_Enum> manaPool, bool isDayRules, GameAPI ar, List<Crystal_Enum> cost, Action<GameAPI> cancelCallback, Action<GameAPI> acceptCallback, bool all) {
            this.cancelCallback = cancelCallback;
            this.acceptCallback = acceptCallback;
            this.ar = ar;
            payAll = all;
            if (all) {
                paymentNeededText.text = "Payment Needed";
            } else {
                paymentNeededText.text = "Pay One";
            }
            this.cardTitle = cardTitle;
            topText.text = cardTitle;
            gameObject.SetActive(true);
            GoldManaPanel.gameObject.SetActive(false);
            payment = new List<PaymentVO>();
            playerCrystalData = crystal.Data;
            playerManaData = mana.Data;
            manaPoolAvailableData = manaPoolAvailable;
            manaPoolData = manaPool;
            bool dungeonOrTomb = ar.P.GameEffects.ContainsKeyAny(GameEffect_Enum.SH_Tomb, GameEffect_Enum.SH_Dungeon);
            dayRules = isDayRules && !dungeonOrTomb;
            int[] costByColor = { 0, 0, 0, 0, 0, 0 };
            cost.ForEach(c => {
                switch (c) {
                    case Crystal_Enum.Blue: { costByColor[0]++; break; }
                    case Crystal_Enum.Red: { costByColor[1]++; break; }
                    case Crystal_Enum.Green: { costByColor[2]++; break; }
                    case Crystal_Enum.White: { costByColor[3]++; break; }
                    case Crystal_Enum.Gold: { costByColor[4]++; break; }
                    case Crystal_Enum.Black: { costByColor[5]++; break; }
                }
            });
            this.cost = costByColor;
            ResetUI();
        }

        public void ResetUI() {
            sun.SetActive(DayRules);
            moon.SetActive(!DayRules);
            UpdateAvailableMana();
            for (int i = 0; i < 4; i++) {
                if (playerCrystalData[i] > 0) {
                    playerCrystal[i].gameObject.SetActive(true);
                    playerCrystal[i].UpdateUI_Text("(" + playerCrystalData[i] + ") " + color[i]);
                } else { playerCrystal[i].gameObject.SetActive(false); }
            }
            for (int i = 0; i < 6; i++) {
                if (playerManaData[i] > 0) {
                    playerMana[i].gameObject.SetActive(true);
                    playerMana[i].UpdateUI_Text("(" + playerManaData[i] + ") " + color[i]);
                } else { playerMana[i].gameObject.SetActive(false); }
            }
            for (int i = 0; i < 9; i++) {
                if (manaPoolData.Count > i) {
                    manaPool[i].gameObject.SetActive(true);
                    manaPool[i].UpdateUI_Image(BasicUtil.Convert_CrystalToManaDieImageId(manaPoolData[i]));
                } else { manaPool[i].gameObject.SetActive(false); }
            }
            UpdatePayment();
        }

        public void UpdatePayment() {
            for (int i = 0; i < 6; i++) {
                if (cost[i] > 0) {
                    int paid = getCrystalsUsedForPayment(crystalEnum[i]);
                    paymentNeeded[i].gameObject.SetActive(true);
                    paymentNeeded[i].UpdateUI_Text("(" + paid + "/" + cost[i] + ") " + color[i]);
                } else { paymentNeeded[i].gameObject.SetActive(false); }
            }
        }

        public void UpdateAvailableMana() {
            manaPoolAvailable.text = "Available (" + getAvailableMana() + ")";
        }

        public int getAvailableMana() {
            int totalManaPoolUsed = 0;
            payment.ForEach(p => { if (p.PaymentType == PaymentVO.PaymentType_Enum.ManaPool) totalManaPoolUsed++; });
            return manaPoolAvailableData - totalManaPoolUsed;
        }

        public int getAvailablePlayerCrystal(int index) {
            int total = 0;
            Crystal_Enum mana = crystalEnum[index];
            payment.ForEach(p => { if (p.PaymentType == PaymentVO.PaymentType_Enum.Crystal && p.Mana == mana) total++; });
            return playerCrystalData[index] - total;
        }
        public int getAvailablePlayerMana(int index) {
            int total = 0;
            Crystal_Enum mana = crystalEnum[index];
            payment.ForEach(p => { if (p.PaymentType == PaymentVO.PaymentType_Enum.Mana && p.Mana == mana) total++; });
            return playerManaData[index] - total;
        }


        #region Player's Mana
        public void OnClick_PlayerCrystal(int index) {
            if (getAvailablePlayerCrystal(index) > 0) {
                if (!selectMana(crystalEnum[index], PaymentVO.PaymentType_Enum.Crystal)) {
                    playerCrystal[index].ShakeButton();
                } else {
                    playerCrystal[index].UpdateUI_Text("(" + getAvailablePlayerCrystal(index) + ") " + color[index]);
                }
            } else {
                Message("You do not have Enough Crystals");
                playerCrystal[index].ShakeButton();
            }
        }
        public void OnClick_PlayerMana(int index) {
            if (getAvailablePlayerMana(index) > 0) {
                if (!selectMana(crystalEnum[index], PaymentVO.PaymentType_Enum.Mana)) {
                    playerMana[index].ShakeButton();
                } else {
                    playerMana[index].UpdateUI_Text("(" + getAvailablePlayerMana(index) + ") " + color[index]);
                }
            } else {
                Message("You do not have Enough Mana");
                playerMana[index].ShakeButton();
            }
        }

        private bool selectMana(Crystal_Enum mana, PaymentVO.PaymentType_Enum manaType) {
            string msg = checkIfNeeded(mana);
            if (msg.Length > 0) {
                Message(msg);
                return false;
            } else {
                bool manaStorm = ar.P.GameEffects.ContainsKey(GameEffect_Enum.AC_ManaStorm);
                if (manaStorm && (mana == Crystal_Enum.Gold || mana == Crystal_Enum.Black)) {
                    List<Crystal_Enum> l = getNeededPaymentTypes();
                    if (!DayRules && mana == Crystal_Enum.Black) {
                        if (getCrystalsUsedForPayment(Crystal_Enum.Black) < cost[getIndexForCrystalEnum(Crystal_Enum.Black)]) {
                            l.Add(Crystal_Enum.Black);
                        }
                    }
                    if (l.Count > 1) {
                        GoldManaPanel.SetupUI(l, manaType, mana);
                    } else {
                        PaymentVO p = new PaymentVO(mana, manaType);
                        p.ManaUsedAs = l[0];
                        payment.Add(p);
                    }
                } else if (mana == Crystal_Enum.Gold) {
                    if (cardTitle.Equals("Payment :: Polarization") && !DayRules) {
                        PaymentVO p = new PaymentVO(mana, manaType);
                        p.ManaUsedAs = Crystal_Enum.Gold;
                        payment.Add(p);
                    } else {
                        List<Crystal_Enum> l = getNeededPaymentTypes();
                        if (l.Count > 1) {
                            GoldManaPanel.SetupUI(l, manaType, mana);
                        } else {
                            PaymentVO p = new PaymentVO(mana, manaType);
                            p.ManaUsedAs = l[0];
                            payment.Add(p);
                        }
                    }
                } else {
                    payment.Add(new PaymentVO(mana, manaType));
                }
            }
            UpdatePayment();
            return true;
        }
        public void OnClick_GoldManaButton(int index, PaymentVO.PaymentType_Enum manaType, Crystal_Enum manaStartValue) {
            GoldManaPanel.gameObject.SetActive(false);
            PaymentVO p = new PaymentVO(manaStartValue, manaType);
            p.ManaUsedAs = crystalEnum[index];
            payment.Add(p);
            playerMana[4].UpdateUI_Text("(" + getAvailablePlayerMana(4) + ") " + color[4]);
            UpdatePayment();
        }

        #endregion

        #region Mana Pool

        public void OnClick_ManaPool(int index) {
            Crystal_Enum mana = BasicUtil.Convert_ManaDieToCrystalId(manaPool[index].ButtonImageId);
            if (getAvailableMana() > 0) {
                if (!selectMana(mana, PaymentVO.PaymentType_Enum.ManaPool)) {
                    manaPool[index].ShakeButton();
                } else {
                    manaPool[index].gameObject.SetActive(false);
                    UpdateAvailableMana();
                }
            } else {
                Message("You can not take any more mana from the mana pool.");
                manaPool[index].ShakeButton();
            }
        }

        public void OnClick_ManaSteal() { }

        #endregion

        #region Actions
        public void OnClick_Clear() {
            payment.Clear();
            ResetUI();
        }

        public void OnClick_Accept() {
            if (payAll) {
                bool priceMet = true;
                for (int i = 0; i < 6; i++) {
                    priceMet &= (getCrystalsUsedForPayment(crystalEnum[i]) == cost[i]);
                }
                if (priceMet) {
                    gameObject.SetActive(false);
                    ar.SetPayment(payment);
                    acceptCallback(ar);
                } else {
                    Message("You have not met the price!");
                    acceptButton.ShakeButton();
                }
            } else {
                if (payment.Count == 1) {
                    gameObject.SetActive(false);
                    ar.SetPayment(payment);
                    acceptCallback(ar);
                } else {
                    Message("You have not met the price!");
                    acceptButton.ShakeButton();
                }
            }
        }

        public void OnClick_Cancel() {
            gameObject.SetActive(false);
            cancelCallback(ar);
        }
        #endregion

        private string checkIfNeeded(Crystal_Enum mana) {
            if (!payAll && payment.Count == 1) {
                return "No more mana is required!";
            }
            bool manaStorm = ar.P.GameEffects.ContainsKey(GameEffect_Enum.AC_ManaStorm);
            if (manaStorm && (mana == Crystal_Enum.Gold || mana == Crystal_Enum.Black)) {
                if (getCrystalsUsedForPayment(Crystal_Enum.Blue) < cost[getIndexForCrystalEnum(Crystal_Enum.Blue)] ||
                    getCrystalsUsedForPayment(Crystal_Enum.Red) < cost[getIndexForCrystalEnum(Crystal_Enum.Red)] ||
                    getCrystalsUsedForPayment(Crystal_Enum.Green) < cost[getIndexForCrystalEnum(Crystal_Enum.Green)] ||
                    getCrystalsUsedForPayment(Crystal_Enum.White) < cost[getIndexForCrystalEnum(Crystal_Enum.White)]
                    ) {
                    return "";
                }
            }
            if (mana == Crystal_Enum.Gold) {
                if (!DayRules) {
                    if (cardTitle.Equals("Payment :: Polarization")) {
                        return "";
                    }
                    if (!ar.P.GameEffects.ContainsKey(GameEffect_Enum.CT_AmuletOfTheSun)) {
                        return "Gold Mana can not be used during the night!";
                    }
                }
                if (getCrystalsUsedForPayment(Crystal_Enum.Blue) < cost[getIndexForCrystalEnum(Crystal_Enum.Blue)] ||
                    getCrystalsUsedForPayment(Crystal_Enum.Red) < cost[getIndexForCrystalEnum(Crystal_Enum.Red)] ||
                    getCrystalsUsedForPayment(Crystal_Enum.Green) < cost[getIndexForCrystalEnum(Crystal_Enum.Green)] ||
                    getCrystalsUsedForPayment(Crystal_Enum.White) < cost[getIndexForCrystalEnum(Crystal_Enum.White)]
                    ) {
                    return "";
                }
            } else {
                if (mana == Crystal_Enum.Black && DayRules) {
                    if (cardTitle.Equals("Payment :: Polarization")) {
                        return "";
                    }
                    if (!ar.P.GameEffects.ContainsKey(GameEffect_Enum.CT_AmuletOfDarkness)) {
                        return "Black Mana can not be used during the day!";
                    }
                }
                if (getCrystalsUsedForPayment(mana) < cost[getIndexForCrystalEnum(mana)]) {
                    return "";
                }
            }
            return "No more " + color[getIndexForCrystalEnum(mana)] + " Mana is required!";
        }

        private int getCrystalsUsedForPayment(Crystal_Enum mana) {
            int total = 0;
            payment.ForEach(p => { if (p.ManaUsedAs == mana) total++; });
            return total;
        }

        private List<Crystal_Enum> getNeededPaymentTypes() {
            List<Crystal_Enum> mana = new List<Crystal_Enum>();
            if (getCrystalsUsedForPayment(Crystal_Enum.Blue) < cost[getIndexForCrystalEnum(Crystal_Enum.Blue)]) {
                mana.Add(Crystal_Enum.Blue);
            }
            if (getCrystalsUsedForPayment(Crystal_Enum.Red) < cost[getIndexForCrystalEnum(Crystal_Enum.Red)]) {
                mana.Add(Crystal_Enum.Red);
            }
            if (getCrystalsUsedForPayment(Crystal_Enum.Green) < cost[getIndexForCrystalEnum(Crystal_Enum.Green)]) {
                mana.Add(Crystal_Enum.Green);
            }
            if (getCrystalsUsedForPayment(Crystal_Enum.White) < cost[getIndexForCrystalEnum(Crystal_Enum.White)]) {
                mana.Add(Crystal_Enum.White);
            }
            if (getCrystalsUsedForPayment(Crystal_Enum.Gold) < cost[getIndexForCrystalEnum(Crystal_Enum.Gold)]) {
                mana.Add(Crystal_Enum.Gold);
            }
            return mana;
        }

        private int getIndexForCrystalEnum(Crystal_Enum mana) {
            switch (mana) {
                case Crystal_Enum.Blue: { return 0; }
                case Crystal_Enum.Red: { return 1; }
                case Crystal_Enum.Green: { return 2; }
                case Crystal_Enum.White: { return 3; }
                case Crystal_Enum.Gold: { return 4; }
                case Crystal_Enum.Black: { return 5; }
            }
            return -1;
        }

        private void Message(string msg) {
            NotificationCanvas.Msg(msg);
        }
    }
}
