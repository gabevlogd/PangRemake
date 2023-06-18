using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class ClimbState : State<PlayerState>
{
    public ClimbState(PlayerState stateID, StatesManager<PlayerState> stateManager = null) : base(stateID, stateManager)
    {
    }
}
