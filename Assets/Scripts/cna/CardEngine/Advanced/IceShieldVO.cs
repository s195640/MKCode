using cna.poo;
namespace cna {
    public partial class IceShieldVO : CardActionVO {

        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Cold += 3;
            ar.BattleBlock(a);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            AttackData a = new AttackData();
            a.Cold += (3 + ar.CardModifier);
            ar.BattleBlock(a);
            ar.AddGameEffect(GameEffect_Enum.AC_IceShield);
            return ar;
        }
    }
}
