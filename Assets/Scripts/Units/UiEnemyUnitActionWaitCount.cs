using System.Collections;
using Marvest.SceneGlobalVariables.Stage;
using UnityEngine;
using UnityEngine.UI;

namespace Marvest.Units
{
    public class UiEnemyUnitActionWaitCount : MonoBehaviour
    {
        public Text Count;
        public float StartAnimationScale;
        public float AnimationDuration;

        private EnemyUnit enemy;
        private float progress;
        private bool isInAnimation;
        private Vector2 defaultSizeDelta;
        private int defaultFontSize;

        public static UiEnemyUnitActionWaitCount Create(EnemyUnit parent)
        {
            var gameObject = Instantiate(Resources.Load("Prefabs/Units/UiEnemyUnitActionWaitCount")) as GameObject;
            var waitCount = gameObject.GetComponent<UiEnemyUnitActionWaitCount>();
            var canvas = StageSceneGlobalVariables.Instance.UiForegroundCanvas;
            canvas.AddToCanvas(waitCount.gameObject);
            waitCount.Initialize(parent);

            return waitCount;
        }

        private void Initialize(EnemyUnit parent)
        {
            defaultSizeDelta = GetComponent<RectTransform>().sizeDelta;
            defaultFontSize = Count.fontSize;
            enemy = parent;
            UpdatePositionAndScale();
            Refresh();
        }

        private void Update()
        {
            UpdatePositionAndScale();
        }

        private void UpdatePositionAndScale()
        {
            if (enemy == null)
            {
                return;
            }

            var enemyBounds = enemy.GetComponent<SpriteRenderer>().bounds;
            var enemySize = enemyBounds.size;
            transform.position = enemy.transform.position - enemySize / 2;
            var scale = enemy.transform.localScale.x;
            GetComponent<RectTransform>().sizeDelta = defaultSizeDelta * scale;
            Count.fontSize = (int) (defaultFontSize * scale);
        }

        public void Refresh()
        {
            StartCoroutine(CountdownAnimation());
        }

        public void Remove()
        {
            Destroy(gameObject);
        }

        private IEnumerator CountdownAnimation()
        {
            yield return new WaitWhile(() => isInAnimation);

            Count.text = enemy.ActionWaitCount.ToString();

            progress = 0f;
            isInAnimation = true;
            while (progress < AnimationDuration)
            {
                progress += Time.deltaTime;
                Count.GetComponent<RectTransform>().localScale = Vector3.Lerp(
                    Vector3.one * StartAnimationScale,
                    Vector3.one,
                    progress / AnimationDuration);

                yield return new WaitForEndOfFrame();
            }

            isInAnimation = false;
        }
    }
}