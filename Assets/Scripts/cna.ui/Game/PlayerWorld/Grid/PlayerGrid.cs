using cna.poo;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace cna.ui {
    public class PlayerGrid : MonoBehaviour {
        [SerializeField] private Camera WorldCamera;
        [SerializeField] private RectTransform GameDisplay;
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
            if (isPointValid()) {
                HexItemDetail hexItemDetail = new HexItemDetail(TerrainTilemap, StructureTilemap, MainGrid, WorldCamera);
                SelectionHex.UpdateUI(hexItemDetail);
                WorldCamera.GetComponent<WorldCamera>().UpdateUI(hexItemDetail);
                OnClick_Hex(hexItemDetail);
            }
        }
        public bool isPointValid() {
            return (!ConformationCanvas.Active || ConformationCanvas.Minimized)
                && D.ScreenState == ScreenState_Enum.Map
                && RectTransformUtility.RectangleContainsScreenPoint(GameDisplay, Input.mousePosition, WorldCamera);
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
                                    if (!D.LocalPlayer.VisableMonsters.Contains(hd.Monsters[0].Uniqueid)) {
                                        TriggerBattlePanel.SetupUI(hd, (hd) => { performMovement(hd); D.A.pd_StartOfTurn = D.LocalPlayer.Clone();  }, TriggerBattlePanel.STANDARD_BATTLE_NO_UNDO);
                                    } else {
                                        TriggerBattlePanel.SetupUI(hd, performMovement);
                                    }
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
                    GameAPI ar = new GameAPI(hd.G, hd.LocalPlayer);
                    ar.TurnPhase(TurnPhase_Enum.Move);
                    ar.RemoveGameEffect(GameEffect_Enum.CS_UndergroundTravel);
                    ar.SetCurrentLocation(hd.GridPosition, hd.TriggerCombat, hd.Monsters);
                    ar.AddLog("[Underground Travel]");
                    ar.CompleteAction();
                }
            }
        }

        public void WingsOfWind(HexItemDetail hd) {
            if (hd.IsLegalMovement && !hd.TriggerCombatNoRamp) {
                if (hd.Distance <= 5) {
                    GameAPI ar = new GameAPI(hd.G, hd.LocalPlayer);
                    ar.TurnPhase(TurnPhase_Enum.Move);
                    ar.RemoveGameEffect(GameEffect_Enum.CS_WingsOfWind);
                    ar.SetCurrentLocation(hd.GridPosition, hd.TriggerCombat, hd.Monsters);
                    ar.AddLog("[Wings of Wind]");
                    ar.ActionMovement(-1 * hd.Distance);
                    ar.CompleteAction();
                }
            }
        }

        public void UndergroundAttack(HexItemDetail hd) {
            if (hd.IsLegalMovement && hd.TriggerCombatNoRamp && hd.IsSiteFortified) {
                if (hd.Distance <= 3) {
                    if (!D.LocalPlayer.VisableMonsters.Contains(hd.Monsters[0].Uniqueid)) {
                        TriggerBattlePanel.SetupUI(hd, (hd) => { UndergroundAttack_move(hd); D.A.pd_StartOfTurn = D.LocalPlayer.Clone(); }, TriggerBattlePanel.STANDARD_BATTLE_NO_UNDO);
                    } else {
                        TriggerBattlePanel.SetupUI(hd, UndergroundAttack_move);
                    }
                    //TriggerBattlePanel.SetupUI(hd, UndergroundAttack_move);
                }
            }
        }

        public void UndergroundAttack_move(HexItemDetail hd) {
            GameAPI ar = new GameAPI(hd.G, hd.LocalPlayer);
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.RemoveGameEffect(GameEffect_Enum.CS_UndergroundAttack);
            ar.SetCurrentLocation(hd.GridPosition, hd.TriggerCombat, hd.Monsters);
            ar.AddLog("[Underground Attack]");
            ar.CompleteAction();
        }

        public void SpaceBending(HexItemDetail hd) {
            if (hd.IsLegalMovement && !hd.TriggerCombatNoRamp) {
                if (hd.Distance <= 2) {
                    GameAPI ar = new GameAPI(hd.G, hd.LocalPlayer);
                    ar.TurnPhase(TurnPhase_Enum.Move);
                    ar.RemoveGameEffect(GameEffect_Enum.CS_SpaceBending);
                    ar.SetCurrentLocation(hd.GridPosition, hd.TriggerCombat, hd.Monsters);
                    ar.AddLog("[Space Bending]");
                    ar.CompleteAction();
                }
            }
        }

        public void Flight(HexItemDetail hd) {
            if (hd.IsLegalMovement && !hd.TriggerCombatNoRamp) {
                if (hd.Distance == 1) {
                    GameAPI ar = new GameAPI(hd.G, hd.LocalPlayer);
                    ar.TurnPhase(TurnPhase_Enum.Move);
                    ar.RemoveGameEffect(GameEffect_Enum.GREEN_Flight);
                    ar.SetCurrentLocation(hd.GridPosition, hd.TriggerCombat, hd.Monsters);
                    ar.AddLog("[Flight]");
                    ar.CompleteAction();
                } else if (hd.Distance == 2) {
                    if (hd.LocalPlayer.Movement >= 2) {
                        GameAPI ar = new GameAPI(hd.G, hd.LocalPlayer);
                        ar.TurnPhase(TurnPhase_Enum.Move);
                        ar.RemoveGameEffect(GameEffect_Enum.GREEN_Flight);
                        ar.SetCurrentLocation(hd.GridPosition, hd.TriggerCombat, hd.Monsters);
                        ar.AddLog("[Flight]");
                        ar.ActionMovement(-2);
                        ar.CompleteAction();
                    } else {
                        ActionCard.Msg("Flight costs 2 movement for a distanct of 2, you do not have enough movement!");
                    }
                }
            }
        }

        public void performMovement(HexItemDetail hd) {
            GameAPI ar = new GameAPI(hd.G, hd.LocalPlayer);
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.SetCurrentLocation(hd.GridPosition, hd.TriggerCombat, hd.Monsters);
            ar.AddLog("[Move]");
            ar.ActionMovement(-1 * hd.PlayerMovementCost);
            ar.CompleteAction();
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
            PlayerData pd = D.LocalPlayer;
            for (int i = 0; i < pd.Board.PlayerMap.Count; i++) {
                Vector3Int pos = D.Scenario.ConvertIndexToWorld(i);
                drawMapHex(D.HexMap[pd.Board.PlayerMap[i]], pos);
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