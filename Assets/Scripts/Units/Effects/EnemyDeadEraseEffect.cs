using System.Collections;
using UnityEngine;

namespace Marvest.Units.Effects
{
    public class EnemyDeadEraseEffect : MonoBehaviour
    {
        private const float EFFECT_DURATION = 1.2f;
        private const float START_SHAKE_SCALE = 0.1f;
        private const int WAVE_COUNT = 40;
        private const int FLASH_COUNT = 40;

        private const float FLASH_INTERVAL = EFFECT_DURATION / FLASH_COUNT;

        private EnemyUnit enemy;
        private float progress;
        private Vector3 startPosition;
        private float prevFlashTime;

        private void Start()
        {
            enemy = GetComponent<EnemyUnit>();
            GetComponent<Collider2D>().enabled = false;
            startPosition = gameObject.transform.position;

            StartCoroutine(Erase());
        }

        private IEnumerator Erase()
        {
            StartCoroutine(ShakeAnimation());
            StartCoroutine(FlashAnimation());

            yield return new WaitUntil(IsFinished);

            enemy.Dead();
            Destroy(this);
        }

        private IEnumerator ShakeAnimation()
        {
            while (!IsFinished())
            {
                progress += Time.deltaTime;
                var t = progress / EFFECT_DURATION;
                var shakeScale = Mathf.Lerp(START_SHAKE_SCALE, 0, t);

                var x = Mathf.Cos(t * Mathf.PI * WAVE_COUNT);
                var y = Mathf.Sin(t * Mathf.PI * WAVE_COUNT);
                gameObject.transform.position = startPosition + new Vector3(x, y, 0) * shakeScale;

                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator FlashAnimation()
        {
            while (!IsFinished())
            {
                yield return new WaitUntil(() => progress / EFFECT_DURATION - prevFlashTime >= FLASH_INTERVAL);

                prevFlashTime = progress / EFFECT_DURATION;
                var renderer = gameObject.GetComponent<Renderer>();
                renderer.enabled = !renderer.enabled;
            }
        }

        private bool IsFinished()
        {
            return progress >= EFFECT_DURATION;
        }
    }
}