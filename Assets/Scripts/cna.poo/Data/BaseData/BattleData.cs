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
    }
}
