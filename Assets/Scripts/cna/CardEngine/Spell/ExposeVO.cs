using cna.poo;
namespace cna {
    public partial class ExposeVO : CardSpellVO {
        public override ActionResultVO ActionValid_00(ActionResultVO ar) {
            ar.BattleRange(new AttackData(2));
            int monsterId = ar.LocalPlayer.Battle.SelectedMonsters[0];
            ar.AddGameEffect(GameEffect_Enum.CS_Expose, monsterId);
            ar.AddLog(D.Cards[monsterId].CardTitle + " Loses fortifications & resistances");
            return ar;
        }
        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("Fortification", Image_Enum.I_monsterfortified),
                new OptionVO("Resistance", Image_Enum.I_resistphysical)
                );
        }

        public void acceptCallback_01(ActionResultVO ar) {
            ar.BattleRange(new AttackData(3));
            GameEffect_Enum ge = ar.SelectedButtonIndex == 0 ? GameEffect_Enum.CS_MassExpose01 : GameEffect_Enum.CS_MassExpose02;
            ar.LocalPlayer.Battle.Monsters.Keys.ForEach(m => {
                ar.AddGameEffect(ge, m);
            });
            ar.FinishCallback(ar);
        }

        public override ActionResultVO checkAllowedToUse(ActionResultVO ar) {
            ar = base.checkAllowedToUse(ar);
            if (ar.Status) {
                if (ar.ActionIndex == 0) {
                    if (ar.LocalPlayer.Battle.SelectedMonsters.Count != 1) {
                        ar.ErrorMsg = "You must select exactly 1 monster!";
                    }
                }
            }
            return ar;
        }
    }
}
