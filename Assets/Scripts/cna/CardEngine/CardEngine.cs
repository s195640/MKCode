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
        public abstract void ProcessActionResultVO(GameAPI r);
        public abstract void PayForAction(GameAPI ar, List<Crystal_Enum> cost, Action<GameAPI> cancelCallback, Action<GameAPI> acceptCallback, bool all = true);
        public abstract bool isConformationCanvasOpen();
        public abstract void SelectSingleCard(GameAPI ar, Action<GameAPI> cancelCallback, Action<GameAPI> acceptCallback, bool allowNone = false);
        public abstract void SelectOptions(GameAPI ar, Action<GameAPI> cancelCallback, Action<GameAPI> acceptCallback, params OptionVO[] options);
        public abstract void Clear();
        public abstract bool CheckTurnAndUI(GameAPI ar);
        public abstract void SelectLevelUp(GameAPI ar, Action<GameAPI> callback, List<int> actionOffering, List<int> skillOffering, List<int> skills);
        public abstract void SelectCards(GameAPI ar, List<int> cards, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<GameAPI>> buttonCallback, List<bool> buttonForce);
        public abstract void SelectManaDie(GameAPI ar, List<Image_Enum> die, string title, string description, V2IntVO selectCount, Image_Enum selectionImage, List<string> buttonText, List<Color> buttonColor, List<Action<GameAPI>> buttonCallback, List<bool> buttonForce);
        public abstract void SelectYesNo(string title, string description, Action yes, Action no);
        public abstract void SelectAcceptPanel(GameAPI ar, string head, string body, List<Action<GameAPI>> callbacks, List<string> buttonText, List<Color32> buttonColor, Color32 backgroundColor);
        public abstract void WaitOnServerPanel(GameAPI ar);
    }
}
