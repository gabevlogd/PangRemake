using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gabevlogd.Patterns;

public class PlayerStats : BaseStats
{
    public static Observable<string> Observable;
    public bool TriggerShield;
    public bool CanClimbUp;
    public bool CanClimbDown;
    private int m_score;

    public PlayerStats(int lifePoint, int score) : base(lifePoint)
    {
        m_score = score;
        Observable = new Observable<string>();
    }

    public void SetLifePoint(int value)
    {
        m_lifePoint += value;
        Observable.NotifyObservers(Constants.HUD, Constants.LIFE, m_lifePoint);
        if (m_lifePoint <= 0)
        {
            GameManager.PlayerWin = false;
            GameManager.GameOver();
        }
    }

    public void SetScore(int value)
    {
        //Debug.Log("SETSCORE");
        m_score += value;
        PlayerPrefs.SetFloat(Constants.SCORE, m_score);
        Observable.NotifyObservers(Constants.HUD, Constants.SCORE, m_score);
    }

}
