using cna.poo;
namespace cna {
    public partial class ChillVO : CardSpellVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            int monsterId = ar.LocalPlayer.Battle.SelectedMonsters[0];
            ar.LocalPlayer.Battle.Monsters[monsterId].Blocked = true;
            ar.AddLog(D.Cards[monsterId].CardTitle + " Does not attack and loses fire resistance!");
            ar.AddGameEffect(GameEffect_Enum.CS_Chill, monsterId);
            return ar;
        }
        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            int monsterId = ar.LocalPlayer.Battle.SelectedMonsters[0];
            ar.LocalPlayer.Battle.Monsters[monsterId].Blocked = true;
            ar.AddLog(D.Cards[monsterId].CardTitle + " Does not attack and gets -4 armor!");
            ar.AddGameEffect(GameEffect_Enum.CS_LethalChill, monsterId);
            return ar;
        }

        public override ActionResultVO checkAllowedToUse(ActionResultVO ar) {
            ar = base.checkAllowedToUse(ar);
            if (ar.Status) {
                if (ar.LocalPlayer.Battle.SelectedMonsters.Count != 1) {
                    ar.ErrorMsg = "You must select ONE monster to use this effect on!";
                }
            }
            return ar;
        }
    }
}
