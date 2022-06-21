using cna.poo;
namespace cna {
    public partial class AmotepFreezersVO : CardUnitVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            AttackData a = new AttackData(5);
            if (ar.P.Battle.BattlePhase == BattlePhase_Enum.Block) {
                ar.BattleBlock(a);
            } else {
                ar.BattleAttack(a);
            }
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            int monsterId = ar.P.Battle.SelectedMonsters[0];
            ar.P.Battle.Monsters[monsterId].Blocked = true;
            ar.AddLog(D.Cards[monsterId].CardTitle + " Does not attack and gets -3 armor!");
            ar.AddGameEffect(GameEffect_Enum.CUE_AmotepFreezers, monsterId);
            return ar;
        }

        public override GameAPI checkAllowedToUse(GameAPI ar) {
            ar = base.checkAllowedToUse(ar);
            if (ar.Status) {
                if (ar.ActionIndex == 1) {
                    if (ar.P.Battle.SelectedMonsters.Count != 1) {
                        ar.ErrorMsg = "You must select ONE monster to use this effect on!";
                    }
                }
            }
            return ar;
        }
    }
}
