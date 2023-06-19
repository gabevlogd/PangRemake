using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class GrapplingGun : BasicWeapon
{
    public static Observable<string> Observable;

    private void Awake()
    {
        if (Observable == null) Observable = new Observable<string>();
    }

    public override void Shoot()
    {
        Observable.NotifyObservers(Constants.AUDIO, Constants.HOOK);
        base.Shoot();
    }
}
