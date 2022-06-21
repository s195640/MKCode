using cna.poo;
namespace cna {
    public partial class BLUE_ResistanceBreakVO : CardSkillVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            int monsterId = ar.P.Battle.SelectedMonsters[0];
            ar.AddGameEffect(GameEffect_Enum.BLUE_ResistanceBreak, monsterId);
            return ar;
        }

        public override GameAPI checkAllowedToUse(GameAPI ar) {
            ar = base.checkAllowedToUse(ar);
            if (ar.Status) {
                if (ar.P.Battle.SelectedMonsters.Count != 1) {
                    ar.ErrorMsg = "You must select one and only one monster to use this effect on!";
                }
            }
            return ar;
        }
    }
}
