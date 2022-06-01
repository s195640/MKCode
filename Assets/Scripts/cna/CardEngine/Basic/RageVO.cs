using cna.poo;

namespace cna {
    public partial class RageVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(2));
            } else {
                ar.BattleAttack(new AttackData(2));
            }
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.BattleAttack(new AttackData(4 + ar.CardModifier));
            return ar;
        }
    }
}
