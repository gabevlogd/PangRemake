using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public Ball BallPrefab;
    public enum BallSize { S, M, L, XL };
    public BallSize Size;

    public float StartLaterlaVeclocity = 0;

    private Rigidbody m_rigidbody;

    private Vector3 m_velocity;

    private float m_gravity = 9.81f;
    private float m_time;
    private float m_maxHeight;
    private float m_startingVy;

    private bool m_firstFall = true;
    

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        if (StartLaterlaVeclocity == 0) StartLaterlaVeclocity = 2f;
    }

    private void Start()
    {
        transform.localScale = GetBallSize();
        m_maxHeight = transform.localScale.y * 0.5f * 15f;
        m_velocity = new Vector3(StartLaterlaVeclocity, 0f, 0f);
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
    }

    private void Update()
    {
        m_time += Time.deltaTime;

        if (m_firstFall) CalculateVerticalVelocity(0f);
        else CalculateVerticalVelocity(m_startingVy);

        SetVelocity();

        //test
        if (Input.GetKey(KeyCode.T)) m_time = 0;
    }

    /// <summary>
    /// Equation of vertical motion
    /// </summary>
    /// <param name="startingVy">y component of the velocity vector</param>
    private void CalculateVerticalVelocity(float startingVy) => m_velocity.y = startingVy - m_gravity * m_time;

    private void SetVelocity() => m_rigidbody.velocity = m_velocity;

    /// <summary>
    /// Returns the ball size based on enum Size's value
    /// </summary>
    private Vector3 GetBallSize()
    {
        switch (Size)
        {
            case BallSize.XL:
                return new Vector3(1f,1f,1f);
            case BallSize.L:
                return new Vector3(0.8f, 0.8f, 0.8f);
            case BallSize.M:
                return new Vector3(0.6f, 0.6f, 0.6f);
            case BallSize.S:
                return new Vector3(0.4f, 0.4f, 0.4f);
            default:
                Debug.Log("Ball size error");
                return Vector3.zero;
        }
    }

    /// <summary>
    /// Spawns two new smaller balls in opposite directions
    /// </summary>
    public void SpwanNewBalls()
    {
        if (Size == BallSize.S) return;

        Ball ballOne = Instantiate(BallPrefab, transform.position, Quaternion.identity);
        Ball ballTwo = Instantiate(BallPrefab, transform.position, Quaternion.identity);

        ballOne.StartLaterlaVeclocity = 2f;
        ballTwo.StartLaterlaVeclocity = -2f;

        ballOne.Size = (BallSize)((int)this.Size - 1);
        ballTwo.Size = (BallSize)((int)this.Size - 1);

        //Debug.Log(ballOne.StartLaterlaVeclocity);
        //Debug.Log(ballTwo.StartLaterlaVeclocity);
    }

}
