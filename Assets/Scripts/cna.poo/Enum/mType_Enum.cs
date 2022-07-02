namespace cna.poo {
    public enum mType_Enum {
        NA,
        OnConnect = 1,
        OnDisconnect = 2,
        OnServerDisconnect,
        OnReconnect,
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
        PlayerData_ToHost,
        PlayerData_FromHost,
    }
}
