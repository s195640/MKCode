using cna.poo;

namespace cna {
    public partial class IllusionistsVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(4);
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            int monsterId = ar.P.Battle.SelectedMonsters[0];
            ar.P.Battle.Monsters[monsterId].Blocked = true;
            ar.AddLog(D.Cards[monsterId].CardTitle + " Does not attack!");
            return ar;
        }

        public override GameAPI ActionValid_02(GameAPI ar) {
            ar.CrystalWhite(1);
            return ar;
        }


        public override GameAPI checkAllowedToUse(GameAPI ar) {
            ar = base.checkAllowedToUse(ar);
            if (ar.Status) {
                if (ar.ActionIndex == 1) {
                    if (ar.P.Battle.SelectedMonsters.Count > 0) {
                        int monsterId = ar.P.Battle.SelectedMonsters[0];
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
