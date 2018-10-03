using System.Collections.Generic;
using System.Linq;
using Marvest.DesignPatterns;

namespace Marvest.StageData.Waves
{
    public class Waves : StateMachine<WavesState>
    {
        private List<Wave> waveList = new List<Wave>();
        private int waveIndex;

        public int WaveCount
        {
            get { return waveList.Count; }
        }

        public bool HasNextWave
        {
            get { return waveIndex + 1 < WaveCount; }
        }

        public Wave CurrentWave
        {
            get { return waveList[waveIndex]; }
        }

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            InitializeWave();

            GoToState(WavesState.WaveStandby);
        }

        private void InitializeWave()
        {
            waveList = gameObject.GetComponentsInChildren<Wave>().Select(
                (wave, index) =>
                {
                    wave.InitWaveNumber(index + 1);
                    wave.gameObject.SetActive(false);
                    return wave;
                }).ToList();
        }

        private void InitializeStateMachine()
        {
            {
                var state = new State<WavesState>(WavesState.WaveStandby);
                state.SetUpAction = OnSetUpWaveStandby;
                AddState(state);
            }
            {
                var state = new State<WavesState>(WavesState.LoadWave);
                state.SetUpAction = OnSetUpLoadWave;
                AddState(state);
            }
            {
                var state = new State<WavesState>(WavesState.FinishLastWave);
                AddState(state);
            }
        }

        #region state wave standby

        private void OnSetUpWaveStandby()
        {
            if (waveIndex == WaveCount)
            {
                GoToState(WavesState.FinishLastWave);
                return;
            }

            CurrentWave.gameObject.SetActive(true);
        }

        #endregion state wave standby

        #region state load wave

        private void OnSetUpLoadWave()
        {
            if (!HasNextWave)
            {
                GoToState(WavesState.FinishLastWave);
                return;
            }

            ++waveIndex;
            GoToState(WavesState.WaveStandby);
        }

        #endregion state load wave
    }
}