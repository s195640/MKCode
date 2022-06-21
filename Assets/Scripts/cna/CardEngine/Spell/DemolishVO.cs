using cna.poo;
namespace cna {
    public partial class DemolishVO : CardSpellVO {
        public override GameAPI ActionValid_00(GameAPI ar) {
            ar.P.Battle.Monsters.Keys.ForEach(m => ar.AddGameEffect(GameEffect_Enum.CS_Demolish, m));
            ar.AddLog("All monsters get -1 Armor!");
            return ar;
        }

        public override GameAPI ActionValid_01(GameAPI ar) {
            int monsterId = ar.P.Battle.SelectedMonsters[0];
            ar.P.Battle.Monsters[monsterId].Dead = true;
            ar.AddLog(D.Cards[monsterId].CardTitle + " Killed! all others get -1 Armor!");
            ar.P.Battle.Monsters.Keys.ForEach(m => ar.AddGameEffect(GameEffect_Enum.CS_Disintegrate, m));
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
