using System;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class MonsterMetaData : Data {
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
    }
}
