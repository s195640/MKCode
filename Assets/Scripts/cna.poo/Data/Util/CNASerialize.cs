using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

namespace cna.poo {
    public static class CNASerialize {

        #region Serialize
        public static string Sz(List<bool> list, string delimiter = "^") {
            string data = "";
            bool flag = false;
            foreach (bool i in list) {
                if (flag) data += delimiter; else flag = true;
                data += i == true ? "1" : "0";
            }
            return "[" + data + "]";
        }
        public static string Sz(List<MapHexId_Enum> list, string delimiter = "^") {
            string data = "";
            bool flag = false;
            foreach (MapHexId_Enum i in list) {
                if (flag) data += delimiter; else flag = true;
                data += (int)i;
            }
            return "[" + data + "]";
        }

        public static string Sz(List<int> list, string delimiter = "^") {
            string data = string.Join(delimiter, list);
            return "[" + data + "]";
        }
        public static string Sz(List<string> list, string delimiter = "^") {
            string data = string.Join(delimiter, list);
            return "[" + data + "]";
        }
        public static string Sz<T>(List<T> list, string delimiter = "^") where T : BaseData {
            string data = "";
            bool flag = false;
            foreach (T i in list) {
                if (flag) data += delimiter; else flag = true;
                data += i.Serialize();
            }
            return "[" + data + "]";
        }

        public static string Sz<T>(T item) where T : BaseData {
            return item.Serialize();
        }
        public static string Sz(Game_Enum item) {
            return "" + (int)item;
        }
        public static string Sz(GameMapLayout_Enum item) {
            return "" + (int)item;
        }
        public static string Sz(Image_Enum item) {
            return "" + (int)item;
        }
        public static string Sz(TurnPhase_Enum item) {
            return "" + (int)item;
        }
        public static string Sz(mType_Enum item) {
            return "" + (int)item;
        }
        public static string Sz(BattlePhase_Enum item) {
            return "" + (int)item;
        }
        public static string Sz(Crystal_Enum item) {
            return "" + (int)item;
        }
        public static string Sz(ManaPool_Enum item) {
            return "" + (int)item;
        }
        public static string Sz(int item) {
            return "" + item;
        }
        public static string Sz(long item) {
            return "" + item;
        }
        public static string Sz(bool item) {
            return item ? "1" : "0";
        }
        public static string Sz(string item) {
            return item;
        }
        public static string Sz(byte[] item) {
            return Encoding.ASCII.GetString(item);
        }
        #endregion

        #region Deserialize

        public static void Dz(string data, out List<bool> list, string delimiter = "^") {
            list = Split(data, delimiter).ConvertAll(i => i.Equals("1") ? true : false);
        }

        public static void Dz(string data, out List<int> list, string delimiter = "^") {
            list = Split(data, delimiter).ConvertAll(i => Convert.ToInt32(i));
        }

        public static void Dz(string data, out List<GameEffect_Enum> list, string delimiter = "^") {
            list = Split(data, delimiter).ConvertAll(i => (GameEffect_Enum)Convert.ToInt32(i));
        }
        public static void Dz(string data, out List<CardState_Enum> list, string delimiter = "^") {
            list = Split(data, delimiter).ConvertAll(i => (CardState_Enum)Convert.ToInt32(i));
        }
        public static void Dz(string data, out List<MapHexId_Enum> list, string delimiter = "^") {
            list = Split(data, delimiter).ConvertAll(i => (MapHexId_Enum)Convert.ToInt32(i));
        }

        public static void Dz<T>(string data, out List<T> list, string delimiter = "^") where T : BaseData, new() {
            list = Split(data, delimiter).ConvertAll(i => {
                T item = new T();
                item.Deserialize(i);
                return item;
            });
        }

        public static void Dz<T>(string data, out T item) where T : BaseData, new() {
            item = new T();
            item.Deserialize(data);
        }
        public static void Dz(string data, out bool item) {
            item = data.Equals("1") ? true : false;
        }
        public static void Dz(string data, out int item) {
            item = Convert.ToInt32(data);
        }
        public static void Dz(string data, out long item) {
            item = Convert.ToInt64(data);
        }
        public static void Dz(string data, out string item) {
            item = data;
        }
        public static void Dz(string data, out Game_Enum item) {
            item = (Game_Enum)Convert.ToInt32(data);
        }
        public static void Dz(string data, out GameMapLayout_Enum item) {
            item = (GameMapLayout_Enum)Convert.ToInt32(data);
        }
        public static void Dz(string data, out Image_Enum item) {
            item = (Image_Enum)Convert.ToInt32(data);
        }
        public static void Dz(string data, out BattlePhase_Enum item) {
            item = (BattlePhase_Enum)Convert.ToInt32(data);
        }
        public static void Dz(string data, out TurnPhase_Enum item) {
            item = (TurnPhase_Enum)Convert.ToInt32(data);
        }
        public static void Dz(string data, out mType_Enum item) {
            item = (mType_Enum)Convert.ToInt32(data);
        }
        public static void Dz(string data, out Crystal_Enum item) {
            item = (Crystal_Enum)Convert.ToInt32(data);
        }
        public static void Dz(string data, out ManaPool_Enum item) {
            item = (ManaPool_Enum)Convert.ToInt32(data);
        }
        public static void Dz(string data, out byte[] item) {
            item = Encoding.ASCII.GetBytes(data);
        }

        public static List<string> Split(string d, string delimiter) {
            string data = d.Substring(1, d.Length - 2);
            if (data.Length == 0) {
                return new List<string>();
            } else {
                //return data.Split(delimiter).ToList();
                return DeserizlizeSplit(data, delimiter);
            }
        }

        public static List<string> DeserizlizeSplit(string data, string delimiter = "%") {
            int first = data.IndexOf('[');
            if (first >= 0) {
                List<string> l = new List<string>();
                //  BEFORE [
                if (first > 0) {
                    l.AddRange(DeserizlizeSplit(data.Substring(0, first - 1), delimiter));
                }
                //  IN []
                int count = 1;
                int i = first + 1;
                while (count > 0) {
                    if (data[i] == ']') {
                        count--;
                    } else if (data[i] == '[') {
                        count++;
                    }
                    i++;
                }
                l.Add(data.Substring(first, i - first));
                //  AFTER ]
                if (data.Length > i) {
                    l.AddRange(DeserizlizeSplit(data.Substring(i + 1), delimiter));
                }
                return l;
            } else {
                return data.Split(delimiter).ToList();
            }
        }
        #endregion

        #region Zip/Unzip

        public static string Zip(string text) {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true)) {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        public static string Unzip(string compressedText) {
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream()) {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress)) {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        #endregion
    }
}
