using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class BattleData : BaseData {
        public BattleData() { }

        [SerializeField] private BattlePhase_Enum battlePhase = BattlePhase_Enum.NA;
        [SerializeField] private CNAMap<int, MonsterMetaData> monsters = new CNAMap<int, MonsterMetaData>();
        [SerializeField] private List<int> selectedMonsters = new List<int>();

        [SerializeField] private AttackData siege = new AttackData();
        [SerializeField] private AttackData range = new AttackData();
        [SerializeField] private AttackData shield = new AttackData();
        [SerializeField] private AttackData attack = new AttackData();


        public BattlePhase_Enum BattlePhase { get => battlePhase; set => battlePhase = value; }
        public CNAMap<int, MonsterMetaData> Monsters { get => monsters; set => monsters = value; }
        public List<int> SelectedMonsters { get => selectedMonsters; set => selectedMonsters = value; }
        public int SelectedUnit { get; set; }
        public AttackData Siege { get => siege; set => siege = value; }
        public AttackData Range { get => range; set => range = value; }
        public AttackData Shield { get => shield; set => shield = value; }
        public AttackData Attack { get => attack; set => attack = value; }

        public void Clear() {
            Siege.Clear();
            Range.Clear();
            Shield.Clear();
            Attack.Clear();
            Monsters.Clear();
            SelectedMonsters.Clear();
            SelectedUnit = 0;
            battlePhase = BattlePhase_Enum.NA;
        }

        public void UpdateData(BattleData b) {
            battlePhase = b.battlePhase;
            monsters.Clear();
            b.monsters.Keys.ForEach(key => {
                MonsterMetaData mmd = new MonsterMetaData();
                mmd.UpdateData(b.monsters[key]);
                monsters.Add(key, mmd);
            });
            selectedMonsters.Clear();
            selectedMonsters.AddRange(b.selectedMonsters);
            siege.UpdateData(b.siege);
            range.UpdateData(b.range);
            shield.UpdateData(b.shield);
            attack.UpdateData(b.attack);
        }

        public override string Serialize() {
            string data = CNASerialize.Sz(battlePhase) + "%"
                + CNASerialize.Sz(monsters) + "%"
                + CNASerialize.Sz(selectedMonsters) + "%"
                + CNASerialize.Sz(siege) + "%"
                + CNASerialize.Sz(range) + "%"
                + CNASerialize.Sz(shield) + "%"
                + CNASerialize.Sz(attack);
            return "[" + data + "]";
        }

        public override void Deserialize(string data) {
            List<string> d = CNASerialize.DeserizlizeSplit(data.Substring(1, data.Length - 2));
            CNASerialize.Dz(d[0], out battlePhase);
            CNASerialize.Dz(d[1], out monsters);
            CNASerialize.Dz(d[2], out selectedMonsters);
            CNASerialize.Dz(d[3], out siege);
            CNASerialize.Dz(d[4], out range);
            CNASerialize.Dz(d[5], out shield);
            CNASerialize.Dz(d[6], out attack);
        }
    }
}
