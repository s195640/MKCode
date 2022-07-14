using System.Collections;
using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class PlayerCrystalPanel : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI CrystalBlue;
        [SerializeField] private TextMeshProUGUI CrystalRed;
        [SerializeField] private TextMeshProUGUI CrystalGreen;
        [SerializeField] private TextMeshProUGUI CrystalWhite;

        [SerializeField] private TextMeshProUGUI ManaGold;
        [SerializeField] private TextMeshProUGUI ManaBlue;
        [SerializeField] private TextMeshProUGUI ManaRed;
        [SerializeField] private TextMeshProUGUI ManaGreen;
        [SerializeField] private TextMeshProUGUI ManaWhite;
        [SerializeField] private TextMeshProUGUI ManaBlack;

        [SerializeField] private TextMeshProUGUI AvailableMana;

        [SerializeField] private AddressableImage[] manaPool;


        public void UpdateUI(PlayerData pd) {
            CrystalBlue.text = "" + pd.Crystal.Blue;
            CrystalRed.text = "" + pd.Crystal.Red;
            CrystalGreen.text = "" + pd.Crystal.Green;
            CrystalWhite.text = "" + pd.Crystal.White;

            ManaGold.text = "" + pd.Mana.Gold;
            ManaBlue.text = "" + pd.Mana.Blue;
            ManaRed.text = "" + pd.Mana.Red;
            ManaGreen.text = "" + pd.Mana.Green;
            ManaWhite.text = "" + pd.Mana.White;
            ManaBlack.text = "" + pd.Mana.Black;

            AvailableMana.text = "[" + pd.ManaPoolAvailable + "]";

            foreach (AddressableImage i in manaPool) {
                i.gameObject.SetActive(false);
            }
            ManaPoolData[] d = pd.ManaPool.ToArray();
            for (int i = 0; i < d.Length; i++) {
                manaPool[i].gameObject.SetActive(true);
                manaPool[i].ImageEnum = BasicUtil.Convert_CrystalToManaDieImageId(d[i].ManaColor);
            }
        }
    }
}
