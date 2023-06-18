using UnityEngine;
using UnityEngine.UI;
using Gabevlogd.Patterns;

public class HUDManager : MonoBehaviour, IObserver
{
    public Text Score;
    public Image Timer;
    public Image LifePoint;

    private float m_timer = 100f;
    private float MaxLifePoint;

    private void Awake() => MaxLifePoint = FindObjectOfType<Player>().DefaultLifePoint;

    private void OnEnable() => PlayerStats.Observable.Register(Constants.HUD, this);
    private void OnDisable() => PlayerStats.Observable.Unregister(Constants.HUD, this);

    private void Update() => UpdateTimer();
    


    public void UpdateObserver(string message = null, int value = -1)
    {
        if (message == Constants.LIFE)
        {
            LifePoint.fillAmount = (float)value / MaxLifePoint;
        }
        if (message == Constants.SCORE)
        {
            Score.text = Constants.SCORE + ": " + value.ToString();
        }
    }

    public void UpdateTimer()
    {
        if(m_timer >= 0f)
        {
            m_timer -= Time.deltaTime;
            Timer.fillAmount = m_timer / 100f;
        }
        
    }
}
