using cna.poo;
namespace cna {
    public partial class HornofWrathVO : CardArtifactVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.BattleSiege(new AttackData(6));
            ar.AddWound(1);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.BattleSiege(new AttackData(6));
            ar.AddGameEffect(GameEffect_Enum.CT_HornOfWrath);
            return ar;
        }
    }
}
