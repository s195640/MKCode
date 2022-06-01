using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class WrapList<I> {
        [SerializeField] private List<I> values;
        public WrapList() {
            values = new List<I>();
        }
        public WrapList(params I[] param) {
            values = new List<I>();
            values.AddRange(param);
        }
        public List<I> Values { get => values; set => values = value; }
        public int Count { get => values.Count; }

        public I[] ToArray() { return Values.ToArray(); }

        public void AddRange(IEnumerable<I> collection) {
            Values.AddRange(collection);
        }

        public void Add(I val) {
            Values.Add(val);
        }

        public void Remove(I val) {
            if (Values.Contains(val)) {
                Values.Remove(val);
            }
        }

        public bool Contains(I val) {
            return Values.Contains(val);
        }

        public bool ContainsAny(params I[] vals) {
            List<I> l = new List<I>(vals);
            bool found = false;
            Values.ForEach(v => found = found || l.Contains(v));
            return found;
        }
    }
}
