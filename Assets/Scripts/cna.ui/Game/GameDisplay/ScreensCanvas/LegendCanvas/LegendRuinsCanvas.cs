using System;
using System.Collections;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class LegendRuinsCanvas : UIGameBase {

        [SerializeField] private List<MonsterCardSlot> Alters;
        [SerializeField] private List<MonsterCardSlot> Monsters;

        private List<Image_Enum> yellow_R_alters = new List<Image_Enum>();
        private List<Image_Enum> yellow_R_monsters = new List<Image_Enum>();

        public override void SetupUI() {
            populateMonsterImageEnums();
            clearMonsterSlots();
            populateMonsterSlots(D.LocalPlayer);
        }

        public void UpdateUI() {
            CheckSetupUI();
        }

        private void populateMonsterImageEnums() {
            yellow_R_alters.Clear();
            yellow_R_monsters.Clear();
            foreach (Image_Enum imageEnum in Enum.GetValues(typeof(Image_Enum))) {
                string imageName = imageEnum.ToString();
                if (!imageName.EndsWith("_back")) {
                    if (imageName.StartsWith("R_altars")) {
                        yellow_R_alters.Add(imageEnum);
                    } else if (imageName.StartsWith("R_enemies")) {
                        yellow_R_monsters.Add(imageEnum);
                    }
                }
            }
        }
        private void clearMonsterSlots() {
            Alters.ForEach(m => m.gameObject.SetActive(false));
            Monsters.ForEach(m => m.gameObject.SetActive(false));
        }

        private void populateMonsterSlots(PlayerData pd) {
            for (int i = 0; i < yellow_R_alters.Count; i++) {
                Alters[i].gameObject.SetActive(true);
                Alters[i].SetupUI(pd, D.Cards.Find(c => c.CardType == CardType_Enum.AncientRuins_Alter && c.CardImage == yellow_R_alters[i]).UniqueId, true);
            }
            for (int i = 0; i < yellow_R_monsters.Count; i++) {
                Monsters[i].gameObject.SetActive(true);
                Monsters[i].SetupUI(pd, D.Cards.Find(c => c.CardType == CardType_Enum.AncientRuins_Monster && c.CardImage == yellow_R_monsters[i]).UniqueId, true);
            }
        }
    }
}
