using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    ingameUiController ui;

    private void Start()
    {
        ui = FindObjectOfType<ingameUiController>().GetComponent<ingameUiController>();
    }

    public void _resume()
    {
        FindObjectOfType<audioManager>().play("click");
        ui.setPauseMenuDown();
    }
    public void _restart()
    {
        FindObjectOfType<audioManager>().play("click");
        Time.timeScale = 1;
        Invoke("restart", 0.14f);
    }
    public void _mainmenu()
    {
        FindObjectOfType<audioManager>().play("click");
        Time.timeScale = 1;
        Invoke("mainmenu", 0.14f);
    }

    void restart()
    {
        FindObjectOfType<spawner>().spawn();
        Time.timeScale = 1;
    }

    void mainmenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

}
