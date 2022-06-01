using cna.poo;
namespace cna {
    public partial class DemolishVO : CardSpellVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.LocalPlayer.Battle.Monsters.Keys.ForEach(m => ar.AddGameEffect(GameEffect_Enum.CS_Demolish, m));
            ar.AddLog("All monsters get -1 Armor!");
            return ar;
        }

        public override ActionResultVO ActionValid_01(ActionResultVO ar) {
            int monsterId = ar.LocalPlayer.Battle.SelectedMonsters[0];
            ar.LocalPlayer.Battle.Monsters[monsterId].Dead = true;
            ar.AddLog(D.Cards[monsterId].CardTitle + " Killed! all others get -1 Armor!");
            ar.LocalPlayer.Battle.Monsters.Keys.ForEach(m => ar.AddGameEffect(GameEffect_Enum.CS_Disintegrate, m));
            return ar;
        }

        public override ActionResultVO checkAllowedToUse(ActionResultVO ar) {
            ar = base.checkAllowedToUse(ar);
            if (ar.Status) {
                if (ar.ActionIndex == 1) {
                    if (ar.LocalPlayer.Battle.SelectedMonsters.Count != 1) {
                        ar.ErrorMsg = "You must select ONE monster to use this effect on!";
                    }
                }
            }
            return ar;
        }
    }
}
