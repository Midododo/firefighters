using Marvest.Skills.Bullets;

namespace Marvest.Skills
{
    public class BulletShootAround : AbstractBulletShoot
    {
        public float BulletSpeed = 10f;
        public int BulletAmount = 8;

        protected override void ShootBullet()
        {
            var position = Subject.UnitGameObject.transform.position;
            var attackPower = GetBulletAttackPower();
            for (var i = 0; i < BulletAmount; i++)
            {
                var bulletGameObject = Instantiate(bulletPrefab);
                bulletGameObject.transform.position = position;
                var hitPlayerBullet = bulletGameObject.GetComponent<StraightBullet>();
                var direction = 360 / BulletAmount * i;
                hitPlayerBullet.SetUp(BulletSpeed, direction, attackPower);
                bullets.Add(bulletGameObject);
            }
        }
    }
}