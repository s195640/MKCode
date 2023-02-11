using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace cna {
    public class UpdateCards : MonoBehaviour {
        private string path = @"C:\Users\carmi\Documents\Unity\MageKnight04\Assets\Scripts\cna\CardEngine\";

        private void Start() {
            //generateBasicCards();
            //generateUnitCards();
            //generateSkillCards();
            generateAdvancedCards();
            //generateSpellCards();
            //generateArtifactCards();
            //List<List<string>> csv = readFile();
            //
            //writeFiles(csv);
            //writeCreate();
        }

        private void generateArtifactCards() {
            deleteGeneratedFilesInFolder(path + @"Artifact\Generated");
            List<List<string>> csv = readFile(@"Artifact\Artifact.csv");
            StringBuilder basicSb = new StringBuilder();
            foreach (List<string> line in csv) {
                List<string> attr = new List<string>();
                string type = line[1];
                string title = line[2];
                string cardImage = "Image_Enum." + line[3];
                string className = title.Replace(" ", "") + "VO";
                string filePath = path + @"Artifact\Generated\" + className + ".cs";
                string action01 = line[4];
                string action02 = line[5];
                string action = "new List<string> { \"" + action01 + "\",\"" + action02 + "\" }";
                string allowed = buildAllowedStr("TurnPhase_Enum", line[6], line[7]);
                string allowedbattle = buildAllowedStr("BattlePhase_Enum", line[8], line[9]);
                attr.Add("uniqueId");
                attr.Add("\"" + title + "\"");
                attr.Add(cardImage);
                attr.Add(action);
                attr.Add(allowed);
                attr.Add(allowedbattle);
                basicSb.AppendLine(string.Format("D.Cards.Add(new {0}(D.Cards.Count));", className));
                using (StreamWriter file = new StreamWriter(filePath, false)) {
                    file.WriteLine(string.Format("using System.Collections.Generic;"));
                    file.WriteLine(string.Format("using cna.poo;"));
                    file.WriteLine(string.Format("namespace cna {{"));
                    file.WriteLine(string.Format("    public partial class {0} : CardArtifactVO {{", className));
                    file.WriteLine(string.Format("        public {0}(int uniqueId) : base(", className));
                    for (int i = 0; i < attr.Count; i++) {
                        string l = "            " + attr[i];
                        if (i < attr.Count - 1) l += ",";
                        file.WriteLine(l);
                    }
                    file.WriteLine(string.Format("            ) {{ }}"));
                    file.WriteLine(string.Format("    }}"));
                    file.WriteLine(string.Format("}}"));
                }
            }
            Debug.Log(basicSb.ToString());
        }


        private void generateSpellCards() {
            deleteGeneratedFilesInFolder(path + @"Spell\Generated");
            List<List<string>> csv = readFile(@"Spell\Spell.csv");
            StringBuilder basicSb = new StringBuilder();
            foreach (List<string> line in csv) {
                List<string> attr = new List<string>();
                string type = line[1];
                string title01 = line[2];
                string title02 = line[3];
                string cardImage = "Image_Enum." + line[4];
                string cardColor = "CardColor_Enum." + line[5];
                string className = title01.Replace(" ", "") + "VO";
                string filePath = path + @"Spell\Generated\" + className + ".cs";
                string action01 = line[6];
                string action02 = line[7];
                string action = "new List<string> { \"" + action01 + "\",\"" + action02 + "\" }";
                string cost = buildAllowedStr("Crystal_Enum", line[8], line[9]);
                string allowed = buildAllowedStr("TurnPhase_Enum", line[10], line[11]);
                string allowedbattle = buildAllowedStr("BattlePhase_Enum", line[12], line[13]);
                attr.Add("uniqueId");
                attr.Add("\"" + title01 + "\"");
                attr.Add("\"" + title02 + "\"");
                attr.Add("CardType_Enum.Spell");
                attr.Add(cardImage);
                attr.Add(action);
                attr.Add(cost);
                attr.Add(allowed);
                attr.Add(allowedbattle);
                attr.Add(cardColor);
                basicSb.AppendLine(string.Format("D.Cards.Add(new {0}(D.Cards.Count));", className));
                using (StreamWriter file = new StreamWriter(filePath, false)) {
                    file.WriteLine(string.Format("using System.Collections.Generic;"));
                    file.WriteLine(string.Format("using cna.poo;"));
                    file.WriteLine(string.Format("namespace cna {{"));
                    file.WriteLine(string.Format("    public partial class {0} : CardSpellVO {{", className));
                    file.WriteLine(string.Format("        public {0}(int uniqueId) : base(", className));
                    for (int i = 0; i < attr.Count; i++) {
                        string l = "            " + attr[i];
                        if (i < attr.Count - 1) l += ",";
                        file.WriteLine(l);
                    }
                    file.WriteLine(string.Format("            ) {{ }}"));
                    file.WriteLine(string.Format("    }}"));
                    file.WriteLine(string.Format("}}"));
                }
            }
            Debug.Log(basicSb.ToString());
        }



        private void generateAdvancedCards() {
            deleteGeneratedFilesInFolder(path + @"Advanced\Generated");
            List<List<string>> csv = readFile(@"Advanced\Advanced.csv");
            StringBuilder basicSb = new StringBuilder();
            foreach (List<string> line in csv) {
                List<string> attr = new List<string>();
                string type = line[1];
                string title = line[2];
                string cardImage = "Image_Enum." + line[3];
                string cardColor = "CardColor_Enum." + line[4];
                string className = title.Replace(" ", "") + "VO";
                string filePath = path + @"Advanced\Generated\" + className + ".cs";
                string action01 = line[5];
                string action02 = line[6];
                string action = "new List<string> { \"" + action01 + "\",\"" + action02 + "\" }";
                string cost = buildAllowedStr("Crystal_Enum", line[7], line[8]);
                string allowed = buildAllowedStr("TurnPhase_Enum", line[9], line[10]);
                string allowedbattle = buildAllowedStr("BattlePhase_Enum", line[11], line[12]);
                attr.Add("uniqueId");
                attr.Add("\"" + title + "\"");
                attr.Add(cardImage);
                attr.Add("CardType_Enum.Advanced");
                attr.Add(action);
                attr.Add(cost);
                attr.Add(allowed);
                attr.Add(allowedbattle);
                attr.Add(cardColor);
                basicSb.AppendLine(string.Format("D.Cards.Add(new {0}(D.Cards.Count));", className));
                using (StreamWriter file = new StreamWriter(filePath, false)) {
                    file.WriteLine(string.Format("using System.Collections.Generic;"));
                    file.WriteLine(string.Format("using cna.poo;"));
                    file.WriteLine(string.Format("namespace cna {{"));
                    file.WriteLine(string.Format("    public partial class {0} : CardActionVO {{", className));
                    file.WriteLine(string.Format("        public {0}(int uniqueId) : base(", className));
                    for (int i = 0; i < attr.Count; i++) {
                        string l = "            " + attr[i];
                        if (i < attr.Count - 1) l += ",";
                        file.WriteLine(l);
                    }
                    file.WriteLine(string.Format("            ) {{ }}"));
                    file.WriteLine(string.Format("    }}"));
                    file.WriteLine(string.Format("}}"));
                }
            }
            Debug.Log(basicSb.ToString());
        }

        private void generateBasicCards() {
            deleteGeneratedFilesInFolder(path + @"Basic\Generated");
            List<List<string>> csv = readFile(@"Basic\Basic.csv");
            StringBuilder basicSb = new StringBuilder();
            foreach (List<string> line in csv) {
                List<string> attr = new List<string>();
                string type = line[1];
                string title = line[2];
                string cardImage = "Image_Enum." + line[3];
                string cardColor = "CardColor_Enum." + line[4];
                string className = title.Replace(" ", "") + "VO";
                string filePath = path + @"Basic\Generated\" + className + ".cs";
                string action01 = line[5];
                string action02 = line[6];
                string action = "new List<string> { \"" + action01 + "\",\"" + action02 + "\" }";
                string cost = buildAllowedStr("Crystal_Enum", line[7], line[8]);
                string allowed = buildAllowedStr("TurnPhase_Enum", line[9], line[10]);
                string allowedbattle = buildAllowedStr("BattlePhase_Enum", line[11], line[12]);
                attr.Add("uniqueId");
                attr.Add("\"" + title + "\"");
                attr.Add(cardImage);
                attr.Add("CardType_Enum.Basic");
                attr.Add(action);
                attr.Add(cost);
                attr.Add(allowed);
                attr.Add(allowedbattle);
                attr.Add(cardColor);
                basicSb.AppendLine(string.Format("D.Cards.Add(new {0}(D.Cards.Count, avatar));", className));
                using (StreamWriter file = new StreamWriter(filePath, false)) {
                    file.WriteLine(string.Format("using System.Collections.Generic;"));
                    file.WriteLine(string.Format("using cna.poo;"));
                    file.WriteLine(string.Format("namespace cna {{"));
                    file.WriteLine(string.Format("    public partial class {0} : CardActionVO {{", className));
                    file.WriteLine(string.Format("        public {0}(int uniqueId, Image_Enum avatar) : base(", className));
                    for (int i = 0; i < attr.Count; i++) {
                        string l = "            " + attr[i];
                        if (i < attr.Count - 1) l += ",";
                        file.WriteLine(l);
                    }
                    file.WriteLine(string.Format("            ) {{ Avatar = avatar; }}"));
                    file.WriteLine(string.Format("    }}"));
                    file.WriteLine(string.Format("}}"));
                }
            }
            Debug.Log(basicSb.ToString());
        }
        private void generateUnitCards() {
            deleteGeneratedFilesInFolder(path + @"Unit\Generated");
            List<List<string>> csv = readFile(@"Unit\Unit.csv");
            StringBuilder sb = new StringBuilder();
            foreach (List<string> line in csv) {
                List<string> attr = new List<string>();
                string type = "CardType_Enum." + line[1];
                string title = line[2];
                string cardImage = "Image_Enum." + line[3];
                string className = title.Replace(" ", "") + "VO";
                string filePath = path + @"Unit\Generated\" + className + ".cs";
                string action01 = line[4];
                string action02 = line[5];
                string action03 = line[6];
                string action = "new List<string> {";
                if (action01.Trim().Length > 0) {
                    action += ("\"" + action01 + "\"");
                }
                if (action02.Trim().Length > 0) {
                    action += (", \"" + action02 + "\"");
                }
                if (action03.Trim().Length > 0) {
                    action += (", \"" + action03 + "\"");
                }
                action += "}";
                string cost = buildAllowedStr("Crystal_Enum", line[7], line[8], line[9]);
                string influence = line[10];
                string armor = line[11];
                string level = line[12];
                string location = "new List<Image_Enum> {";
                if (line[13].Trim().Length > 0) {
                    bool flag = false;
                    foreach (string a in line[13].Split("-")) {
                        if (flag) location += ", ";
                        flag = true;
                        location += ("Image_Enum.I_unit" + a);
                    }
                }
                location += "}";
                string resist = "new List<Image_Enum> {";
                if (line[14].Trim().Length > 0) {
                    bool flag = false;
                    foreach (string a in line[14].Split("-")) {
                        if (flag) resist += ", ";
                        flag = true;
                        resist += ("Image_Enum.I_resist" + a);
                    }
                }
                resist += "}";
                string allowed = buildAllowedStr("TurnPhase_Enum", line[15], line[16], line[17]);
                string allowedbattle = buildAllowedStr("BattlePhase_Enum", line[18], line[19], line[20]);
                attr.Add("uniqueId");
                attr.Add("\"" + title + "\"");
                attr.Add(cardImage);
                attr.Add(type);
                attr.Add(action);
                attr.Add(cost);
                attr.Add(allowed);
                attr.Add(allowedbattle);
                attr.Add("CardColor_Enum.NA");
                attr.Add(influence);
                attr.Add(level);
                attr.Add(armor);
                attr.Add(location);
                attr.Add(resist);

                sb.AppendLine(string.Format("D.Cards.Add(new {0}(D.Cards.Count));", className));
                using (StreamWriter file = new StreamWriter(filePath, false)) {
                    file.WriteLine(string.Format("using System.Collections.Generic;"));
                    file.WriteLine(string.Format("using cna.poo;"));
                    file.WriteLine(string.Format("namespace cna {{"));
                    file.WriteLine(string.Format("    public partial class {0} : CardUnitVO {{", className));
                    file.WriteLine(string.Format("        public {0}(int uniqueId) : base(", className));
                    for (int i = 0; i < attr.Count; i++) {
                        string l = "            " + attr[i];
                        if (i < attr.Count - 1) l += ",";
                        file.WriteLine(l);
                    }
                    file.WriteLine(string.Format("            ) {{ }}"));
                    file.WriteLine(string.Format("    }}"));
                    file.WriteLine(string.Format("}}"));
                }
            }
            Debug.Log(sb.ToString());
        }
        private void generateSkillCards() {
            deleteGeneratedFilesInFolder(path + @"Skill\Generated");
            List<List<string>> csv = readFile(@"Skill\Skill.csv");
            StringBuilder sb = new StringBuilder();
            foreach (List<string> line in csv) {
                List<string> attr = new List<string>();
                string type = "CardType_Enum." + line[0];
                string title = line[1];
                string refresh = "SkillRefresh_Enum." + line[2];
                string cardImage = "Image_Enum." + line[3];
                string cardImageBack = "Image_Enum." + line[4];
                string color = "Image_Enum." + line[5];
                string className = line[5].Replace("A_MEEPLE_", "") + "_" + title.Replace(" ", "").Replace("'", "") + "VO";
                string filePath = path + @"Skill\Generated\" + className + ".cs";
                string allowed = buildAllowedStr("TurnPhase_Enum", line[6]);
                string allowedbattle = buildAllowedStr("BattlePhase_Enum", line[7]);
                string interactive = line[8].Equals("no") ? "false" : "true";
                string description = "\"" + line[9] + "\"";

                attr.Add("uniqueId");
                attr.Add("\"" + title + "\"");
                attr.Add(type);
                attr.Add(refresh);
                attr.Add(cardImage);
                attr.Add(cardImageBack);
                attr.Add(color);
                attr.Add(allowed);
                attr.Add(allowedbattle);
                attr.Add(interactive);
                attr.Add(description);

                sb.AppendLine(string.Format("D.Cards.Add(new {0}(D.Cards.Count));", className));
                using (StreamWriter file = new StreamWriter(filePath, false)) {
                    file.WriteLine(string.Format("using System.Collections.Generic;"));
                    file.WriteLine(string.Format("using cna.poo;"));
                    file.WriteLine(string.Format("namespace cna {{"));
                    file.WriteLine(string.Format("    public partial class {0} : CardSkillVO {{", className));
                    file.WriteLine(string.Format("        public {0}(int uniqueId) : base(", className));
                    for (int i = 0; i < attr.Count; i++) {
                        string l = "            " + attr[i];
                        if (i < attr.Count - 1) l += ",";
                        file.WriteLine(l);
                    }
                    file.WriteLine(string.Format("            ) {{ }}"));
                    file.WriteLine(string.Format("    }}"));
                    file.WriteLine(string.Format("}}"));
                }
            }
            Debug.Log(sb.ToString());
        }




        private string buildAllowedStr(string strType, params string[] allowed) {
            string str = "new List<List<" + strType + ">> {";
            bool flag01 = false;
            foreach (string a in allowed) {
                if (flag01) str += ", ";
                flag01 = true;
                str += "new List<" + strType + ">() {";
                if (a.Trim().Length > 0) {
                    bool flag02 = false;
                    foreach (string z in a.Split("-")) {
                        if (flag02) str += ", ";
                        flag02 = true;
                        str += (strType + "." + z);
                    }
                }
                str += "}";
            }
            str += "}";
            return str;
        }



        private void deleteGeneratedFilesInFolder(string path) {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.GetFiles()) {
                file.Delete();
            }
        }

        private List<List<string>> readFile(string fileName) {
            List<List<string>> csv = new List<List<string>>();
            bool firstline = true;
            string[] lines = System.IO.File.ReadAllLines(path + fileName);
            foreach (string line in lines) {
                if (firstline) {
                    firstline = false;
                    continue;
                }
                List<string> fields = new List<string>();
                string[] f = line.Split(',');
                for (int i = 0; i < f.Length; i++) {
                    if (f[i].Length > 0 && f[i][0] == '"') {
                        StringBuilder sb = new StringBuilder();
                        while (true) {
                            string val = f[i];
                            sb.Append(val);
                            if (val[val.Length - 1] == '"') {
                                break;
                            } else {
                                sb.Append(",");
                            }
                            i++;
                        }
                        fields.Add(sb.ToString().Replace("\"", ""));
                    } else {
                        fields.Add(f[i]);
                    }
                }
                csv.Add(fields);
            }
            return csv;
        }
    }
}
