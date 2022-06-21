using cna.poo;
namespace cna {
    public partial class SwordofJusticeVO : CardArtifactVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.AddGameEffect(GameEffect_Enum.CT_SwordOfJustice01);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.AddGameEffect(GameEffect_Enum.CT_SwordOfJustice02);
            ar.P.Battle.Monsters.Keys.ForEach(m => ar.AddGameEffect(GameEffect_Enum.CT_SwordOfJustice02, m));
            return ar;
        }
    }
}
