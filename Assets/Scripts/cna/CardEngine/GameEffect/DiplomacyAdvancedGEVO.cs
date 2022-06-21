using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class DiplomacyAdvancedGEVO : CardGameEffectVO {
        public DiplomacyAdvancedGEVO(int uniqueId) : base(
            uniqueId, "Diplomacy Advanced", Image_Enum.I_influencegold,
                CardType_Enum.GameEffect,
                GameEffect_Enum.AC_Diplomacy02,
                GameEffectDuration_Enum.Turn,
                CNAColor.ColorLightBlue,
                "During combat this turn, you may spend Influence points to get Ice Block, Fire Block, or Block 1 for each.",
                true, true, false
            ) {
            GameEffectClickable = true;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.Battle } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block } };
        }

        public override void ActionPaymentComplete_00(GameAPI ar) {
            if (ar.P.Influence > 1) {
                ar.SelectOptions(acceptCallback_00,
                new OptionVO("Block 1", Image_Enum.I_shield),
                new OptionVO("Fire Block 1", Image_Enum.I_shield),
                new OptionVO("Ice Block 1", Image_Enum.I_shield)
                );
            } else {
                ar.ErrorMsg = "You do not have enough influence points";
            }
        }
        public void acceptCallback_00(GameAPI ar) {
            ar.ActionInfluence(-1);
            switch (ar.SelectedButtonIndex) {
                case 0: {
                    ar.BattleBlock(new AttackData(1));
                    break;
                }
                case 1: {
                    AttackData a = new AttackData();
                    a.Fire++;
                    ar.BattleBlock(a);
                    break;
                }
                case 2: {
                    AttackData a = new AttackData();
                    a.Cold++;
                    ar.BattleBlock(a);
                    break;
                }
            }
            ar.FinishCallback(ar);
        }
    }
}
