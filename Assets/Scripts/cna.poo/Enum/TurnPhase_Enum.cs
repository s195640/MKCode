namespace cna.poo {
    public enum TurnPhase_Enum {
        NA = 0,
        NotTurn,
        TacticsSelect,
        TacticsAction,
        TacticsHost,
        TacticsEnd,
        //  Player Turn
        HostSaveGame,
        SetupTurn,
        NotifyTurn,
        StartTurn,
        Move,
        Influence,
        Battle,
        AfterBattle,
        Resting,
        Exhaustion,
        Reward,
        EndTurn
    }
}
