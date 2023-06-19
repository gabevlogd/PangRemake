using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class Shield : PickUpBase
{
    public static Observable<string> Observable;
    public static readonly int Duration = 5;

    private void Awake()
    {
        if (Observable == null) Observable = new Observable<string>();
    }

    protected override void PerformPickUpEffect(Player player)
    {
        Observable.NotifyObservers(Constants.AUDIO, Constants.PICK_UP); //play pick up sound;
        player.Stats.TriggerShield = true;
    }
}
