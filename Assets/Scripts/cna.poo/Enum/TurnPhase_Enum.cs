namespace cna.poo {
    public enum TurnPhase_Enum {
        NA = 0,
        NotTurn,
        //  Tactics
        TacticsNotTurn,
        TacticsSelect,
        TacticsAction,
        TacticsEnd,
        //  Player Turn
        PlayerNotTurn,
        SetupTurn,
        StartTurn,
        Move,
        Influence,
        Battle,
        AfterBattle,
        Resting,
        Exhaustion,
        Reward,
        EndTurn_TheRightMoment,
        EndTurn,
        EndOfRound,
    }
}
