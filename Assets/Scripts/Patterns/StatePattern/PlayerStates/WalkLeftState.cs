using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class WalkLeftState : State<PlayerState> 
{
    private PlayerStatesManager m_playerStatesManager;
    private Animator m_animator;

    public WalkLeftState(PlayerState stateID, StatesManager<PlayerState> stateManager = null) : base(stateID, stateManager)
    {
        m_playerStatesManager = (PlayerStatesManager)m_stateManager;
        
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (m_animator == null) m_animator = m_playerStatesManager.PlayerTransform.GetComponentInChildren<Animator>();
        m_animator.SetBool("IsWalkingLeft", true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        m_playerStatesManager.PlayerTransform.Translate(Vector3.left * 4f * Time.deltaTime);

    }

    public override void OnExit()
    {
        base.OnExit();
        m_animator.SetBool("IsWalkingLeft", false);
        m_animator.transform.localPosition = Vector3.zero;
        m_animator.transform.localRotation = Quaternion.identity;
    }
}