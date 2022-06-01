using cna.poo;
namespace cna {
    public partial class AmotepGunnersVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(5));
            } else {
                ar.BattleAttack(new AttackData(5));
            }
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Fire = 6;
            ar.BattleRange(a);
            return ar;
        }
    }
}
