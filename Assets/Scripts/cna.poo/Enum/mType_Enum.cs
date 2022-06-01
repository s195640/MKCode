namespace cna.poo {
    public enum mType_Enum {
        NA,
        OnConnect = 1,
        OnDisconnect = 2,
        OnServerDisconnect,
        RequestGameList,
        LobbyGame,
        RequestJoinGame,
        RequestJoinGameRejected,
        GameData_Host,
        GameData_Request,
        GameData_Demand,
        GameData_Destroy,
        Chat,
        GameLog,
    }
}
