using cna.poo;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace cna.ui {
    public class PlayerGrid : MonoBehaviour {
        [SerializeField] private Camera WorldCamera;
        [SerializeField] private Tilemap TerrainTilemap;
        [SerializeField] private Tilemap StructureTilemap;
        [SerializeField] private MonsterTilemap MonsterTilemap;
        [SerializeField] private ShieldTilemap ShieldTilemap;
        [SerializeField] private Tilemap TerrainBoarderTilemap;
        [SerializeField] private PlayerTilemap playerTilemap;
        [SerializeField] private Grid MainGrid;
        [SerializeField] private SelectionHex SelectionHex;
        [SerializeField] private ActionCardSlot ActionCard;
        [SerializeField] private ConformationCanvas ConformationCanvas;
        [SerializeField] private TriggerBattlePanel TriggerBattlePanel;
        private bool CheckMouseDown = false;



        public void LateUpdate() {
            Vector3 viewPoint = WorldCamera.ScreenToViewportPoint(Input.mousePosition);
            //  Debug.Log(string.Format("input {0}, View {1}, World {2}", Input.mousePosition, viewPoint, worldPoint));
            if (isPointValid(viewPoint)) {
                HexItemDetail hexItemDetail = new HexItemDetail(TerrainTilemap, StructureTilemap, MainGrid, WorldCamera);
                SelectionHex.UpdateUI(hexItemDetail);
                WorldCamera.GetComponent<WorldCamera>().UpdateUI(hexItemDetail);
                OnClick_Hex(hexItemDetail);
            }
        }
        public bool isPointValid(Vector3 point) {
            return point.x >= 0 && point.x <= 1 && point.y >= 0 && point.y <= 1 && (!ConformationCanvas.Active || ConformationCanvas.Minimized);
        }

        public void OnClick_Hex(HexItemDetail hexItemDetail) {
            if (Input.GetMouseButtonUp(0) && CheckMouseDown) {
                CheckMouseDown = false;
                if (hexItemDetail.ShiftDown || ConformationCanvas.Minimized || !movePlayer(hexItemDetail)) {
                    ActionCard.SetupUI(hexItemDetail);
                }
            } else {
                if (Input.GetMouseButtonDown(0)) {
                    CheckMouseDown = true;
                }
            }
        }

        public bool movePlayer(HexItemDetail hd) {
            if (D.isTurn && D.LocalPlayer.PlayerTurnPhase <= TurnPhase_Enum.Move) {
                if (hd.IsFlight) {
                    Flight(hd);
                } else if (hd.IsSpaceBending) {
                    SpaceBending(hd);
                } else if (hd.IsUndergroundTravel) {
                    UndergroundTravel(hd);
                } else if (hd.IsUndergroundAttack) {
                    UndergroundAttack(hd);
                } else if (hd.IsWingsOfWind) {
                    WingsOfWind(hd);
                } else {
                    if (hd.IsPlayerAdjacent) {
                        if (hd.IsLegalMovement) {
                            if (hd.PlayerCostMet) {
                                if (hd.TriggerCombat) {
                                    SelectionHex.Show(false);
                                    TriggerBattlePanel.SetupUI(hd, performMovement);
                                } else {
                                    performMovement(hd);
                                }
                            } else {
                                ActionCard.Msg("You do not have enough movement points to move");
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void UndergroundTravel(HexItemDetail hd) {
            if (hd.IsLegalMovement && !hd.TriggerCombatNoRamp) {
                if (hd.Distance <= 3) {
                    if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Move) {
                        D.LocalPlayer.PlayerTurnPhase = TurnPhase_Enum.Move;
                    }
                    D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.CS_UndergroundTravel);
                    D.LocalPlayer.GridLocationHistory.Insert(0, hd.GridPosition);
                    D.C.LogMessage("[Underground Travel]");
                    D.C.Send_GameData();
                }
            }
        }

        public void WingsOfWind(HexItemDetail hd) {
            if (hd.IsLegalMovement && !hd.TriggerCombatNoRamp) {
                if (hd.Distance <= 5) {
                    D.LocalPlayer.Movement -= hd.Distance;
                    if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Move) {
                        D.LocalPlayer.PlayerTurnPhase = TurnPhase_Enum.Move;
                    }
                    D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.CS_WingsOfWind);
                    D.LocalPlayer.GridLocationHistory.Insert(0, hd.GridPosition);
                    D.C.LogMessage("[Wings of Wind]");
                    D.C.Send_GameData();
                }
            }
        }

        public void UndergroundAttack(HexItemDetail hd) {
            if (hd.IsLegalMovement && hd.TriggerCombatNoRamp && hd.IsSiteFortified) {
                if (hd.Distance <= 3) {
                    TriggerBattlePanel.SetupUI(hd, UndergroundAttack_move);
                }
            }
        }

        public void UndergroundAttack_move(HexItemDetail hd) {
            if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Move) {
                D.LocalPlayer.PlayerTurnPhase = TurnPhase_Enum.Move;
            }
            D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.CS_UndergroundAttack);
            D.LocalPlayer.GridLocationHistory.Insert(0, hd.GridPosition);
            D.C.LogMessage("[Underground Attack]");
            D.C.Send_GameData();
        }

        public void SpaceBending(HexItemDetail hd) {
            if (hd.IsLegalMovement && !hd.TriggerCombatNoRamp) {
                if (hd.Distance <= 2) {
                    if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Move) {
                        D.LocalPlayer.PlayerTurnPhase = TurnPhase_Enum.Move;
                    }
                    D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.CS_SpaceBending);
                    D.LocalPlayer.GridLocationHistory.Insert(0, hd.GridPosition);
                    D.C.LogMessage("[Space Bending]");
                    D.C.Send_GameData();
                }
            }
        }

        public void Flight(HexItemDetail hd) {
            if (hd.IsLegalMovement && !hd.TriggerCombatNoRamp) {
                if (hd.Distance == 1) {
                    if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Move) {
                        D.LocalPlayer.PlayerTurnPhase = TurnPhase_Enum.Move;
                    }
                    D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.GREEN_Flight);
                    D.LocalPlayer.GridLocationHistory.Insert(0, hd.GridPosition);
                    D.C.LogMessage("[Flight]");
                    D.C.Send_GameData();
                } else if (hd.Distance == 2) {
                    D.LocalPlayer.Movement -= 2;
                    if (D.LocalPlayer.PlayerTurnPhase < TurnPhase_Enum.Move) {
                        D.LocalPlayer.PlayerTurnPhase = TurnPhase_Enum.Move;
                    }
                    D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.GREEN_Flight);
                    D.LocalPlayer.GridLocationHistory.Insert(0, hd.GridPosition);
                    D.C.LogMessage("[Flight] :: Move -2");
                    D.C.Send_GameData();
                }
            }
        }


        public void performMovement(HexItemDetail hd) {
            PlayerData pd = D.LocalPlayer;
            if (pd.PlayerTurnPhase < TurnPhase_Enum.Move) {
                pd.PlayerTurnPhase = TurnPhase_Enum.Move;
            }
            pd.Movement -= hd.PlayerMovementCost;
            pd.GridLocationHistory.Insert(0, hd.GridPosition);
            addLoctionGameEffect(hd);
            BasicUtil.UpdateMovementGameEffects(pd);
            D.C.LogMessage("[Move] :: Move -" + hd.PlayerMovementCost);
            D.C.Send_GameData();
        }



        public Image_Enum getTerrainAtLoc(V2IntVO loc) {
            int mapIndex = D.Scenario.ConvertWorldToIndex(loc);
            int locIndex = D.Scenario.ConvertWorldToLocIndex(loc);
            MapHexId_Enum mapHexId_Enum = D.G.Board.CurrentMap[mapIndex];
            MapHexVO mapHex = D.HexMap[mapHexId_Enum];
            return mapHex.TerrainList[locIndex];
        }


        public void addLoctionGameEffect(HexItemDetail hd) {
            D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.SH_MageTower);
            D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.SH_Keep);
            D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.SH_City_Red);
            D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.SH_City_Green);
            D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.SH_City_White);
            D.LocalPlayer.GameEffects.Remove(GameEffect_Enum.SH_City_Blue);
            GameEffect_Enum ge = GameEffect_Enum.NA;
            switch (hd.Structure) {
                case Image_Enum.SH_MageTower: {
                    ge = GameEffect_Enum.SH_MageTower;
                    break;
                }
                case Image_Enum.SH_Keep: {
                    ge = GameEffect_Enum.SH_Keep;
                    break;
                }
                case Image_Enum.SH_City_Blue: {
                    ge = GameEffect_Enum.SH_City_Blue;
                    break;
                }
                case Image_Enum.SH_City_Green: {
                    ge = GameEffect_Enum.SH_City_Green;
                    break;
                }
                case Image_Enum.SH_City_Red: {
                    ge = GameEffect_Enum.SH_City_Red;
                    break;
                }
                case Image_Enum.SH_City_White: {
                    ge = GameEffect_Enum.SH_City_White;
                    break;
                }
            }
            D.LocalPlayer.AddGameEffect(ge);
            if (hd.TriggerCombat) {
                hd.Monsters.ForEach(m => {
                    switch (m.Structure) {
                        case Image_Enum.SH_MageTower: {
                            D.LocalPlayer.AddGameEffect(GameEffect_Enum.SH_MageTower, m.Uniqueid);
                            break;
                        }
                        case Image_Enum.SH_Keep: {
                            D.LocalPlayer.AddGameEffect(GameEffect_Enum.SH_Keep, m.Uniqueid);
                            break;
                        }
                        case Image_Enum.SH_City_Blue: {
                            D.LocalPlayer.AddGameEffect(GameEffect_Enum.SH_City_Blue, m.Uniqueid);
                            break;
                        }
                        case Image_Enum.SH_City_Green: {
                            D.LocalPlayer.AddGameEffect(GameEffect_Enum.SH_City_Green, m.Uniqueid);
                            break;
                        }
                        case Image_Enum.SH_City_Red: {
                            D.LocalPlayer.AddGameEffect(GameEffect_Enum.SH_City_Red, m.Uniqueid);
                            break;
                        }
                        case Image_Enum.SH_City_White: {
                            D.LocalPlayer.AddGameEffect(GameEffect_Enum.SH_City_White, m.Uniqueid);
                            break;
                        }
                    }
                });
            }
        }

        public void UpdateUI() {
            drawCurrentMap();
            playerTilemap.UpdateUI();
            MonsterTilemap.UpdateUI();
            ShieldTilemap.UpdateUI();
        }

        public void drawCurrentMap() {
            TerrainTilemap.ClearAllTiles();
            StructureTilemap.ClearAllTiles();
            TerrainBoarderTilemap.ClearAllTiles();
            for (int i = 0; i < D.Board.CurrentMap.Count; i++) {
                Vector3Int pos = D.Scenario.ConvertIndexToWorld(i);
                drawMapHex(D.HexMap[D.Board.CurrentMap[i]], pos);
            }
        }

        private void drawMapHex(MapHexVO mapHex, Vector3Int gridCenterPos) {
            if (gridCenterPos.z >= 0) {
                int x = gridCenterPos.x;
                int y = gridCenterPos.y;
                int offset = 0;
                if ((y / 2) * 2 == y) {
                    offset = -1;
                }
                // Center
                DrawHex(mapHex, 2, new Vector3Int(x - 1, y, 0));
                DrawHex(mapHex, 3, new Vector3Int(x, y, 0));
                DrawHex(mapHex, 4, new Vector3Int(x + 1, y, 0));
                // Top 
                DrawHex(mapHex, 0, new Vector3Int(x + offset, y + 1, 0));
                DrawHex(mapHex, 1, new Vector3Int(x + offset + 1, y + 1, 0));
                // Bottom 
                DrawHex(mapHex, 5, new Vector3Int(x + offset, y - 1, 0));
                DrawHex(mapHex, 6, new Vector3Int(x + offset + 1, y - 1, 0));
                TerrainBoarderTilemap.SetTile(gridCenterPos, D.TerrainMap[Image_Enum.TH_Boarder]);
            }
        }

        private void DrawHex(MapHexVO mapHex, int index, Vector3Int pos) {
            if (mapHex.TerrainList[index] != Image_Enum.NA) {
                TerrainTilemap.SetTile(pos, D.TerrainMap[mapHex.TerrainList[index]]);
            } else {
                TerrainTilemap.SetTile(pos, null);
            }
            if (mapHex.StructureList[index] != Image_Enum.NA) {
                StructureTilemap.SetTile(pos, D.StructureMap[mapHex.StructureList[index]]);
            } else {
                StructureTilemap.SetTile(pos, null);
            }
        }
    }
}