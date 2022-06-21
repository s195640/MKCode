using cna.poo;
namespace cna {
    public partial class BurningShieldVO : CardSpellVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            AttackData a = new AttackData();
            a.Fire = 4;
            ar.BattleBlock(a);
            ar.AddGameEffect(GameEffect_Enum.CS_BurningShield);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            AttackData a = new AttackData();
            a.Fire = 4;
            ar.BattleBlock(a);
            ar.AddGameEffect(GameEffect_Enum.CS_ExplodingShield);
            return ar;
        }
    }
}
