using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Linq;
using cna.poo;
using Application = UnityEngine.Application;
using System.Collections.Generic;
using AOT;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace cna.connector {
    public static class SaveLoadUtil {
        public static void SaveGameToFile(Data gd) {
            string version = Application.version;
            string gameStartDate = "" + gd.GameData.GameStartTime;
            string playerList = gd.GameData.HostKey + "~" + string.Join("~", gd.Players.FindAll(p => !p.DummyPlayer).ConvertAll(p => p.Name));
            string turnCounter = "" + gd.Board.TurnCounter;
            string fileName = string.Format("cna_v{0}_{1}_{2}_{3}", version, gameStartDate, playerList, turnCounter.PadLeft(4, '0'));
            string json = gd.ToDataStr();
            WriteData(fileName, json);
        }

        #region LoadGame

        public static Data LoadGame(string gameName) {
            string data;
#if UNITY_WEBGL && !UNITY_EDITOR
            data = LoadGame_WebGl(gameName);
#else
            data = LoadGame_Else(gameName);
#endif
            Data gd;
            string unzip = CNASerialize.Unzip(data);
            CNASerialize.Dz(unzip, out gd);
            return gd;
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")] private static extern string LoadDataJs(string gameName);
        private static string LoadGame_WebGl(string gameName) {
            string data = LoadDataJs(gameName);
            return data;
        }
#endif

        private static string LoadGame_Else(string gameName) {
            string filePath = GetFilePath("SavedGames", gameName);
            string data = File.ReadAllText(filePath);
            return data;
        }

        #endregion

        #region ReadData
        public static List<string> LoadGameNames() {
#if UNITY_WEBGL && !UNITY_EDITOR
            return LoadGameNames_WebGl();
#else
            return LoadGameNames_Else();
#endif
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")] private static extern string LoadGameNamesJs();
        public static List<string> LoadGameNames_WebGl() {
            List<string> games = new List<string>();
            string gameNames = LoadGameNamesJs();
            foreach (string s in gameNames.Split(";")) {
                if (s.Length > 0) {
                    games.Add(s.Substring(5));
                }
            }
            return games;
        }
#endif

        public static List<string> LoadGameNames_Else() {
            string dirPath = GetFilePath("SavedGames");
            DirectoryInfo info = new DirectoryInfo(dirPath);
            FileInfo[] files = info.GetFiles("*.gd").OrderByDescending(p => p.Name).ToArray();
            List<string> fileNames = new List<string>();
            foreach (FileInfo i in files) {
                if (i.Name.StartsWith("cna_v")) {
                    fileNames.Add(i.Name.Substring(5).Replace(".gd", ""));
                }
            }
            return fileNames;
        }
        #endregion

        #region WriteData

        private static void WriteData(string name, string json) {
            //Debug.Log("WriteData()");
#if UNITY_WEBGL && !UNITY_EDITOR
            WriteData_WebGl(name, json);
#else
            WriteData_Else(name, json);
#endif
        }

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")] private static extern void WriteDataJs(string name, string value, string exp_days);
        private static void WriteData_WebGl(string name, string json) {
            Debug.Log("WriteData_WebGl(name = " + name + ")");
            WriteDataJs(name, json, "5");
        }
#endif

        private static void WriteData_Else(string name, string json) {
            //Debug.Log("WriteData_Else(name = " + name + ")");
            string dirPath = GetFilePath("SavedGames", name);
            byte[] byteData;

            byteData = Encoding.ASCII.GetBytes(json);
            if (!Directory.Exists(Path.GetDirectoryName(dirPath))) {
                Directory.CreateDirectory(Path.GetDirectoryName(dirPath));
            }
            File.WriteAllBytes(dirPath, byteData);
        }

        private static string GetFilePath(string FolderName, string FileName = "") {
            string filePath = Path.Combine(Application.persistentDataPath, ("data/" + FolderName));
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        filePath = Path.Combine(Application.streamingAssetsPath, ("data/" + FolderName));
#endif
            if (FileName != "") {
                filePath = Path.Combine(filePath, (FileName + ".gd"));
            }
            return filePath;
        }

        #endregion

    }
}
