using System;
using System.Collections;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class LegendCanvas : UIGameBase {

        [SerializeField] private List<MonsterCardSlot> GreenMonsters;
        [SerializeField] private List<MonsterCardSlot> GreyMonsters;
        [SerializeField] private List<MonsterCardSlot> VioletMonsters;
        [SerializeField] private List<MonsterCardSlot> BrownMonsters;
        [SerializeField] private List<MonsterCardSlot> WhiteMonsters;
        [SerializeField] private List<MonsterCardSlot> RedMonsters;

        private List<Image_Enum> green_CMG = new List<Image_Enum>();
        private List<Image_Enum> grey_CMY = new List<Image_Enum>();
        private List<Image_Enum> violet_CMV = new List<Image_Enum>();
        private List<Image_Enum> brown_CMB = new List<Image_Enum>();
        private List<Image_Enum> white_CMW = new List<Image_Enum>();
        private List<Image_Enum> red_CMR = new List<Image_Enum>();
        private List<Image_Enum> yellow_R = new List<Image_Enum>();


        public override void SetupUI() {
            populateMonsterImageEnums();
            clearMonsterSlots();
            populateMonsterSlots();
        }

        public void UpdateUI() {
            CheckSetupUI();
        }


        private void populateMonsterImageEnums() {
            green_CMG.Clear();
            grey_CMY.Clear();
            violet_CMV.Clear();
            brown_CMB.Clear();
            white_CMW.Clear();
            red_CMR.Clear();
            yellow_R.Clear();
            foreach (Image_Enum imageEnum in Enum.GetValues(typeof(Image_Enum))) {
                string imageName = imageEnum.ToString();
                if (!imageName.EndsWith("_back")) {
                    if (imageName.StartsWith("CMG_")) {
                        green_CMG.Add(imageEnum);
                    } else if (imageName.StartsWith("CMY_")) {
                        grey_CMY.Add(imageEnum);
                    } else if (imageName.StartsWith("CMB_")) {
                        brown_CMB.Add(imageEnum);
                    } else if (imageName.StartsWith("CMV_")) {
                        violet_CMV.Add(imageEnum);
                    } else if (imageName.StartsWith("CMW_")) {
                        white_CMW.Add(imageEnum);
                    } else if (imageName.StartsWith("CMR_")) {
                        red_CMR.Add(imageEnum);
                    } else if (imageName.StartsWith("R_")) {
                        yellow_R.Add(imageEnum);
                    }
                }
            }
        }


        private void clearMonsterSlots() {
            GreenMonsters.ForEach(m => m.gameObject.SetActive(false));
            GreyMonsters.ForEach(m => m.gameObject.SetActive(false));
            VioletMonsters.ForEach(m => m.gameObject.SetActive(false));
            BrownMonsters.ForEach(m => m.gameObject.SetActive(false));
            WhiteMonsters.ForEach(m => m.gameObject.SetActive(false));
            RedMonsters.ForEach(m => m.gameObject.SetActive(false));
        }

        private void populateMonsterSlots() {
            for (int i = 0; i < green_CMG.Count; i++) {
                GreenMonsters[i].gameObject.SetActive(true);
                GreenMonsters[i].SetupUI(D.Cards.Find(c => c.CardType == CardType_Enum.Monster && c.CardImage == green_CMG[i]).UniqueId, true);
            }
            for (int i = 0; i < grey_CMY.Count; i++) {
                GreyMonsters[i].gameObject.SetActive(true);
                GreyMonsters[i].SetupUI(D.Cards.Find(c => c.CardType == CardType_Enum.Monster && c.CardImage == grey_CMY[i]).UniqueId, true);
            }
            for (int i = 0; i < violet_CMV.Count; i++) {
                VioletMonsters[i].gameObject.SetActive(true);
                VioletMonsters[i].SetupUI(D.Cards.Find(c => c.CardType == CardType_Enum.Monster && c.CardImage == violet_CMV[i]).UniqueId, true);
            }
            for (int i = 0; i < brown_CMB.Count; i++) {
                BrownMonsters[i].gameObject.SetActive(true);
                BrownMonsters[i].SetupUI(D.Cards.Find(c => c.CardType == CardType_Enum.Monster && c.CardImage == brown_CMB[i]).UniqueId, true);
            }
            for (int i = 0; i < white_CMW.Count; i++) {
                WhiteMonsters[i].gameObject.SetActive(true);
                WhiteMonsters[i].SetupUI(D.Cards.Find(c => c.CardType == CardType_Enum.Monster && c.CardImage == white_CMW[i]).UniqueId, true);
            }
            for (int i = 0; i < red_CMR.Count; i++) {
                RedMonsters[i].gameObject.SetActive(true);
                RedMonsters[i].SetupUI(D.Cards.Find(c => c.CardType == CardType_Enum.Monster && c.CardImage == red_CMR[i]).UniqueId, true);
            }
        }
    }
}
