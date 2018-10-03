using Marvest.SceneGlobalVariables.Stage;
using UnityEngine;
using UnityEngine.UI;

namespace Marvest.Units
{
    public class UiEnemyHitPointGauge : MonoBehaviour
    {
        public Image HitpointGauge;
        public RectTransform HitpointGaugeFrame;

        private EnemyUnit enemyUnit;
        private RectTransform gaugeRectTransform;
        private Vector2 startOffsetMax;
        private Vector2 endOffsetMax;
        private int maxHitPoint;

        public static UiEnemyHitPointGauge Create(EnemyUnit parent)
        {
            var gameObject = Instantiate(Resources.Load("Prefabs/Units/UiEnemyHitPointGauge")) as GameObject;
            var gauge = gameObject.GetComponent<UiEnemyHitPointGauge>();
            StageSceneGlobalVariables.Instance.UiForegroundCanvas.AddToCanvas(gauge.gameObject);
            gauge.Initialize(parent);

            return gauge;
        }

        private void Initialize(EnemyUnit parent)
        {
            enemyUnit = parent;
            gaugeRectTransform = HitpointGauge.GetComponent<RectTransform>();
            startOffsetMax = gaugeRectTransform.offsetMax;
            endOffsetMax = new Vector2(-HitpointGaugeFrame.sizeDelta.x - startOffsetMax.x, startOffsetMax.y);
            maxHitPoint = enemyUnit.HitPoint;

            UpdatePositionAndScale();
        }


        private void Update()
        {
            UpdatePositionAndScale();
        }

        private void UpdatePositionAndScale()
        {
            if (enemyUnit == null)
            {
                return;
            }

            // Canvas座標での敵ユニットのスプライトサイズを計算
            var spriteBounds = enemyUnit.GetComponent<SpriteRenderer>().bounds;
            var spriteMinPos = spriteBounds.min;
            var spriteMaxPos = spriteBounds.max;

            var canvas = StageSceneGlobalVariables.Instance.UiForegroundCanvas.Canvas;
            var canvasCamera = canvas.worldCamera;
            var canvasRect = canvas.GetComponent<RectTransform>();

            Vector2 canvasMinPos;
            Vector2 canvasMaxPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                canvasCamera.WorldToScreenPoint(spriteMinPos),
                canvasCamera,
                out canvasMinPos
            );
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                canvasCamera.WorldToScreenPoint(spriteMaxPos),
                canvasCamera,
                out canvasMaxPos
            );
            var spriteSize = canvasMaxPos - canvasMinPos;

            // スプライトサイズに合わせてゲージの表倍率を変更
            var frameSize = HitpointGaugeFrame.sizeDelta;
            var scale = spriteSize.x / frameSize.x;
            HitpointGaugeFrame.localScale = new Vector3(scale, scale, HitpointGaugeFrame.localScale.z);

            // ゲージの表示位置更新
            var parentPosition = enemyUnit.transform.position;
            var offset = new Vector3(0, spriteBounds.extents.y);
            gameObject.transform.position = parentPosition + offset;
        }

        public void Refresh()
        {
            gaugeRectTransform.offsetMax = Vector2.Lerp(endOffsetMax, startOffsetMax, enemyUnit.HitPoint / (float) maxHitPoint);
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}