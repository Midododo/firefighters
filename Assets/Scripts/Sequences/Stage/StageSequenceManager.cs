using Marvest.DesignPatterns;
using Marvest.Parties;
using Marvest.SceneGlobalVariables.Stage;
using Marvest.StageData.Waves;
using Marvest.TransitionEffects;
using UnityEngine.SceneManagement;

namespace Marvest.Sequences.Stage
{
    public class StageSequenceManager : StateMachine<StageSequenceState>
    {
        public UiWaveStartAnimation WaveStartAnimation;

        private string loadSceneName;

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            GoToState(StageSequenceState.Start);
        }

        private void InitializeStateMachine()
        {
            // Start
            {
                var state = new State<StageSequenceState>(StageSequenceState.Start);
                state.SetUpAction = OnSetUpStart;
                AddState(state);
            }
            // FeedInTransition
            {
                var state = new State<StageSequenceState>(StageSequenceState.FeedInTransition);
                state.SetUpAction = OnSetUpFeedInTransition;
                state.UpdateAction = OnUpdateFeedInTransition;
                AddState(state);
            }
            // PlayerPartyTurn
            {
                var state = new State<StageSequenceState>(StageSequenceState.PlayerPartyTurn);
                state.SetUpAction = OnSetUpPlayerPartyTurn;
                state.UpdateAction = OnUpdatePlayerPartyTurn;
                AddState(state);
            }
            // EnemyExecutedAll
            {
                var state = new State<StageSequenceState>(StageSequenceState.EnemyExecutedAll);
                state.SetUpAction = OnSetUpEnemyExecutedAll;
                AddState(state);
            }
            // EnemyPartyTurn
            {
                var state = new State<StageSequenceState>(StageSequenceState.EnemyPartyTurn);
                state.SetUpAction = OnSetUpEnemyPartyTurn;
                state.UpdateAction = OnUpdateEnemyPartyTurn;
                AddState(state);
            }
            // EnemyPartyTurnEnd
            {
                var state = new State<StageSequenceState>(StageSequenceState.EnemyPartyTurnEnd);
                state.SetUpAction = OnSetUpEnemyPartyTurnEnd;
                AddState(state);
            }
            // LoadNextWave
            {
                var state = new State<StageSequenceState>(StageSequenceState.LoadNextWave);
                state.SetUpAction = OnSetUpLoadNextWave;
                state.UpdateAction = OnUpdateLoadNextWave;
                AddState(state);
            }
            // WaveStart
            {
                var state = new State<StageSequenceState>(StageSequenceState.WaveStart);
                state.SetUpAction = OnSetUpWaveStart;
                state.UpdateAction = OnUpdateWaveStart;
                AddState(state);
            }
            // GameClear
            {
                var state = new State<StageSequenceState>(StageSequenceState.GameClear);
                state.SetUpAction = OnSetUpGameClear;
                AddState(state);
            }
            // GameOver
            {
                var state = new State<StageSequenceState>(StageSequenceState.GameOver);
                state.SetUpAction = OnSetUpGameOver;
                AddState(state);
            }
            // FeedOutTransition
            {
                var state = new State<StageSequenceState>(StageSequenceState.FeedOutTransition);
                state.SetUpAction = OnSetUpFeedOutTransition;
                state.UpdateAction = OnUpdateFeedOutTransition;
                AddState(state);
            }
            // End
            {
                var state = new State<StageSequenceState>(StageSequenceState.End);
                state.SetUpAction = OnSetUpEnd;
                AddState(state);
            }
        }

        #region state start

        private void OnSetUpStart()
        {
            GoToState(StageSequenceState.FeedInTransition);
        }

        #endregion state start

        #region state feed in transition

        private void OnSetUpFeedInTransition()
        {
            StageSceneGlobalVariables.Instance.TransitionEffect.TransitionEffectSpriteMask.SetTransitionType(TransitionType.Circle);
            StageSceneGlobalVariables.Instance.TransitionEffect.ExecuteFeedIn();
        }

        private void OnUpdateFeedInTransition()
        {
            if (StageSceneGlobalVariables.Instance.TransitionEffect.IsComplete())
            {
                GoToState(StageSequenceState.WaveStart);
            }
        }

        #endregion state feed in transition

        #region state player party turn

