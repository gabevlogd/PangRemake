using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Transform UpStep;
    public Transform DownStep;

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            SetClimbingAbility(player);
            SetPlayerOffset(player);
        }
    }

    private void SetPlayerOffset(Player player)
    {
        if ((Input.GetKey(KeyCode.W) && player.Stats.CanClimbUp) || (Input.GetKey(KeyCode.S) && player.Stats.CanClimbDown))
        {
            player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
    }

    private void SetClimbingAbility(Player player)
    {
        if (player.transform.position.y < UpStep.transform.position.y) player.Stats.CanClimbUp = true;
        else player.Stats.CanClimbUp = false;

        if (player.transform.position.y > DownStep.transform.position.y) player.Stats.CanClimbDown = true;
        else player.Stats.CanClimbDown = false;
    }
}
