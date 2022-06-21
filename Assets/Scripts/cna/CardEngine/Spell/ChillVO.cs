using cna.poo;
namespace cna {
    public partial class ChillVO : CardSpellVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            int monsterId = ar.P.Battle.SelectedMonsters[0];
            ar.P.Battle.Monsters[monsterId].Blocked = true;
            ar.AddLog(D.Cards[monsterId].CardTitle + " Does not attack and loses fire resistance!");
            ar.AddGameEffect(GameEffect_Enum.CS_Chill, monsterId);
            return ar;
        }
        public override GameAPI ActionValid_01(GameAPI ar) {
            int monsterId = ar.P.Battle.SelectedMonsters[0];
            ar.P.Battle.Monsters[monsterId].Blocked = true;
            ar.AddLog(D.Cards[monsterId].CardTitle + " Does not attack and gets -4 armor!");
            ar.AddGameEffect(GameEffect_Enum.CS_LethalChill, monsterId);
            return ar;
        }

        public override GameAPI checkAllowedToUse(GameAPI ar) {
            ar = base.checkAllowedToUse(ar);
            if (ar.Status) {
                if (ar.P.Battle.SelectedMonsters.Count != 1) {
                    ar.ErrorMsg = "You must select ONE monster to use this effect on!";
                }
            }
            return ar;
        }
    }
}
