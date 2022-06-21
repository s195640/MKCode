using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace cna.poo {
    [Serializable]
    public class V2IntVO : BaseData {
        [SerializeField] private int x;
        [SerializeField] private int y;
        public static V2IntVO zero { get { return new V2IntVO(0, 0); } }
        public static V2IntVO one { get { return new V2IntVO(1, 1); } }

        public V2IntVO() { }
        public V2IntVO(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public V2IntVO(Vector3Int v) {
            x = v.x;
            y = v.y;
        }
        public V2IntVO(Vector2Int v) {
            x = v.x;
            y = v.y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        public Vector2Int Vector2Int { get => new Vector2Int(x, y); }
        public Vector3Int Vector3Int { get => new Vector3Int(x, y, 0); }

        public override bool Equals(object obj) {
            if (obj == null) {
                return false;
            }
            Type t = obj.GetType();
            if (t.Equals(typeof(Vector3Int))) {
                return x == ((Vector3Int)obj).x && y == ((Vector3Int)obj).y;
            } else if (t.Equals(typeof(Vector2Int))) {
                return x == ((Vector2Int)obj).x && y == ((Vector2Int)obj).y;
            } else if (t.Equals(typeof(V2IntVO))) {
                return x == ((V2IntVO)obj).x && y == ((V2IntVO)obj).y;
            }
            return false;
        }

        public override int GetHashCode() {
            return HashCode.Combine(x, y);
        }

        public static V2IntVO operator +(V2IntVO a, V2IntVO b) => new V2IntVO(a.X + b.X, a.Y + b.Y);
        public static V2IntVO operator -(V2IntVO a, V2IntVO b) => new V2IntVO(a.X - b.X, a.Y - b.Y);
    }
}
