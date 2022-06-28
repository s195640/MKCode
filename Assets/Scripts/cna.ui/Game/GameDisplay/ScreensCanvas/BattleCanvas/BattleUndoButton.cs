using cna.poo;
using UnityEngine;

namespace cna.ui {
    public class BattleUndoButton : MonoBehaviour {
        [SerializeField] private ButtonContainer Provoke;
        [SerializeField] private ButtonContainer Range;
        [SerializeField] private ButtonContainer Block;
        [SerializeField] private ButtonContainer Damage;
        [SerializeField] private ButtonContainer Attack;

        enum ButtonState_ENUM {
            COMPLETE,
            ACTIVE,
            DISABLED
        }
        private Color disabledButtonColor = new Color(.7f, .7f, .7f, 1);
        private Color activePhaseColor = new Color(.6f, 1f, .6f, 1);
        private Color completePhaseColor = new Color(.8f, .3f, .3f, 1);

        public void UpdateUI() {
            switch (D.LocalPlayer.Battle.BattlePhase) {
                case BattlePhase_Enum.StartOfBattle: {
                    configureButton(Provoke, ButtonState_ENUM.DISABLED);
                    configureButton(Range, ButtonState_ENUM.DISABLED);
                    configureButton(Block, ButtonState_ENUM.DISABLED);
                    configureButton(Damage, ButtonState_ENUM.DISABLED);
                    configureButton(Attack, ButtonState_ENUM.DISABLED);
                    break;
                }
                case BattlePhase_Enum.Provoke: {
                    configureButton(Provoke, ButtonState_ENUM.ACTIVE);
                    configureButton(Range, ButtonState_ENUM.DISABLED);
                    configureButton(Block, ButtonState_ENUM.DISABLED);
                    configureButton(Damage, ButtonState_ENUM.DISABLED);
                    configureButton(Attack, ButtonState_ENUM.DISABLED);
                    break;
                }
                case BattlePhase_Enum.RangeSiege: {
                    configureButton(Provoke, ButtonState_ENUM.COMPLETE);
                    configureButton(Range, ButtonState_ENUM.ACTIVE);
                    configureButton(Block, ButtonState_ENUM.DISABLED);
                    configureButton(Damage, ButtonState_ENUM.DISABLED);
                    configureButton(Attack, ButtonState_ENUM.DISABLED);
                    break;
                }
                case BattlePhase_Enum.Block: {
                    configureButton(Provoke, ButtonState_ENUM.COMPLETE);
                    configureButton(Range, ButtonState_ENUM.COMPLETE);
                    configureButton(Block, ButtonState_ENUM.ACTIVE);
                    configureButton(Damage, ButtonState_ENUM.DISABLED);
                    configureButton(Attack, ButtonState_ENUM.DISABLED);
                    break;
                }
                case BattlePhase_Enum.AssignDamage: {
                    configureButton(Provoke, ButtonState_ENUM.COMPLETE);
                    configureButton(Range, ButtonState_ENUM.COMPLETE);
                    configureButton(Block, ButtonState_ENUM.COMPLETE);
                    configureButton(Damage, ButtonState_ENUM.ACTIVE);
                    configureButton(Attack, ButtonState_ENUM.DISABLED);
                    break;
                }
                case BattlePhase_Enum.Attack: {
                    configureButton(Provoke, ButtonState_ENUM.COMPLETE);
                    configureButton(Range, ButtonState_ENUM.COMPLETE);
                    configureButton(Block, ButtonState_ENUM.COMPLETE);
                    configureButton(Damage, ButtonState_ENUM.COMPLETE);
                    configureButton(Attack, ButtonState_ENUM.ACTIVE);
                    break;
                }
                case BattlePhase_Enum.EndOfBattle: {
                    configureButton(Provoke, ButtonState_ENUM.COMPLETE);
                    configureButton(Range, ButtonState_ENUM.COMPLETE);
                    configureButton(Block, ButtonState_ENUM.COMPLETE);
                    configureButton(Damage, ButtonState_ENUM.COMPLETE);
                    configureButton(Attack, ButtonState_ENUM.COMPLETE);
                    break;
                }
            }
        }

        private void configureButton(ButtonContainer b, ButtonState_ENUM state) {
            switch (state) {
                case ButtonState_ENUM.COMPLETE: {
                    b.ButtonBolor = completePhaseColor;
                    b.Active = true;
                    break;
                }
                case ButtonState_ENUM.ACTIVE: {
                    b.ButtonBolor = activePhaseColor;
                    b.Active = true;
                    break;
                }
                case ButtonState_ENUM.DISABLED: {
                    b.ButtonBolor = disabledButtonColor;
                    b.Active = false;
                    break;
                }
            }
        }
    }
}
