using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class WalkRightState : State<PlayerState>
{
    private PlayerStatesManager m_playerStatesManager;
    private Animator m_animator;

    public WalkRightState(PlayerState stateID, StatesManager<PlayerState> stateManager = null) : base(stateID, stateManager)
    {
        m_playerStatesManager = (PlayerStatesManager)m_stateManager;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (m_animator == null) m_animator = m_playerStatesManager.PlayerTransform.GetComponentInChildren<Animator>();
        m_animator.SetBool("IsWalkingRight", true);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!Physics.Raycast(m_playerStatesManager.PlayerTransform.position, Vector3.right, 1f)) m_playerStatesManager.PlayerTransform.Translate(Vector3.right * 4f * Time.deltaTime);

    }

    public override void OnExit()
    {
        base.OnExit();
        m_animator.SetBool("IsWalkingRight", false);
        m_animator.transform.localPosition = Vector3.zero;
        m_animator.transform.localRotation = Quaternion.identity;
    }
}
