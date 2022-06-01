using System;
using System.Collections.Generic;
using cna.poo;
using UnityEngine;

namespace cna {
    public abstract class CardEngine : MonoBehaviour {
        [SerializeField] protected int uniqueCardId;
        [SerializeField] protected CardVO card;
        [SerializeField] protected CardHolder_Enum cardHolder;

        public CardHolder_Enum CardHolder { get => cardHolder; set => cardHolder = value; }
        public int UniqueCardId { get => uniqueCardId; set => uniqueCardId = value; }
        public CardVO Card { get => card; set => card = value; }
        public abstract void UpdateUI();
        public abstract void Msg(string msg);
        public abstract void ProcessActionResultVO(ActionResultVO r);
        public abstract void PayForAction(ActionResultVO ar, List<Crystal_Enum> cost, Action<ActionResultVO> cancelCallback, Action<ActionResultVO> acceptCallback, bool all = true);
        public abstract bool isConformationCanvasOpen();
        public abstract void SelectSingleCard(ActionResultVO ar, Action<ActionResultVO> cancelCallback, Action<ActionResultVO> acceptCallback, bool allowNone = false);
        public abstract void SelectOptions(ActionResultVO ar, Action<ActionResultVO> cancelCallback, Action<ActionResultVO> acceptCallback, params OptionVO[] options);
        public abstract void Clear();
        public abstract bool CheckTurnAndUI(ActionResultVO ar);
        public abstract void SelectLevelUp(ActionResultVO ar, Action<ActionResultVO> callback, List<int> actionOffering, List<int> skillOffering, List<int> skills);
        public abstract void SelectCards(ActionResultVO ar, List<int> cards, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<ActionResultVO>> buttonCallback, List<bool> buttonForce);
        public abstract void SelectManaDie(ActionResultVO ar, List<Image_Enum> die, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<ActionResultVO>> buttonCallback, List<bool> buttonForce);
        public abstract void SelectYesNo(string title, string description, Action yes, Action no);

    }
}
