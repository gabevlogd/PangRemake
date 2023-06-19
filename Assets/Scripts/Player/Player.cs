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
    private Coroutine m_lastFreezeRoutine;

    private void Awake()
    {
        if (Stats == null) Stats = new PlayerStats(0, 0);
        if (m_statesManager == null) m_statesManager = new PlayerStatesManager(transform);
        m_statesManager.CurrentState = m_statesManager.AllStates[PlayerState.Idle];
        
    }

    private void Start()
    {
        InitPlayerStats();
        //Debug.Log(PlayerPrefs.GetFloat(Constants.SCORE));
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
            GameManager.CheckWinCondition();
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
        if (Stats.TriggerFreezer) PerformFreezerEffect();
    }

    /// <summary>
    /// Sets the current state of the player based on the inputs
    /// </summary>
    private void SetState()
    {
        if ((Input.GetKey(KeyCode.W) && Stats.CanClimbUp) || (Input.GetKey(KeyCode.S) && Stats.CanClimbDown) || (Stats.CanClimbUp && Stats.CanClimbDown)) m_statesManager.ChangeState(PlayerState.Climb);
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Weapon.CanShoot) m_statesManager.ChangeState(PlayerState.Shoot);
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
        if (GameManager.Instance.CurrentLevel == 1) Stats.SetScore(0);
        else Stats.SetScore((int)PlayerPrefs.GetFloat(Constants.SCORE));
    }


    private void PerformShieldEffect()
    {
        Stats.TriggerShield = false;
        //StopAllCoroutines();
        StartCoroutine(Invlunerability(Shield.Duration));
        PlayerStats.Observable.NotifyObservers(Constants.HUD, Constants.SHIELD, Shield.Duration);
    }

    private void PerformFreezerEffect()
    {
        Stats.TriggerFreezer = false;
        if (m_lastFreezeRoutine != null) StopCoroutine(m_lastFreezeRoutine);
        m_lastFreezeRoutine = StartCoroutine(FreezeBall(BallsFreezer.Duration));
        PlayerStats.Observable.NotifyObservers(Constants.HUD, Constants.FREEZE, Shield.Duration);
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

    private IEnumerator FreezeBall(float duration)
    {
        Ball.FreezeTime = true;
        yield return new WaitForSeconds(duration);
        Ball.FreezeTime = false;
        m_lastFreezeRoutine = null;
    }

}
