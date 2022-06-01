using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class CNAMap<I, J> {
        [SerializeField] private List<I> keys;
        [SerializeField] private List<J> values;

        public CNAMap() {
            keys = new List<I>();
            values = new List<J>();
        }

        public List<I> Keys { get => keys; set => keys = value; }
        public List<J> Values { get => values; set => values = value; }
        public int Count { get => keys.Count; }

        public void Add(I key, J value) {
            if (ContainsKey(key)) {
                int index = IndexOf(key);
                keys.RemoveAt(index);
                values.RemoveAt(index);
            }
            keys.Add(key);
            values.Add(value);
        }
        public void Remove(I i) {
            int index = IndexOf(i);
            if (index >= 0) {
                keys.RemoveAt(index);
                values.RemoveAt(index);
            }
        }

        public void RemoveRange(List<I> i) {
            foreach (I j in i)
                Remove(j);
        }

        public void Clear() {
            keys.Clear();
            values.Clear();
        }

        public bool ContainsKey(I key) {
            return Keys.Contains(key);
        }
        public bool ContainsKeyAny(params I[] k) {
            foreach (I _k in k) {
                if (ContainsKey(_k)) {
                    return true;
                }
            }
            return false;
        }
        public J this[I i] {
            get => values[IndexOf(i)];
        }

        public int IndexOf(I i) {
            return keys.FindIndex(k => k.Equals(i));
        }
    }
}
