using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBullet : BasicBullet
{
    public int MaxLifespan;

    private LineRenderer m_lineRenderer;
    private Vector3 m_lastPosition;
    private int m_indexVertex = 3;
    private float m_lifespan;

    protected override void Awake()
    {
        base.Awake();
        InitLineRenderer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ball ball))
        {
            DestroyDetectedBall(ball);
            Destroy(this.gameObject);
        }
        else m_rigidbody.velocity = Vector3.zero;
    }

    private void Update()
    {
        UpdateLifespanCooldown();
        UpdateRopeLenght();
        RopeCollisionDetector();
    }

    /// <summary>
    /// Detects all the gamobject under the HookBullet
    /// </summary>
    private void RopeCollisionDetector()
    {
        int layerMask = 1 << 8; //layerMask of the player to ignore 
        RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, Vector3.down, 20f, ~layerMask);
        //Debug.Log(raycastHits.Length);
        if (raycastHits.Length > 1) DestroyDetectedEntity(raycastHits);
    }

    /// <summary>
    /// Destroys all the balls detected by the RopeCollisionDetector()
    /// </summary>
    private void DestroyDetectedEntity(RaycastHit[] raycastHits)
    {
        foreach(RaycastHit raycastHit in raycastHits)
        {
            if (raycastHit.collider.TryGetComponent(out Ball ball))
            {
                DestroyDetectedBall(ball);
            }
        }

        Destroy(this.gameObject);
    }

    private void DestroyDetectedBall(Ball ball)
    {
        ball.SpwanNewBalls();
        LevelManager.Instance.Player.Stats.SetScore(ball.GetPoint());
        Destroy(ball.gameObject);
    }

    /// <summary>
    /// Increases the length of the rope as the grappling hook ascends
    /// </summary>
    private void UpdateRopeLenght()
    {
        if (m_lastPosition != transform.position)
        {
            m_lastPosition = transform.position;
            m_lineRenderer.positionCount++;
            m_lineRenderer.SetPosition(m_indexVertex, m_lastPosition);
            m_indexVertex++;
        }
    }

    /// <summary>
    /// Initializes the line renderer with the starting line vertices from the ground to the hook
    /// </summary>
    private void InitLineRenderer()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.positionCount = 3;
        for (int i = 0; i < m_lineRenderer.positionCount; i++)
        {
            m_lineRenderer.SetPosition(i, transform.position - new Vector3(0f, 0.5f + i, 0f));
        }
    }

    /// <summary>
    /// Hook lifespan timer
    /// </summary>
    private void UpdateLifespanCooldown()
    {
        if (m_lifespan >= MaxLifespan) Destroy(this.gameObject);
        else m_lifespan += Time.deltaTime;
    }


}
