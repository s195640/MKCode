using System.Collections.Generic;
using cna.poo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cna.ui {
    public class GameLobby : MonoBehaviour {

        [Header("GameObjects")]
        [SerializeField] private Button exitButton;
        [SerializeField] private Button startGameButton;
        [SerializeField] private Transform playerListContent;
        [SerializeField] private Transform avatarListContent;
        [SerializeField] private TMP_Dropdown gameLayoutDropdown;
        [SerializeField] private Toggle easyStartToggle;
        [SerializeField] private CNA_Slider rounds;
        [SerializeField] private CNA_Slider basicTiles;
        [SerializeField] private CNA_Slider coreTiles;
        [SerializeField] private CNA_Slider cityTiles;
        [SerializeField] private Toggle dummyPlayerToggle;

        [Header("Prefab")]
        [SerializeField] private GameLobbyAvatar gameLobbyAvatar_Pref;
        [SerializeField] private GameLobbyPlayer gameLobbyPlayer_Pref;

        [SerializeField] private List<GameLobbyPlayer> gameLobbyPlayers;
        [SerializeField] private List<GameLobbyAvatar> gameLobbyAvatars;

        public GameLobby() : base() {
            gameLobbyPlayers = new List<GameLobbyPlayer>();
            gameLobbyAvatars = new List<GameLobbyAvatar>();
        }

        void Start() {
            exitButton.onClick.AddListener(ExitButtonCallback);
            startGameButton.onClick.AddListener(StartGameButtonCallback);
            gameLayoutDropdown.onValueChanged.AddListener(delegate { GameLayoutDropdownCallback(gameLayoutDropdown.value); });
            easyStartToggle.onValueChanged.AddListener(delegate { EasyStartToggleCallback(easyStartToggle.isOn); });
            basicTiles.Setup(D.G.Gld.EasyStart ? D.G.Gld.GameMapLayout == GameMapLayout_Enum.Wedge ? 2 : 3 : 0, 11, D.G.Gld.BasicTiles, BasicTileSliderCallback);
            coreTiles.Setup(0, 4, D.G.Gld.CoreTiles, CoreTileSliderCallback);
            cityTiles.Setup(1, 4, D.G.Gld.CityTiles, CityTileSliderCallback);
            rounds.Setup(1, 10, D.G.Gld.Rounds, RoundSliderCallback);
            dummyPlayerToggle.onValueChanged.AddListener(delegate { DummyPlayerToggleCallback(dummyPlayerToggle.isOn); });
        }

        public void UpdateUI() {
            updatePlayerList();
            updateAvatarList();
            updateGameMenu();
        }

        private void updatePlayerList() {
            //  Remove/Update
            foreach (GameLobbyPlayer glp in gameLobbyPlayers.ToArray()) {
                PlayerData pd = D.G.Gld.Players.Find(p => p.Equals(glp.PlayerData));
                if (pd == null) {
                    Destroy(glp.gameObject);
                    gameLobbyPlayers.Remove(glp);
                } else {
                    glp.UpdateUI(pd);
                }
            }
            //  Add
            foreach (PlayerData pd in D.G.Gld.Players) {
                if (!gameLobbyPlayers.Exists(p => p.PlayerData.Equals(pd))) {
                    GameLobbyPlayer go = Instantiate(gameLobbyPlayer_Pref, new Vector3(0, 0, 0), Quaternion.identity);
                    if (D.LocalPlayer.Key == pd.Key) {
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
            if (gameLayoutDropdown.value != (int)D.G.Gld.GameMapLayout - 2) {
                gameLayoutDropdown.value = (int)D.G.Gld.GameMapLayout - 2;
            }
            if (D.G.Gld.EasyStart != easyStartToggle.isOn) {
                easyStartToggle.isOn = D.G.Gld.EasyStart;
            }
            if (D.G.Gld.BasicTiles != (int)basicTiles.Value) {
                basicTiles.Value = D.G.Gld.BasicTiles;
            }
            if (D.G.Gld.CoreTiles != (int)coreTiles.Value) {
                coreTiles.Value = D.G.Gld.CoreTiles;
            }
            if (D.G.Gld.CityTiles != (int)cityTiles.Value) {
                cityTiles.Value = D.G.Gld.CityTiles;
            }
            basicTiles.MinValue = D.G.Gld.EasyStart ? D.G.Gld.GameMapLayout == GameMapLayout_Enum.Wedge ? 2 : 3 : 0;
            if (D.G.Gld.DummyPlayer != dummyPlayerToggle.isOn) {
                dummyPlayerToggle.isOn = D.G.Gld.DummyPlayer;
            }
        }


        private void AvatarButtonCallback(Image_Enum avatar_Enum) {
            GameData gdClone = D.G.Clone();
            gdClone.Gld.Players.Find(p => p.Equals(D.LocalPlayer)).Avatar = avatar_Enum;
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
                D.G.HostId = D.LocalPlayer.Key;
                D.G.Players = D.G.Gld.Players;
                D.G.GameStatus = Game_Enum.New_Game;
            }
        }

        private void GameLayoutDropdownCallback(int value) {
            if (D.G.Gld.GameMapLayout != (GameMapLayout_Enum)(value + 2)) {
                D.G.Gld.GameMapLayout = (GameMapLayout_Enum)(value + 2);
                if (D.G.Gld.EasyStart) {
                    if (D.G.Gld.GameMapLayout != GameMapLayout_Enum.Wedge) {
                        if (D.G.Gld.BasicTiles < 3) {
                            D.G.Gld.BasicTiles = 3;
                        }
                    }
                }
                D.C.Send_GameData();
            }
        }
        private void EasyStartToggleCallback(bool value) {
            if (D.G.Gld.EasyStart != value) {
                D.G.Gld.EasyStart = value;
                if (D.G.Gld.EasyStart) {
                    if (D.G.Gld.GameMapLayout == GameMapLayout_Enum.Wedge) {
                        if (D.G.Gld.BasicTiles < 2) {
                            D.G.Gld.BasicTiles = 2;
                        }
                    } else {
                        if (D.G.Gld.BasicTiles < 3) {
                            D.G.Gld.BasicTiles = 3;
                        }
                    }
                }
                D.C.Send_GameData();
            }
        }

        private void BasicTileSliderCallback(float value) {
            if (D.G.Gld.BasicTiles != (int)value) {
                D.G.Gld.BasicTiles = (int)value;
                D.C.Send_GameData();
            }
        }
        private void CoreTileSliderCallback(float value) {
            if (D.G.Gld.CoreTiles != (int)value) {
                D.G.Gld.CoreTiles = (int)value;
                D.C.Send_GameData();
            }
        }
        private void CityTileSliderCallback(float value) {
            if (D.G.Gld.CityTiles != (int)value) {
                D.G.Gld.CityTiles = (int)value;
                D.C.Send_GameData();
            }
        }

        private void DummyPlayerToggleCallback(bool value) {
            if (D.G.Gld.DummyPlayer != value) {
                D.G.Gld.DummyPlayer = value;
                D.C.Send_GameData();
            }
        }
        private void RoundSliderCallback(float value) {
            if (D.G.Gld.Rounds != (int)value) {
                D.G.Gld.Rounds = (int)value;
                D.C.Send_GameData();
            }
        }
    }
}
