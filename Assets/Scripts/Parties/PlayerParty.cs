using System.Collections.Generic;
using System.Linq;
using Marvest.DesignPatterns;
using Marvest.Gestures;
using Marvest.SceneGlobalVariables.Stage;
using Marvest.Units;
using UnityEngine;

namespace Marvest.Parties
{
    public class PlayerParty : StateMachine<PlayerPartyState>
    {
        private const int MAX_UNIT_COUNT = 4;

        public int HitPoint = 10;
        public Shooter Shooter;
        public UiPlayerPartyHitPointGauge HitpointGauge;
        public UiPlayerPartySkillButtons SkillButtons;
		public int Combo = 0;

        private List<PlayerUnit> playerUnits = new List<PlayerUnit>();
        private PlayerUnit subject;
        private int subjectPlayerIndex = -1;

        public List<PlayerUnit> PlayerUnits
        {
            get { return playerUnits; }
        }

        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            InitializePlayerUnits();
            HitpointGauge.Initialize(this);
        }

        private void InitializePlayerUnits()
        {
            playerUnits = gameObject.GetComponentsInChildren<PlayerUnit>().ToList();
            if (playerUnits.Count > MAX_UNIT_COUNT)
            {
                Debug.LogWarningFormat("パーティが{0}人を超えたため、超えた分は削除します", MAX_UNIT_COUNT);
                foreach (var unit in playerUnits.Skip(MAX_UNIT_COUNT))
                {
                    Destroy(unit.gameObject);
                }

                playerUnits = playerUnits.Take(MAX_UNIT_COUNT).ToList();
            }

            SkillButtons.AttachParty(this);
        }

        private void InitializeStateMachine()
        {
            // TurnStart
            {
                var state = new State<PlayerPartyState>(PlayerPartyState.TurnStart);
                state.SetUpAction = OnSetUpTurnStart;
                AddState(state);
            }
            // IdleUntilShoot
            {
                var state = new State<PlayerPartyState>(PlayerPartyState.IdleUntilShoot);
                state.SetUpAction = OnSetUpIdleUntilShoot;
                state.UpdateAction = OnUpdateIdleUntilShoot;
                state.CleanUpAction = OnCleanUpIdleUntilShoot;
                AddState(state);
            }
            // UnitMove
            {
                var state = new State<PlayerPartyState>(PlayerPartyState.UnitMove);
                state.UpdateAction = OnUpdateUnitMove;
                AddState(state);
            }
            // TurnEnd
            {
                var state = new State<PlayerPartyState>(PlayerPartyState.TurnEnd);
                AddState(state);
            }
        }

        public bool CanAction()
        {
            return playerUnits.Count > 0;
        }

        public bool IsTurnEnd()
        {
            return IsStateSame(PlayerPartyState.TurnEnd);
        }

        public bool IsAllDead()
        {
            return HitPoint <= 0;
        }

        public void ReceiveDamage(int power)
        {
            HitPoint = Mathf.Max(0, HitPoint - power);
            HitpointGauge.Refresh();
        }

        #region state turn start

        private void OnSetUpTurnStart()
        {
            GoToNextSubject();
            DisplaySubjectToFront();
            Shooter.SetUp(subject);
			Combo = 0;

            GoToState(PlayerPartyState.IdleUntilShoot);
        }

        private void GoToNextSubject()
        {
            subjectPlayerIndex = (subjectPlayerIndex + 1) % playerUnits.Count;
            subject = playerUnits[subjectPlayerIndex];
        }

        private void DisplaySubjectToFront()
        {
            foreach (var playerUnit in playerUnits)
            {
                playerUnit.DisplayToBack();
            }

            subject.DisplayToFront();
        }

        #endregion state turn start

        #region state idle until shoot

        private void OnSetUpIdleUntilShoot()
        {
            Shooter.Enable();
            subject.StartMovableEffect();
        }

        private void OnUpdateIdleUntilShoot()
        {
			SkillButtons.PlaySkill (this);
            if (Shooter.DoneShoot())
            {
                Shooter.Disable();
                GoToState(PlayerPartyState.UnitMove);
            }
        }

        private void OnCleanUpIdleUntilShoot()
        {
            Shooter.Disable();
            subject.StopMovableEffect();
        }

        #endregion state idle until shoot

        #region state unit move

        private void OnUpdateUnitMove()
        {
            if (subject.IsStop() && !StageSceneGlobalVariables.Instance.EnemyParty.ExistsUnitIsDying())
            {
                GoToState(PlayerPartyState.TurnEnd);
				SkillButtons.WaitSkilTimeDown (this);
            }
        }

        #endregion state unit move
    }
}