
using System;
using cna.poo;

namespace cna.connector {
    public class SoloConnector : BaseConnector {
        public SoloConnector(string un, Action<wsData> onEvent) {
            player = new PlayerData(un, 0);
            OnEvent = onEvent;
        }
    }
}