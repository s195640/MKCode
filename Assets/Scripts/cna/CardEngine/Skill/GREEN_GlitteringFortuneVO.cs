using cna.poo;
namespace cna {
    public partial class GREEN_GlitteringFortuneVO : CardSkillVO {

        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            CrystalData c = ar.LocalPlayer.Crystal;
            int i = 0;
            if (c.Blue > 0) { i++; }
            if (c.Green > 0) { i++; }
            if (c.Red > 0) { i++; }
            if (c.White > 0) { i++; }
            ar.ActionInfluence(i);
            return ar;
        }
    }
}
