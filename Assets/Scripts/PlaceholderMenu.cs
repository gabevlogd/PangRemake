using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceholderMenu : MonoBehaviour
{
    public GameObject WinMessage;
    public GameObject LoseMessage;

    private void Awake()
    {
        if (GameManager.PlayerWin) WinMessage.SetActive(true);
        else LoseMessage.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
