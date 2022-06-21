using cna.poo;
namespace cna {
    public partial class FireballVO : CardSpellVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            AttackData a = new AttackData();
            a.Fire = 5;
            ar.BattleRange(a);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            AttackData a = new AttackData();
            a.Fire = 8;
            ar.BattleSiege(a);
            ar.AddWound(1);
            return ar;
        }
    }
}
