using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BasicWeapon Weapon;
    public PlayerStats Stats;
    [Min(1)]
    public int DefaultLifePoint;

    private PlayerStatesManager m_statesManager;

    private void Awake()
    {
        Stats = new PlayerStats(0, 0);

        m_statesManager = new PlayerStatesManager(transform);
        m_statesManager.CurrentState = m_statesManager.AllStates[PlayerState.Idle];
    }

    private void Start()
    {
        InitPlayerStats();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.transform.TryGetComponent(out Ball ball))
        {
            StartCoroutine(Invlunerability(2f));
            Stats.SetLifePoint(-ball.Damage);
            ball.SpwanNewBalls();
            Destroy(ball.gameObject);
        }
    }

    private void Update()
    {
        SetState();
        m_statesManager.CurrentState.OnUpdate();
    }

    /// <summary>
    /// Sets the current state of the player based on the inputs
    /// </summary>
    private void SetState()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Weapon.CanShoot) m_statesManager.ChangeState(PlayerState.Shoot);
        else if (Input.GetKey(KeyCode.A)) m_statesManager.ChangeState(PlayerState.WalkLeft);
        else if (Input.GetKey(KeyCode.D)) m_statesManager.ChangeState(PlayerState.WalkRight);
        else if (Input.GetKey(KeyCode.W)) m_statesManager.ChangeState(PlayerState.Climb);
        else m_statesManager.ChangeState(PlayerState.Idle);
    }

    /// <summary>
    /// Initialize stats of the player
    /// </summary>
    private void InitPlayerStats()
    {
        Stats.SetLifePoint(DefaultLifePoint);
        Stats.SetScore(0);
    }

    public IEnumerator Invlunerability(float duration)
    {
        //Debug.Log("Invlunerability");
        GetComponent<Collider>().isTrigger = true;
        yield return new WaitForSeconds(duration);
        GetComponent<Collider>().isTrigger = false;
    }

}
