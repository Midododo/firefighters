using Marvest.Parties;
using Marvest.SceneGlobalVariables.Stage;
using Marvest.Skills.Bullets;
using Marvest.Units.Effects;
using Units.Effects;
using UnityEngine;

namespace Marvest.Units
{
    public class PlayerUnit : MonoBehaviour
    {
        private const int SORTING_ORDER_FRONT = 1;
        private const int SORTING_ORDER_BACK = 0;

        private const float STOP_THRESHOLD = 1f;

        public int AttackPower = 1;
		public float bairitu = 1.0f;

        private void Update()
        {
            // 閾値をこえたら強制的に停止
            if (GetComponent<Rigidbody2D>().velocity.magnitude < STOP_THRESHOLD)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }

        public void Shoot(Vector2 force)
        {
			GetComponent<Rigidbody2D>().velocity = force /* bairitu*/;
            StopMovableEffect();
        }

        public bool IsStop()
        {
            return GetComponent<Rigidbody2D>().velocity.magnitude <= 0f;
        }

        private void ReceiveDamage(int power)
        {
            StageSceneGlobalVariables.Instance.PlayerParty.ReceiveDamage(power);
            DamageTextEffect.Create(gameObject, power);
        }

        public void DisplayToFront()
        {
            GetComponent<SpriteRenderer>().sortingOrder = SORTING_ORDER_FRONT;
        }

        public void DisplayToBack()
        {
            GetComponent<SpriteRenderer>().sortingOrder = SORTING_ORDER_BACK;
        }

        public void StartMovableEffect()
        {
            gameObject.GetComponent<MovablePlayerUnitEffect>().Enable();
        }

        public void StopMovableEffect()
        {
            gameObject.GetComponent<MovablePlayerUnitEffect>().Disable();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Bullet"))
            {
                var bullet = collider.GetComponent<StraightBullet>();
                StageSceneGlobalVariables.Instance.SoundEffect.PlayBulletHitSe();
                ReceiveDamage(bullet.AttackPower);
                bullet.SelfDestroy();
            }
        }
    }
}