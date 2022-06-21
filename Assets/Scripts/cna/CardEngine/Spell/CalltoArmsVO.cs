using cna.poo;
namespace cna {
    public partial class CalltoArmsVO : CardSpellVO {

        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.AddGameEffect(GameEffect_Enum.CS_CallToArms, 0);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            ar.AddGameEffect(GameEffect_Enum.CS_CallToGlory, 0);
            return ar;
        }
    }
}
