using System.Collections.Generic;
using cna.poo;

namespace cna {
    public class ManaStealGEVO : CardGameEffectVO {
        public ManaStealGEVO(int uniqueId) : base(
            uniqueId, "Mana Steal", Image_Enum.I_mana_gold,
                CardType_Enum.GameEffect,
                GameEffect_Enum.T_ManaSteal,
                GameEffectDuration_Enum.Round,
                CNAColor.ColorLightBlue,
                "Use the mana on this card during your turn.",
                true, false, false
            ) {
            GameEffectClickable = true;
            Actions = new List<string>() { "" };
            Costs = new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA } };
            Allowed = new List<List<TurnPhase_Enum>>() { new List<TurnPhase_Enum>() { TurnPhase_Enum.StartTurn, TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle, TurnPhase_Enum.AfterBattle } };
            BattleAllowed = new List<List<BattlePhase_Enum>>() { new List<BattlePhase_Enum>() { BattlePhase_Enum.Provoke, BattlePhase_Enum.RangeSiege, BattlePhase_Enum.Block, BattlePhase_Enum.AssignDamage, BattlePhase_Enum.Attack, BattlePhase_Enum.EndOfBattle } };
        }

        public override Image_Enum CardImage {
            get {
                return BasicUtil.Convert_CrystalToManaDieImageId((Crystal_Enum)D.LocalPlayer.GameEffects[GameEffect_Enum.T_ManaSteal].Values[0]);
            }
        }

        public override GameAPI ActionValid_00(GameAPI ar) {
            D.Action.Clear();
            ar.AddMana((Crystal_Enum)ar.P.GameEffects[GameEffect_Enum.T_ManaSteal].Values[0]);
            ar.RemoveGameEffect(GameEffect_Enum.T_ManaSteal);
            ManaPoolData mpd = ar.P.ManaPoolFull.Find(mp => mp.Status.Equals(ManaPool_Enum.ManaSteal));
            mpd.Status = ManaPool_Enum.Used;
            return ar;
        }
    }
}
