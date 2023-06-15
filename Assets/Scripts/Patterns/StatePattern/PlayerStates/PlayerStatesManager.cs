using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class PlayerStatesManager : StatesManager<PlayerState>
{
    public Transform PlayerTransform; 

    public PlayerStatesManager(Transform playerTransform, Dictionary<PlayerState, State<PlayerState>> allStates = null, State<PlayerState> currentState = null, State<PlayerState> previousState = null) : base(allStates, currentState, previousState)
    {
        PlayerTransform = playerTransform;
    }

    protected override void InitStates()
    {
        AllStates.Add(PlayerState.Idle, new IdleState(PlayerState.Idle, this));
        AllStates.Add(PlayerState.WalkLeft, new WalkLeftState(PlayerState.WalkLeft, this));
        AllStates.Add(PlayerState.WalkRight, new WalkRightState(PlayerState.WalkRight, this));
        AllStates.Add(PlayerState.Climb, new ClimbState(PlayerState.Climb, this));
        AllStates.Add(PlayerState.Shoot, new ShootState(PlayerState.Shoot, this));
    }
}

public enum PlayerState
{
    Idle,
    WalkLeft,
    WalkRight,
    Climb,
    Shoot
}
