using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class PlayerDeckData : BaseData {

        [SerializeField] private CNAMap<int, WrapList<CardState_Enum>> state;
        [SerializeField] private CNAMap<int, int> banners;
        [SerializeField] private List<int> hand;
        [SerializeField] private List<int> deck;
        [SerializeField] private List<int> discard;
        [SerializeField] private List<int> unit;
        [SerializeField] private List<int> skill;
        [SerializeField] private V2IntVO handSize = V2IntVO.zero;
        [SerializeField] private int unitHandLimit;
        [SerializeField] private int tacticsCardId;

        public PlayerDeckData() {
            state = new CNAMap<int, WrapList<CardState_Enum>>();
            banners = new CNAMap<int, int>();
            hand = new List<int>();
            deck = new List<int>();
            discard = new List<int>();
            unit = new List<int>();
            skill = new List<int>();
            handSize = new V2IntVO(5, 0);
            unitHandLimit = 1;
            tacticsCardId = 0;
        }
        public CNAMap<int, WrapList<CardState_Enum>> State { get => state; set => state = value; }
        public List<int> Hand { get => hand; set => hand = value; }
        public List<int> Deck { get => deck; set => deck = value; }
        public List<int> Discard { get => discard; set => discard = value; }
        public List<int> Unit { get => unit; set => unit = value; }
        public List<int> Skill { get => skill; set => skill = value; }
        public int UnitHandLimit { get => unitHandLimit; set => unitHandLimit = value; }
        public V2IntVO HandSize { get => handSize; set => handSize = value; }
        public int TotalHandSize { get => HandSize.X + HandSize.Y; }
        public CNAMap<int, int> Banners { get => banners; set => banners = value; }
        public int TacticsCardId { get => tacticsCardId; set => tacticsCardId = value; }

        public void AddState(int cardId, CardState_Enum state) {
            if (State.ContainsKey(cardId)) {
                State[cardId].Add(state);
            } else {
                WrapList<CardState_Enum> l = new WrapList<CardState_Enum>();
                l.Add(state);
                State.Add(cardId, l);
            }
        }
        public void ClearState(int cardId) {
            if (State.ContainsKey(cardId)) {
                State.Remove(cardId);
            }
        }

        public bool StateContainsAny(int cardId, params CardState_Enum[] states) {
            if (State.ContainsKey(cardId)) {
                return State[cardId].ContainsAny(states);
            }
            return false;
        }
        public bool StateContains(int cardId, CardState_Enum state) {
            if (State.ContainsKey(cardId)) {
                return State[cardId].Values.Contains(state);
            }
            return false;
        }

        public void Clear() {
            Hand.Clear();
            Deck.Clear();
            Discard.Clear();
            Skill.Clear();
            Unit.Clear();
            State.Clear();
            Banners.Clear();
            handSize = new V2IntVO(5, 0);
            unitHandLimit = 1;
        }

        public void ClearNewRound() {
            tacticsCardId = 0;
            Deck.AddRange(Discard);
            Deck.AddRange(Hand.FindAll(c => !StateContains(c, CardState_Enum.Trashed)));
            Discard.Clear();
            Hand.Clear();
            foreach (int u in Unit.ToArray()) {
                if (StateContains(u, CardState_Enum.Unit_Paralyzed)) {
                    Unit.Remove(u);
                    State.Remove(u);
                } else if (StateContains(u, CardState_Enum.Unit_Exhausted)) {
                    State[u].Remove(CardState_Enum.Unit_Exhausted);
                    if (State[u].Count == 0) {
                        State.Remove(u);
                    }
                } else if (StateContains(u, CardState_Enum.Unit_UsedInBattle)) {
                    State[u].Remove(CardState_Enum.Unit_UsedInBattle);
                    if (State[u].Count == 0) {
                        State.Remove(u);
                    }
                }
            }
            State.RemoveRange(State.Keys.FindAll(c => !Unit.Contains(c)));
        }
    }
}
