using System.Collections.Generic;
using cna.poo;
namespace cna {
    public class BasicVO : CardActionVO {
        public BasicVO(int uniqueId) : base(
            uniqueId,
            "Basic Action",
            Image_Enum.NA,
            CardType_Enum.BasicAction,
            new List<string> { "+1 to Move, Influence, Attack, or Block" },
            new List<List<Crystal_Enum>> { new List<Crystal_Enum> { Crystal_Enum.NA }, new List<Crystal_Enum> { Crystal_Enum.NA } },
            new List<List<TurnPhase_Enum>> { new List<TurnPhase_Enum>() { TurnPhase_Enum.Move, TurnPhase_Enum.Influence, TurnPhase_Enum.Battle } },
            new List<List<BattlePhase_Enum>> { new List<BattlePhase_Enum>() { BattlePhase_Enum.Block, BattlePhase_Enum.Attack } },
            CardColor_Enum.NA
            ) { }

        private int val = 1;
        private CNAMap<GameEffect_Enum, WrapList<int>> gameEffects;
        private TurnPhase_Enum playerPhase;
        private BattlePhase_Enum playerBattlePhase;

        public override void OnClick_ActionBasicButton(ActionResultVO ar) {
            playerPhase = ar.LocalPlayer.PlayerTurnPhase;
            gameEffects = ar.LocalPlayer.GameEffects;
            playerBattlePhase = ar.LocalPlayer.Battle.BattlePhase;
            if (D.Action.CheckTurnAndUI(ar)) {
                switch (playerPhase) {
                    case TurnPhase_Enum.StartTurn:
                    case TurnPhase_Enum.Move: { ActionMove(ar); break; }
                    case TurnPhase_Enum.Influence: { ActionInfluence(ar); break; }
                    case TurnPhase_Enum.Battle: {
                        switch (playerBattlePhase) {
                            case BattlePhase_Enum.RangeSiege: { ActionRangeSiege(ar); break; }
                            case BattlePhase_Enum.Block: { ActionBlock(ar); break; }
                            case BattlePhase_Enum.AssignDamage: { ActionAssignDamage(ar); break; }
                            case BattlePhase_Enum.Attack: { ActionAttack(ar); break; }
                            default: { ar.ErrorMsg = string.Format("Basic action can only be played during Block & Attack battle phase."); break; }
                        }
                        break;
                    }
                    default: { ar.ErrorMsg = string.Format("Basic action can not be played in the current player phase."); break; }
                }
            }
            D.Action.ProcessActionResultVO(ar);
        }



        private void ActionMove(ActionResultVO ar) {
            ar.AddCardState();
            ar.TurnPhase(TurnPhase_Enum.Move);
            ar.ActionMovement(1);
        }
        private void ActionInfluence(ActionResultVO ar) {
            ar.AddCardState();
            ar.TurnPhase(TurnPhase_Enum.Influence);
            ar.ActionInfluence(1);
        }
        private void ActionRangeSiege(ActionResultVO ar) {
            if (gameEffects.ContainsKey(GameEffect_Enum.AC_Agility02)) {
                ActionMove(ar);
            } else {
                ar.ErrorMsg = string.Format("Basic action can only be played during Block & Attack battle phase.");
            }
        }

        private void ActionBlock(ActionResultVO ar) {
            if (gameEffects.ContainsKeyAny(GameEffect_Enum.AC_Diplomacy01, GameEffect_Enum.AC_Diplomacy02)) {
                ActionInfluence(ar);
            } else {
                ar.AddCardState();
                ar.BattleBlock(new AttackData() { Physical = val });
            }
        }
        private void ActionAttack(ActionResultVO ar) {
            if (gameEffects.ContainsKeyAny(GameEffect_Enum.AC_Agility01, GameEffect_Enum.AC_Agility02)) {
                ar.AddCardState();
                ar.BattleAttack(new AttackData() { Physical = val });
            } else {
                ar.AddCardState();
                ar.BattleAttack(new AttackData() { Physical = val });
            }
        }

        private void ActionAssignDamage(ActionResultVO ar) {
            if (gameEffects.ContainsKey(GameEffect_Enum.CS_WingsOfNight)) {
                ActionMove(ar);
            } else {
                ar.ErrorMsg = string.Format("Basic action can only be played during Block & Attack battle phase.");
            }
        }

    }
}
