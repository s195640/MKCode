using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace cna.ui {
    public class TestStart : AppEngine {
        [SerializeField] private GameObject afterLoad;
        //[SerializeField] private Tilemap TerrainTilemap;
        //[SerializeField] private Tilemap StructureTilemap;
        //[SerializeField] private Tilemap TerrainBoarderTilemap;
        //[SerializeField] private Camera WorldCamera;
        //[SerializeField] private Grid MainGrid;


        //private string old;
        //[SerializeField] private int seed;
        //[SerializeField] GameMapLayout_Enum gameMapLayout;
        //[SerializeField] private bool easyStart;
        //[SerializeField] [Range(0, 11)] private int basic;
        //[SerializeField] [Range(0, 4)] private int core;
        //[SerializeField] [Range(0, 4)] private int city;


        //[SerializeField] private ScenarioBase scenario;
        //[SerializeField] private List<MapHexId_Enum> mapDeck;
        //[SerializeField] private int mapDeckIndex;
        //[SerializeField] private List<MapHexId_Enum> currentMap;

        public void Start() {
        }

        public override void Update() {

        }

        public override void UpdateUI() {
            afterLoad.SetActive(true);
            //FindObjectOfType<PlayerWorld>().UpdateUI();
        }

        public override void Clear() { }

        public override void AddGood(int val, Image_Enum i) { }

        //public void LateUpdate() {
        //    if (afterLoad.activeSelf) {
        //        if (Input.GetMouseButton(0)) {
        //            Vector3 mouseInput = Input.mousePosition;
        //            Vector3 worldPoint = WorldCamera.ScreenToWorldPoint(mouseInput);
        //            Vector3Int gridPosition = MainGrid.WorldToCell(worldPoint);
        //            int index = scenario.ConvertGridLocToIndex(gridPosition);
        //            addNewTile(index);
        //        }
        //    }
        //}

        //public void Update() {
        //    if (afterLoad.activeSelf) {
        //        UpdateUI();
        //    }
        //}



        //public override void UpdateUI() {
        //    afterLoad.SetActive(true);
        //    string checkChange = string.Format("{0}{1}{2}{3}{4}{5}", seed, gameMapLayout, easyStart, basic, core, city);
        //    if (!checkChange.Equals(old)) {
        //        old = checkChange;
        //        if (gameMapLayout >= GameMapLayout_Enum.Wedge) {
        //            scenario = MapBase.Create(gameMapLayout);
        //            UnityEngine.Random.InitState(seed);
        //            buildMapDeck(gameMapLayout == GameMapLayout_Enum.Wedge, easyStart, basic, core, city);
        //            buildStartMap();
        //            drawCurrentMap();
        //        }
        //    }
        //}

        //public void addNewTile(int i) {
        //    if (i > 0 && i < currentMap.Count) {
        //        if (currentMap[i] == MapHexId_Enum.Basic_Back) {
        //            currentMap[i] = mapDeck[mapDeckIndex];
        //            mapDeckIndex++;
        //            rebuildCurrentMap();
        //            drawCurrentMap();
        //        } else if (currentMap[i] == MapHexId_Enum.Core_Back) {
        //            currentMap[i] = mapDeck[mapDeckIndex];
        //            mapDeckIndex++;
        //            rebuildCurrentMap();
        //            drawCurrentMap();
        //        }
        //    }
        //}

        //public void rebuildCurrentMap() {
        //    int totalTilesLeft = mapDeck.Count - mapDeckIndex;
        //    int gameTilesLeft = 1 + (basic + core + city) - mapDeckIndex;
        //    bool nextTileBasic = mapDeckIndex <= basic;
        //    for (int i = 0; i < currentMap.Count; i++) {
        //        if (currentMap[i] <= MapHexId_Enum.Core_Back) {
        //            if (recalculateHex(i, scenario, currentMap, totalTilesLeft, gameTilesLeft, nextTileBasic)) {
        //                currentMap[i] = mapDeck[mapDeckIndex] < MapHexId_Enum.Core_01 ? MapHexId_Enum.Basic_Back : MapHexId_Enum.Core_Back;
        //            } else {
        //                currentMap[i] = MapHexId_Enum.Invalid;
        //            }
        //        }
        //    }
        //}

        //public static bool recalculateHex(int index, MapBase scenario, List<MapHexId_Enum> currentMap, int totalTilesLeft, int gameTilesLeft, bool nextTileBasic) {
        //    if (totalTilesLeft > 0) {
        //        List<int> adj = scenario.AdjBoard[index].FindAll(i => currentMap.Count > i && currentMap[i] > MapHexId_Enum.Core_Back);
        //        if (gameTilesLeft > 0) {
        //            if (adj.Count > 1) {
        //                return true;
        //            } else if (adj.Count > 0 && nextTileBasic) {
        //                foreach (int j in adj) {
        //                    List<int> adj2 = scenario.AdjBoard[j];
        //                    int totalAdj2 = adj2.FindAll(i => currentMap.Count > i && currentMap[i] > MapHexId_Enum.Core_Back).Count;
        //                    if (totalAdj2 > 1) {
        //                        return true;
        //                    }
        //                }
        //            }
        //        } else if (adj.Count > 2) {
        //            return true;
        //        }
        //    }
        //    return false;
        //}


        ////public static MapHexId_Enum recalculateHex(int index, MapBase scenario, List<MapHexId_Enum> currentMap,
        ////    int GameBasicHexDeck_Count, int GameCoreHexDeck_Count, int BasicHexDeck_Count, int CoreHexDeck_Count
        ////    ) {
        ////    if (currentMap[index] <= MapHexId_Enum.Core_Back) {
        ////        MapHexId_Enum calc;
        ////        bool isBasic = false;
        ////        List<int> adj = scenario.AdjBoard[index];
        ////        int adv = 0;
        ////        foreach (int j in adj) {
        ////            if (j < currentMap.Count) {
        ////                if (currentMap[j] > MapHexId_Enum.Core_Back) {
        ////                    adv++;
        ////                    List<int> adj2 = scenario.AdjBoard[j];
        ////                    int total = 0;
        ////                    foreach (int k in adj2) {
        ////                        if (k < currentMap.Count) {
        ////                            if (currentMap[k] > MapHexId_Enum.Core_Back) {
        ////                                total++;
        ////                                if (total >= 2) {
        ////                                    isBasic = true;
        ////                                    break;
        ////                                }
        ////                            }
        ////                        }
        ////                    }
        ////                }
        ////            }
        ////        }
        ////        if (adv > 1 && (adj.Count > 4 || scenario.GameMapLayout != GameMapLayout_Enum.Wedge)) {
        ////            //if (GameBasicHexDeck_Count == 0) {
        ////            //    if (GameCoreHexDeck_Count != 0) {
        ////            //        calc = MapHexId_Enum.Core_Back;
        ////            //    } else if (BasicHexDeck_Count != 0) {
        ////            //        calc = MapHexId_Enum.Basic_Back;
        ////            //    } else if (CoreHexDeck_Count != 0) {
        ////            //        calc = MapHexId_Enum.Core_Back;
        ////            //    } else {
        ////            //        calc = MapHexId_Enum.Invalid;
        ////            //    }
        ////            //} else {
        ////            //    calc = MapHexId_Enum.Basic_Back;
        ////            //}
        ////            calc = MapHexId_Enum.Basic_Back;
        ////        } else if (isBasic) {
        ////            calc = MapHexId_Enum.Basic_Back;
        ////            //if (GameBasicHexDeck_Count != 0) {
        ////            //    calc = MapHexId_Enum.Basic_Back;
        ////            //} else if (BasicHexDeck_Count != 0) {
        ////            //    calc = MapHexId_Enum.Basic_Back;
        ////            //} else {
        ////            //    calc = MapHexId_Enum.Invalid;
        ////            //}
        ////        } else {
        ////            calc = MapHexId_Enum.Invalid;
        ////        }
        ////        return calc;
        ////    } else {
        ////        return currentMap[index];
        ////    }
        ////}

        //public void drawCurrentMap() {
        //    TerrainTilemap.ClearAllTiles();
        //    StructureTilemap.ClearAllTiles();
        //    TerrainBoarderTilemap.ClearAllTiles();
        //    for (int i = 0; i < currentMap.Count; i++) {
        //        Vector3Int pos = scenario.ConvertIndexToWorld(i);
        //        drawMapHex(D.HexMap[currentMap[i]], pos);
        //    }
        //}

        //private void drawMapHex(MapHexVO mapHex, Vector3Int gridCenterPos) {
        //    if (gridCenterPos.z >= 0) {
        //        int x = gridCenterPos.x;
        //        int y = gridCenterPos.y;
        //        int offset = 0;
        //        if ((y / 2) * 2 == y) {
        //            offset = -1;
        //        }
        //        // Center
        //        DrawHex(mapHex, 2, new Vector3Int(x - 1, y, 0));
        //        DrawHex(mapHex, 3, new Vector3Int(x, y, 0));
        //        DrawHex(mapHex, 4, new Vector3Int(x + 1, y, 0));
        //        // Top 
        //        DrawHex(mapHex, 0, new Vector3Int(x + offset, y + 1, 0));
        //        DrawHex(mapHex, 1, new Vector3Int(x + offset + 1, y + 1, 0));
        //        // Bottom 
        //        DrawHex(mapHex, 5, new Vector3Int(x + offset, y - 1, 0));
        //        DrawHex(mapHex, 6, new Vector3Int(x + offset + 1, y - 1, 0));
        //        TerrainBoarderTilemap.SetTile(gridCenterPos, D.TerrainMap[Image_Enum.TH_Boarder]);
        //    }
        //}

        //private void DrawHex(MapHexVO mapHex, int index, Vector3Int pos) {
        //    if (mapHex.TerrainList[index] != Image_Enum.NA) {
        //        TerrainTilemap.SetTile(pos, D.TerrainMap[mapHex.TerrainList[index]]);
        //    } else {
        //        TerrainTilemap.SetTile(pos, null);
        //    }
        //    if (mapHex.StructureList[index] != Image_Enum.NA) {
        //        StructureTilemap.SetTile(pos, D.StructureMap[mapHex.StructureList[index]]);
        //    } else {
        //        StructureTilemap.SetTile(pos, null);
        //    }
        //}

        //public void buildStartMap() {
        //    currentMap = new List<MapHexId_Enum>();
        //    mapDeckIndex = 0;
        //    int row_1 = gameMapLayout == GameMapLayout_Enum.Wedge ? 3 : 4;
        //    for (mapDeckIndex = 0; mapDeckIndex < row_1; mapDeckIndex++) {
        //        currentMap.Add(mapDeck[mapDeckIndex]);
        //    }
        //    int row_2 = gameMapLayout == GameMapLayout_Enum.MapFullx5 ? 5 : gameMapLayout == GameMapLayout_Enum.MapFullx4 ? 4 : 3;
        //    for (int i = 0; i < row_2; i++) {
        //        currentMap.Add(MapHexId_Enum.Basic_Back);
        //    }
        //    int totalLeft = scenario.MaxBoardSize - currentMap.Count;
        //    for (int i = 0; i < totalLeft; i++) {
        //        currentMap.Add(MapHexId_Enum.Invalid);
        //    }
        //    rebuildCurrentMap();
        //}

        //public void buildMapDeck(bool isWedge, bool easyStart, int basic, int core, int city) {
        //    mapDeck = new List<MapHexId_Enum>();
        //    mapDeck = new List<MapHexId_Enum>();

        //    List<MapHexId_Enum> basicIds = new List<MapHexId_Enum>();
        //    List<MapHexId_Enum> coreIds = new List<MapHexId_Enum>();
        //    List<MapHexId_Enum> citeIds = new List<MapHexId_Enum>();
        //    foreach (MapHexId_Enum id in Enum.GetValues(typeof(MapHexId_Enum))) {
        //        if (id >= MapHexId_Enum.City_Green) {
        //            citeIds.Add(id);
        //        } else if (id >= MapHexId_Enum.Core_01) {
        //            coreIds.Add(id);
        //        } else if (id >= MapHexId_Enum.Basic_01) {
        //            basicIds.Add(id);
        //        }
        //    }

        //    int b = 0;
        //    mapDeck.Add(isWedge ? MapHexId_Enum.Start_A : MapHexId_Enum.Start_B);
        //    if (easyStart) {
        //        basicIds.Remove(MapHexId_Enum.Basic_01);
        //        mapDeck.Add(MapHexId_Enum.Basic_01);
        //        mapDeck.Add(MapHexId_Enum.Basic_02);
        //        basicIds.Remove(MapHexId_Enum.Basic_02);
        //        b += 2;
        //        if (!isWedge) {
        //            mapDeck.Add(MapHexId_Enum.Basic_03);
        //            basicIds.Remove(MapHexId_Enum.Basic_03);
        //            b++;
        //        }
        //    }
        //    for (; b < basic; b++) {
        //        int rand = UnityEngine.Random.Range(0, basicIds.Count);
        //        mapDeck.Add(basicIds[rand]);
        //        basicIds.RemoveAt(rand);
        //    }
        //    List<MapHexId_Enum> advancedIds = new List<MapHexId_Enum>();
        //    for (int i = 0; i < city; i++) {
        //        int rand = UnityEngine.Random.Range(0, citeIds.Count);
        //        advancedIds.Add(citeIds[rand]);
        //        citeIds.RemoveAt(rand);
        //    }
        //    for (int i = 0; i < core; i++) {
        //        int rand = UnityEngine.Random.Range(0, coreIds.Count);
        //        advancedIds.Add(coreIds[rand]);
        //        coreIds.RemoveAt(rand);
        //    }
        //    int total = advancedIds.Count;
        //    for (int i = 0; i < total; i++) {
        //        int rand = UnityEngine.Random.Range(0, advancedIds.Count);
        //        mapDeck.Add(advancedIds[rand]);
        //        advancedIds.RemoveAt(rand);
        //    }
        //    total = basicIds.Count;
        //    for (int i = 0; i < total; i++) {
        //        int rand = UnityEngine.Random.Range(0, basicIds.Count);
        //        mapDeck.Add(basicIds[rand]);
        //        basicIds.RemoveAt(rand);
        //    }
        //    total = coreIds.Count;
        //    for (int i = 0; i < total; i++) {
        //        int rand = UnityEngine.Random.Range(0, coreIds.Count);
        //        mapDeck.Add(coreIds[rand]);
        //        coreIds.RemoveAt(rand);
        //    }
        //}




        ////[SerializeField] private Sprite ss = default;
        ////

        ////void Start() {

        ////    Tile tt = new Tile();
        ////    tt.sprite = ss;

        ////    //Tile t = Resources.Load<Tile>("Tiles/desert");
        ////    Vector3Int pos = Vector3Int.zero;
        ////    TerrainTilemap.SetTile(pos, tt);
        ////}
    }
}
