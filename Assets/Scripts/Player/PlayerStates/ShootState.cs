using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class ShootState : State<PlayerState>
{
    private PlayerStatesManager m_playerStatesManager;
    private BasicWeapon m_weapon;

    public ShootState(PlayerState stateID, StatesManager<PlayerState> stateManager = null) : base(stateID, stateManager)
    {
        m_playerStatesManager = (PlayerStatesManager)m_stateManager;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (m_weapon == null) m_weapon = m_playerStatesManager.PlayerTransform.GetComponentInChildren<BasicWeapon>();

        m_weapon.Shoot();
    }
}
