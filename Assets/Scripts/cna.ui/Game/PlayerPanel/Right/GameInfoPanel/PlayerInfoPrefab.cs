using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class PlayerInfoPrefab : MonoBehaviour {
        [Header("Dummy")]
        [SerializeField] private GameObject DummyPlayer;
        [SerializeField] private AddressableImage Dummy_Shield;
        [SerializeField] private TextMeshProUGUI Dummy_PlayerName;
        [SerializeField] private TextMeshProUGUI Dummy_Deck;
        [SerializeField] private TextMeshProUGUI Dummy_BlueCrystalVal;
        [SerializeField] private TextMeshProUGUI Dummy_RedCrystalVal;
        [SerializeField] private TextMeshProUGUI Dummy_GreenCrystalVal;
        [SerializeField] private TextMeshProUGUI Dummy_WhiteCrystalVal;

        [Header("Normal")]
        [SerializeField] private GameObject NormalPlayer;



        [SerializeField] private Image PlayerTurnImage;

        [SerializeField] private AddressableImage Shield;
        [SerializeField] private TextMeshProUGUI PlayerName;

        [SerializeField] private TextMeshProUGUI FameVal;
        [SerializeField] private TextMeshProUGUI FameNextLevelVal;

        [SerializeField] private TextMeshProUGUI RepVal;
        [SerializeField] private Transform RepImage_Good;
        [SerializeField] private Transform RepImage_Bad;

        [SerializeField] private TextMeshProUGUI ArmorVal;

        [SerializeField] private TextMeshProUGUI HandLimitVal;
        [SerializeField] private TextMeshProUGUI HandLimitBonus;

        [SerializeField] private TextMeshProUGUI DeckVal;

        [SerializeField] private TextMeshProUGUI BlueCrystalVal;
        [SerializeField] private TextMeshProUGUI RedCrystalVal;
        [SerializeField] private TextMeshProUGUI GreenCrystalVal;
        [SerializeField] private TextMeshProUGUI WhiteCrystalVal;

        [SerializeField] private TextMeshProUGUI GoldManaVal;
        [SerializeField] private TextMeshProUGUI BlueManaVal;
        [SerializeField] private TextMeshProUGUI RedManaVal;
        [SerializeField] private TextMeshProUGUI GreenManaVal;
        [SerializeField] private TextMeshProUGUI WhiteManaVal;
        [SerializeField] private TextMeshProUGUI BlackManaVal;

        [SerializeField] private GameObject MoveInfo_go;
        [SerializeField] private TextMeshProUGUI MoveVal;

        [SerializeField] private GameObject InfluenceInfo_go;
        [SerializeField] private TextMeshProUGUI InfluenceVal;

        [SerializeField] private GameObject HealInfo_go;
        [SerializeField] private TextMeshProUGUI HealVal;

        [SerializeField] private GameObject SiegeInfo_go;
        [SerializeField] private TextMeshProUGUI SiegeVal;
        [SerializeField] private PlayerInfoMagicPrefab SiegeMagic;

        [SerializeField] private GameObject RangeInfo_go;
        [SerializeField] private TextMeshProUGUI RangeVal;
        [SerializeField] private PlayerInfoMagicPrefab RangeMagic;

        [SerializeField] private GameObject BlockInfo_go;
        [SerializeField] private TextMeshProUGUI BlockVal;
        [SerializeField] private PlayerInfoMagicPrefab BlockMagic;

        [SerializeField] private GameObject AttackInfo_go;
        [SerializeField] private TextMeshProUGUI AttackVal;
        [SerializeField] private PlayerInfoMagicPrefab AttackMagic;

        private static Color TURN_COLOR = new Color(0f, 1f, 0f, 1f);
        private static Color NOT_TURN_COLOR = new Color(0.9215686f, 8352941f, 7411765f, 1f);

        [SerializeField] public int playerKey;

        [SerializeField] private PlayerData player = null;
        public PlayerData Player { get { if (player == null) { player = D.G.Players.Find(p => p.Key == playerKey); } return player; } }

        public void SetupUI(int key) {
            player = null;
            playerKey = key;
            if (Player.DummyPlayer) {
                NormalPlayer.SetActive(false);
                DummyPlayer.SetActive(true);
                Dummy_Shield.ImageEnum = D.AvatarMetaDataMap[Player.Avatar].AvatarShieldId;
                Dummy_PlayerName.text = Player.Name;
            } else {
                NormalPlayer.SetActive(true);
                DummyPlayer.SetActive(false);
                Shield.ImageEnum = D.AvatarMetaDataMap[Player.Avatar].AvatarShieldId;
                PlayerName.text = Player.Name;
            }
        }
        public void UpdateUI() {
            player = null;
            setPlayerTurn();
            if (Player.DummyPlayer) {
                Dummy_Deck.text = "" + Player.Deck.Deck.Count;
                Dummy_BlueCrystalVal.text = "" + Player.Crystal.Blue;
                Dummy_RedCrystalVal.text = "" + Player.Crystal.Red;
                Dummy_GreenCrystalVal.text = "" + Player.Crystal.Green;
                Dummy_WhiteCrystalVal.text = "" + Player.Crystal.White;
            } else {
                MoveInfo_go.SetActive(Player.Movement > 0);
                MoveVal.text = "" + Player.Movement;
                InfluenceInfo_go.SetActive(Player.Influence > 0);
                InfluenceVal.text = "" + Player.Influence;
                HealInfo_go.SetActive(Player.Healpoints > 0);
                HealVal.text = "" + Player.Healpoints;
                int siege = Player.Battle.Siege.getTotal();
                int range = Player.Battle.Range.getTotal();
                int block = Player.Battle.Shield.getTotal();
                int attack = Player.Battle.Attack.getTotal();
                SiegeInfo_go.SetActive(siege > 0);
                RangeInfo_go.SetActive(range > 0);
                BlockInfo_go.SetActive(block > 0);
                AttackInfo_go.SetActive(attack > 0);
                SiegeVal.text = "" + siege;
                RangeVal.text = "" + range;
                BlockVal.text = "" + block;
                AttackVal.text = "" + attack;
                SiegeMagic.UpdateUI(Player.Battle.Siege);
                RangeMagic.UpdateUI(Player.Battle.Range);
                BlockMagic.UpdateUI(Player.Battle.Shield);
                AttackMagic.UpdateUI(Player.Battle.Attack);

                BlueCrystalVal.text = "" + Player.Crystal.Blue;
                RedCrystalVal.text = "" + Player.Crystal.Red;
                GreenCrystalVal.text = "" + Player.Crystal.Green;
                WhiteCrystalVal.text = "" + Player.Crystal.White;
                GoldManaVal.text = "" + Player.Mana.Gold;
                BlueManaVal.text = "" + Player.Mana.Blue;
                RedManaVal.text = "" + Player.Mana.Red;
                GreenManaVal.text = "" + Player.Mana.Green;
                WhiteManaVal.text = "" + Player.Mana.White;
                BlackManaVal.text = "" + Player.Mana.Black;


                FameVal.text = "" + Player.TotalFame;
                int currentLevel = BasicUtil.GetPlayerLevel(Player.TotalFame);
                int fameForNextLevel = BasicUtil.GetFameForLevel(currentLevel + 1);
                int fameNeededForNextLevel = fameForNextLevel - Player.TotalFame;
                FameNextLevelVal.text = "[" + fameNeededForNextLevel + "]";

                if (Player.RepLevel >= 0) {
                    RepImage_Good.gameObject.SetActive(true);
                    RepImage_Bad.gameObject.SetActive(false);
                } else {
                    RepImage_Good.gameObject.SetActive(false);
                    RepImage_Bad.gameObject.SetActive(true);
                }
                int rep = BasicUtil.GetRepForLevel(Player.RepLevel);
                if (Player.RepLevel == -7) {
                    RepVal.text = "X";
                } else {
                    if (Player.RepLevel > 0) {
                        RepVal.text = "+" + rep;
                    } else {
                        RepVal.text = "" + rep;
                    }
                }

                DeckVal.text = "" + Player.Deck.Deck.Count;

                ArmorVal.text = "" + Player.Armor;

                HandLimitVal.text = "" + Player.Deck.TotalHandSize;
                HandLimitBonus.text = "" + Player.Deck.HandSize.Y;
            }
        }

        public void setPlayerTurn() {
            PlayerTurnImage.color = D.CurrentTurn.Key == playerKey ? TURN_COLOR : NOT_TURN_COLOR;
        }
    }
}
