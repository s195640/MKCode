using cna.poo;
namespace cna {
    public partial class IceGolemsVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(3));
            } else {
                ar.BattleAttack(new AttackData(3));
            }
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Cold = 6;
            ar.BattleAttack(a);
            return ar;
        }
    }
}
