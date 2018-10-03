using System;

namespace Marvest.DesignPatterns
{
    public class State<T>
    {
        public State(T id)
        {
            Id = id;
        }

        public T Id { get; private set; }

        public Action SetUpAction { private get; set; }

        public Action UpdateAction { private get; set; }

        public Action CleanUpAction { private get; set; }

        public void SetUp()
        {
            if (SetUpAction != null)
            {
                SetUpAction.Invoke();
            }
        }

        public void Update()
        {
            if (UpdateAction != null)
            {
                UpdateAction.Invoke();
            }
        }

        public void CleanUp()
        {
            if (CleanUpAction != null)
            {
                CleanUpAction.Invoke();
            }
        }
    }
}