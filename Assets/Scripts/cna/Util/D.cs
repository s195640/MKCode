using System.Collections.Generic;
using cna.connector;
using cna.poo;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace cna {
    public class D {
        private static string gameid = "";
        private static int seed = 0;
        private static BaseConnector connector;
        private static CardEngine cardEngine;
        private static AppEngine appEngine;
        private static BattleEngine battleEngine;
        private static AppConnector appConnector;
        private static Dictionary<Image_Enum, Sprite> spriteMap;
        private static Dictionary<Image_Enum, AvatarMetaData> avatarMetaDataMap;
        private static Dictionary<Image_Enum, Tile> terrainMap;
        private static Dictionary<Image_Enum, Tile> structureMap;
        private static Dictionary<MapHexId_Enum, MapHexVO> hexMap;
        private static List<CardVO> cards;
        public static CardEngine Action { get { if (cardEngine == null) { cardEngine = Object.FindObjectOfType<CardEngine>(); } return cardEngine; } }
        public static void Msg(string msg) { Action.Msg(msg); }
        public static AppEngine A { get { if (appEngine == null) { appEngine = Object.FindObjectOfType<AppEngine>(); } return appEngine; } }
        public static AppConnector C { get { if (appConnector == null) { appConnector = Object.FindObjectOfType<AppConnector>(); } return appConnector; } }
        public static Data G { get => A.masterGameData; set => A.masterGameData = value; }
        public static BattleEngine B { get { if (battleEngine == null) { battleEngine = Resources.FindObjectsOfTypeAll<BattleEngine>()[0]; } return battleEngine; } }
        public static int LocalPlayerKey { get => Connector.Player.Key; }
        public static int HostPlayerKey { get => GLD.HostKey; }
        public static PlayerData LocalPlayer { get { return G.Players.Find(p => p.Key.Equals(LocalPlayerKey)); } }
        public static PlayerData CurrentTurn { get { return G.Players.Find(p => p.Key == G.Board.PlayerTurnOrder[G.Board.PlayerTurnIndex]); } }
        public static PlayerData DummyPlayer { get { return GetPlayerByKey(-999); } }
        public static BoardData Board { get => G.Board; }
        public static GameData GLD { get => G.GameData; }
        public static ScenarioBase Scenario {
            get {
                if (!gameid.Equals(G.GameId) || seed != GLD.Seed || A.scenario == null) {
                    gameid = G.GameId;
                    seed = GLD.Seed;
                    A.scenario = ScenarioBase.Create();
                }
                return A.scenario;
            }
        }
        public static ClientState_Enum ClientState { get => A.clientState; set => A.clientState = value; }
        public static List<LobbyData> LobbyDataList { get => C.lobbyDataList; set => C.lobbyDataList = value; }
        public static PlayerData HostPlayer {
            get {
                if (ClientState == ClientState_Enum.NOT_CONNECTED) {
                    return null;
                } else {
                    return G.Players.Find(p => p.Key == G.HostPlayerKey);
                }
            }
        }
        public static bool isHost { get { return LocalPlayerKey == G.HostPlayerKey || ClientState == ClientState_Enum.SINGLE_PLAYER; } }
        public static bool isTurn {
            get {
                PlayerData l = LocalPlayer;
                return l.PlayerTurnPhase >= TurnPhase_Enum.StartTurn && l.PlayerTurnPhase < TurnPhase_Enum.EndTurn;
            }
        }
        public static bool hasCoreBeenDrawn {
            get {
                bool foundCore = false;
                D.Board.CurrentMap.ForEach(m => {
                    if (m >= MapHexId_Enum.Core_01) {
                        foundCore = true;
                    }
                });
                return foundCore;
            }
        }
        public static BaseConnector Connector { get => connector; set => connector = value; }
        public static Queue<ChatItemData> ChatQueue { get => C.chatQueue; set => C.chatQueue = value; }
        public static Queue<LogData> LogQueue { get => C.logQueue; set => C.logQueue = value; }
        public static ScreenState_Enum ScreenState { get => A.screenState; set => A.screenState = value; }
        public static void Send(wsMsg msg) {
            Connector.sendMsg(msg.ToDataStr());
        }
        public static Dictionary<MapHexId_Enum, MapHexVO> HexMap {
            get {
                if (hexMap == null) {
                    hexMap = new Dictionary<MapHexId_Enum, MapHexVO>();
                    hexMap.Add(MapHexId_Enum.Invalid, new MapHexVO(MapHexId_Enum.Invalid, new Image_Enum[] { Image_Enum.TH_Invalid, Image_Enum.TH_Invalid, Image_Enum.TH_Invalid, Image_Enum.TH_Invalid, Image_Enum.TH_Invalid, Image_Enum.TH_Invalid, Image_Enum.TH_Invalid }, new Image_Enum[] { Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.Explore, new MapHexVO(MapHexId_Enum.Explore, new Image_Enum[] { Image_Enum.TH_ExploreOuter, Image_Enum.TH_ExploreOuter, Image_Enum.TH_ExploreOuter, Image_Enum.TH_ExploreInner, Image_Enum.TH_ExploreOuter, Image_Enum.TH_ExploreOuter, Image_Enum.TH_ExploreOuter }, new Image_Enum[] { Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.Basic_Back, new MapHexVO(MapHexId_Enum.Basic_Back, new Image_Enum[] { Image_Enum.TH_UnexploredBasic, Image_Enum.TH_UnexploredBasic, Image_Enum.TH_UnexploredBasic, Image_Enum.TH_UnexploredBasic, Image_Enum.TH_UnexploredBasic, Image_Enum.TH_UnexploredBasic, Image_Enum.TH_UnexploredBasic }, new Image_Enum[] { Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.Core_Back, new MapHexVO(MapHexId_Enum.Core_Back, new Image_Enum[] { Image_Enum.TH_UnexploredAdvanced, Image_Enum.TH_UnexploredAdvanced, Image_Enum.TH_UnexploredAdvanced, Image_Enum.TH_UnexploredAdvanced, Image_Enum.TH_UnexploredAdvanced, Image_Enum.TH_UnexploredAdvanced, Image_Enum.TH_UnexploredAdvanced }, new Image_Enum[] { Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.Start_A, new MapHexVO(MapHexId_Enum.Start_A, new Image_Enum[] { Image_Enum.TH_Plains, Image_Enum.TH_Forest, Image_Enum.TH_Lake, Image_Enum.TH_Plains, Image_Enum.TH_Plains, Image_Enum.TH_Lake, Image_Enum.TH_Lake }, new Image_Enum[] { Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_StartShrine, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.Start_B, new MapHexVO(MapHexId_Enum.Start_B, new Image_Enum[] { Image_Enum.TH_Plains, Image_Enum.TH_Forest, Image_Enum.TH_Lake, Image_Enum.TH_Plains, Image_Enum.TH_Plains, Image_Enum.TH_Lake, Image_Enum.TH_Plains }, new Image_Enum[] { Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_StartShrine, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.Basic_01, new MapHexVO(MapHexId_Enum.Basic_01, new Image_Enum[] { Image_Enum.TH_Forest, Image_Enum.TH_Lake, Image_Enum.TH_Forest, Image_Enum.TH_Forest, Image_Enum.TH_Plains, Image_Enum.TH_Plains, Image_Enum.TH_Plains }, new Image_Enum[] { Image_Enum.SH_MaraudingOrcs, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_MagicGlade, Image_Enum.SH_Village, Image_Enum.NA, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.Basic_02, new MapHexVO(MapHexId_Enum.Basic_02, new Image_Enum[] { Image_Enum.TH_Hill, Image_Enum.TH_Forest, Image_Enum.TH_Plains, Image_Enum.TH_Hill, Image_Enum.TH_Plains, Image_Enum.TH_Hill, Image_Enum.TH_Plains }, new Image_Enum[] { Image_Enum.SH_MaraudingOrcs, Image_Enum.SH_MagicGlade, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_Village, Image_Enum.SH_CrystalMines_Green, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.Basic_03, new MapHexVO(MapHexId_Enum.Basic_03, new Image_Enum[] { Image_Enum.TH_Plains, Image_Enum.TH_Hill, Image_Enum.TH_Plains, Image_Enum.TH_Forest, Image_Enum.TH_Hill, Image_Enum.TH_Plains, Image_Enum.TH_Hill }, new Image_Enum[] { Image_Enum.NA, Image_Enum.SH_Keep, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_Village, Image_Enum.SH_CrystalMines_White }));
                    hexMap.Add(MapHexId_Enum.Basic_04, new MapHexVO(MapHexId_Enum.Basic_04, new Image_Enum[] { Image_Enum.TH_Desert, Image_Enum.TH_Desert, Image_Enum.TH_Hill, Image_Enum.TH_Desert, Image_Enum.TH_Mountain, Image_Enum.TH_Plains, Image_Enum.TH_Plains }, new Image_Enum[] { Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_MaraudingOrcs, Image_Enum.SH_MageTower, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_Village }));
                    hexMap.Add(MapHexId_Enum.Basic_05, new MapHexVO(MapHexId_Enum.Basic_05, new Image_Enum[] { Image_Enum.TH_Forest, Image_Enum.TH_Plains, Image_Enum.TH_Forest, Image_Enum.TH_Lake, Image_Enum.TH_Plains, Image_Enum.TH_Forest, Image_Enum.TH_Hill }, new Image_Enum[] { Image_Enum.NA, Image_Enum.SH_Monastery, Image_Enum.SH_MagicGlade, Image_Enum.NA, Image_Enum.SH_MaraudingOrcs, Image_Enum.NA, Image_Enum.SH_CrystalMines_Blue }));
                    hexMap.Add(MapHexId_Enum.Basic_06, new MapHexVO(MapHexId_Enum.Basic_06, new Image_Enum[] { Image_Enum.TH_Mountain, Image_Enum.TH_Forest, Image_Enum.TH_Hill, Image_Enum.TH_Hill, Image_Enum.TH_Plains, Image_Enum.TH_Hill, Image_Enum.TH_Forest }, new Image_Enum[] { Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_MonsterDen, Image_Enum.SH_CrystalMines_Red, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_MaraudingOrcs }));
                    hexMap.Add(MapHexId_Enum.Basic_07, new MapHexVO(MapHexId_Enum.Basic_07, new Image_Enum[] { Image_Enum.TH_Lake, Image_Enum.TH_Forest, Image_Enum.TH_Plains, Image_Enum.TH_Swamp, Image_Enum.TH_Forest, Image_Enum.TH_Plains, Image_Enum.TH_Plains }, new Image_Enum[] { Image_Enum.NA, Image_Enum.SH_MaraudingOrcs, Image_Enum.SH_Monastery, Image_Enum.NA, Image_Enum.SH_MagicGlade, Image_Enum.NA, Image_Enum.SH_Dungeon }));
                    hexMap.Add(MapHexId_Enum.Basic_08, new MapHexVO(MapHexId_Enum.Basic_08, new Image_Enum[] { Image_Enum.TH_Forest, Image_Enum.TH_Forest, Image_Enum.TH_Forest, Image_Enum.TH_Swamp, Image_Enum.TH_Plains, Image_Enum.TH_Swamp, Image_Enum.TH_Swamp }, new Image_Enum[] { Image_Enum.SH_MagicGlade, Image_Enum.SH_AncientRuins, Image_Enum.NA, Image_Enum.SH_MaraudingOrcs, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_Village }));
                    hexMap.Add(MapHexId_Enum.Basic_09, new MapHexVO(MapHexId_Enum.Basic_09, new Image_Enum[] { Image_Enum.TH_Wasteland, Image_Enum.TH_Mountain, Image_Enum.TH_Plains, Image_Enum.TH_Mountain, Image_Enum.TH_Wasteland, Image_Enum.TH_Wasteland, Image_Enum.TH_Plains }, new Image_Enum[] { Image_Enum.SH_Dungeon, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_Keep, Image_Enum.SH_MageTower, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.Basic_10, new MapHexVO(MapHexId_Enum.Basic_10, new Image_Enum[] { Image_Enum.TH_Hill, Image_Enum.TH_Forest, Image_Enum.TH_Hill, Image_Enum.TH_Mountain, Image_Enum.TH_Plains, Image_Enum.TH_Hill, Image_Enum.TH_Hill }, new Image_Enum[] { Image_Enum.SH_MonsterDen, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_Keep, Image_Enum.SH_AncientRuins }));
                    hexMap.Add(MapHexId_Enum.Basic_11, new MapHexVO(MapHexId_Enum.Basic_11, new Image_Enum[] { Image_Enum.TH_Hill, Image_Enum.TH_Lake, Image_Enum.TH_Plains, Image_Enum.TH_Plains, Image_Enum.TH_Lake, Image_Enum.TH_Lake, Image_Enum.TH_Hill }, new Image_Enum[] { Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_AncientRuins, Image_Enum.SH_MageTower, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_MaraudingOrcs }));
                    hexMap.Add(MapHexId_Enum.Core_01, new MapHexVO(MapHexId_Enum.Core_01, new Image_Enum[] { Image_Enum.TH_Mountain, Image_Enum.TH_Desert, Image_Enum.TH_Hill, Image_Enum.TH_Desert, Image_Enum.TH_Desert, Image_Enum.TH_Hill, Image_Enum.TH_Desert }, new Image_Enum[] { Image_Enum.NA, Image_Enum.SH_Tomb, Image_Enum.SH_SpawningGround, Image_Enum.SH_Monastery, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.Core_02, new MapHexVO(MapHexId_Enum.Core_02, new Image_Enum[] { Image_Enum.TH_Lake, Image_Enum.TH_Swamp, Image_Enum.TH_Forest, Image_Enum.TH_Lake, Image_Enum.TH_Hill, Image_Enum.TH_Swamp, Image_Enum.TH_Swamp }, new Image_Enum[] { Image_Enum.NA, Image_Enum.SH_AncientRuins, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_CrystalMines_Green, Image_Enum.SH_MageTower, Image_Enum.SH_Draconum }));
                    hexMap.Add(MapHexId_Enum.Core_03, new MapHexVO(MapHexId_Enum.Core_03, new Image_Enum[] { Image_Enum.TH_Mountain, Image_Enum.TH_Wasteland, Image_Enum.TH_Wasteland, Image_Enum.TH_Wasteland, Image_Enum.TH_Hill, Image_Enum.TH_Hill, Image_Enum.TH_Wasteland }, new Image_Enum[] { Image_Enum.NA, Image_Enum.SH_AncientRuins, Image_Enum.SH_Tomb, Image_Enum.NA, Image_Enum.SH_MageTower, Image_Enum.SH_CrystalMines_White, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.Core_04, new MapHexVO(MapHexId_Enum.Core_04, new Image_Enum[] { Image_Enum.TH_Hill, Image_Enum.TH_Hill, Image_Enum.TH_Wasteland, Image_Enum.TH_Mountain, Image_Enum.TH_Wasteland, Image_Enum.TH_Wasteland, Image_Enum.TH_Wasteland }, new Image_Enum[] { Image_Enum.SH_CrystalMines_Blue, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_Draconum, Image_Enum.SH_Keep, Image_Enum.SH_AncientRuins, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.City_Green, new MapHexVO(MapHexId_Enum.City_Green, new Image_Enum[] { Image_Enum.TH_Forest, Image_Enum.TH_Swamp, Image_Enum.TH_Lake, Image_Enum.TH_Plains, Image_Enum.TH_Swamp, Image_Enum.TH_Forest, Image_Enum.TH_Swamp }, new Image_Enum[] { Image_Enum.SH_MagicGlade, Image_Enum.SH_Village, Image_Enum.NA, Image_Enum.SH_City_Green, Image_Enum.SH_MaraudingOrcs, Image_Enum.SH_MaraudingOrcs, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.City_Blue, new MapHexVO(MapHexId_Enum.City_Blue, new Image_Enum[] { Image_Enum.TH_Forest, Image_Enum.TH_Plains, Image_Enum.TH_Mountain, Image_Enum.TH_Plains, Image_Enum.TH_Lake, Image_Enum.TH_Hill, Image_Enum.TH_Lake }, new Image_Enum[] { Image_Enum.NA, Image_Enum.SH_Monastery, Image_Enum.SH_Draconum, Image_Enum.SH_City_Blue, Image_Enum.NA, Image_Enum.NA, Image_Enum.NA }));
                    hexMap.Add(MapHexId_Enum.City_White, new MapHexVO(MapHexId_Enum.City_White, new Image_Enum[] { Image_Enum.TH_Wasteland, Image_Enum.TH_Plains, Image_Enum.TH_Wasteland, Image_Enum.TH_Plains, Image_Enum.TH_Forest, Image_Enum.TH_Lake, Image_Enum.TH_Lake }, new Image_Enum[] { Image_Enum.SH_SpawningGround, Image_Enum.NA, Image_Enum.SH_Keep, Image_Enum.SH_City_White, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_Draconum }));
                    hexMap.Add(MapHexId_Enum.City_Red, new MapHexVO(MapHexId_Enum.City_Red, new Image_Enum[] { Image_Enum.TH_Desert, Image_Enum.TH_Hill, Image_Enum.TH_Wasteland, Image_Enum.TH_Plains, Image_Enum.TH_Desert, Image_Enum.TH_Wasteland, Image_Enum.TH_Desert }, new Image_Enum[] { Image_Enum.SH_AncientRuins, Image_Enum.SH_CrystalMines_Red, Image_Enum.SH_Draconum, Image_Enum.SH_City_Red, Image_Enum.NA, Image_Enum.NA, Image_Enum.SH_Draconum }));
                }
                return hexMap;
            }
        }

        public static Dictionary<Image_Enum, Sprite> SpriteMap { get => spriteMap; set => spriteMap = value; }
        public static Dictionary<Image_Enum, AvatarMetaData> AvatarMetaDataMap { get => avatarMetaDataMap; set => avatarMetaDataMap = value; }
        public static Dictionary<Image_Enum, Tile> TerrainMap { get => terrainMap; set => terrainMap = value; }
        public static Dictionary<Image_Enum, Tile> StructureMap { get => structureMap; set => structureMap = value; }
        public static List<CardVO> Cards { get => cards; set => cards = value; }
        public static CardVO GetGameEffectCard(GameEffect_Enum ge) {
            return Cards.Find(c => c.GameEffectId == ge);
        }

        public static PlayerData GetPlayerByKey(int key) {
            return G.Players.Find(p => p.Key == key);
        }
    }
}