        private void OnSetUpPlayerPartyTurn()
        {
            var playerParty = StageSceneGlobalVariables.Instance.PlayerParty;

            if (playerParty.CanAction())
            {
                playerParty.GoToState(PlayerPartyState.TurnStart);
            }
            else
            {
                GoToState(StageSequenceState.EnemyPartyTurn);
            }
        }

        private void OnUpdatePlayerPartyTurn()
        {
            if (StageSceneGlobalVariables.Instance.PlayerParty.IsTurnEnd())
            {
                if (StageSceneGlobalVariables.Instance.EnemyParty.IsAllDead())
                {
                    GoToState(StageSequenceState.EnemyExecutedAll);
                }
                else
                {
                    GoToState(StageSequenceState.EnemyPartyTurn);
                }
            }
        }

        #endregion state player party turn

        #region state enemy executed all

        private void OnSetUpEnemyExecutedAll()
        {
            GoToState(StageSequenceState.LoadNextWave);
        }

        #endregion state enemy executed all

        #region state enemy party turn

        private void OnSetUpEnemyPartyTurn()
        {
            var enemyParty = StageSceneGlobalVariables.Instance.EnemyParty;
            if (enemyParty.IsAllDead())
            {
                GoToState(StageSequenceState.EnemyExecutedAll);
                return;
            }

            enemyParty.GoToState(EnemyPartyState.TurnStart);
        }

        private void OnUpdateEnemyPartyTurn()
        {
			int PartyHP = StageSceneGlobalVariables.Instance.PlayerParty.HitPoint;
			if(PartyHP <= 0)
			{
				GoToState(StageSequenceState.GameOver);
			}

            if (StageSceneGlobalVariables.Instance.EnemyParty.IsTurnEnd())
            {
                GoToState(StageSequenceState.EnemyPartyTurnEnd);
            }
        }

        #endregion state enemy party turn

        #region state enemy party turn end

        private void OnSetUpEnemyPartyTurnEnd()
        {
            GoToState(StageSequenceState.PlayerPartyTurn);
        }

        #endregion state enemy party turn

        #region state load next wave

        private void OnSetUpLoadNextWave()
        {
            StageSceneGlobalVariables.Instance.Waves.GoToState(WavesState.LoadWave);
        }

        private void OnUpdateLoadNextWave()
        {
            var waves = StageSceneGlobalVariables.Instance.Waves;

            if (waves.IsStateSame(WavesState.FinishLastWave))
            {
                GoToState(StageSequenceState.GameClear);
            }
            else if (waves.IsStateSame(WavesState.WaveStandby))
            {
                GoToState(StageSequenceState.WaveStart);
            }
        }

        #endregion state load next wave

        #region state wave start

        private void OnSetUpWaveStart()
        {
            var waves = StageSceneGlobalVariables.Instance.Waves;
            StageSceneGlobalVariables.Instance.UiWaveNumber.SetWave(waves.CurrentWave);
            WaveStartAnimation.SetWaveText(waves.CurrentWave, waves);
            WaveStartAnimation.Run();
        }

        private void OnUpdateWaveStart()
        {
            if (WaveStartAnimation.IsFinish)
            {
                GoToState(StageSequenceState.PlayerPartyTurn);
            }
        }

        #endregion state wave start

        #region state game clear

        private void OnSetUpGameClear()
        {
            loadSceneName = "GameClear";
            GoToState(StageSequenceState.FeedOutTransition);
        }

        #endregion state game clear

        #region state game over

        private void OnSetUpGameOver()
        {
            loadSceneName = "GameOver";
            GoToState(StageSequenceState.FeedOutTransition);
        }

        #endregion state game over

        #region state feed out transition

        private void OnSetUpFeedOutTransition()
        {
            StageSceneGlobalVariables.Instance.TransitionEffect.TransitionEffectSpriteMask.SetTransitionType(TransitionType.Line);
            StageSceneGlobalVariables.Instance.TransitionEffect.ExecuteFeedOut();
        }

        private void OnUpdateFeedOutTransition()
        {
            if (StageSceneGlobalVariables.Instance.TransitionEffect.IsComplete())
            {
                SceneManager.LoadScene(loadSceneName);
            }
        }

        #endregion state feed out transition

        #region state end

        private void OnSetUpEnd()
        {
            GoToState(StageSequenceState.PlayerPartyTurn);
        }

        #endregion state end
    }
}