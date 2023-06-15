using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform FirePoint;
    public int WeaponUsageCooldown;
    [HideInInspector]
    public bool CanShoot = true;

    protected float m_actualCooldown;

    protected virtual void Update()
    {
        if (!CanShoot) UpdateCooldown();
    }

    public virtual void Shoot()
    {
        CanShoot = false;
        Instantiate(BulletPrefab, FirePoint.position, Quaternion.identity);
    }

    /// <summary>
    /// Weapon usage cooldown timer
    /// </summary>
    protected void UpdateCooldown()
    {
        if (m_actualCooldown < WeaponUsageCooldown) m_actualCooldown += Time.deltaTime;
        else
        {
            CanShoot = true;
            m_actualCooldown = 0f;
        }
    }
}
