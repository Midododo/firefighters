using UnityEngine.SceneManagement;
using Marvest.DesignPatterns;
using Marvest.TransitionEffects;

namespace Marvest.Sequences.GameClear
{
    public class GameClearSequenceManager : StateMachine<GameClearSequenceState>
    {
        public TransitionEffect TransitionEffect;

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            GoToState(GameClearSequenceState.Start);
        }

        private void InitializeStateMachine()
        {
            // Start
            {
                var state = new State<GameClearSequenceState>(GameClearSequenceState.Start);
                state.SetUpAction = OnSetUpStart;
                AddState(state);
            }
            // FeedInTransition
            {
                var state = new State<GameClearSequenceState>(GameClearSequenceState.FeedInTransition);
                state.SetUpAction = OnSetUpFeedInTransition;
                state.UpdateAction = OnUpdateFeedInTransition;
                AddState(state);
            }
            // Idle
            {
                var state = new State<GameClearSequenceState>(GameClearSequenceState.Idle);
                AddState(state);
            }
            // FeedOutTransition
            {
                var state = new State<GameClearSequenceState>(GameClearSequenceState.FeedOutTransition);
                state.SetUpAction = OnSetUpFeedOutTransition;
                state.UpdateAction = OnUpdateFeedOutTransition;
                AddState(state);
            }
        }

        #region state start

        private void OnSetUpStart()
        {
            GoToState(GameClearSequenceState.FeedInTransition);
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
                GoToState(GameClearSequenceState.Idle);
            }
        }

        #endregion state feed in transition

        #region state idle

        public void OnClickScreen()
        {
            if (!IsStateSame(GameClearSequenceState.Idle))
            {
                return;
            }

            GoToState(GameClearSequenceState.FeedOutTransition);
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