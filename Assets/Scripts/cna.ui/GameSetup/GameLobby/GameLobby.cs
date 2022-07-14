using System.Collections.Generic;
using System.Linq;
using cna.connector;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class GameLobby : MonoBehaviour {

        [Header("GameObjects")]
        [SerializeField] private Button exitButton;
        [SerializeField] private Button startGameButton;
        [SerializeField] private Button loadGameButton;
        [SerializeField] private Transform playerListContent;
        [SerializeField] private Transform avatarListContent;
        [SerializeField] private TMP_Dropdown gameLayoutDropdown;
        [SerializeField] private Toggle easyStartToggle;
        [SerializeField] private CNA_Slider rounds;
        [SerializeField] private CNA_Slider basicTiles;
        [SerializeField] private CNA_Slider coreTiles;
        [SerializeField] private CNA_Slider cityTiles;
        [SerializeField] private CNA_Slider cityLevel;
        [SerializeField] private Toggle dummyPlayerToggle;
        [SerializeField] private GameObject startbutton;
        [SerializeField] private GameObject loadMenu;

        [Header("Prefab")]
        [SerializeField] private GameLobbyAvatar gameLobbyAvatar_Pref;
        [SerializeField] private GameLobbyPlayer gameLobbyPlayer_Pref;

        [SerializeField] private List<GameLobbyPlayer> gameLobbyPlayers;
        [SerializeField] private List<GameLobbyAvatar> gameLobbyAvatars;


        [Header("Load Games")]
        private SortedDictionary<long, List<LoadGameVO>> games = new SortedDictionary<long, List<LoadGameVO>>();

        [SerializeField] private Transform savedGameContent;
        [SerializeField] private SavedGamePrefab prefab;
        private List<SavedGamePrefab> savedGameList = new List<SavedGamePrefab>();
        private SavedGamePrefab selectedGame;
        [SerializeField] private Transform bookMarkContent;
        [SerializeField] private BookmarkGamePrefab prefab2;
        private List<BookmarkGamePrefab> bookmarkList = new List<BookmarkGamePrefab>();
        private BookmarkGamePrefab selectedBookmark;
        [SerializeField] private CNA_Button loadSelectedGameButton;

        public SavedGamePrefab SelectedGame {
            get => selectedGame; set {
                if (SelectedGame != null) {
                    SelectedGame.Selected(false);
                }
                selectedGame = value;
                if (SelectedGame != null) {
                    SelectedGame.Selected(true);
                }
                SelectedBookmark = null;
            }
        }

        public BookmarkGamePrefab SelectedBookmark {
            get => selectedBookmark; set {
                if (selectedBookmark != null) {
                    selectedBookmark.Selected(false);
                }
                selectedBookmark = value;
                if (selectedBookmark != null) {
                    loadSelectedGameButton.Active = true;
                    selectedBookmark.Selected(true);
                } else {
                    loadSelectedGameButton.Active = false;
                }
            }
        }

        public GameLobby() : base() {
            gameLobbyPlayers = new List<GameLobbyPlayer>();
            gameLobbyAvatars = new List<GameLobbyAvatar>();
        }

        void Start() {
            exitButton.onClick.AddListener(ExitButtonCallback);
            startGameButton.onClick.AddListener(StartGameButtonCallback);
            loadGameButton.onClick.AddListener(LoadGameButtonCallback);
            gameLayoutDropdown.onValueChanged.AddListener(delegate { GameLayoutDropdownCallback(gameLayoutDropdown.value); });
            easyStartToggle.onValueChanged.AddListener(delegate { EasyStartToggleCallback(easyStartToggle.isOn); });
            basicTiles.Setup(0, 8, D.G.GameData.BasicTiles, BasicTileSliderCallback);
            coreTiles.Setup(0, 4, D.G.GameData.CoreTiles, CoreTileSliderCallback);
            cityTiles.Setup(1, 4, D.G.GameData.CityTiles, CityTileSliderCallback);
            cityLevel.Setup(1, 11, D.G.GameData.Level, CityLevelSliderCallback);
            rounds.Setup(1, 10, D.G.GameData.Rounds, RoundSliderCallback);
            dummyPlayerToggle.onValueChanged.AddListener(delegate { DummyPlayerToggleCallback(dummyPlayerToggle.isOn); });
            startbutton.SetActive(D.isHost);
            loadMenu.SetActive(false);
        }

        public void UpdateUI() {
            updatePlayerList();
            updateAvatarList();
            updateGameMenu();
        }

        private void updatePlayerList() {
            //  Remove/Update
            foreach (GameLobbyPlayer glp in gameLobbyPlayers.ToArray()) {
                PlayerData pd = D.G.Players.Find(p => p.Equals(glp.PlayerData));
                if (pd == null) {
                    Destroy(glp.gameObject);
                    gameLobbyPlayers.Remove(glp);
                } else {
                    glp.UpdateUI(pd);
                }
            }
            //  Add
            foreach (PlayerData pd in D.G.Players) {
                if (!gameLobbyPlayers.Exists(p => p.PlayerData.Equals(pd))) {
                    GameLobbyPlayer go = Instantiate(gameLobbyPlayer_Pref, new Vector3(0, 0, 0), Quaternion.identity);
                    if (D.LocalPlayerKey == pd.Key) {
                        go.UpdateUI(pd, AvatarRandomButtonCallback, PlayerReadyButtonCallback);
                    } else {
                        go.UpdateUI(pd);
                    }
                    go.transform.SetParent(playerListContent, false);
                    gameLobbyPlayers.Add(go);
                }
            }
        }

        private void updateAvatarList() {
            if (gameLobbyAvatars.Count == 0) {
                foreach (var item in D.AvatarMetaDataMap.Keys) {
                    if (item != Image_Enum.A_MEEPLE_RANDOM) {
                        GameLobbyAvatar go = Instantiate(gameLobbyAvatar_Pref, new Vector3(0, 0, 0), Quaternion.identity);
                        go.UpdateUI(item, AvatarButtonCallback);
                        go.transform.SetParent(avatarListContent, false);
                        gameLobbyAvatars.Add(go);
                    }
                }
            } else {
                gameLobbyAvatars.ForEach(a => a.UpdateUI());
            }
        }

        private void updateGameMenu() {
            if (gameLayoutDropdown.value != (int)D.G.GameData.GameMapLayout - 2) {
                gameLayoutDropdown.value = (int)D.G.GameData.GameMapLayout - 2;
            }
            if (D.G.GameData.EasyStart != easyStartToggle.isOn) {
                easyStartToggle.isOn = D.G.GameData.EasyStart;
            }
            if (D.G.GameData.Rounds != (int)rounds.Value) {
                rounds.Value = D.G.GameData.Rounds;
            }
            if (D.G.GameData.BasicTiles != (int)basicTiles.Value) {
                basicTiles.Value = D.G.GameData.BasicTiles;
            }
            if (D.G.GameData.CoreTiles != (int)coreTiles.Value) {
                coreTiles.Value = D.G.GameData.CoreTiles;
            }
            if (D.G.GameData.CityTiles != (int)cityTiles.Value) {
                cityTiles.Value = D.G.GameData.CityTiles;
            }
            if (D.G.GameData.Level != (int)cityLevel.Value) {
                cityLevel.Value = D.G.GameData.Level;
            }
            if (D.G.GameData.DummyPlayer != dummyPlayerToggle.isOn) {
                dummyPlayerToggle.isOn = D.G.GameData.DummyPlayer;
            }
        }


        private void AvatarButtonCallback(Image_Enum avatar_Enum) {
            Data gdClone = D.G.Clone();
            gdClone.Players.Find(p => p.Equals(D.LocalPlayer)).Avatar = avatar_Enum;
            D.C.Send_GameDataRequest(gdClone, gdClone.GameId);
        }

        private void AvatarRandomButtonCallback(Image_Enum avatar_Enum) {
            if (avatar_Enum != Image_Enum.A_MEEPLE_RANDOM) {
                D.LocalPlayer.Avatar = Image_Enum.A_MEEPLE_RANDOM;
                D.C.Send_GameData();
            }
        }
        private void PlayerReadyButtonCallback(bool value) {
            D.LocalPlayer.PlayerReady = value;
            D.C.Send_GameData();
        }

        private void ExitButtonCallback() {
            D.C.QuitGame();
        }

        private void StartGameButtonCallback() {
            if (D.ClientState == ClientState_Enum.CONNECTED_HOST || D.ClientState == ClientState_Enum.SINGLE_PLAYER) {
                D.G.GameStatus = Game_Enum.New_Game;
            }
        }



        private void GameLayoutDropdownCallback(int value) {
            if (D.G.GameData.GameMapLayout != (GameMapLayout_Enum)(value + 2)) {
                D.G.GameData.GameMapLayout = (GameMapLayout_Enum)(value + 2);
                D.C.Send_GameData();
            }
        }
        private void EasyStartToggleCallback(bool value) {
            if (D.G.GameData.EasyStart != value) {
                D.G.GameData.EasyStart = value;
                D.C.Send_GameData();
            }
        }

        private void BasicTileSliderCallback(float value) {
            if (D.G.GameData.BasicTiles != (int)value) {
                D.G.GameData.BasicTiles = (int)value;
                D.C.Send_GameData();
            }
        }
        private void CoreTileSliderCallback(float value) {
            if (D.G.GameData.CoreTiles != (int)value) {
                D.G.GameData.CoreTiles = (int)value;
                D.C.Send_GameData();
            }
        }
        private void CityTileSliderCallback(float value) {
            if (D.G.GameData.CityTiles != (int)value) {
                D.G.GameData.CityTiles = (int)value;
                D.C.Send_GameData();
            }
        }

        private void CityLevelSliderCallback(float value) {
            if (D.G.GameData.Level != (int)value) {
                D.G.GameData.Level = (int)value;
                D.C.Send_GameData();
            }
        }

        private void DummyPlayerToggleCallback(bool value) {
            if (D.G.GameData.DummyPlayer != value) {
                D.G.GameData.DummyPlayer = value;
                D.C.Send_GameData();
            }
        }
        private void RoundSliderCallback(float value) {
            if (D.G.GameData.Rounds != (int)value) {
                D.G.GameData.Rounds = (int)value;
                D.C.Send_GameData();
            }
        }

        private void LoadGameButtonCallback() {
            loadMenu.SetActive(true);
            savedGameList.ForEach(s => Destroy(s.gameObject));
            savedGameList.Clear();
            bookmarkList.ForEach(s => Destroy(s.gameObject));
            bookmarkList.Clear();
            SelectedGame = null;
            SelectedBookmark = null;
            games.Clear();
            List<string> fileNames = SaveLoadUtil.LoadGameNames();
            fileNames.ForEach(fileName => {
                LoadGameVO lg = new LoadGameVO(fileName);
                if (lg.verison.Equals(Application.version)) {
                    if (!games.ContainsKey(lg.time)) {
                        games.Add(lg.time, new List<LoadGameVO>());
                    }
                    games[lg.time].Add(lg);
                }
            });

            foreach (var x in games.Reverse()) {
                SavedGamePrefab p = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                p.transform.SetParent(savedGameContent);
                p.transform.localScale = Vector3.one;
                p.SetupUI(x.Key, x.Value[0], OnClick_SelectGame);
                savedGameList.Add(p);
            }
        }

        public void OnClick_LoadGame() {
            loadMenu.SetActive(false);
            D.G = SaveLoadUtil.LoadGame(selectedBookmark.FileName);
            SelectedGame = null;
            D.C.Send_GameData();
        }
        public void OnClick_Cancel() {
            loadMenu.SetActive(false);
        }

        public void OnClick_SelectGame(SavedGamePrefab savedGame) {
            SelectedGame = savedGame;
            bookmarkList.ForEach(s => Destroy(s.gameObject));
            bookmarkList.Clear();
            SelectedBookmark = null;
            games[savedGame.Time].OrderByDescending(lg => lg.turn).ToList().ForEach(lg => {
                BookmarkGamePrefab p = Instantiate(prefab2, Vector3.zero, Quaternion.identity);
                p.transform.SetParent(bookMarkContent);
                p.transform.localScale = Vector3.one;
                p.SetupUI(lg, OnClick_SelectBookmark);
                bookmarkList.Add(p);
            });
        }

        public void OnClick_SelectBookmark(BookmarkGamePrefab bookmark) {
            SelectedBookmark = bookmark;
        }
    }
}
