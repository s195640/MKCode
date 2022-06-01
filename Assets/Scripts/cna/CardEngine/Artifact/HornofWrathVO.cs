using cna.poo;
namespace cna {
    public partial class HornofWrathVO : CardArtifactVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.BattleSiege(new AttackData(6));
            ar.AddWound(1);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.BattleSiege(new AttackData(6));
            ar.AddGameEffect(GameEffect_Enum.CT_HornOfWrath);
            return ar;
        }
    }
}
