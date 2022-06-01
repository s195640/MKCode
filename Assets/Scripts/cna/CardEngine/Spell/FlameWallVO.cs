using cna.poo;
namespace cna {
    public partial class FlameWallVO : CardSpellVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
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

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            int totalMonsters = ar.LocalPlayer.Battle.Monsters.Count * 2;
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
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
