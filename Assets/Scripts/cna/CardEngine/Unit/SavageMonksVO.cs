using cna.poo;

namespace cna {
    public partial class SavageMonksVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(3));
            } else {
                ar.BattleAttack(new AttackData(3));
            }
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.BattleSiege(new AttackData(4));
            return ar;
        }
    }
}
