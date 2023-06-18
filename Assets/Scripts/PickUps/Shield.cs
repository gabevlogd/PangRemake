using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PickUpBase//add observer interface to start coroutine invulnerbaility
{


    protected override void PerformPickUpEffect(Player player)
    {
        StopAllCoroutines();

        StartCoroutine(player.Invlunerability(2f));
    }
}
