using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ingameUiController : MonoBehaviour
{
    public float coinMultiplyer;
    public Text countDown;
    public Text timer;
    public Text timeTakenToFinish;
    public Text coinEarnedText;
    public Text speed;
    public GameObject endRacePanel;
    public GameObject pausemenu;
    public GameObject greyPanel;
    public Button resume;
    public Button restart;
    public Button restartRacefinish;
    public GameObject speedometer;
    public Button mainMenu;
    public Button mainMenuRacefinish;
    [HideInInspector]
    public bool carControlable;
    float currTime;
    Rigidbody car;
    [HideInInspector]
    public bool pauseMenuUp;

    void Start()
    {
        Cursor.visible = false;
        setOnClick();
        pauseMenuUp = false;
        carControlable = false;
        countDown.text = "3";
        Invoke("setCountDownTo2", 1);
        endRacePanel.SetActive(false);
        pausemenu.SetActive(false);
        currTime = 0;
        car = FindObjectOfType<car>().transform.GetComponent<Rigidbody>();
        speedometer.SetActive(false);
    }

    void Update()
    {
        //games sounds shoould stop when i go to pausemenu
        //if (pauseMenuUp) FindObjectOfType<audioManager>().stopAll();
        if (carControlable)
        {
            currTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currTime);
            timer.text = time.ToString(@"mm\:ss\:fff");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenuUp)
            {
                setPauseMenuUp();
            }
            else
            {
                setPauseMenuDown();
            }
        }
        speed.text = ((int)(car.velocity.magnitude * 2f )).ToString();
    }

    public void raceCompleted()
    {
        Cursor.visible = true;
        speedometer.SetActive(false);
        endRacePanel.SetActive(true);
        carControlable = false;
        timer.gameObject.SetActive(false);
        timeTakenToFinish.text = "Time Taken      " + timer.text;
        int coinsEarned = (int)((1000000 / (int)currTime) * coinMultiplyer);
        coinEarnedText.text = "Coins earned     " + coinsEarned.ToString();
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + coinsEarned);
        FindObjectOfType<audioManager>().play("uiSong"); //-------------------------uisound
        FindObjectOfType<car>().GetComponent<car>().stopCar();
    }

    void setCountDownTo2()
    {
        countDown.text = "2";
        Invoke("setCountDownTo1", 1);
    }

    void setCountDownTo1()
    {
        countDown.text = "1";
        Invoke("removeCountDown", 1);
    }

    void removeCountDown()
    {
        carControlable = true;
        countDown.gameObject.SetActive(false);
        greyPanel.SetActive(false);
        speedometer.SetActive(true);
    }

    public void setPauseMenuUp()
    {
        Cursor.visible = true;
        FindObjectOfType<car>().GetComponent<AudioSource>().Stop();
        string[] carSoundNames = {"raceCar","basicCar","monsterTruck"}; //----------------------------------to stop the engine sound when pause menu is up
        foreach(string carSound in carSoundNames)
        {
            FindObjectOfType<audioManager>().stop(carSound);
        }
        pausemenu.SetActive(true);
        Time.timeScale = 0;
        pauseMenuUp = true;
    }
    public void setPauseMenuDown()
    {
        FindObjectOfType<car>().GetComponent<AudioSource>().Play();
        pausemenu.SetActive(false);
        Time.timeScale = 1;
        pauseMenuUp = false;
        Cursor.visible = false;
    }
    void setOnClick()
    {
        pauseMenu menuFun = FindObjectOfType<pauseMenu>().gameObject.GetComponent<pauseMenu>();
        resume.onClick.AddListener(menuFun._resume);
        restart.onClick.AddListener(menuFun._restart);
        restartRacefinish.onClick.AddListener(menuFun._restart);
        mainMenu.onClick.AddListener(menuFun._mainmenu);
        mainMenuRacefinish.onClick.AddListener(menuFun._mainmenu);
    }
}
