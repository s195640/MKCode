using System.Collections;
using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class PlayerScoreContainerPrefab : MonoBehaviour {
        [SerializeField] private List<Image> ImageColor;
        [SerializeField] private AddressableImage ShieldImage;
        [SerializeField] private TextMeshProUGUI PlayerName;
        [SerializeField] private TextMeshProUGUI PlayerScore;//0
        [SerializeField] private TextMeshProUGUI PlayerTime;
        [SerializeField] private TextMeshProUGUI PlayerFame;//1

        [SerializeField] private TextMeshProUGUI GreatestKnowledge;//2
        [SerializeField] private TextMeshProUGUI Spell;//3
        [SerializeField] private TextMeshProUGUI Advanced;//4

        [SerializeField] private TextMeshProUGUI GreatestLoot;//5
        [SerializeField] private TextMeshProUGUI Artifact;//6
        [SerializeField] private TextMeshProUGUI Crystals;//7

        [SerializeField] private TextMeshProUGUI GreatestLeader;//8
        [SerializeField] private TextMeshProUGUI Units;//9
        [SerializeField] private TextMeshProUGUI WoundedUnits;//10

        [SerializeField] private TextMeshProUGUI GreatestConqueror;//11
        [SerializeField] private TextMeshProUGUI Keep;//12
        [SerializeField] private TextMeshProUGUI MageTower;//13
        [SerializeField] private TextMeshProUGUI Monastery;//14

        [SerializeField] private TextMeshProUGUI GreatestAdventurer;//15
        [SerializeField] private TextMeshProUGUI AncientRuins;//16
        [SerializeField] private TextMeshProUGUI Dungeon;//17
        [SerializeField] private TextMeshProUGUI Tomb;//18
        [SerializeField] private TextMeshProUGUI MonsterDen;//19
        [SerializeField] private TextMeshProUGUI SpawningGround;//20

        [SerializeField] private TextMeshProUGUI GreatestBeating;//21
        [SerializeField] private TextMeshProUGUI Wounds;//22

        [SerializeField] private TextMeshProUGUI GreatestCities;//23
        [SerializeField] private TextMeshProUGUI Cites;//24

        Dictionary<int, int[]> points = new Dictionary<int, int[]>();

        private int PlayerKey;

        public void SetupUI(int playerkey) {
            PlayerKey = playerkey;
            Data g = D.G;
            PlayerData pd = g.Players.Find(p => p.Key == PlayerKey);
            PlayerName.text = pd.Name;
            AvatarMetaData amd = D.AvatarMetaDataMap[pd.Avatar];
            ShieldImage.ImageEnum = amd.AvatarShieldId;
            ImageColor.ForEach(i => {
                i.color = amd.AvatarColor;
            });
        }

        public void UpdateUI() {
            BuildBasePoints();
            CalcGreatest();
            PopulateGui();
        }

        public void PopulateGui() {
            int[] x = points[PlayerKey];
            PlayerFame.text = "" + x[1];
            GreatestKnowledge.text = "" + x[2];
            Spell.text = "" + x[3];
            Advanced.text = "" + x[4];
            GreatestLoot.text = "" + x[5];
            Artifact.text = "" + x[6];
            Crystals.text = "" + x[7];
            GreatestLeader.text = "" + x[8];
            Units.text = "" + x[9];
            WoundedUnits.text = "" + x[10];
            GreatestConqueror.text = "" + x[11];
            Keep.text = "" + x[12];
            MageTower.text = "" + x[13];
            Monastery.text = "" + x[14];
            GreatestAdventurer.text = "" + x[15];
            AncientRuins.text = "" + x[16];
            Dungeon.text = "" + x[17];
            Tomb.text = "" + x[18];
            MonsterDen.text = "" + x[19];
            SpawningGround.text = "" + x[20];
            GreatestBeating.text = "" + x[21];
            Wounds.text = "" + x[22];
            GreatestCities.text = "" + x[23];
            Cites.text = "" + x[24];
            int total = 0;
            foreach (int i in x) {
                total += i;
            }
            PlayerScore.text = "" + total;

            PlayerTime.text = "00:00:00";
        }

        public void CalcGreatest() {
            int totalPlayers = D.G.Players.Count - (D.G.GameData.DummyPlayer ? 1 : 0);
            if (totalPlayers > 1) {
                int gknowledge = -1;
                int gloot = -1;
                int gleader = -1;
                int gconqueror = -1;
                int gadventurer = -1;
                int gbeating = 9999;
                int gcities = -1;

                List<int> gknowledgeId = new List<int>();
                List<int> glootId = new List<int>();
                List<int> gleaderId = new List<int>();
                List<int> gconquerorId = new List<int>();
                List<int> gadventurerId = new List<int>();
                List<int> gbeatingId = new List<int>();
                List<int> gcitiesId = new List<int>();

                foreach (int playerId in points.Keys) {
                    int[] x = points[playerId];
                    int knowledge = x[3] + x[4];
                    int loot = x[6] + x[7];
                    int leader = x[9] + x[10];
                    int conqueror = x[12] + x[13] + x[14];
                    int adventurer = x[16] + x[17] + x[18] + x[19] + x[20];
                    int beating = x[22];
                    int cities = x[24];
                    if (knowledge > gknowledge) {
                        gknowledge = knowledge;
                        gknowledgeId.Clear();
                        gknowledgeId.Add(playerId);
                    } else if (knowledge == gknowledge) {
                        gknowledgeId.Add(playerId);
                    }
                    if (loot > gloot) {
                        gloot = loot;
                        glootId.Clear();
                        glootId.Add(playerId);
                    } else if (loot == gloot) {
                        glootId.Add(playerId);
                    }
                    if (leader > gleader) {
                        gleader = leader;
                        gleaderId.Clear();
                        gleaderId.Add(playerId);
                    } else if (leader == gleader) {
                        gleaderId.Add(playerId);
                    }
                    if (conqueror > gconqueror) {
                        gconqueror = conqueror;
                        gconquerorId.Clear();
                        gconquerorId.Add(playerId);
                    } else if (conqueror == gconqueror) {
                        gconquerorId.Add(playerId);
                    }
                    if (adventurer > gadventurer) {
                        gadventurer = adventurer;
                        gadventurerId.Clear();
                        gadventurerId.Add(playerId);
                    } else if (adventurer == gadventurer) {
                        gadventurerId.Add(playerId);
                    }
                    if (beating < gbeating) {
                        gbeating = beating;
                        gbeatingId.Clear();
                        gbeatingId.Add(playerId);
                    } else if (beating == gbeating) {
                        gbeatingId.Add(playerId);
                    }
                    if (cities > gcities) {
                        gcities = cities;
                        gcitiesId.Clear();
                        gcitiesId.Add(playerId);
                    } else if (cities == gcities) {
                        gcitiesId.Add(playerId);
                    }
                }
                gknowledgeId.ForEach(p => points[p][2] = gknowledgeId.Count > 1 ? 1 : 3);
                glootId.ForEach(p => points[p][5] = glootId.Count > 1 ? 1 : 3);
                gleaderId.ForEach(p => points[p][8] = gleaderId.Count > 1 ? 1 : 3);
                gconquerorId.ForEach(p => points[p][11] = gconquerorId.Count > 1 ? 1 : 3);
                gadventurerId.ForEach(p => points[p][15] = gadventurerId.Count > 1 ? 1 : 3);
                gbeatingId.ForEach(p => points[p][21] = gbeatingId.Count > 1 ? 1 : 3);
                gcitiesId.ForEach(p => points[p][23] = gcitiesId.Count > 1 ? 2 : 5);
            }
        }

        public void BuildBasePoints() {
            points.Clear();
            D.G.Players.ForEach(p => {
                if (!p.DummyPlayer) {
                    int[] x = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    x[1] = p.TotalFame;
                    p.Deck.Deck.ForEach(c => { addCards(c, ref x, p); });
                    p.Deck.Discard.ForEach(c => { addCards(c, ref x, p); });
                    p.Deck.Hand.ForEach(c => { addCards(c, ref x, p); });
                    p.Deck.Unit.ForEach(c => { addCards(c, ref x, p); });
                    x[7] = (p.Crystal.Blue + p.Crystal.Green + p.Crystal.Red + p.Crystal.White) / 2;
                    p.Shields.ForEach(pos => {
                        switch (BasicUtil.GetStructureAtLoc(pos)) {
                            case Image_Enum.SH_Keep: { x[12] += 2; break; }
                            case Image_Enum.SH_MageTower: { x[13] += 2; break; }
                            case Image_Enum.SH_Monastery: { x[14] += 2; break; }
                            case Image_Enum.SH_AncientRuins: { x[16] += 2; break; }
                            case Image_Enum.SH_Dungeon: { x[17] += 2; break; }
                            case Image_Enum.SH_Tomb: { x[18] += 2; break; }
                            case Image_Enum.SH_MonsterDen: { x[19] += 2; break; }
                            case Image_Enum.SH_SpawningGround: { x[20] += 2; break; }
                            case Image_Enum.SH_City_Blue: { x[24]++; break; }
                            case Image_Enum.SH_City_Green: { x[24]++; break; }
                            case Image_Enum.SH_City_Red: { x[24]++; break; }
                            case Image_Enum.SH_City_White: { x[24]++; break; }
                        }
                    });
                    points.Add(p.Key, x);
                }
            });
        }


        private void addCards(int c, ref int[] x, PlayerData pd) {
            CardVO card = D.Cards[c];
            if (pd.Deck.State.ContainsKey(c) && pd.Deck.State[c].ContainsAny(CardState_Enum.Trashed)) {
                return;
            }
            switch (card.CardType) {
                case CardType_Enum.Spell: { x[3] += 2; break; }
                case CardType_Enum.Advanced: { x[4] += 1; break; }
                case CardType_Enum.Artifact: { x[6] += 2; break; }
                case CardType_Enum.Wound: { x[22] -= 2; break; }
                case CardType_Enum.Unit_Normal:
                case CardType_Enum.Unit_Elite: {
                    if (pd.Deck.State.ContainsKey(c)) {
                        if (pd.Deck.State[c].ContainsAny(CardState_Enum.Unit_Wounded)) {
                            x[10] += (card.UnitLevel / 2);
                        } else {
                            x[9] += card.UnitLevel;
                        }
                    } else {
                        x[9] += card.UnitLevel;
                    }
                    if (pd.Deck.Banners.ContainsKey(c)) {
                        x[6] += 2;
                    }
                    break;
                }
            }
        }



    }
}
