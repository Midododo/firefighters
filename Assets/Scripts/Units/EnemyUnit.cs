using Marvest.Units.Effects;
using Marvest.SceneGlobalVariables.Stage;
using Marvest.Skills.Bullets;
using Units.Effects;
using UnityEngine;

namespace Marvest.Units
{
    public class EnemyUnit : MonoBehaviour, ISkillableUnit
    {
        public int AttackPower = 1;
        public int HitPoint = 5;
        public int ActionWait = 3;
		public int DeadScore = 20;
        public GameObject Skill;

        private bool isDead;
        private UiEnemyHitPointGauge gauge;
        private UiEnemyUnitActionWaitCount uiEnemyUnitActionWaitCount;

        public int ActionWaitCount { get; private set; }

        #region interface skillable unit

        public int BaseSkillPower
        {
            get { return AttackPower; }
        }

        public GameObject UnitGameObject
        {
            get { return gameObject; }
        }

        public string BulletPrefabName
        {
            get { return "Prefabs/Skills/Bullets/HitPlayerBullet"; }
        }

        #endregion interface skillable unit

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            ActionWaitCount = ActionWait;
            isDead = false;
            Show();
        }

        private void Start()
        {
            gauge = UiEnemyHitPointGauge.Create(this);
            uiEnemyUnitActionWaitCount = UiEnemyUnitActionWaitCount.Create(this);
        }

        /**
         * 敵が味方と重なっている場合出現時にダメージを受けてしまうので
         * EnterではなくExitでダメージ判定
         */
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("PlayerUnit"))
            {
                var unit = collision.collider.gameObject.GetComponent<PlayerUnit>();
                OnHitByUnit(unit);
            }
        }

        private void OnHitByUnit(PlayerUnit unit)
        {
            StageSceneGlobalVariables.Instance.SoundEffect.PlayStoneHitSe();
			ReceiveDamage(unit.AttackPower);
			int combo = StageSceneGlobalVariables.Instance.PlayerParty.Combo;
			StageSceneGlobalVariables.Instance.Score += 10 * combo;	//スコア加算
        }

        private void ReceiveDamage(int power)
        {
            HitPoint -= power;
            if (HitPoint < 0)
            {
                HitPoint = 0;
            }

            DamageTextEffect.Create(gameObject, power);
            gauge.Refresh();

			StageSceneGlobalVariables.Instance.PlayerParty.Combo += 1;
			int combo = StageSceneGlobalVariables.Instance.PlayerParty.Combo;
			if (combo >= 2) {
				ComboText.Create (gameObject, combo);
			}

            if (HitPoint == 0)
            {
                gameObject.AddComponent<EnemyDeadEraseEffect>();
				StageSceneGlobalVariables.Instance.Score += DeadScore;	//スコア加算
            }
        }

        public void Dead()
        {
            isDead = true;
            Hide();
            gauge.Remove();
            uiEnemyUnitActionWaitCount.Remove();
        }

        public bool IsDead()
        {
            return isDead;
        }

        public bool IsDying()
        {
            return HitPoint == 0 && !isDead;
        }

        public void DecreaseActionWaitCount()
        {
            ActionWaitCount--;
            RefreashActionWaitCount();
        }

        public void ResetActionWaitCount()
        {
            ActionWaitCount = ActionWait;
            RefreashActionWaitCount();
        }

        private void RefreashActionWaitCount()
        {
            if (!IsDead())
            {
                uiEnemyUnitActionWaitCount.Refresh();
            }
        }

        public bool CanAction()
        {
            return !isDead && ActionWaitCount <= 0;
        }

        public void Show()
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }

        public void Hide()
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
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