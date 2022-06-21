using cna.poo;
namespace cna {
    public partial class IceShieldVO : CardActionVO {

        public override GameAPI ActionValid_00(GameAPI ar) {
            AttackData a = new AttackData();
            a.Cold += 3;
            ar.BattleBlock(a);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            AttackData a = new AttackData();
            a.Cold += (3 + ar.CardModifier);
            ar.BattleBlock(a);
            ar.AddGameEffect(GameEffect_Enum.AC_IceShield);
            return ar;
        }
    }
}
