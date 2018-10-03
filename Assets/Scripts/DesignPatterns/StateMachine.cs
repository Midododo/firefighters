using System.Collections.Generic;
using UnityEngine;

namespace Marvest.DesignPatterns
{
    public class StateMachine<T> : MonoBehaviour
    {
        private Dictionary<T, State<T>> stateList = new Dictionary<T, State<T>>();
        private State<T> state;

        public State<T> CurrentState
        {
            get { return state; }
        }

        public void GoToState(T nextStateId)
        {
            if (!stateList.ContainsKey(nextStateId))
            {
                Debug.LogErrorFormat("error: not exist state: {0}", nextStateId);
                return;
            }

            if (state != null)
            {
                state.CleanUp();
            }

            state = stateList[nextStateId];
            state.SetUp();
        }

        protected void AddState(State<T> state)
        {
            var stateId = state.Id;
            if (stateList.ContainsKey(stateId))
            {
                Debug.LogErrorFormat("error: already exist state: {0}", stateId);
                return;
            }

            stateList.Add(stateId, state);
        }

        protected void Update()
        {
            if (state != null)
            {
                state.Update();
            }
        }

        public bool IsStateSame(T stateId)
        {
            if (state == null)
            {
                return false;
            }

            return state.Id.Equals(stateId);
        }
    }
}