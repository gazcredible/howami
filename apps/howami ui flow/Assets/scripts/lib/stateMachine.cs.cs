using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    abstract public class TemplateState
    {
        public abstract void Init(Object obj = null);
        public abstract void Update();
        public virtual void Exit() { }

    };

    public class TemplateStateMachine<T> where T : TemplateState
    {
        public String CurrentState
        {
            get { return currentState; }
        }

        protected String currentState;
        protected String desiredState;

        protected int stateTick;
        protected int lifetimeTick;

        public int LifetimeTick
        {
            get { return lifetimeTick; }
        }

        public Dictionary<String, T> stateLookup;

        public TemplateStateMachine()
        {
            stateLookup = new Dictionary<String, T>();

            currentState = "";
            desiredState = "";
        }

        public bool IsRunning()
        {
            return stateLookup.Count > 0;
        }

        public void AddState(String name, T state)
        {
            T s;

            if (stateLookup.TryGetValue(name, out s) == false)
            {
                stateLookup[name] = state;
            }
            else
            {
                throw new Exception();
            }
        }

        public T GetState(String name)
        {
            return stateLookup[name];
        }



        public void SetState(String NewState)
        {
            T val;

            if (currentState != NewState)
            {
                if (stateLookup.TryGetValue(NewState, out val) == false)
                {
                    throw new Exception("Unknown state: " + NewState);
                }

                desiredState = NewState;
            }
        }

        public virtual void Update(Object obj = null)
        {
            doStateChange(obj);

            if (currentState != "")
            {
                stateLookup[currentState].Update();

                stateTick++;
                lifetimeTick++;
            }

            doStateChange(obj);
        }

        void doStateChange(Object obj)
        {
            if (desiredState != "")
            {
                if (currentState != "")
                {
                    stateLookup[currentState].Exit();
                }

                currentState = desiredState;
                desiredState = "";

                if (currentState != "")
                {
                    stateLookup[currentState].Init(obj);
                    stateTick = 0;
                }
            }
        }
    }
}
