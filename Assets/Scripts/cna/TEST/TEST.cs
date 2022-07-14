using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using cna.poo;
using UnityEngine;

namespace cna {
    public class TEST : MonoBehaviour {
        void Start() {
            doWork2();
        }



        public void doWork2() {
            //string data = "[login%0%51%0%8%[[=]%[=]%127^132^119^130^133^121^124%126^120^122^131^125^128^129^123^118%%%%5:0%1%476]%0%0%0:0%188^169%[0%[=]%%0:0:0:0:%0:0:0:0:%0:0:0:0:%0:0:0:0:]%0:0%0%0%[0%0%0%0%0%0%0%0%0%0%0%0]%[0%0%0%0%0%0%0%0%0%0%0%0]%2%0%1%0%[1=0]%%[6%1%]^[2%1%]^[5%1%]%[318^323^316^420^431%453^451^450%423^415^416%%11^105^103^107^2^2^2^2^2^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1%[3:-1^2:3^0:4=188^207^169]%3%0%0%0%0%0%0%3%2%3%0]%0%0%2%637931589071844146]^[DUMMY%-999%52%0%17%[[=]%[=]%%134^136^135^138^144^142^145^146^149^147^148%139^137^140^143^141%%%5:0%1%475]%0%0%0:0%%[0%[=]%%0:0:0:0:%0:0:0:0:%0:0:0:0:%0:0:0:0:]%0:0%0%0%[2%1%0%0%0%0%0%0%0%0%0%0]%[0%0%0%0%0%0%0%0%0%0%0%0]%2%0%0%1%[=]%%%[%%%%%[=]%0%0%0%0%0%0%0%0%0%0%0]%0%0%0%0]";
            //string data = "105%[4%2%1%0%0%0%0%0%0%0%1%-999^0%11^105^103^107^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1%[3:-1^2:3^0:4=188^207^169]]%[ba9cdb72-3f37-466f-af60-481b3aa43143%0%1799027665%5%5%3%2%6%5%0%1%]%[login%0%51%0%8%[[=]%[=]%127^132^119^130^133^121^124%126^120^122^131^125^128^129^123^118%%%%5:0%1%476]%0%0%0:0%188^169%[0%[=]%%0:0:0:0:%0:0:0:0:%0:0:0:0:%0:0:0:0:]%0:0%0%0%[0%0%0%0%0%0%0%0%0%0%0%0]%[0%0%0%0%0%0%0%0%0%0%0%0]%2%0%1%0%[1=0]%%[6%1%]^[2%1%]^[5%1%]%[318^323^316^420^431%453^451^450%423^415^416%%11^105^103^107^2^2^2^2^2^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1^1%[3:-1^2:3^0:4=188^207^169]%3%0%0%0%0%0%0%3%2%3%0]%0%0%2%637931589071844146]^[DUMMY%-999%52%0%17%[[=]%[=]%%134^136^135^138^144^142^145^146^149^147^148%139^137^140^143^141%%%5:0%1%475]%0%0%0:0%%[0%[=]%%0:0:0:0:%0:0:0:0:%0:0:0:0:%0:0:0:0:]%0:0%0%0%[2%1%0%0%0%0%0%0%0%0%0%0]%[0%0%0%0%0%0%0%0%0%0%0%0]%2%0%0%1%[=]%%%[%%%%%[=]%0%0%0%0%0%0%0%0%0%0%0]%0%0%0%0]";


            //List<GameEffect_Enum> k;
            //CNASerialize.Dz(data, out k);
            //Debug.Log(k);

            CNAList<GameEffect_Enum> list = new CNAList<GameEffect_Enum>();
            list.Add(GameEffect_Enum.AC_Agility01);
            string data = "[1]";
            list.Deserialize(data);


            //List<string> vals = CNASerialize.DeserizlizeSplit(data);
            //foreach (string v in vals) {
            //    Debug.Log(v);
            //}
        }


