using UnityEngine;

namespace Marvest.Units.Effects
{
    public class MovablePlayerUnitEffect : MonoBehaviour
    {
        private const float MAX_BRIGHTNESS_VOLUME = 0.15f;
        private const float FLASH_SPEED = 2;
        private static readonly Color COLOR_BASE = new Color(0.5f, 0.5f, 0.5f);

        private PlayerUnit unit;
        private SpriteRenderer unitRenderer;

        private void Start()
        {
            unit = GetComponent<PlayerUnit>();
            unitRenderer = unit.gameObject.GetComponent<SpriteRenderer>();
            Disable();
        }

        private void Update()
        {
            var val = Mathf.Abs(Mathf.Sin(Time.time * FLASH_SPEED)) * MAX_BRIGHTNESS_VOLUME;
            unitRenderer.material.SetColor("_Color", COLOR_BASE + new Color(val, val, val));
        }

        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            unitRenderer.material.SetColor("_Color", COLOR_BASE);
            enabled = false;
        }
    }
}