using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private SphereCollider m_sphereCollider;

    private Vector3 m_velocity;

    private float m_gravity = 9.81f;
    private float m_time;
    private float m_maxHeight;
    private float m_startingVy;

    private bool m_firstFall;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_sphereCollider = GetComponent<SphereCollider>();
        m_firstFall = true;

        m_maxHeight = m_sphereCollider.radius * 15f;
        
        m_velocity = new Vector3(2f, 0f, 0f);
    }


    private void OnCollisionEnter(Collision collision)
    {
        m_firstFall = false;

        if (collision.GetContact(0).normal.y > 0.5f)
        {
            float targetHeight = Mathf.Abs(m_maxHeight - transform.position.y);
            m_startingVy = Mathf.Sqrt(2f * m_gravity * targetHeight);
            m_time = 0;

        }
        else if (collision.GetContact(0).normal.y < -0.5f)
        {
            m_startingVy = -m_velocity.y;
            m_time = 0;
        }
        //Need to improve this part (more specific conditions needed)
        if (Mathf.Abs(collision.GetContact(0).normal.x) > 0.5f) m_velocity.x *= -1f;

        //Debug.Log(collision.GetContact(0).normal);
    }

    private void Update()
    {
        m_time += Time.deltaTime;

        if (m_firstFall) CalculateVerticalVelocity(0f);
        else CalculateVerticalVelocity(m_startingVy);

        SetVelocity();
    }

    /// <summary>
    /// Equation of vertical motion
    /// </summary>
    /// <param name="startingVy">y component of the velocity vector</param>
    private void CalculateVerticalVelocity(float startingVy) => m_velocity.y = startingVy - m_gravity * m_time;

    private void SetVelocity() => m_rigidbody.velocity = m_velocity;
}
