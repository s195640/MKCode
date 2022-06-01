using cna.poo;

namespace cna {
    public partial class SavageMonksVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            if (ar.LocalPlayer.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(new AttackData(3));
            } else {
                ar.BattleAttack(new AttackData(3));
            }
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.BattleSiege(new AttackData(4));
            return ar;
        }
    }
}
