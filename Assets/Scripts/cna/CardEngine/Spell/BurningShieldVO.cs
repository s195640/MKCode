using cna.poo;
namespace cna {
    public partial class BurningShieldVO : CardSpellVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Fire = 4;
            ar.BattleBlock(a);
            ar.AddGameEffect(GameEffect_Enum.CS_BurningShield);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Fire = 4;
            ar.BattleBlock(a);
            ar.AddGameEffect(GameEffect_Enum.CS_ExplodingShield);
            return ar;
        }
    }
}
