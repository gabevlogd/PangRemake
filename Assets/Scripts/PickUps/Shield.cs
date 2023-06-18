using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PickUpBase
{
    public static readonly int Duration = 5;

    protected override void PerformPickUpEffect(Player player) => player.Stats.TriggerShield = true;
    
}
