using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpBase : MonoBehaviour
{
    public int MaxLifespan;
    public int FallingSpeed;

    private float m_lifespan;
    private bool m_IsFalling = true;

    protected virtual void Update()
    {
        if (m_IsFalling) PickUpFalling();
        else UpdateLifespanCooldown();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            PerformPickUpEffect(player);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Performs the effect of the pick up
    /// </summary>
    /// <param name="player">player reference</param>
    protected abstract void PerformPickUpEffect(Player player);

    /// <summary>
    /// Hook lifespan timer
    /// </summary>
    private void UpdateLifespanCooldown()
    {
        if (m_lifespan >= MaxLifespan) Destroy(this.gameObject);
        else m_lifespan += Time.deltaTime;
    }

    private void PickUpFalling()
    {
        if (transform.position.y <= 1)
        {
            m_IsFalling = false;
            return;
        }
        else transform.Translate(Vector3.down * FallingSpeed * Time.deltaTime);
    }


}

