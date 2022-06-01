using cna.poo;
namespace cna {
    public partial class IceMagesVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Cold = 4;
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(a);
            } else {
                ar.BattleAttack(a);
            }
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Cold = 4;
            ar.BattleSiege(a);
            return ar;
        }

        public override ActionResultVO ActionValid_02(ActionResultVO ar) {
            ar.CrystalBlue(1);
            ar.ManaBlue(1);
            return ar;
        }
    }
}
