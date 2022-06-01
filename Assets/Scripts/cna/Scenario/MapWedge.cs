using System.Collections.Generic;
using UnityEngine;

namespace cna {
    /*
           [10] [11] [12] [13] [14]
              [6]  [7]  [8]  [9]
                 [3]  [4]  [5]
                   [1]  [2]
                      [0]
    */
    public class MapWedge : ScenarioBase {


        protected override void setupLocationMap() {
            LocationMap = new Dictionary<int, Vector3Int>();
            LocationMap.Add(0, new Vector3Int(0, 0, 0));
            int numInRow = 2;
            int currentRow = 2;
            int count = 0;
            int x = -1;
            bool alt = true;
            int y = 3;
            int ymod = -1;
            for (int i = 1; i < 91; i++) {
                if (count == numInRow) {
                    numInRow++;
                    count = 0;
                    currentRow++;
                    x = currentRow / 2 * -1;
                    ymod++;
                    y = 2 * currentRow + ymod;
                    alt = currentRow % 2 == 0;
                }
                if (count != 0) {
                    if (alt) {
                        x += 3;
                    } else {
                        x += 2;
                    }
                    alt = !alt;
                    y--;
                }
                LocationMap.Add(i, new Vector3Int(x, y, 0));
                count++;
            }




            maxBoardSize = LocationMap.Count;
        }

        //protected void setupLocationMap1() {
        //    gameMapLayout = poo.GameMapLayout_Enum.Wedge;
        //    LocationMap = new Dictionary<int, Vector3Int>();
        //    LocationMap.Add(0, new Vector3Int(0, 0, 0));

        //    LocationMap.Add(1, new Vector3Int(-1, 3, 0));
        //    LocationMap.Add(2, new Vector3Int(2, 2, 0));

        //    LocationMap.Add(3, new Vector3Int(-1, 6, 0));
        //    LocationMap.Add(4, new Vector3Int(1, 5, 0));
        //    LocationMap.Add(5, new Vector3Int(4, 4, 0));

        //    LocationMap.Add(6, new Vector3Int(-2, 9, 0));
        //    LocationMap.Add(7, new Vector3Int(1, 8, 0));
        //    LocationMap.Add(8, new Vector3Int(3, 7, 0));
        //    LocationMap.Add(9, new Vector3Int(6, 6, 0));

        //    LocationMap.Add(10, new Vector3Int(-2, 12, 0));
        //    LocationMap.Add(11, new Vector3Int(0, 11, 0));
        //    LocationMap.Add(12, new Vector3Int(3, 10, 0));
        //    LocationMap.Add(13, new Vector3Int(5, 9, 0));
        //    LocationMap.Add(14, new Vector3Int(8, 8, 0));

        //    LocationMap.Add(15, new Vector3Int(-3, 15, 0));
        //    LocationMap.Add(16, new Vector3Int(0, 14, 0));
        //    LocationMap.Add(17, new Vector3Int(2, 13, 0));
        //    LocationMap.Add(18, new Vector3Int(5, 12, 0));
        //    LocationMap.Add(19, new Vector3Int(7, 11, 0));
        //    LocationMap.Add(20, new Vector3Int(10, 10, 0));

        //    LocationMap.Add(21, new Vector3Int(-3, 18, 0));
        //    LocationMap.Add(22, new Vector3Int(-1, 17, 0));
        //    LocationMap.Add(23, new Vector3Int(2, 16, 0));
        //    LocationMap.Add(24, new Vector3Int(4, 15, 0));
        //    LocationMap.Add(25, new Vector3Int(7, 14, 0));
        //    LocationMap.Add(26, new Vector3Int(9, 13, 0));
        //    LocationMap.Add(27, new Vector3Int(12, 12, 0));

        //    LocationMap.Add(28, new Vector3Int(-4, 21, 0));
        //    LocationMap.Add(29, new Vector3Int(-1, 20, 0));
        //    LocationMap.Add(30, new Vector3Int(1, 19, 0));
        //    LocationMap.Add(31, new Vector3Int(4, 18, 0));
        //    LocationMap.Add(32, new Vector3Int(6, 17, 0));
        //    LocationMap.Add(33, new Vector3Int(9, 16, 0));
        //    LocationMap.Add(34, new Vector3Int(11, 15, 0));
        //    LocationMap.Add(35, new Vector3Int(14, 14, 0));

        //    LocationMap.Add(36, new Vector3Int(-4, 24, 0));
        //    LocationMap.Add(37, new Vector3Int(-2, 23, 0));
        //    LocationMap.Add(38, new Vector3Int(1, 22, 0));
        //    LocationMap.Add(39, new Vector3Int(3, 21, 0));
        //    LocationMap.Add(40, new Vector3Int(6, 20, 0));
        //    LocationMap.Add(41, new Vector3Int(8, 19, 0));
        //    LocationMap.Add(42, new Vector3Int(11, 18, 0));
        //    LocationMap.Add(43, new Vector3Int(13, 17, 0));
        //    LocationMap.Add(44, new Vector3Int(16, 16, 0));

        //    maxBoardSize = LocationMap.Count;
        //}

