
using System;
using System.Collections.Generic;

namespace cna.ui {
    public class LoadGameVO {
        public string verison = "0";
        public long time = 0;
        public string hostKey = "0";
        public string turn = "0";
        private List<string> players = new List<string>();
        public string fileName = "";

        public string getDescriptionName() {
            DateTime dt = new DateTime(time);
            string dtStr = dt.ToString("MM-dd HH:mm:ss");
            return dtStr + " " + string.Join(",", players);
        }

        public LoadGameVO(string fileName) {
            this.fileName = "cna_v" + fileName;
            if (fileName.Length > 0) {
                string[] f = fileName.Split("_");
                if (f.Length == 4) {
                    string ps = f[2];
                    if (ps.Length > 0) {
                        string[] p = ps.Split("~");
                        if (p.Length > 0) {
                            verison = f[0];
                            time = long.Parse(f[1]);
                            turn = f[3];
                            hostKey = p[0];
                            for (int i = 1; i < p.Length; i++) {
                                players.Add(p[i]);
                            }
                        }
                    }
                }
            }
        }
    }
}
