using System;
using UnityEngine;


namespace cna.poo {
    [Serializable]
    public class CrystalData : BaseData {
        public CrystalData() { }

        public CrystalData(int blue, int red, int green, int white, int gold, int black) {
            this.blue = blue;
            this.red = red;
            this.green = green;
            this.white = white;
            this.gold = gold;
            this.black = black;
            ClearSpent();
        }

        [SerializeField] private int blue;
        [SerializeField] private int red;
        [SerializeField] private int green;
        [SerializeField] private int white;
        [SerializeField] private int gold;
        [SerializeField] private int black;
        [SerializeField] private int spentBlue;
        [SerializeField] private int spentRed;
        [SerializeField] private int spentGreen;
        [SerializeField] private int spentWhite;
        [SerializeField] private int spentGold;
        [SerializeField] private int spentBlack;

        public int Blue { get => blue; set { if (value < blue) { SpentBlue += (blue - value); } blue = value; } }
        public int Red { get => red; set { if (value < red) { SpentRed += (red - value); } red = value; } }
        public int Green { get => green; set { if (value < green) { SpentGreen += (green - value); } green = value; } }
        public int White { get => white; set { if (value < white) { SpentWhite += (white - value); } white = value; } }
        public int Gold { get => gold; set { if (value < gold) { SpentGold += (gold - value); } gold = value; } }
        public int Black { get => black; set { if (value < black) { SpentBlack += (black - value); } black = value; } }
        public int[] Data {
            get {
                int[] d = { blue, red, green, white, gold, black };
                return d;
            }
        }

        public int SpentBlue { get => spentBlue; set => spentBlue = value; }
        public int SpentRed { get => spentRed; set => spentRed = value; }
        public int SpentGreen { get => spentGreen; set => spentGreen = value; }
        public int SpentWhite { get => spentWhite; set => spentWhite = value; }
        public int SpentGold { get => spentGold; set => spentGold = value; }
        public int SpentBlack { get => spentBlack; set => spentBlack = value; }

        public void Clear() {
            blue = 0;
            red = 0;
            green = 0;
            white = 0;
            gold = 0;
            black = 0;
        }

        public void AddCrystal(Crystal_Enum c) {
            AddCrystal(c, 1);
        }
        public void UseCrystal(Crystal_Enum c) {
            AddCrystal(c, -1);
        }
        public void AddCrystal(Crystal_Enum c, int val) {
            switch (c) {
                case Crystal_Enum.Gold: {
                    Gold += val;
                    break;
                }
                case Crystal_Enum.Blue: {
                    Blue += val;
                    break;
                }
                case Crystal_Enum.Red: {
                    Red += val;
                    break;
                }
                case Crystal_Enum.Green: {
                    Green += val;
                    break;
                }
                case Crystal_Enum.White: {
                    White += val;
                    break;
                }
                case Crystal_Enum.Black: {
                    Black += val;
                    break;
                }
            }
        }
        public bool HasCrystal(Crystal_Enum h) {
            switch (h) {
                case Crystal_Enum.Gold: {
                    return Gold > 0;
                }
                case Crystal_Enum.Blue: {
                    return Blue > 0;
                }
                case Crystal_Enum.Red: {
                    return Red > 0;
                }
                case Crystal_Enum.Green: {
                    return Green > 0;
                }
                case Crystal_Enum.White: {
                    return White > 0;
                }
                case Crystal_Enum.Black: {
                    return Black > 0;
                }
                default: {
                    return false;
                }
            }
        }
        public int GetCrystalCount(Crystal_Enum h) {
            switch (h) {
                case Crystal_Enum.Gold: {
                    return Gold;
                }
                case Crystal_Enum.Blue: {
                    return Blue;
                }
                case Crystal_Enum.Red: {
                    return Red;
                }
                case Crystal_Enum.Green: {
                    return Green;
                }
                case Crystal_Enum.White: {
                    return White;
                }
                case Crystal_Enum.Black: {
                    return Black;
                }
                default: {
                    return -1;
                }
            }
        }
        public int GetTotal() {
            return Gold + Black + Blue + Red + Green + White;
        }

        public void ClearSpent() {
            SpentBlue = 0;
            SpentRed = 0;
            SpentGreen = 0;
            SpentWhite = 0;
            SpentGold = 0;
            SpentBlack = 0;
        }

        public void RemoveExtraCrystals(int max) {
            if (blue > max) blue = max;
            if (red > max) red = max;
            if (white > max) white = max;
            if (green > max) green = max;
            if (gold > max) gold = max;
            if (black > max) black = max;
        }
    }
}
