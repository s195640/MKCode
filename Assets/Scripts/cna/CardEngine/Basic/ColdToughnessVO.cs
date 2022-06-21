using cna.poo;

namespace cna {
    public partial class ColdToughnessVO : CardActionVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Attack) {
                AttackData attack = new AttackData();
                attack.Cold += 2;
                ar.BattleAttack(attack);
            } else {
                AttackData attack = new AttackData();
                attack.Cold += 3;
                ar.BattleBlock(attack);
            }
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.AddGameEffect(GameEffect_Enum.ColdToughness);
            AttackData attack = new AttackData();
            attack.Cold = 5 + ar.CardModifier;
            ar.BattleBlock(attack);
            return ar;
        }
    }
}
