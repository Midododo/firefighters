namespace Marvest.Sequences.Stage
{
    public enum StageSequenceState
    {
        Start,
        FeedInTransition,
        PlayerPartyTurn,
        EnemyExecutedAll,
        EnemyPartyTurn,
        EnemyPartyTurnEnd,
        LoadNextWave,
        WaveStart,
        GameClear,
        GameOver,
        FeedOutTransition,
        End,
    }
}