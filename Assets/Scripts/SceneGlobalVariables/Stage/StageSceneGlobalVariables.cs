using Marvest.Canvases;
using Marvest.Parties;
using Marvest.Sounds;
using Marvest.StageData.Waves;
using UnityEngine;
using UnityEngine.SceneManagement;
using Marvest.TransitionEffects;

namespace Marvest.SceneGlobalVariables.Stage
{
    public class StageSceneGlobalVariables : MonoBehaviour
    {
        public PlayerParty PlayerParty;
        public Waves Waves;
        public TransitionEffect TransitionEffect;
        public UiWaveNumber UiWaveNumber;
        public SoundEffect SoundEffect;
		public int Score = 0;

        public EnemyParty EnemyParty
        {
            get { return Waves.CurrentWave.EnemyParty; }
        }

        private UiCanvas foregroundCanvas;

        public UiCanvas UiForegroundCanvas
        {
            get { return foregroundCanvas ?? (foregroundCanvas = new UiCanvas("ForegroundCanvas")); }
        }

        private UiCanvas backgroundCanvas;

        public UiCanvas UiBackgroundCanvas
        {
            get { return backgroundCanvas ?? (backgroundCanvas = new UiCanvas("BackgroundCanvas")); }
        }

        private static StageSceneGlobalVariables instance;

        public static StageSceneGlobalVariables Instance
        {
            get { return instance; }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                SceneManager.activeSceneChanged += OnSceneChange;
            }
            else if (instance != this)
            {
                SceneManager.activeSceneChanged -= OnSceneChange;
                Destroy(gameObject);
            }
        }

        private void OnSceneChange(Scene prevScene, Scene nextScene)
        {
            foregroundCanvas = null;
            backgroundCanvas = null;
        }
    }
}