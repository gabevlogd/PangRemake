using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class BallsFreezer : PickUpBase
{
    public static readonly int Duration = 5;

    protected override void PerformPickUpEffect(Player player)
    {
        Observable.NotifyObservers(Constants.AUDIO, Constants.PICK_UP); //play pick up sound;
        player.Stats.TriggerFreezer = true;
        //Debug.Log(player.Stats.TriggerFreezer);
    }
}
