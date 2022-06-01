using cna.poo;
namespace cna {
    public partial class BLUE_ResistanceBreakVO : CardSkillVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            int monsterId = ar.LocalPlayer.Battle.SelectedMonsters[0];
            ar.AddGameEffect(GameEffect_Enum.BLUE_ResistanceBreak, monsterId);
            return ar;
        }

        public override ActionResultVO checkAllowedToUse(ActionResultVO ar) {
            ar = base.checkAllowedToUse(ar);
            if (ar.Status) {
                if (ar.LocalPlayer.Battle.SelectedMonsters.Count != 1) {
                    ar.ErrorMsg = "You must select one and only one monster to use this effect on!";
                }
            }
            return ar;
        }
    }
}
