using System;
using System.Collections.Generic;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class CNAMap<I, J> : BaseData
        where I : new()
        where J : new() {
        [SerializeField] private List<I> keys;
        [SerializeField] private List<J> values;

        public CNAMap() {
            keys = new List<I>();
            values = new List<J>();
        }

        public CNAMap(List<I> k, List<J> j) {
            keys = k;
            values = j;
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

        public override string Serialize() {
            string delimiter = "^";
            string k = "";
            if (typeof(I).IsSubclassOf(typeof(BaseData))) {
                bool flag = false;
                foreach (I item in keys) {
                    if (flag) k += delimiter; else flag = true;
                    k += (item as BaseData).Serialize();
                }
            } else if (typeof(I) == typeof(int)) {
                k = string.Join(delimiter, keys);
            } else if (typeof(I) == typeof(GameEffect_Enum)) {
                bool flag = false;
                foreach (I item in keys) {
                    if (flag) k += delimiter; else flag = true;
                    k += "" + (int)(GameEffect_Enum)(object)item;
                }
            }
            string v = "";
            if (typeof(J).IsSubclassOf(typeof(BaseData))) {
                bool flag = false;
                foreach (J item in values) {
                    if (flag) v += delimiter; else flag = true;
                    v += (item as BaseData).Serialize();
                }
            } else if (typeof(J) == typeof(int)) {
                v = string.Join(delimiter, values);
            }
            return "[[" + k + "]=[" + v + "]]";
        }

        public override void Deserialize(string data) {
            string delimiter = "^";
            string[] d = data.Substring(1, data.Length - 2).Split("=");
            if (typeof(I).IsSubclassOf(typeof(BaseData))) {
                keys = CNASerialize.Split(d[0], delimiter).ConvertAll(i => {
                    I item = new I();
                    (item as BaseData).Deserialize(i);
                    return item;
                });
            } else if (typeof(I) == typeof(int)) {
                List<int> k;
                CNASerialize.Dz(d[0], out k);
                keys = (List<I>)Convert.ChangeType(k, typeof(List<I>));
            } else if (typeof(I) == typeof(GameEffect_Enum)) {
                List<GameEffect_Enum> k;
                CNASerialize.Dz(d[0], out k);
                keys = (List<I>)Convert.ChangeType(k, typeof(List<I>));
            }
            if (typeof(J).IsSubclassOf(typeof(BaseData))) {
                values = CNASerialize.Split(d[1], delimiter).ConvertAll(i => {
                    J item = new J();
                    (item as BaseData).Deserialize(i);
                    return item;
                });
            } else if (typeof(J) == typeof(int)) {
                List<int> v;
                CNASerialize.Dz(d[0], out v);
                values = (List<J>)Convert.ChangeType(v, typeof(List<J>));
            }
        }
    }
}
