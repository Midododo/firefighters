using Marvest.Parties;
using UnityEngine;

namespace Marvest.StageData.Waves
{
    public class Wave : MonoBehaviour
    {
        public EnemyParty EnemyParty { get; private set; }

        public int WaveNumber { get; private set; }

        private void Start()
        {
            Initialize();
        }

        public void InitWaveNumber(int waveNumber)
        {
            WaveNumber = waveNumber;
        }

        private void Initialize()
        {
            EnemyParty = gameObject.GetComponentInChildren<EnemyParty>();
        }
    }
}