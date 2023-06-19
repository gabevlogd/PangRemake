using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gabevlogd.Patterns;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public static bool PlayerWin;

    public Player Player;
    public Observable<string> Observable;

    public int CurrentLevel;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (Observable == null) Observable = new Observable<string>();
    }

    private void Start()
    {
        Observable.NotifyObservers(Constants.AUDIO, Constants.MUSIC); // play soundTrack;
    }

    public static void LoadNextLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public static void GameOver() => SceneManager.LoadScene(3);

    public static void CheckWinCondition()
    {
        Instance.Invoke("WinCondition", 1f);
    }

    /// <summary>
    /// check if there are other balls in the scene
    /// </summary>
    private void WinCondition()
    {
        Ball ball = FindObjectOfType<Ball>();
        Debug.Log(ball);

        if (ball == null)
        {
            if (Instance.CurrentLevel == 3)
            {
                PlayerWin = true;
                GameOver();
            }
            else LoadNextLevel();
        }
    }
}
