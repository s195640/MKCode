using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cna.poo {
    public enum ClientState_Enum {
        NOT_CONNECTED = 0,
        SINGLE_PLAYER = 100,
        MULTI_PLAYER = 200,
        CONNECTING,
        CONNECTED,
        CONNECTED_JOINING_GAME,
        CONNECTED_HOST,
        CONNECTED_PLAYER
    }
}
