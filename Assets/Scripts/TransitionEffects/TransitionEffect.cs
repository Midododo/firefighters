using Marvest.DesignPatterns;
using UnityEngine;

namespace Marvest.TransitionEffects
{
    public class TransitionEffect : StateMachine<TransitionEffectState>
    {
        private const float MAX_ALPHA_CUTOFF = 1f;
        private const float MIN_ALPHA_CUTOFF = 0f;

        public float TransitionDuration = 1f;

        public TransitionEffectSpriteMask TransitionEffectSpriteMask;

        private float transitionProgress;

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            // Idle
            {
                var state = new State<TransitionEffectState>(TransitionEffectState.Idle);
                AddState(state);
            }
            // FeedIn
            {
                var state = new State<TransitionEffectState>(TransitionEffectState.FeedIn);
                state.SetUpAction = OnSetUpFeedIn;
                state.UpdateAction = OnUpdateFeedIn;
                state.CleanUpAction = OnCleanUpFeedIn;
                AddState(state);
            }
            // FeedOut
            {
                var state = new State<TransitionEffectState>(TransitionEffectState.FeedOut);
                state.SetUpAction = OnSetUpFeedOut;
                state.UpdateAction = OnUpdateFeedOut;
                AddState(state);
            }
        }

        public bool IsComplete()
        {
            return transitionProgress >= TransitionDuration;
        }

        public void ExecuteFeedIn()
        {
            GoToState(TransitionEffectState.FeedIn);
        }

        public void ExecuteFeedOut()
        {
            GoToState(TransitionEffectState.FeedOut);
        }

        #region state feed in

        private void OnSetUpFeedIn()
        {
            TransitionEffectSpriteMask.SetAlphaCutOff(MIN_ALPHA_CUTOFF);
            transitionProgress = 0f;
        }

        private void OnUpdateFeedIn()
        {
            transitionProgress += Time.deltaTime;
            TransitionEffectSpriteMask.SetAlphaCutOff(MAX_ALPHA_CUTOFF * transitionProgress / TransitionDuration);

            if (IsComplete())
            {
                GoToState(TransitionEffectState.Idle);
            }
        }

        private void OnCleanUpFeedIn()
        {
            TransitionEffectSpriteMask.SetAlphaCutOff(MAX_ALPHA_CUTOFF);
        }

        #endregion state feed in

        #region state feed out

        private void OnSetUpFeedOut()
        {
            TransitionEffectSpriteMask.SetAlphaCutOff(MAX_ALPHA_CUTOFF);
            transitionProgress = 0f;
        }

        private void OnUpdateFeedOut()
        {
            transitionProgress += Time.deltaTime;
            TransitionEffectSpriteMask.SetAlphaCutOff(MAX_ALPHA_CUTOFF - MAX_ALPHA_CUTOFF * transitionProgress / TransitionDuration);
        }

        #endregion state feed out
    }
}