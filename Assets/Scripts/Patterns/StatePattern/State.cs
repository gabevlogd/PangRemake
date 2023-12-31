using UnityEngine;

namespace Gabevlogd.Patterns
{

    /// <summary>
    /// Basic state template 
    /// </summary>
    /// <typeparam name="TStateIDType">The type of the state ID</typeparam>
    public class State<TStateIDType>
    {
        public TStateIDType StateID;
        protected StatesManager<TStateIDType> m_stateManager;

        public State(TStateIDType stateID, StatesManager<TStateIDType> stateManager = null)
        {
            StateID = stateID;
            m_stateManager = stateManager;
        }

        public virtual void OnEnter()
        {
            //Debug.Log("OnEnter " + StateID);

        }

        public virtual void OnUpdate()
        {
            //Debug.Log("OnUpadte " + StateID);
        }

        public virtual void OnExit()
        {
            //Debug.Log("OnExit " + StateID);
        }
    }
}

