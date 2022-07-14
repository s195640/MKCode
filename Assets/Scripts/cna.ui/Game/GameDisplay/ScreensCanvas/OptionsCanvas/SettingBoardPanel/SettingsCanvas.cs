using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using cna.connector;
using cna.poo;
using TMPro;
using UnityEngine;

namespace cna.ui {
    public class SettingsCanvas : BaseCanvas {

        private SortedDictionary<long, List<LoadGameVO>> games = new SortedDictionary<long, List<LoadGameVO>>();


        [SerializeField] private Transform savedGameContent;
        [SerializeField] private SavedGamePrefab prefab;
        private List<SavedGamePrefab> savedGameList = new List<SavedGamePrefab>();
        private SavedGamePrefab selectedGame;

        [SerializeField] private Transform bookMarkContent;
        [SerializeField] private BookmarkGamePrefab prefab2;
        private List<BookmarkGamePrefab> bookmarkList = new List<BookmarkGamePrefab>();
        private BookmarkGamePrefab selectedBookmark;

        [SerializeField] private CNA_Button loadGameButton;
        [SerializeField] private TextMeshProUGUI gameId;

        public void UpdateUI() {
            gameId.text = "Game Id - " + D.G.GameId;
        }

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
                    loadGameButton.Active = true;
                    selectedBookmark.Selected(true);
                } else {
                    loadGameButton.Active = false;
                }
            }
        }

        public void OnClick_LoadGameList() {
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

        public void OnClick_CreateNewGame() {
            SelectedGame = null;
            PlayerData dummy = D.G.Players.Find(p => p.DummyPlayer);
            if (dummy != null) {
                D.G.Players.Remove(dummy);
            }
            D.GLD.GameStartTime = DateTime.Now.Ticks;
            D.GLD.GameId = Guid.NewGuid().ToString();
            D.G.GameStatus = Game_Enum.CHAR_CREATION;
            D.C.Send_GameData();
        }

        public void OnClick_LoadGame() {
            D.G = SaveLoadUtil.LoadGame(selectedBookmark.FileName);
            SelectedGame = null;
            D.C.Send_GameData();
        }
    }

}
