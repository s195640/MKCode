using cna.poo;
namespace cna {
    public partial class CrystalMasteryVO : CardActionVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            CrystalData c = ar.LocalPlayer.Crystal;
            if (c.GetTotal() > 0) {
                if (c.Blue > 0) {
                    ar.CrystalBlue(1);
                }
                if (c.Green > 0) {
                    ar.CrystalGreen(1);
                }
                if (c.White > 0) {
                    ar.CrystalWhite(1);
                }
                if (c.Red > 0) {
                    ar.CrystalRed(1);
                }
            } else {
                ar.ErrorMsg = "You need to have at least 1 crytal to play this card.";
            }
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            ar.AddGameEffect(GameEffect_Enum.AC_CrystalMastery);
            return ar;
        }
    }
}
