using cna.poo;

namespace cna {
    public partial class RageVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(2));
            } else {
                ar.BattleAttack(new AttackData(2));
            }
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.BattleAttack(new AttackData(4 + ar.CardModifier));
            return ar;
        }
    }
}
