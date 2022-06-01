using cna.poo;
namespace cna {
    public partial class FireMagesVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Fire = 3;
            ar.BattleRange(a);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Fire = 6;
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(a);
            } else {
                ar.BattleAttack(a);
            }
            return ar;
        }

        public override ActionResultVO ActionValid_02(ActionResultVO ar) {
            ar.CrystalRed(1);
            ar.ManaRed(1);
            return ar;
        }
    }
}
