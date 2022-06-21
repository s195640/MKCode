using System;
using System.Collections.Generic;

namespace cna.poo {
    [Serializable]
    public class wsMsg : BaseData {
        public List<int> u = new List<int>();
        public wsData d;
    }
}
