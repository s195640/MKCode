using System.Collections.Generic;
using UnityEngine;

namespace cna {
    public class MapFullx3 : ScenarioBase {
        protected override void setupLocationMap() {
            LocationMap = new Dictionary<int, Vector3Int>();
            LocationMap.Add(0, new Vector3Int(0, 0, 0));

            LocationMap.Add(1, new Vector3Int(2, -1, 0));
            LocationMap.Add(2, new Vector3Int(2, 2, 0));
            LocationMap.Add(3, new Vector3Int(-1, 3, 0));

            LocationMap.Add(6, new Vector3Int(1, 5, 0));
            LocationMap.Add(5, new Vector3Int(4, 4, 0));
            LocationMap.Add(4, new Vector3Int(4, 1, 0));

            LocationMap.Add(9, new Vector3Int(3, 7, 0));
            LocationMap.Add(8, new Vector3Int(6, 6, 0));
            LocationMap.Add(7, new Vector3Int(6, 3, 0));

            LocationMap.Add(12, new Vector3Int(5, 9, 0));
            LocationMap.Add(11, new Vector3Int(8, 8, 0));
            LocationMap.Add(10, new Vector3Int(8, 5, 0));

            LocationMap.Add(15, new Vector3Int(7, 11, 0));
            LocationMap.Add(14, new Vector3Int(10, 10, 0));
            LocationMap.Add(13, new Vector3Int(10, 7, 0));

            LocationMap.Add(18, new Vector3Int(9, 13, 0));
            LocationMap.Add(17, new Vector3Int(12, 12, 0));
            LocationMap.Add(16, new Vector3Int(12, 9, 0));

            LocationMap.Add(21, new Vector3Int(11, 15, 0));
            LocationMap.Add(20, new Vector3Int(14, 14, 0));
            LocationMap.Add(19, new Vector3Int(14, 11, 0));

            LocationMap.Add(24, new Vector3Int(13, 17, 0));
            LocationMap.Add(23, new Vector3Int(16, 16, 0));
            LocationMap.Add(22, new Vector3Int(16, 13, 0));

            LocationMap.Add(27, new Vector3Int(15, 19, 0));
            LocationMap.Add(26, new Vector3Int(18, 18, 0));
            LocationMap.Add(25, new Vector3Int(18, 15, 0));

            LocationMap.Add(30, new Vector3Int(17, 21, 0));
            LocationMap.Add(29, new Vector3Int(20, 20, 0));
            LocationMap.Add(28, new Vector3Int(20, 17, 0));

            LocationMap.Add(33, new Vector3Int(19, 23, 0));
            LocationMap.Add(32, new Vector3Int(22, 22, 0));
            LocationMap.Add(31, new Vector3Int(22, 19, 0));
            maxBoardSize = LocationMap.Count;
        }
        protected override void setupAdjBoard() {
            AdjBoard = new Dictionary<int, List<int>>();
            AdjBoard.Add(0, new List<int>() { 1, 2, 3 });
            AdjBoard.Add(1, new List<int>() { 0, 2, 4 });
            AdjBoard.Add(2, new List<int>() { 0, 1, 3, 4, 5, 6 });
            AdjBoard.Add(3, new List<int>() { 0, 2, 6 });

            for (int index = 4; index < 100;) {
                for (int i = 0; i < 3; i++) {
                    switch (i) {
                        case 0: {
                            int a1 = index - 3;
                            int a2 = index - 2;
                            int a3 = index + 1;
                            int a4 = index + 3;
                            AdjBoard.Add(index, new List<int>() { a1, a2, a3, a4 });
                            break;
                        }
                        case 1: {
                            int a1 = index - 3;
                            int a2 = index - 1;
                            int a3 = index + 1;
                            int a4 = index + 2;
                            int a5 = index + 3;
                            int a6 = index + 4;
                            AdjBoard.Add(index, new List<int>() { a1, a2, a3, a4, a5, a6 });
                            break;
                        }
                        case 2: {
                            int a1 = index - 4;
                            int a2 = index - 3;
                            int a3 = index - 1;
                            int a4 = index + 3;
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
