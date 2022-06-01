using System.Collections.Generic;
using cna.poo;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace cna.ui {
    public class ProvokeMonsterPanel : MonoBehaviour {

        [SerializeField] private List<AddressableImage> terrainHexes = new List<AddressableImage>();
        [SerializeField] private List<AddressableImage> structureHexes = new List<AddressableImage>();
        [SerializeField] private List<MonsterCardSlot> monsterHexes = new List<MonsterCardSlot>();
        [SerializeField] private List<GameObject> provokeButtons = new List<GameObject>();
        [SerializeField] private List<GameObject> provokeSelected = new List<GameObject>();
        [SerializeField] private AddressableImage avatarHex;
        [SerializeField] private Tilemap TerrainTilemap;
        [SerializeField] private Tilemap StructureTilemap;
        [SerializeField] private int[] provokeMonsterIds = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
        [SerializeField] private List<V2IntVO> provokeMonsterLoc;

        public void SetupUI() {
            bool provokeMonstersAvailable = false;
            provokeMonsterLoc = BasicUtil.GetAdjacentPoints(D.LocalPlayer.CurrentGridLoc);
            provokeMonsterLoc.Insert(3, D.LocalPlayer.CurrentGridLoc);
            for (int i = 0; i < provokeMonsterLoc.Count; i++) {
                V2IntVO p = provokeMonsterLoc[i];
                Image_Enum t = BasicUtil.GetTilemapId(p, TerrainTilemap);
                if (t == Image_Enum.NA) {
                    t = Image_Enum.TH_Invalid;
                }
                terrainHexes[i].ImageEnum = t;
                Image_Enum s = BasicUtil.GetTilemapId(p, StructureTilemap);
                structureHexes[i].gameObject.SetActive(s != Image_Enum.NA);
                structureHexes[i].ImageEnum = s;
                monsterHexes[i].gameObject.SetActive(false);
                provokeButtons[i].SetActive(false);
                provokeSelected[i].SetActive(false);
                if ((s == Image_Enum.SH_Draconum || s == Image_Enum.SH_MaraudingOrcs) && D.G.Monsters.Map.ContainsKey(p)) {
                    List<int> mList = D.G.Monsters.Map[p].Values;
                    if (mList.Count > 0) {
                        if (!D.LocalPlayer.Battle.Monsters.Keys.Contains(mList[0])) {
                            provokeMonstersAvailable = true;
                            provokeMonsterIds[i] = mList[0];
                            monsterHexes[i].gameObject.SetActive(true);
                            monsterHexes[i].SetupUI(mList[0]);
                            provokeButtons[i].SetActive(true);
                        }
                    }
                }
            }
            avatarHex.ImageEnum = D.LocalPlayer.Avatar;
            if (!provokeMonstersAvailable) {
                D.LocalPlayer.Battle.BattlePhase = BattlePhase_Enum.RangeSiege;
            }
        }

        public void OnClick_ProvokeMonster(int index) {
            bool current = provokeSelected[index].activeSelf;
            provokeSelected[index].SetActive(!current);
            if (current) {
                D.LocalPlayer.Battle.Monsters.Remove(provokeMonsterIds[index]);
            } else {
                MonsterMetaData monster = new MonsterMetaData(provokeMonsterIds[index], provokeMonsterLoc[index], structureHexes[index].ImageEnum);
                monster.Provoked = true;
                D.LocalPlayer.Battle.Monsters.Add(provokeMonsterIds[index], monster);
            }
        }

        public List<int> getProvokedMonsters() {
            List<int> m = new List<int>();
            for (int i = 0; i < 7; i++) {
                if (provokeSelected[i].activeSelf) {
                    m.Add(provokeMonsterIds[i]);
                }
            }
            return m;
        }
    }
}
