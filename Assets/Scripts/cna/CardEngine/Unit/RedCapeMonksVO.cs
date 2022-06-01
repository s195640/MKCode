using cna.poo;

namespace cna {
    public partial class RedCapeMonksVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(3));
            } else {
                ar.BattleAttack(new AttackData(3));
            }
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                AttackData a = new AttackData();
                a.Fire = 4;
                ar.BattleBlock(a);
            } else {
                AttackData a = new AttackData();
                a.Fire = 4;
                ar.BattleAttack(a);
            }
            return ar;
        }
    }
}
