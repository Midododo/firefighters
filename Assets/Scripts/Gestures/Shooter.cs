using Marvest.Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Marvest.Gestures
{
    public class Shooter :
        MonoBehaviour,
        IDragHandler,
        IBeginDragHandler,
        IEndDragHandler
    {
        private const float MAX_DRAG_MAGNITUDE = 720f;
        private const float MIN_DRAG_MAGNITUDE = 20f;

        public float ForceMagnification = 0.2f;

        private bool enableShoot;
        private bool doneShoot;
        private PlayerUnit targetUnit;
        private UiShooterArrow uiShooterArrow;
        private Physics2DRaycaster physics2DRaycaster;

        private void Start()
        {
            uiShooterArrow = UiShooterArrow.Create();
            physics2DRaycaster = Camera.main.GetComponent<Physics2DRaycaster>();
        }

        private Vector2 ConvertDragDiffToShotForce(Vector2 dragDiff)
        {
            return -dragDiff * ForceMagnification;
        }

        private Vector2 GetLimitedDragDiff(PointerEventData data)
        {
            var dragDiff = data.position - data.pressPosition;
            return Vector2.ClampMagnitude(dragDiff, MAX_DRAG_MAGNITUDE);
        }

        private bool CanShootDragDiff(Vector2 dragDiff)
        {
            return dragDiff.magnitude >= MIN_DRAG_MAGNITUDE;
        }

        private void RefreshShootArrow(Vector2 dragDiff)
        {
            if (CanShootDragDiff(dragDiff))
            {
                uiShooterArrow.Refresh(dragDiff);
            }
            else
            {
                uiShooterArrow.Refresh(Vector2.zero);
            }
        }

        public void SetUp(PlayerUnit unit)
        {
            targetUnit = unit;
            uiShooterArrow.SetPosition(unit.transform.position);
        }

        public void Enable()
        {
            enableShoot = true;
            doneShoot = false;
            physics2DRaycaster.enabled = true;
        }

        public void Disable()
        {
            enableShoot = false;
            uiShooterArrow.Hide();
            physics2DRaycaster.enabled = false;
        }

        public bool DoneShoot()
        {
            return doneShoot;
        }

        #region event handler

        public void OnDrag(PointerEventData data)
        {
            if (!enableShoot)
                return;

            RefreshShootArrow(GetLimitedDragDiff(data));
        }

        public void OnBeginDrag(PointerEventData data)
        {
            if (!enableShoot)
                return;

            uiShooterArrow.Show();
            RefreshShootArrow(GetLimitedDragDiff(data));
        }

        public void OnEndDrag(PointerEventData data)
        {
            if (!enableShoot)
                return;

            var dragDiff = GetLimitedDragDiff(data);
            if (CanShootDragDiff(dragDiff))
            {
                if (targetUnit)
                {
                    var force = ConvertDragDiffToShotForce(GetLimitedDragDiff(data));
                    doneShoot = true;
                    targetUnit.Shoot(force);
                }
            }
        }

        #endregion event handler
    }
}