using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    
    public float test;

    private Rigidbody m_rigidbody;
    private Vector3 m_startVelocity;
    private Vector3 m_lastVelocity;
    private float m_gravity = 9.81f;
    private float m_time;
    private float m_startingY;
    private bool m_startingFall;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_startingFall = true;
        m_lastVelocity = m_rigidbody.velocity;
        m_startingY = transform.position.y;
        m_startVelocity = new Vector3(2f, Mathf.Sqrt(2f * m_gravity * m_startingY), m_rigidbody.velocity.z);
    }


    private void OnCollisionEnter(Collision collision)
    {
        m_startingFall = false;

        if (collision.GetContact(0).normal.x != 0f) m_startVelocity.x *= -1f;

        else
        {
            m_rigidbody.velocity = m_startVelocity;
            m_time = 0;
        }




        //Debug.Log(collision.GetContact(0).normal);

    }

    private void Update()
    {
        m_time += Time.deltaTime;
        float Vy;

        if (m_startingFall)
        {
            Vy = - m_gravity * m_time;
        }
        else
        {
            Vy = m_startVelocity.y - m_gravity * m_time;
        }
        

        m_rigidbody.velocity = new Vector3(m_startVelocity.x, Vy, m_lastVelocity.z);

        m_lastVelocity = m_rigidbody.velocity;
    }
}
