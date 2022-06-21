using cna.poo;
namespace cna {
    public partial class FlameWallVO : CardSpellVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                AttackData a = new AttackData();
                a.Fire += 5;
                ar.BattleRange(a);
            } else {
                AttackData a = new AttackData();
                a.Fire += 7;
                ar.BattleRange(a);
            }
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            int totalMonsters = ar.P.Battle.Monsters.Count * 2;
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                AttackData a = new AttackData();
                a.Fire = 5 + totalMonsters;
                ar.BattleRange(a);
            } else {
                AttackData a = new AttackData();
                a.Fire = 7 + totalMonsters;
                ar.BattleRange(a);
            }
            return ar;
        }
    }
}
