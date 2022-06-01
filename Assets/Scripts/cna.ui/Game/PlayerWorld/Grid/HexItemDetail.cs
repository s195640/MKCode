using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace cna.ui {
    public class HexItemDetail {
        private Grid MainGrid;
        private Tilemap TerrainTilemap;
        private Tilemap StructureTilemap;
        private Camera WorldCamera;
        private PlayerData localPlayer;
        private GameData g;
        private bool turn;
        private V2IntVO playerLocation;
        private V2IntVO gridPosition;
        private Vector3 screenPoint;
        private Vector3 gridWorldPosition;
        private Vector3 worldPoint;
        private Vector3 avatarWorldPosition;
        private bool shiftDown;
        private Image_Enum terrain;
        private Image_Enum structure;
        private bool isPlayerAdjacent;
        private int distance;
        private bool triggerCombat;
        private bool triggerCombatNoRamp;
        private int playerMovementCost;
        private bool isLegalMovement;
        private bool playerCostMet;
        private bool isRampaging;
        private List<MonsterMetaData> monsters;
        private bool isFlight;
        private bool isSpaceBending;
        private bool isUndergroundTravel;
        private bool isUndergroundAttack;
        private bool isWingsOfWind;
        private Image_Enum adventureAction;

        public V2IntVO PlayerLocation { get => playerLocation; private set => playerLocation = value; }
        public V2IntVO GridPosition { get => gridPosition; private set => gridPosition = value; }
        public Vector3 ScreenPoint { get => screenPoint; private set => screenPoint = value; }
        public Vector3 GridWorldPosition { get => gridWorldPosition; private set => gridWorldPosition = value; }
        public Vector3 WorldPoint { get => worldPoint; private set => worldPoint = value; }
        public Vector3 AvatarWorldPosition { get => avatarWorldPosition; private set => avatarWorldPosition = value; }
        public bool ShiftDown { get => shiftDown; private set => shiftDown = value; }
        public Image_Enum Terrain { get => terrain; private set => terrain = value; }
        public Image_Enum Structure { get => structure; private set => structure = value; }
        public bool IsPlayerAdjacent { get => isPlayerAdjacent; private set => isPlayerAdjacent = value; }
        public int Distance { get => distance; private set => distance = value; }
        public bool TriggerCombat { get => triggerCombat; private set => triggerCombat = value; }
        public bool TriggerCombatNoRamp { get => triggerCombatNoRamp; private set => triggerCombatNoRamp = value; }
        public int PlayerMovementCost { get => playerMovementCost; private set => playerMovementCost = value; }
        public bool IsLegalMovement { get => isLegalMovement; private set => isLegalMovement = value; }
        public bool PlayerCostMet { get => playerCostMet; private set => playerCostMet = value; }
        public bool IsRampaging { get => isRampaging; private set => isRampaging = value; }
        public List<MonsterMetaData> Monsters { get => monsters; set => monsters = value; }
        public bool IsFlight { get => isFlight; private set => isFlight = value; }


        public PlayerData LocalPlayer { get => localPlayer; private set => localPlayer = value; }
        public GameData G { get => g; private set => g = value; }
        public bool isTurn { get => turn; private set => turn = value; }
        public bool IsSpaceBending { get => isSpaceBending; set => isSpaceBending = value; }
        public bool IsUndergroundTravel { get => isUndergroundTravel; set => isUndergroundTravel = value; }
        public bool IsUndergroundAttack { get => isUndergroundAttack; set => isUndergroundAttack = value; }
        public bool IsSiteFortified {
            get {
                return Structure == Image_Enum.SH_Keep
                    || Structure == Image_Enum.SH_MageTower
                    || Structure == Image_Enum.SH_City_Blue
                    || Structure == Image_Enum.SH_City_Green
                    || Structure == Image_Enum.SH_City_Red
                    || Structure == Image_Enum.SH_City_White;
            }
        }

        public bool IsWingsOfWind { get => isWingsOfWind; set => isWingsOfWind = value; }
        public bool IsAdventureAction { get => AdventureAction != Image_Enum.NA; }
        public Image_Enum AdventureAction { get => adventureAction; set => adventureAction = value; }

        public HexItemDetail(Tilemap terrainTilemap, Tilemap structureTilemap, Grid mainGrid, Camera worldCamera) {
            Monsters = new List<MonsterMetaData>();
            LocalPlayer = D.LocalPlayer;
            PlayerLocation = localPlayer.CurrentGridLoc;
            G = D.G;
            isTurn = D.isTurn;
            MainGrid = mainGrid;
            TerrainTilemap = terrainTilemap;
            StructureTilemap = structureTilemap;
            WorldCamera = worldCamera;
            setupDetails();
        }

        private void setupDetails() {
            AdventureAction = Image_Enum.NA;
            ShiftDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            WorldPoint = WorldCamera.ScreenToWorldPoint(Input.mousePosition);
            GridPosition = new V2IntVO(MainGrid.WorldToCell(WorldPoint));
            GridWorldPosition = MainGrid.CellToWorld(GridPosition.Vector3Int);
            ScreenPoint = WorldCamera.WorldToScreenPoint(GridWorldPosition);
            AvatarWorldPosition = MainGrid.CellToLocal(PlayerLocation.Vector3Int);
            IsPlayerAdjacent = BasicUtil.IsAdjacent(PlayerLocation, GridPosition);
            Distance = BasicUtil.Distance(PlayerLocation, GridPosition);
            Terrain = BasicUtil.GetTilemapId(GridPosition, TerrainTilemap);
            Structure = BasicUtil.GetTilemapId(GridPosition, StructureTilemap);
            PlayerMovementCost = calculateMovementCost();
            IsLegalMovement = PlayerMovementCost >= 0;
            PlayerCostMet = PlayerMovementCost >= 0 && LocalPlayer.Movement >= PlayerMovementCost;
            TriggerCombatNoRamp = (Structure == Image_Enum.SH_Keep || Structure == Image_Enum.SH_MageTower || Structure == Image_Enum.SH_City_Blue || Structure == Image_Enum.SH_City_Red || Structure == Image_Enum.SH_City_Green || Structure == Image_Enum.SH_City_White) && G.Monsters.Map.ContainsKey(GridPosition);
            IsFlight = LocalPlayer.GameEffects.Keys.Contains(GameEffect_Enum.GREEN_Flight);
            IsSpaceBending = LocalPlayer.GameEffects.Keys.Contains(GameEffect_Enum.CS_SpaceBending);
            IsUndergroundTravel = LocalPlayer.GameEffects.Keys.Contains(GameEffect_Enum.CS_UndergroundTravel);
            IsUndergroundAttack = LocalPlayer.GameEffects.Keys.Contains(GameEffect_Enum.CS_UndergroundAttack);
            IsWingsOfWind = LocalPlayer.GameEffects.Keys.Contains(GameEffect_Enum.CS_WingsOfWind);
            if (TriggerCombatNoRamp) {
                Monsters.AddRange(G.Monsters.Map[GridPosition].Values.ConvertAll(m => {
                    return new MonsterMetaData(m, GridPosition, Structure);
                }));
            }
            if (Structure == Image_Enum.SH_MaraudingOrcs || Structure == Image_Enum.SH_Draconum) {
                if (G.Monsters.Map.ContainsKey(GridPosition)) {
                    IsLegalMovement = false;
                    Monsters.AddRange(G.Monsters.Map[GridPosition].Values.ConvertAll(m => {
                        return new MonsterMetaData(m, GridPosition, Structure);
                    }));
                }
            }

            if (IsLegalMovement) {
                List<V2IntVO> rampMonsterPlayerLoc = getSurroundingRampagingMonsters(PlayerLocation);
                List<V2IntVO> rampMonsterGridLoc = getSurroundingRampagingMonsters(GridPosition);
                HashSet<V2IntVO> rampMonsterLoc = new HashSet<V2IntVO>();
                foreach (V2IntVO p in rampMonsterPlayerLoc) {
                    if (rampMonsterGridLoc.Contains(p)) {
                        rampMonsterLoc.Add(p);
                    }
                }
                foreach (V2IntVO p in rampMonsterGridLoc) {
                    if (rampMonsterPlayerLoc.Contains(p)) {
                        rampMonsterLoc.Add(p);
                    }
                }
                IsRampaging = rampMonsterLoc.Count > 0;
                foreach (V2IntVO p in rampMonsterLoc) {
                    Monsters.AddRange(G.Monsters.Map[p].Values.ConvertAll(m => {
                        return new MonsterMetaData(m, p, BasicUtil.GetTilemapId(p, StructureTilemap));
                    }));
                }
                TriggerCombat = TriggerCombatNoRamp || IsRampaging;
            }
        }

        private List<V2IntVO> getSurroundingRampagingMonsters(V2IntVO pos) {
            List<V2IntVO> monsters = new List<V2IntVO>();
            List<V2IntVO> adjToPlayer = BasicUtil.GetAdjacentPoints(pos);
            foreach (V2IntVO adjPos in adjToPlayer) {
                Image_Enum adjStructure = BasicUtil.GetTilemapId(adjPos, StructureTilemap);
                if (adjStructure == Image_Enum.SH_MaraudingOrcs || adjStructure == Image_Enum.SH_Draconum) {
                    if (G.Monsters.Map.ContainsKey(adjPos)) {
                        monsters.Add(adjPos);
                    }
                }
            }
            return monsters;
        }

        private int calculateMovementCost() {
            int baseMovementCost = D.Scenario.getTerrainMovementCost(Terrain);
            CNAMap<GameEffect_Enum, WrapList<int>> gameEffect = LocalPlayer.GameEffects;
            gameEffect.Keys.ForEach(ge => {
                int count = gameEffect[ge].Count;
                switch (ge) {
                    case GameEffect_Enum.Foresters: {
                        if (Terrain == Image_Enum.TH_Forest
                            || Terrain == Image_Enum.TH_Hill
                            || Terrain == Image_Enum.TH_Swamp) {
                            baseMovementCost -= count;
                            if (baseMovementCost < 0) {
                                baseMovementCost = 0;
                            }
                        }
                        break;
                    }
                    case GameEffect_Enum.AC_FrostBridge01: {
                        if (Terrain == Image_Enum.TH_Swamp) {
                            if (baseMovementCost > 0) {
                                baseMovementCost = 1;
                            }
                        }
                        break;
                    }
                    case GameEffect_Enum.AC_FrostBridge02: {
                        if (Terrain == Image_Enum.TH_Swamp) {
                            if (baseMovementCost > 0) {
                                baseMovementCost = 1;
                            }
                        } else if (Terrain == Image_Enum.TH_Lake) {
                            if (baseMovementCost < 0 || baseMovementCost > 1) {
                                baseMovementCost = 1;
                            }
                        }
                        break;
                    }
                    case GameEffect_Enum.AC_PathFinding01: {
                        if (Terrain == Image_Enum.TH_Forest
                            || Terrain == Image_Enum.TH_Hill
                            || Terrain == Image_Enum.TH_Swamp
                            || Terrain == Image_Enum.TH_Desert
                            || Terrain == Image_Enum.TH_Wasteland
                            || Terrain == Image_Enum.TH_Plains) {
                            if (baseMovementCost > 2) {
                                baseMovementCost -= count;
                                if (baseMovementCost < 2) {
                                    baseMovementCost = 2;
                                }
                            }
                        }
                        break;
                    }
                    case GameEffect_Enum.AC_PathFinding02: {
                        if (Terrain == Image_Enum.TH_Forest
                            || Terrain == Image_Enum.TH_Hill
                            || Terrain == Image_Enum.TH_Swamp
                            || Terrain == Image_Enum.TH_Desert
                            || Terrain == Image_Enum.TH_Wasteland
                            || Terrain == Image_Enum.TH_Plains) {
                            if (baseMovementCost > 2) {
                                baseMovementCost = 2;
                            }
                        }
                        break;
                    }
                    case GameEffect_Enum.AC_SongOfWind01: {
                        if (Terrain == Image_Enum.TH_Desert
                            || Terrain == Image_Enum.TH_Wasteland
                            || Terrain == Image_Enum.TH_Plains) {
                            baseMovementCost -= count;
                            if (baseMovementCost < 0) {
                                baseMovementCost = 0;
                            }
                        }
                        break;
                    }
                    case GameEffect_Enum.AC_SongOfWind02: {
                        if (Terrain == Image_Enum.TH_Desert
                            || Terrain == Image_Enum.TH_Wasteland
                            || Terrain == Image_Enum.TH_Plains) {
                            baseMovementCost -= (2 * count);
                            if (baseMovementCost < 0) {
                                baseMovementCost = 0;
                            }
                        }
                        break;
                    }
                    case GameEffect_Enum.AC_SongOfWind03: {
                        if (Terrain == Image_Enum.TH_Lake) {
                            baseMovementCost = 0;
                        }
                        break;
                    }
                    case GameEffect_Enum.CT_AmuletOfTheSun: {
                        if (Terrain == Image_Enum.TH_Forest) {
                            if (baseMovementCost > 3) {
                                baseMovementCost = 3;
                            }
                        }
                        break;
                    }
                    case GameEffect_Enum.CT_AmuletOfDarkness: {
                        if (Terrain == Image_Enum.TH_Desert) {
                            if (baseMovementCost > 3) {
                                baseMovementCost = 3;
                            }
                        }
                        break;
                    }
                }
            });
            return baseMovementCost;
        }

        public override bool Equals(object obj) {
            return obj is HexItemDetail detail &&
                    turn == detail.turn &&
                    EqualityComparer<V2IntVO>.Default.Equals(playerLocation, detail.playerLocation) &&
                    EqualityComparer<V2IntVO>.Default.Equals(gridPosition, detail.gridPosition) &&
                    screenPoint.Equals(detail.screenPoint) &&
                    gridWorldPosition.Equals(detail.gridWorldPosition) &&
                    //worldPoint.Equals(detail.worldPoint) &&
                    //avatarWorldPosition.Equals(detail.avatarWorldPosition) &&
                    shiftDown == detail.shiftDown &&
                    terrain == detail.terrain &&
                    structure == detail.structure &&
                    isPlayerAdjacent == detail.isPlayerAdjacent &&
                    distance == detail.distance &&
                    triggerCombat == detail.triggerCombat &&
                    triggerCombatNoRamp == detail.triggerCombatNoRamp &&
                    playerMovementCost == detail.playerMovementCost &&
                    isLegalMovement == detail.isLegalMovement &&
                    playerCostMet == detail.playerCostMet &&
                    isRampaging == detail.isRampaging &&
                    isFlight == detail.isFlight;
        }

        public override int GetHashCode() {
            HashCode hash = new HashCode();
            hash.Add(turn);
            hash.Add(playerLocation);
            hash.Add(gridPosition);
            hash.Add(screenPoint);
            hash.Add(gridWorldPosition);
            //hash.Add(worldPoint);
            //hash.Add(avatarWorldPosition);
            hash.Add(shiftDown);
            hash.Add(terrain);
            hash.Add(structure);
            hash.Add(isPlayerAdjacent);
            hash.Add(distance);
            hash.Add(triggerCombat);
            hash.Add(triggerCombatNoRamp);
            hash.Add(playerMovementCost);
            hash.Add(isLegalMovement);
            hash.Add(playerCostMet);
            hash.Add(isRampaging);
            hash.Add(isFlight);
            return hash.ToHashCode();
        }
    }

}
