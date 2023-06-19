using UnityEngine;
using UnityEngine.UI;
using Gabevlogd.Patterns;

public class HUDManager : MonoBehaviour, IObserver
{
    public Text Score;
    public Image Timer;
    public Image LifePoint;
    public Image ShieldBar;

    private float m_timer = 100f;
    private float m_shieldTimer;
    private float m_maxLifePoint;

    private void Awake() => m_maxLifePoint = FindObjectOfType<Player>().DefaultLifePoint;

    private void OnEnable() => PlayerStats.Observable.Register(Constants.HUD, this);
    private void OnDisable() => PlayerStats.Observable.Unregister(Constants.HUD, this);

    private void Update()
    {
        UpdateTimer();
        UpdateShieldTimer();
    }

    public void UpdateObserver(string message = null, int value = -1)
    {
        if (message == Constants.LIFE) LifePoint.fillAmount = (float)value / m_maxLifePoint;
        else if (message == Constants.SCORE) Score.text = Constants.SCORE + ": " + value.ToString();
        else if (message == Constants.SHIELD) m_shieldTimer = value;

    }

    private void UpdateTimer()
    {
        if(m_timer >= 0f)
        {
            m_timer -= Time.deltaTime;
            Timer.fillAmount = m_timer / 100f;
        }
        else
        {
            GameManager.PlayerWin = false;
            GameManager.GameOver();
        }
    }

    private void UpdateShieldTimer()
    {
        if (m_shieldTimer >= 0)
        {
            m_shieldTimer -= Time.deltaTime;
            ShieldBar.fillAmount = m_shieldTimer / Shield.Duration;
        }
    }
}
