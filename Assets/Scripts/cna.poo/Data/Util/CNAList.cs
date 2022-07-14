using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;

namespace cna.poo {
    [Serializable]
    public class CNAList<I> : BaseData
        where I : new() {
        [SerializeField] private List<I> values;
        public CNAList() {
            values = new List<I>();
        }
        public CNAList(ICollection<I> p) {
            values = new List<I>();
            values.AddRange(p);
        }
        public CNAList(params I[] param) {
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

        public override string Serialize() {
            string delimiter = "^";
            string v = "";
            if (typeof(I).IsSubclassOf(typeof(BaseData))) {
                bool flag = false;
                foreach (I item in values) {
                    if (flag) v += delimiter; else flag = true;
                    v += (item as BaseData).Serialize();
                }
            } else if (typeof(I) == typeof(int)) {
                v = string.Join(delimiter, values);
            } else if (typeof(I) == typeof(CardState_Enum)) {
                bool flag = false;
                foreach (I item in values) {
                    if (flag) v += delimiter; else flag = true;
                    v += "" + (int)(CardState_Enum)(object)item;
                }
            }
            return "[" + v + "]";
        }
        public override void Deserialize(string data) {
            string delimiter = "^";
            if (typeof(I).IsSubclassOf(typeof(BaseData))) {
                values = CNASerialize.Split(data, delimiter).ConvertAll(i => {
                    I item = new I();
                    (item as BaseData).Deserialize(i);
                    return item;
                });
            } else if (typeof(I) == typeof(int)) {
                List<int> k;
                CNASerialize.Dz(data, out k);
                values = (List<I>)Convert.ChangeType(k, typeof(List<I>));
            } else if (typeof(I) == typeof(CardState_Enum)) {
                List<CardState_Enum> k;
                CNASerialize.Dz(data, out k);
                values = (List<I>)Convert.ChangeType(k, typeof(List<I>));
            }
        }
    }
}
