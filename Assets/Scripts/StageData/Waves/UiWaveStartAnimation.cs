using Marvest.DesignPatterns;
using UnityEngine;
using UnityEngine.UI;

namespace Marvest.StageData.Waves
{
    public class UiWaveStartAnimation : StateMachine<WaveStartAnimationState>
    {
        public Text WaveTitle;

        public float InAnimationDuration;
        public Vector3 InAnimationStartScale;
        private float inAnimationProgress;

        public float IdleAnimationDuration;
        private float idleAnimationProgress;

        public float OutAnimationDuration;
        public Vector3 OutAnimationEndScale;
        private float outAnimationProgress;

        public bool IsFinish
        {
            get { return IsStateSame(WaveStartAnimationState.Finish); }
        }

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Run()
        {
            GoToState(WaveStartAnimationState.InAnimation);
        }

        private void InitializeStateMachine()
        {
            // InAnimation
            {
                var state = new State<WaveStartAnimationState>(WaveStartAnimationState.InAnimation);
                state.SetUpAction = OnSetUpInAnimation;
                state.UpdateAction = OnUpdateInAnimation;
                AddState(state);
            }
            // Idle
            {
                var state = new State<WaveStartAnimationState>(WaveStartAnimationState.Idle);
                state.SetUpAction = OnSetUpIdle;
                state.UpdateAction = OnUpdateIdle;
                AddState(state);
            }
            // OutAnimation
            {
                var state = new State<WaveStartAnimationState>(WaveStartAnimationState.OutAnimation);
                state.SetUpAction = OnSetUpOutAnimation;
                state.UpdateAction = OnUpdateOutAnimation;
                state.CleanUpAction = OnCleanUpOutAnimation;
                AddState(state);
            }
            // Finish
            {
                var state = new State<WaveStartAnimationState>(WaveStartAnimationState.Finish);
                AddState(state);
            }
        }

        public void SetWaveText(Wave wave, Waves waves)
        {
            WaveTitle.text = string.Format("Wave {0} / {1}", wave.WaveNumber, waves.WaveCount);
        }

        #region state in animation

        private void OnSetUpInAnimation()
        {
            gameObject.SetActive(true);
            WaveTitle.rectTransform.localScale = InAnimationStartScale;
            inAnimationProgress = 0f;
        }

        private void OnUpdateInAnimation()
        {
            if (inAnimationProgress >= InAnimationDuration)
            {
                GoToState(WaveStartAnimationState.Idle);
            }

            inAnimationProgress = Mathf.Clamp(inAnimationProgress + Time.deltaTime, 0f, InAnimationDuration);

            WaveTitle.rectTransform.localScale = Vector3.Lerp(
                InAnimationStartScale,
                Vector3.one,
                inAnimationProgress / InAnimationDuration
            );
        }

        #endregion state in animation

        #region state idle

        private void OnSetUpIdle()
        {
            idleAnimationProgress = 0f;
        }

        private void OnUpdateIdle()
        {
            idleAnimationProgress = Mathf.Clamp(idleAnimationProgress + Time.deltaTime, 0f, IdleAnimationDuration);
            if (idleAnimationProgress >= IdleAnimationDuration)
            {
                GoToState(WaveStartAnimationState.OutAnimation);
            }
        }

        #endregion state idle

        #region state out animation

        private void OnSetUpOutAnimation()
        {
            WaveTitle.rectTransform.localScale = Vector3.one;
            outAnimationProgress = 0f;
        }

        private void OnUpdateOutAnimation()
        {
            if (outAnimationProgress >= OutAnimationDuration)
            {
                GoToState(WaveStartAnimationState.Finish);
            }

            outAnimationProgress = Mathf.Clamp(outAnimationProgress + Time.deltaTime, 0f, OutAnimationDuration);

            WaveTitle.rectTransform.localScale = Vector3.Lerp(
                Vector3.one,
                OutAnimationEndScale,
                outAnimationProgress / OutAnimationDuration
            );
        }

        private void OnCleanUpOutAnimation()
        {
            gameObject.SetActive(false);
        }

        #endregion state out animation
    }
}