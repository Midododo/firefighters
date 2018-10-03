using Marvest.SceneGlobalVariables.Stage;
using Marvest.Units;
using UnityEngine;

namespace Marvest.Skills.Bullets
{
    public class StraightBullet : MonoBehaviour
    {
        private float moveSpeed;
        private float moveDirection;

        public int AttackPower { get; private set; }

        public void SetUp(float speed, float direction, int power)
        {
            moveSpeed = speed;
            moveDirection = direction;
            AttackPower = power;
        }

        private void Update()
        {
            if (moveSpeed == 0f)
            {
                Debug.LogError("error: bullet move speed is zero");
                Destroy(gameObject);
                return;
            }

            var deltaMove = moveSpeed * Time.deltaTime;
            var rad = moveDirection * Mathf.Deg2Rad;
            var moveX = deltaMove * Mathf.Cos(rad);
            var moveY = deltaMove * Mathf.Sin(rad);
            transform.Translate(new Vector3(moveX, moveY, 0f));
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("PlayArea"))
            {
                Destroy(gameObject);
            }
        }

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}