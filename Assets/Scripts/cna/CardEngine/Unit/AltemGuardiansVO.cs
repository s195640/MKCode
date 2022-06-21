using cna.poo;
namespace cna {
    public partial class AltemGuardiansVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.BattleAttack(new AttackData(5));
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.BattleBlock(new AttackData(8));
            ar.AddGameEffect(GameEffect_Enum.CUE_AltemGuardians01);
            return ar;
        }
        public override GameAPI ActionValid_02(GameAPI ar) {
            ar.AddGameEffect(GameEffect_Enum.CUE_AltemGuardians02);
            return ar;
        }
    }
}
