using cna.poo;

namespace cna {
    public partial class IllusionistsVO : CardUnitVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(4);
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            int monsterId = ar.LocalPlayer.Battle.SelectedMonsters[0];
            ar.LocalPlayer.Battle.Monsters[monsterId].Blocked = true;
            ar.AddLog(D.Cards[monsterId].CardTitle + " Does not attack!");
            return ar;
        }

        public override ActionResultVO ActionValid_02(ActionResultVO ar) {
            ar.CrystalWhite(1);
            return ar;
        }


        public override ActionResultVO checkAllowedToUse(ActionResultVO ar) {
            ar = base.checkAllowedToUse(ar);
            if (ar.Status) {
                if (ar.ActionIndex == 1) {
                    if (ar.LocalPlayer.Battle.SelectedMonsters.Count > 0) {
                        int monsterId = ar.LocalPlayer.Battle.SelectedMonsters[0];
                        MonsterDetailsVO md = D.B.MonsterDetails[monsterId];
                        if (md.Fortified) {
                            ar.ErrorMsg = "This effect can not be used on fortified monsters!";
                        }
                    } else {
                        ar.ErrorMsg = "You must select a monster to use this effect on!";
                    }
                }
            }
            return ar;
        }
    }
}
