using Marvest.Units;
using UnityEngine;

namespace Marvest.Skills.Bullets
{
    public class BulletRotation : MonoBehaviour
    {
        public float speed = 1f;

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            transform.Rotate(Vector3.forward, speed);
        }
    }
}