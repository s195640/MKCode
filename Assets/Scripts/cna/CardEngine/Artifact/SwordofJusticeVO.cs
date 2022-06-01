using cna.poo;
namespace cna {
    public partial class SwordofJusticeVO : CardArtifactVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.AddGameEffect(GameEffect_Enum.CT_SwordOfJustice01);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.AddGameEffect(GameEffect_Enum.CT_SwordOfJustice02);
            ar.LocalPlayer.Battle.Monsters.Keys.ForEach(m => ar.AddGameEffect(GameEffect_Enum.CT_SwordOfJustice02, m));
            return ar;
        }
    }
}
