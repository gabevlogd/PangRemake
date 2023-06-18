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
        if (Stats == null) Stats = new PlayerStats(0, 0);
        if (m_statesManager == null) m_statesManager = new PlayerStatesManager(transform);
        m_statesManager.CurrentState = m_statesManager.AllStates[PlayerState.Idle];
        
    }

    private void Start()
    {
        InitPlayerStats();
        Debug.Log(PlayerPrefs.GetFloat(Constants.SCORE));
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
            LevelManager.CheckWinCondition();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ladder ladder)) Stats.CanClimbUp = true; 
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Ladder ladder)) Stats.CanClimbUp = false; 
    }

    private void Update()
    {
        SetState();
        m_statesManager.CurrentState.OnUpdate();
        if (Stats.TriggerShield) PerformShieldEffect();
    }

    /// <summary>
    /// Sets the current state of the player based on the inputs
    /// </summary>
    private void SetState()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Weapon.CanShoot) m_statesManager.ChangeState(PlayerState.Shoot);
        else if ((Input.GetKey(KeyCode.W) && Stats.CanClimbUp) || (Input.GetKey(KeyCode.S) && Stats.CanClimbDown)) m_statesManager.ChangeState(PlayerState.Climb);
        else if (Input.GetKey(KeyCode.A)) m_statesManager.ChangeState(PlayerState.WalkLeft);
        else if (Input.GetKey(KeyCode.D)) m_statesManager.ChangeState(PlayerState.WalkRight);
        else m_statesManager.ChangeState(PlayerState.Idle);
    }

    /// <summary>
    /// Initialize stats of the player
    /// </summary>
    private void InitPlayerStats()
    {
        Stats.SetLifePoint(DefaultLifePoint);
        if (LevelManager.Instance.CurrentLevel == 1) Stats.SetScore(0);
        else Stats.SetScore((int)PlayerPrefs.GetFloat(Constants.SCORE));
    }


    private void PerformShieldEffect()
    {
        Stats.TriggerShield = false;
        StopAllCoroutines();
        StartCoroutine(Invlunerability(Shield.Duration));
        PlayerStats.Observable.NotifyObservers(Constants.HUD, Constants.SHIELD, Shield.Duration);
    }

    /// <summary>
    /// Makes the player invulnerable (trigger the collider)
    /// </summary>
    /// <param name="duration">duration of the effect</param>
    public IEnumerator Invlunerability(float duration)
    {
        //Debug.Log("Invlunerability");
        GetComponent<Collider>().isTrigger = true;
        yield return new WaitForSeconds(duration);
        GetComponent<Collider>().isTrigger = false;
    }

}
