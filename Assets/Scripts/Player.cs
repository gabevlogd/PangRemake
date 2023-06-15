using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerStatesManager m_statesManager;

    private void Awake()
    {
        m_statesManager = new PlayerStatesManager(transform);
        m_statesManager.CurrentState = m_statesManager.AllStates[PlayerState.Idle];
    }

    private void Update()
    {
        SetState();
        m_statesManager.CurrentState.OnUpdate();
    }


    private void SetState()
    {
        if (Input.GetKey(KeyCode.Mouse0)) m_statesManager.ChangeState(PlayerState.Shoot);
        else if (Input.GetKey(KeyCode.A)) m_statesManager.ChangeState(PlayerState.WalkLeft);
        else if (Input.GetKey(KeyCode.D)) m_statesManager.ChangeState(PlayerState.WalkRight);
        else if (Input.GetKey(KeyCode.W)) m_statesManager.ChangeState(PlayerState.Climb);
        else m_statesManager.ChangeState(PlayerState.Idle);
    }
}
