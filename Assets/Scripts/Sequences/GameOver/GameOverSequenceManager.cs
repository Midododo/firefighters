using UnityEngine.SceneManagement;
using Marvest.DesignPatterns;
using Marvest.TransitionEffects;

namespace Marvest.Sequences.GameOver
{
    public class GameOverSequenceManager : StateMachine<GameOverSequenceState>
    {
        public TransitionEffect TransitionEffect;

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            GoToState(GameOverSequenceState.Start);
        }

        private void InitializeStateMachine()
        {
            // Start
            {
                var state = new State<GameOverSequenceState>(GameOverSequenceState.Start);
                state.SetUpAction = OnSetUpStart;
                AddState(state);
            }
            // FeedInTransition
            {
                var state = new State<GameOverSequenceState>(GameOverSequenceState.FeedInTransition);
                state.SetUpAction = OnSetUpFeedInTransition;
                state.UpdateAction = OnUpdateFeedInTransition;
                AddState(state);
            }
            // Idle
            {
                var state = new State<GameOverSequenceState>(GameOverSequenceState.Idle);
                AddState(state);
            }
            // FeedOutTransition
            {
                var state = new State<GameOverSequenceState>(GameOverSequenceState.FeedOutTransition);
                state.SetUpAction = OnSetUpFeedOutTransition;
                state.UpdateAction = OnUpdateFeedOutTransition;
                AddState(state);
            }
        }

        #region state start

        private void OnSetUpStart()
        {
            GoToState(GameOverSequenceState.FeedInTransition);
        }

        #endregion state start

        #region state feed in transition

        private void OnSetUpFeedInTransition()
        {
            TransitionEffect.TransitionEffectSpriteMask.SetTransitionType(TransitionType.Line);
            TransitionEffect.ExecuteFeedIn();
        }

        private void OnUpdateFeedInTransition()
        {
            if (TransitionEffect.IsComplete())
            {
                GoToState(GameOverSequenceState.Idle);
            }
        }

        #endregion state feed in transition


        #region state idle

        public void OnClickScreen()
        {
            if (!IsStateSame(GameOverSequenceState.Idle))
            {
                return;
            }

            GoToState(GameOverSequenceState.FeedOutTransition);
        }

        #endregion state idle

        #region state feed out transition

        public void OnSetUpFeedOutTransition()
        {
            TransitionEffect.TransitionEffectSpriteMask.SetTransitionType(TransitionType.Line);
            TransitionEffect.ExecuteFeedOut();
        }

        public void OnUpdateFeedOutTransition()
        {
            if (TransitionEffect.IsComplete())
            {
                SceneManager.LoadScene("Title");
            }
        }

        #endregion state feed out transition
    }
}