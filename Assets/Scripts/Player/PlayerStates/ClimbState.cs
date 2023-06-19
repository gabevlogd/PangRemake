using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class ClimbState : State<PlayerState>
{
    private PlayerStatesManager m_playerStatesManager;
    private Animator m_animator;
    private float m_climbSpeed = 3f;
    private int m_direction;

    public ClimbState(PlayerState stateID, StatesManager<PlayerState> stateManager = null) : base(stateID, stateManager)
    {
        m_playerStatesManager = (PlayerStatesManager)m_stateManager;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (m_animator == null) m_animator = m_playerStatesManager.PlayerTransform.GetComponentInChildren<Animator>();
        m_animator.SetBool("IsClimbing", true);

        if (GameManager.Instance.Player.Stats.CanClimbDown) m_direction = -1;
        else m_direction = 1;
    }


    public override void OnUpdate()
    {
        base.OnUpdate();
        //if (Input.GetKey(KeyCode.W)) m_playerStatesManager.PlayerTransform.Translate(Vector3.up * m_climbSpeed * Time.deltaTime);
        //else if (Input.GetKey(KeyCode.S)) m_playerStatesManager.PlayerTransform.Translate(Vector3.down * m_climbSpeed * Time.deltaTime);
        if ((m_direction == -1 && GameManager.Instance.Player.Stats.CanClimbDown) || (m_direction == 1 && GameManager.Instance.Player.Stats.CanClimbUp)) 
            m_playerStatesManager.PlayerTransform.Translate(Vector3.up * m_climbSpeed * m_direction * Time.deltaTime);
    }
    public override void OnExit()
    {
        base.OnExit();
        m_animator.SetBool("IsClimbing", false);

        //reset player offset
        m_animator.transform.localPosition = Vector3.zero;
        m_animator.transform.localRotation = Quaternion.identity;
        if (m_playerStatesManager.PlayerTransform.transform.position.y < 1f) m_playerStatesManager.PlayerTransform.transform.position = new Vector3(m_playerStatesManager.PlayerTransform.transform.position.x, 0.5f, 0f);
        else m_playerStatesManager.PlayerTransform.transform.position = new Vector3(m_playerStatesManager.PlayerTransform.transform.position.x, m_playerStatesManager.PlayerTransform.transform.position.y, 0f);
    }
}
