using System.Collections.Generic;
using UnityEngine;

namespace cna {
    public class MapFullx5 : ScenarioBase {
        protected override void setupLocationMap() {
            LocationMap = new Dictionary<int, Vector3Int>();
            LocationMap.Add(0, new Vector3Int(0, 0, 0));

            LocationMap.Add(1, new Vector3Int(2, -1, 0));
            LocationMap.Add(2, new Vector3Int(2, 2, 0));
            LocationMap.Add(3, new Vector3Int(-1, 3, 0));

            LocationMap.Add(8, new Vector3Int(-1, 6, 0));
            LocationMap.Add(7, new Vector3Int(1, 5, 0));
            LocationMap.Add(6, new Vector3Int(4, 4, 0));
            LocationMap.Add(5, new Vector3Int(4, 1, 0));
            LocationMap.Add(4, new Vector3Int(5, -2, 0));

            int row = 4;
            int count = 0;
            int x = 7;
            int y = 0;
            int addtoy = 0;
            for (int i = 9; i < 49; i++) {
                if (count == 5) {
                    row++;
                    count = 0;
                    x = row * 2 - 1;
                    addtoy += 2;
                    y = addtoy;
                }
                switch (count) {
                    case 0: { break; }
                    case 1: { x--; y += 3; break; }
                    case 2: { y += 3; break; }
                    case 3: { x -= 3; y += 1; break; }
                    case 4: { x -= 2; y += 1; break; }
                }
                LocationMap.Add(i, new Vector3Int(x, y, 0));
                count++;
            }

            //LocationMap.Add(13, new Vector3Int(1, 8, 0));
            //LocationMap.Add(12, new Vector3Int(3, 7, 0));
            //LocationMap.Add(11, new Vector3Int(6, 6, 0));
            //LocationMap.Add(10, new Vector3Int(6, 3, 0));
            //LocationMap.Add(9, new Vector3Int(7, 0, 0));

            //LocationMap.Add(18, new Vector3Int(3, 10, 0));
            //LocationMap.Add(17, new Vector3Int(5, 9, 0));
            //LocationMap.Add(16, new Vector3Int(8, 8, 0));
            //LocationMap.Add(15, new Vector3Int(8, 5, 0));
            //LocationMap.Add(14, new Vector3Int(9, 2, 0));

            //LocationMap.Add(23, new Vector3Int(5, 12, 0));
            //LocationMap.Add(22, new Vector3Int(7, 11, 0));
            //LocationMap.Add(21, new Vector3Int(10, 10, 0));
            //LocationMap.Add(20, new Vector3Int(10, 7, 0));
            //LocationMap.Add(19, new Vector3Int(11, 4, 0));

            //LocationMap.Add(28, new Vector3Int(7, 14, 0));
            //LocationMap.Add(27, new Vector3Int(9, 13, 0));
            //LocationMap.Add(26, new Vector3Int(12, 12, 0));
            //LocationMap.Add(25, new Vector3Int(12, 9, 0));
            //LocationMap.Add(24, new Vector3Int(13, 6, 0));

            //LocationMap.Add(33, new Vector3Int(9, 16, 0));
            //LocationMap.Add(32, new Vector3Int(11, 15, 0));
            //LocationMap.Add(31, new Vector3Int(14, 14, 0));
            //LocationMap.Add(30, new Vector3Int(14, 11, 0));
            //LocationMap.Add(29, new Vector3Int(15, 8, 0));
            maxBoardSize = LocationMap.Count;
        }
        protected override void setupAdjBoard() {
            AdjBoard = new Dictionary<int, List<int>>();
            AdjBoard.Add(0, new List<int>() { 1, 2, 3 });
            AdjBoard.Add(1, new List<int>() { 0, 2, 4, 5 });
            AdjBoard.Add(2, new List<int>() { 0, 1, 3, 5, 6, 7 });
            AdjBoard.Add(3, new List<int>() { 0, 2, 7, 8 });
            AdjBoard.Add(4, new List<int>() { 1, 5, 9 });
            AdjBoard.Add(5, new List<int>() { 1, 2, 4, 6, 9, 10 });
            AdjBoard.Add(6, new List<int>() { 2, 5, 7, 10, 11, 12 });
            AdjBoard.Add(7, new List<int>() { 2, 3, 6, 8, 12, 13 });
            AdjBoard.Add(8, new List<int>() { 3, 7, 13 });
            for (int index = 9; index < 100;) {
                for (int i = 0; i < 5; i++) {
                    switch (i) {
                        case 0: {
                            int a1 = index - 5;
                            int a2 = index - 4;
                            int a3 = index + 1;
                            int a4 = index + 5;
                            AdjBoard.Add(index, new List<int>() { a1, a2, a3, a4 });
                            break;
                        }
                        case 1: {
                            int a1 = index - 5;
                            int a2 = index - 4;
                            int a3 = index - 1;
                            int a4 = index + 1;
                            int a5 = index + 4;
                            int a6 = index + 5;
                            AdjBoard.Add(index, new List<int>() { a1, a2, a3, a4, a5, a6 });
                            break;
                        }
                        case 2: {
                            int a1 = index - 5;
                            int a2 = index - 1;
                            int a3 = index + 1;
                            int a4 = index + 4;
                            int a5 = index + 5;
                            int a6 = index + 6;
                            AdjBoard.Add(index, new List<int>() { a1, a2, a3, a4, a5, a6 });
                            break;
                        }
                        case 3: {
                            int a1 = index - 6;
                            int a2 = index - 5;
                            int a3 = index - 1;
                            int a4 = index + 1;
                            int a5 = index + 5;
                            int a6 = index + 6;
                            AdjBoard.Add(index, new List<int>() { a1, a2, a3, a4, a5, a6 });
                            break;
                        }
                        case 4: {
                            int a1 = index - 6;
                            int a2 = index - 5;
                            int a3 = index - 1;
                            int a4 = index + 5;
                            AdjBoard.Add(index, new List<int>() { a1, a2, a3, a4 });
                            break;
                        }
                    }
                    index++;
                }
            }
        }
    }
}