        public void doWork() {

            //List<V2IntVO> t = new List<V2IntVO>() { new V2IntVO(-2, 0), new V2IntVO(4, 11), new V2IntVO(-1, 5) };

            //List<bool> t = new List<bool>() { true, false, true };

            //List<int> t = new List<int>() { 1232, 21, 3, 424, 5, -234, 2, -3, 2234, -324 };

            //List<CNAList<int>> t = new List<CNAList<int>>() { new CNAList<int>(new int[] { 0, 12, 2 }), new CNAList<int>(new int[] { 3, -1, 863 }), new CNAList<int>(new int[] { 861, 1, 9 }), };

            //CNAMap<V2IntVO, CNAList<int>> t = new CNAMap<V2IntVO, CNAList<int>>();
            //t.Add(new V2IntVO(-2, 0), new CNAList<int>(new int[] { 0, 12, 2 }));
            //t.Add(new V2IntVO(143, 931), new CNAList<int>(new int[] { }));
            //t.Add(new V2IntVO(1, 7), new CNAList<int>(new int[] { 7, 1 }));
            //t.Add(new V2IntVO(18, -79), new CNAList<int>());
            //t.Add(new V2IntVO(0, 1), new CNAList<int>(new int[] { -12, 1, 9, 345, 96 }));

            Data t = new Data();
            t.GameStatus = Game_Enum.Player_Turn;
            BoardData boardData = new BoardData();
            boardData.EndOfRound = true;
            boardData.PlayerTurnOrder = new List<int>() { 2, -43, 3 };
            boardData.CurrentMap = new List<MapHexId_Enum>() { MapHexId_Enum.Basic_04, MapHexId_Enum.Basic_07 };
            boardData.MonsterData = new CNAMap<V2IntVO, CNAList<int>>();
            boardData.MonsterData.Add(new V2IntVO(-2, 0), new CNAList<int>(new int[] { 0, 12, 2 }));
            boardData.MonsterData.Add(new V2IntVO(143, 931), new CNAList<int>(new int[] { }));
            boardData.MonsterData.Add(new V2IntVO(0, 1), new CNAList<int>(new int[] { -12, 1, 9, 345, 96 }));
            t.Board = boardData;
            GameData gameData = new GameData();
            gameData.GameId = "sadf68ads9f-asdkjfhsdaf86-asd8f7h-asdi8f";
            gameData.HostKey = -323;
            gameData.Seed = 3323234;
            gameData.GameMapLayout = GameMapLayout_Enum.MapFullx5;
            gameData.BasicTiles = 2;
            gameData.CoreTiles = 4;
            gameData.CityTiles = 2;
            gameData.Rounds = 9;
            gameData.Level = 3;
            gameData.EasyStart = true;
            gameData.DummyPlayer = false;
            t.GameData = gameData;
            List<PlayerData> pds = new List<PlayerData>();
            PlayerData pd = new PlayerData("Chris", 100);
            pd.Avatar = Image_Enum.A_MEEPLE_RANDOM;

            pds.Add(pd);
            t.Players = pds;

            //  GenList
            string data01 = CNASerialize.Sz(t);
            t = null;
            CNASerialize.Dz(data01, out t);
            string data02 = CNASerialize.Sz(t);
            Debug.Log(Environment.NewLine + data01 + Environment.NewLine + data02);
        }

        public CNAMap<V2IntVO, CNAList<int>> deserialize(string data) {
            string[] d = data.Split("%");
            List<V2IntVO> k = d[0].Split("^").ToList().ConvertAll(i => new V2IntVO(i));
            List<CNAList<int>> v = d[1].Split("^").ToList().ConvertAll(i => new CNAList<int>(i.Split("*").ToList().ConvertAll(j => Convert.ToInt32(j))));
            CNAMap<V2IntVO, CNAList<int>> forReturn = new CNAMap<V2IntVO, CNAList<int>>(k, v);
            return forReturn;
        }

        public string ToStr2<T>(IList<T> list, string delimiter = "^") {
            string data = "";
            if (typeof(T).IsSubclassOf(typeof(BaseData))) {
                bool flag = false;
                foreach (T i in list) {
                    if (flag) data += delimiter; else flag = true;
                    data += (i as BaseData).Serialize();
                }
            } else {
                data = string.Join(delimiter, list);
            }
            return data;
        }

    }
}
