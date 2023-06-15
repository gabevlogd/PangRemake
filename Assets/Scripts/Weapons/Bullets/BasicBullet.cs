using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BasicBullet : MonoBehaviour
{
    public float VerticalSpeed;
    protected Rigidbody m_rigidbody;

    protected virtual void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        MoveUpwards();
    }

    /// <summary>
    /// Set the starting velocity of the bullet
    /// </summary>
    protected virtual void MoveUpwards() => m_rigidbody.velocity = new Vector3(0f, VerticalSpeed, 0f);
}
