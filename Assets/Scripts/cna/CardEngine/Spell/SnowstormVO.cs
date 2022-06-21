using cna.poo;
namespace cna {
    public partial class SnowstormVO : CardSpellVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            AttackData a = new AttackData();
            a.Cold += 5;
            ar.BattleRange(a);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            AttackData a = new AttackData();
            a.Cold += 8;
            ar.BattleSiege(a);
            ar.AddWound(1);
            return ar;
        }
    }
}
