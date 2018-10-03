using UnityEngine;
using UnityEngine.UI;

namespace Marvest.StageData.Waves
{
    public class UiWaveNumber : MonoBehaviour
    {
        public Text WaveNumberText;

        public void SetWave(Wave wave)
        {
            WaveNumberText.text = wave.WaveNumber.ToString();
        }
    }
}