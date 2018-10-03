using System.Collections.Generic;
using Marvest.DesignPatterns;
using Marvest.Skills.Bullets;
using Marvest.Units;
using UnityEngine;

namespace Marvest.Skills
{
    public abstract class AbstractBulletShoot :
        StateMachine<BulletShootState>,
        ISkill
    {
        protected GameObject bulletPrefab;
        protected List<GameObject> bullets = new List<GameObject>();

        public ISkillableUnit Subject { get; set; }

        public void Invoke()
        {
            GoToState(BulletShootState.AttackStart);
        }

        public bool IsComplete()
        {
            return IsStateSame(BulletShootState.AttackEnd);
        }

        public void SelfDestroy()
        {
            Destroy(gameObject);
        }

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            // AttackStart
            {
                var state = new State<BulletShootState>(BulletShootState.AttackStart);
                state.SetUpAction = OnSetUpAttackStart;
                AddState(state);
            }
            // AttackWait
            {
                var state = new State<BulletShootState>(BulletShootState.AttackWait);
                state.UpdateAction = OnUpdateAttackWait;
                AddState(state);
            }
            // AttackEnd
            {
                var state = new State<BulletShootState>(BulletShootState.AttackEnd);
                AddState(state);
            }
        }

        #region state attack start

        private void OnSetUpAttackStart()
        {
            if (Subject == null)
            {
                GoToState(BulletShootState.AttackEnd);
                return;
            }

            LoadPrefab(Subject.BulletPrefabName);
            ShootBullet();
            GoToState(BulletShootState.AttackWait);
        }

        private void LoadPrefab(string prefabName)
        {
            bulletPrefab = Resources.Load(prefabName) as GameObject;
        }

        abstract protected void ShootBullet();

        virtual protected int GetBulletAttackPower()
        {
            return Subject.BaseSkillPower;
        }

        #endregion state attack start

        #region state attack wait

        private void OnUpdateAttackWait()
        {
            if (IsBulletAllDestroyed())
            {
                GoToState(BulletShootState.AttackEnd);
            }
        }

        private bool IsBulletAllDestroyed()
        {
            return bullets.TrueForAll(
                bullet => bullet == null
            );
        }

        #endregion state attack wait
    }
}