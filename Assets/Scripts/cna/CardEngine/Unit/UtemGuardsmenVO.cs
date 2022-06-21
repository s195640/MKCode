using cna.poo;

namespace cna {
    public partial class UtemGuardsmenVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.BattleAttack(new AttackData(2));
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.BattleBlock(new AttackData(4));
            ar.AddGameEffect(GameEffect_Enum.UtemGuardsmen);
            return ar;
        }
    }
}
