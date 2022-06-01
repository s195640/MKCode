using System;
using System.Collections.Generic;

namespace cna.poo {
    [Serializable]
    public class wsMsg : Data {
        public List<int> u = new List<int>();
        public wsData d;
    }
}
