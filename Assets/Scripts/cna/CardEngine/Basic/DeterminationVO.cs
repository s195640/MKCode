using cna.poo;

namespace cna {
    public partial class DeterminationVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Attack) {
                ar.BattleAttack(new AttackData(2));
            } else {
                ar.BattleBlock(new AttackData(2));
            }
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.BattleBlock(new AttackData(5 + ar.CardModifier));
            return ar;
        }
    }
}