        protected override void setupAdjBoard() {
            AdjBoard = new Dictionary<int, List<int>>();
            AdjBoard.Add(0, new List<int>() { 1, 2 });
            int numInRow = 2;
            int currentRow = 2;
            int count = 0;
            for (int i = 1; i < maxBoardSize; i++) {
                if (count == numInRow) {
                    numInRow++;
                    count = 0;
                    currentRow++;
                }
                if (count == 0) {
                    AdjBoard.Add(i, new List<int>() { (i - currentRow + 1), (i + 1), (i + currentRow), (i + currentRow + 1) });
                } else if (count + 1 == numInRow) {
                    AdjBoard.Add(i, new List<int>() { (i - currentRow), (i - 1), (i + currentRow), (i + currentRow + 1) });
                } else {
                    AdjBoard.Add(i, new List<int>() { (i - currentRow), (i - currentRow + 1), (i - 1), (i + 1), (i + currentRow), (i + currentRow + 1) });
                }
                count++;
            }
        }



        //protected void setupAdjBoard1() {
        //    AdjBoard = new Dictionary<int, List<int>>();
        //    AdjBoard.Add(0, new List<int>() { 1, 2 });
        //    AdjBoard.Add(1, new List<int>() { 0, 2, 3, 4 });
        //    AdjBoard.Add(2, new List<int>() { 0, 1, 4, 5 });
        //    AdjBoard.Add(3, new List<int>() { 1, 4, 6, 7 });
        //    AdjBoard.Add(4, new List<int>() { 1, 2, 3, 5, 7, 8 });
        //    AdjBoard.Add(5, new List<int>() { 2, 4, 8, 9 });
        //    AdjBoard.Add(6, new List<int>() { 3, 7, 10, 11 });
        //    AdjBoard.Add(7, new List<int>() { 3, 4, 6, 8, 11, 12 });
        //    AdjBoard.Add(8, new List<int>() { 4, 5, 7, 9, 12, 13 });
        //    AdjBoard.Add(9, new List<int>() { 5, 8, 13, 14 });
        //    AdjBoard.Add(10, new List<int>() { 6, 11, 15, 16 });
        //    AdjBoard.Add(11, new List<int>() { 6, 7, 10, 12, 16, 17 });
        //    AdjBoard.Add(12, new List<int>() { 7, 8, 11, 13, 17, 18 });
        //    AdjBoard.Add(13, new List<int>() { 8, 9, 12, 14, 18, 19 });
        //    AdjBoard.Add(14, new List<int>() { 9, 13, 19, 20 });
        //    AdjBoard.Add(15, new List<int>() { 10, 16, 21, 22 });
        //    AdjBoard.Add(16, new List<int>() { 10, 11, 15, 17, 22, 23 });
        //    AdjBoard.Add(17, new List<int>() { 11, 12, 16, 18, 23, 24 });
        //    AdjBoard.Add(18, new List<int>() { 12, 13, 17, 19, 24, 25 });
        //    AdjBoard.Add(19, new List<int>() { 13, 14, 18, 20, 25, 26 });
        //    AdjBoard.Add(20, new List<int>() { 14, 19, 26, 27 });
        //    AdjBoard.Add(21, new List<int>() { 15, 22, 28, 29 });
        //    AdjBoard.Add(22, new List<int>() { 15, 16, 21, 23, 29, 30 });
        //    AdjBoard.Add(23, new List<int>() { 16, 17, 22, 24, 30, 31 });
        //    AdjBoard.Add(24, new List<int>() { 17, 18, 23, 25, 31, 32 });
        //    AdjBoard.Add(25, new List<int>() { 18, 19, 24, 26, 32, 33 });
        //    AdjBoard.Add(26, new List<int>() { 19, 20, 25, 27, 33, 34 });
        //    AdjBoard.Add(27, new List<int>() { 20, 26, 34, 35 });
        //    AdjBoard.Add(28, new List<int>() { 21, 29, 36, 37 });
        //    AdjBoard.Add(29, new List<int>() { 21, 22, 28, 30, 37, 38 });
        //    AdjBoard.Add(30, new List<int>() { 22, 23, 29, 31, 38, 39 });
        //    AdjBoard.Add(31, new List<int>() { 23, 24, 30, 32, 39, 40 });
        //    AdjBoard.Add(32, new List<int>() { 24, 25, 31, 33, 40, 41 });
        //    AdjBoard.Add(33, new List<int>() { 25, 26, 32, 34, 41, 42 });
        //    AdjBoard.Add(34, new List<int>() { 26, 27, 33, 35, 42, 43 });
        //    AdjBoard.Add(35, new List<int>() { 27, 34, 43, 44 });
        //    //  LAST ROW
        //    AdjBoard.Add(36, new List<int>() { 28, 37, 45, 46 });
        //    AdjBoard.Add(37, new List<int>() { 28, 29, 36, 38, 46, 47, 48 });
        //    AdjBoard.Add(38, new List<int>() { 29, 30, 37, 39, 47, 48, 49 });
        //    AdjBoard.Add(39, new List<int>() { 30, 31, 38, 40, 48, 49, 50 });
        //    AdjBoard.Add(40, new List<int>() { 31, 32, 39, 41, 49, 50, 51 });
        //    AdjBoard.Add(41, new List<int>() { 32, 33, 40, 42, 50, 51, 52 });
        //    AdjBoard.Add(42, new List<int>() { 33, 34, 41, 43, 51, 52, 53 });
        //    AdjBoard.Add(43, new List<int>() { 34, 35, 42, 44, 52, 53, 54 });
        //    AdjBoard.Add(44, new List<int>() { 35, 43, 54, 55 });
        //}
    }
}
