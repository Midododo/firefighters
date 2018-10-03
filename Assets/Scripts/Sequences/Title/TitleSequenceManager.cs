using Marvest.DesignPatterns;
using Marvest.TransitionEffects;
using UnityEngine.SceneManagement;

namespace Marvest.Sequences.Title
{
    public class TitleSequenceManager : StateMachine<TitleSequenceState>
    {
        public TransitionEffect TransitionEffect;

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            GoToState(TitleSequenceState.Start);
        }

        private void InitializeStateMachine()
        {
            // Start
            {
                var state = new State<TitleSequenceState>(TitleSequenceState.Start);
                state.SetUpAction = OnSetUpStart;
                AddState(state);
            }
            // FeedInTransition
            {
                var state = new State<TitleSequenceState>(TitleSequenceState.FeedInTransition);
                state.SetUpAction = OnSetUpFeedInTransition;
                state.UpdateAction = OnUpdateFeedInTransition;
                AddState(state);
            }
            // Idle
            {
                var state = new State<TitleSequenceState>(TitleSequenceState.Idle);
                AddState(state);
            }
            // FeedOutTransition
            {
                var state = new State<TitleSequenceState>(TitleSequenceState.FeedOutTransition);
                state.SetUpAction = OnSetUpFeedOutTransition;
                state.UpdateAction = OnUpdateFeedOutTransition;
                AddState(state);
            }
        }

        #region state start

        private void OnSetUpStart()
        {
            GoToState(TitleSequenceState.FeedInTransition);
        }

        #endregion state start

        #region state feed in transition

        public void OnSetUpFeedInTransition()
        {
            TransitionEffect.TransitionEffectSpriteMask.SetTransitionType(TransitionType.Line);
            TransitionEffect.ExecuteFeedIn();
        }

        public void OnUpdateFeedInTransition()
        {
            if (TransitionEffect.IsComplete())
            {
                GoToState(TitleSequenceState.Idle);
            }
        }

        #endregion state feed in transition

        #region state idle

        public void OnClickScreen()
        {
            if (!IsStateSame(TitleSequenceState.Idle))
            {
                return;
            }

            GoToState(TitleSequenceState.FeedOutTransition);
        }

        #endregion state idle

        #region state feed out transition

        public void OnSetUpFeedOutTransition()
        {
            TransitionEffect.TransitionEffectSpriteMask.SetTransitionType(TransitionType.Circle);
            TransitionEffect.ExecuteFeedOut();
        }

        public void OnUpdateFeedOutTransition()
        {
            if (TransitionEffect.IsComplete())
            {
				SceneManager.LoadScene("Stage");
            }
        }

        #endregion state feed out transition
    }
}