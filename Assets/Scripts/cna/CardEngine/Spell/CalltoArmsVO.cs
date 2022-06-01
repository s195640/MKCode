using cna.poo;
namespace cna {
    public partial class CalltoArmsVO : CardSpellVO {

        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.AddGameEffect(GameEffect_Enum.CS_CallToArms, 0);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.AddGameEffect(GameEffect_Enum.CS_CallToGlory, 0);
            return ar;
        }
    }
}
