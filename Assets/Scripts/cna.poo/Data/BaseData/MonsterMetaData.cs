using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class MonsterMetaData : BaseData {
        [SerializeField] private int uniqueid;
        [SerializeField] private bool dead = false;
        [SerializeField] private bool blocked = false;
        [SerializeField] private bool assigned = false;
        [SerializeField] private bool summoned = false;
        [SerializeField] private int summoner = 0;
        [SerializeField] private bool provoked = false;
        [SerializeField] private V2IntVO location;
        [SerializeField] private Image_Enum structure;

        public MonsterMetaData() { }

        public MonsterMetaData(int uniqueid, V2IntVO location, Image_Enum structure) {
            this.uniqueid = uniqueid;
            this.location = location;
            this.structure = structure;
        }


        public int Uniqueid { get => uniqueid; set => uniqueid = value; }
        public bool Dead { get => dead; set => dead = value; }
        public bool Blocked { get => blocked; set => blocked = value; }
        public bool Assigned { get => assigned; set => assigned = value; }
        public bool Summoned { get => summoned; set => summoned = value; }
        public int Summoner { get => summoner; set => summoner = value; }
        public bool Provoked { get => provoked; set => provoked = value; }
        public V2IntVO Location { get => location; set => location = value; }
        public Image_Enum Structure { get => structure; set => structure = value; }

        public bool FortifiedStructure {
            get {
                return Structure == Image_Enum.SH_Keep || Structure == Image_Enum.SH_MageTower || Structure == Image_Enum.SH_City_Blue || Structure == Image_Enum.SH_City_Green || Structure == Image_Enum.SH_City_White || Structure == Image_Enum.SH_City_Red;
            }
        }

        public bool CityStructure {
            get {
                return Structure == Image_Enum.SH_City_Blue || Structure == Image_Enum.SH_City_Green || Structure == Image_Enum.SH_City_White || Structure == Image_Enum.SH_City_Red;
            }
        }

        public void UpdateData(MonsterMetaData m) {
            uniqueid = m.uniqueid;
            dead = m.dead;
            blocked = m.blocked;
            assigned = m.assigned;
            summoned = m.summoned;
            summoner = m.summoner;
            provoked = m.provoked;
            location = m.location.Clone();
            structure = m.structure;
        }


        public override string Serialize() {
            string data = CNASerialize.Sz(uniqueid) + "%"
                + CNASerialize.Sz(dead) + "%"
                + CNASerialize.Sz(blocked) + "%"
                + CNASerialize.Sz(assigned) + "%"
                + CNASerialize.Sz(summoned) + "%"
                + CNASerialize.Sz(summoner) + "%"
                + CNASerialize.Sz(provoked) + "%"
                + CNASerialize.Sz(location) + "%"
                + CNASerialize.Sz(structure);
            return "[" + data + "]";
        }

        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out uniqueid);
            CNASerialize.Dz(d[1], out dead);
            CNASerialize.Dz(d[2], out blocked);
            CNASerialize.Dz(d[3], out assigned);
            CNASerialize.Dz(d[4], out summoned);
            CNASerialize.Dz(d[5], out summoner);
            CNASerialize.Dz(d[6], out provoked);
            CNASerialize.Dz(d[7], out location);
            CNASerialize.Dz(d[8], out structure);
        }
    }
}
