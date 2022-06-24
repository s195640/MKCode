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
            basicTiles.Setup(0, 8, D.G.GameData.BasicTiles, BasicTileSliderCallback);
            coreTiles.Setup(0, 4, D.G.GameData.CoreTiles, CoreTileSliderCallback);
            cityTiles.Setup(1, 4, D.G.GameData.CityTiles, CityTileSliderCallback);
            rounds.Setup(1, 10, D.G.GameData.Rounds, RoundSliderCallback);
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
            if (D.G.GameData.BasicTiles != (int)basicTiles.Value) {
                basicTiles.Value = D.G.GameData.BasicTiles;
            }
            if (D.G.GameData.CoreTiles != (int)coreTiles.Value) {
                coreTiles.Value = D.G.GameData.CoreTiles;
            }
            if (D.G.GameData.CityTiles != (int)cityTiles.Value) {
                cityTiles.Value = D.G.GameData.CityTiles;
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
    }
}
