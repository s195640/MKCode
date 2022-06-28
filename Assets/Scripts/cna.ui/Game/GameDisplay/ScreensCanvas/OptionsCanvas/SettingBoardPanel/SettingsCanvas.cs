using System;
using System.Collections.Generic;
using System.IO;
using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class SettingsCanvas : BaseCanvas {

        [SerializeField] private Transform savedGameContent;
        [SerializeField] private SavedGamePrefab prefab;
        private List<SavedGamePrefab> savedGameList = new List<SavedGamePrefab>();
        private SavedGamePrefab selectedGame;

        [SerializeField] private Transform bookMarkContent;
        [SerializeField] private BookmarkGamePrefab prefab2;
        private List<BookmarkGamePrefab> bookmarkList = new List<BookmarkGamePrefab>();
        private BookmarkGamePrefab selectedBookmark;

        [SerializeField] private CNA_Button loadGameButton;

        public void UpdateUI() {
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
            SelectedGame = null;
            SelectedBookmark = null;
            DirectoryInfo[] dir = BasicUtil.LoadGameList();
            foreach (DirectoryInfo di in dir) {
                SavedGamePrefab p = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                p.transform.SetParent(savedGameContent);
                p.transform.localScale = Vector3.one;
                p.SetupUI(di, OnClick_SelectGame);
                savedGameList.Add(p);
            }
        }

        public void OnClick_SelectGame(SavedGamePrefab savedGame) {
            SelectedGame = savedGame;
            bookmarkList.ForEach(s => Destroy(s.gameObject));
            bookmarkList.Clear();
            SelectedBookmark = null;
            FileInfo[] files = BasicUtil.LoadGameBookmarks(SelectedGame.GameId);
            foreach (FileInfo fi in files) {
                BookmarkGamePrefab p = Instantiate(prefab2, Vector3.zero, Quaternion.identity);
                p.transform.SetParent(bookMarkContent);
                p.transform.localScale = Vector3.one;
                p.SetupUI(fi, OnClick_SelectBookmark);
                bookmarkList.Add(p);
            }
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
            D.G.GameStatus = Game_Enum.CHAR_CREATION;
            D.C.Send_GameData();
        }

        public void OnClick_LoadGame() {
            D.G = BasicUtil.LoadGameFromFile(selectedBookmark.fullFileName);
            SelectedGame = null;
            D.C.Send_GameData();
        }
    }
}
