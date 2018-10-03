using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Marvest.DesignPatterns;
using Marvest.Skills;
using Marvest.Units;
using UnityEngine;

namespace Marvest.Parties
{
    public class EnemyParty : StateMachine<EnemyPartyState>
    {
        private const float NEXT_ACTION_DELAY = 0.5f;

        private List<EnemyUnit> enemyUnits = new List<EnemyUnit>();
        private EnemyUnit subject;
        private List<ISkill> invokeSkills = new List<ISkill>();

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            InitializeEnemyUnits();
        }

        private void InitializeEnemyUnits()
        {
            enemyUnits = gameObject.GetComponentsInChildren<EnemyUnit>().ToList();
        }

        private void InitializeStateMachine()
        {
            // TurnStart
            {
                var state = new State<EnemyPartyState>(EnemyPartyState.TurnStart);
                state.SetUpAction = OnSetUpTurnStart;
                AddState(state);
            }
            // UnitAction
            {
                var state = new State<EnemyPartyState>(EnemyPartyState.UnitAction);
                state.SetUpAction = OnSetUpUnitAction;
                AddState(state);
            }
            // ActionWait
            {
                var state = new State<EnemyPartyState>(EnemyPartyState.ActionWait);
                state.UpdateAction = OnUpdateActionWait;
                AddState(state);
            }
            // TurnEnd
            {
                var state = new State<EnemyPartyState>(EnemyPartyState.TurnEnd);
                AddState(state);
            }
        }

        public bool IsAllDead()
        {
            return enemyUnits.TrueForAll(
                enemyUnit => enemyUnit.IsDead()
            );
        }

        public bool ExistsUnitIsDying()
        {
            return enemyUnits.Exists(
                enemyUnit => enemyUnit.IsDying()
            );
        }

        public bool IsTurnEnd()
        {
            return IsStateSame(EnemyPartyState.TurnEnd);
        }

        private bool ExistsUnitCanAction()
        {
            return enemyUnits.Exists(
                enemyUnit => enemyUnit.CanAction()
            );
        }

        #region state turn start

        private void OnSetUpTurnStart()
        {
            DecreaseActionWaitCount();
            if (ExistsUnitCanAction())
            {
                GoToState(EnemyPartyState.UnitAction);
            }
            else
            {
                GoToState(EnemyPartyState.TurnEnd);
            }
        }

        private void DecreaseActionWaitCount()
        {
            foreach (var enemyUnit in enemyUnits)
            {
                enemyUnit.DecreaseActionWaitCount();
            }
        }

        #endregion state turn start

        #region state unit action

        private void OnSetUpUnitAction()
        {
            subject = GetNextSubject();

            var obj = Instantiate(subject.Skill);
            var subjectSkill = obj.GetComponent<ISkill>();
            subjectSkill.Subject = subject;
            subjectSkill.Invoke();
            invokeSkills.Add(subjectSkill);

            StartCoroutine(DelayNextAction());
        }

        private EnemyUnit GetNextSubject()
        {
            return enemyUnits.Find(
                enemyUnit => enemyUnit.CanAction()
            );
        }

        private IEnumerator DelayNextAction()
        {
            yield return new WaitForSeconds(NEXT_ACTION_DELAY);
            subject.ResetActionWaitCount();

            if (ExistsUnitCanAction())
            {
                GoToState(EnemyPartyState.UnitAction);
            }
            else
            {
                GoToState(EnemyPartyState.ActionWait);
            }
        }

        #endregion state unit action

        #region state action wait

        private void OnUpdateActionWait()
        {
            if (IsActionAllComplete())
            {
                DestroyInvokeSkills();
                GoToState(EnemyPartyState.TurnEnd);
                invokeSkills.Clear();
            }
        }

        private bool IsActionAllComplete()
        {
            return invokeSkills.TrueForAll(
                skill => skill.IsComplete()
            );
        }

        private void DestroyInvokeSkills()
        {
            foreach (var skill in invokeSkills)
            {
                skill.SelfDestroy();
            }
        }

        #endregion state action wait
    }
}