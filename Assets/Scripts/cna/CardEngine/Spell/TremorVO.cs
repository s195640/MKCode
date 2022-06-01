using cna.poo;
namespace cna {
    public partial class TremorVO : CardSpellVO {
        public override void ActionPaymentComplete_00(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_00,
                new OptionVO("Single", Image_Enum.I_monsterarmor),
                new OptionVO("Multi", Image_Enum.I_monsterarmor)
                );
        }

        public void acceptCallback_00(ActionResultVO ar) {
            if (ar.SelectedButtonIndex == 0) {
                if (ar.LocalPlayer.Battle.SelectedMonsters.Count != 1) {
                    ar.ErrorMsg = "You must select exactly 1 monster!";
                } else {
                    ar.AddGameEffect(GameEffect_Enum.CS_Tremor01, ar.LocalPlayer.Battle.SelectedMonsters[0]);
                }
            } else {
                ar.LocalPlayer.Battle.Monsters.Keys.ForEach(m => {
                    ar.AddGameEffect(GameEffect_Enum.CS_Tremor02, m);
                });
            }
            ar.FinishCallback(ar);
        }

        public override void ActionPaymentComplete_01(ActionResultVO ar) {
            ar.SelectOptions(acceptCallback_01,
                new OptionVO("Single", Image_Enum.I_monsterarmor),
                new OptionVO("Multi", Image_Enum.I_monsterarmor)
                );
        }

        public void acceptCallback_01(ActionResultVO ar) {
            if (ar.SelectedButtonIndex == 0) {
                if (ar.LocalPlayer.Battle.SelectedMonsters.Count != 1) {
                    ar.ErrorMsg = "You must select exactly 1 monster!";
                } else {
                    ar.AddGameEffect(GameEffect_Enum.CS_Earthquake01, ar.LocalPlayer.Battle.SelectedMonsters[0]);
                }
            } else {
                ar.LocalPlayer.Battle.Monsters.Keys.ForEach(m => {
                    ar.AddGameEffect(GameEffect_Enum.CS_Earthquake02, m);
                });
            }
            ar.FinishCallback(ar);
        }
    }
}
