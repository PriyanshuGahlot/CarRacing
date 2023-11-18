using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startMenuController : MonoBehaviour
{
    public Transform cars;
    public Transform maps;
    public GameObject main;
    public GameObject garage;
    public GameObject mapSelection;
    public GameObject buyCarBtn;
    public GameObject selectCarBtn;
    public GameObject prizeHolder;
    public Text prizeText;
    public Text totalMoneyText;
    public Text carName;
    public Text underline;
    public GameObject notEnufMoneyText;
    public float rotationSpeed;
    public spawner spawner;
    public int raceCarPrize;
    public int muscleCarPrize;
    public int bananaCarPrize;
    public int monsterTruckPrize;
    int selectedCar = 0;
    int selectedMap = 0;   
    //order of childerin in parent should be same as order in the list very imp
    string[] carNames = { "basicCar", "monsterTruck", "bananaCar", "raceCar", "muscleCar" };
    string[] mapNames = { "countrySide", "desert" };
    Dictionary<string, int> carPrizes = new Dictionary<string, int>();
    Vector3 originalCarPos;
    audioManager audio;
    bool garageUp;
    int money;
    Dictionary<string, bool> unlockedCars;


    private void Start()
    {
        //PlayerPrefs.SetInt("money", 0);
        //reset();
        //QualitySettings.SetQualityLevel(2);
        Screen.SetResolution((int)Screen.width, (int)Screen.height, true);
        notEnufMoneyText.SetActive(false);
        if (FindObjectOfType<spawner>() == null) Instantiate(spawner, Vector3.zero, Quaternion.identity);
        audio = FindObjectOfType<audioManager>().GetComponent<audioManager>();
        originalCarPos = cars.position;
        if (FindObjectOfType<spawner>().carToSpawn == "")
        {
            selectedCar = 0;
        }
        else
        {
            int i = 0;
            foreach (string name in carNames)
            {
                if (name == FindObjectOfType<spawner>().carToSpawn) selectedCar = i;
                else i++;
            }
        }
        if (FindObjectOfType<spawner>().mapToSpawn == "")
        {
            selectedMap = 0;
        }
        else
        {
            int i = 0;
            foreach (string name in mapNames)
            {
                if (name == FindObjectOfType<spawner>().mapToSpawn) selectedMap = i;
                else i++;
            }
        }
        main.SetActive(true);
        garage.SetActive(false);
        mapSelection.SetActive(false);
        carPrizes.Add("monsterTruck", monsterTruckPrize);
        carPrizes.Add("bananaCar", bananaCarPrize);
        carPrizes.Add("muscleCar", muscleCarPrize);
        carPrizes.Add("raceCar", raceCarPrize);
        //--------player creation--------------------------------------------------------------------------------------
        money = PlayerPrefs.GetInt("money");
        //if (money == null) money = 0;
        int isBananaCarUnlocked = PlayerPrefs.GetInt("bananaCar");
        //if (isBananaCarUnlocked == null) isBananaCarUnlocked = 0;
        int isRaceCarUnlocked = PlayerPrefs.GetInt("raceCar");
        //if (isRaceCarUnlocked == null) isRaceCarUnlocked = 0;
        int isMuscleCarUnlocked = PlayerPrefs.GetInt("muscleCar");
        //if (isMuscleCarUnlocked == null) isMuscleCarUnlocked = 0;
        int isMonsterTruckUnlocked = PlayerPrefs.GetInt("monsterTruck");
        //if (isMonsterTruckUnlocked == null) isMonsterTruckUnlocked = 0;
        unlockedCars = new Dictionary<string, bool>();
        unlockedCars.Add("basicCar",true);
        unlockedCars.Add("bananaCar", isBananaCarUnlocked != 0);
        unlockedCars.Add("raceCar", isRaceCarUnlocked != 0);
        unlockedCars.Add("muscleCar", isMuscleCarUnlocked != 0);
        unlockedCars.Add("monsterTruck", isMonsterTruckUnlocked != 0);
        //-------------------------------------------------------------------------------------------------------------
        garageUp = false;
        totalMoneyText.text = money.ToString();
        setCars();
    }

    private void Update()
    {
        cars.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }

    void setCars()
    {
        string visibleCar = "";
        for(int i = 0;i<cars.childCount; i++)
        {
            GameObject currCar = cars.GetChild(i).gameObject;
            if (currCar.name != carNames[selectedCar]) currCar.SetActive(false);
            else
            {
                currCar.SetActive(true);
                visibleCar = carNames[selectedCar];
            }
        }
        //set car name
        switch (visibleCar)
        {
            case "basicCar":
                carName.text = "Jerry";
                underline.text = "______";
                break;
            case "raceCar":
                carName.text = "Falcon";
                underline.text = "_______";
                break;
            case "muscleCar":
                carName.text = "The Horse";
                underline.text = "__________";
                break;
            case "bananaCar":
                carName.text = "Kela-Kela";
                underline.text = "__________";
                break;
            case "monsterTruck":
                carName.text = "Ox";
                underline.text = "___";
                break;
            default:
                Debug.Log("no car");
                break;
        }
        if (garageUp)
        {
            if (!unlockedCars[visibleCar])  //car locked
            {
                prizeHolder.SetActive(true);
                prizeText.text = carPrizes[visibleCar].ToString();
                selectCarBtn.SetActive(false);
                buyCarBtn.SetActive(true);
                cars.position = new Vector3(-3.5f, 1, 0);
            }
            else                           //car unlocked
            {
                selectCarBtn.SetActive(true);
                prizeHolder.SetActive(false);
                buyCarBtn.SetActive(false);
                cars.position = new Vector3(0, 1, 0);
            }
        }
    }

    void setMap()
    {
        for (int i = 0; i < maps.childCount; i++)
        {
            GameObject currMap = maps.GetChild(i).gameObject;
            if (currMap.name != mapNames[selectedMap]) currMap.SetActive(false);
            else currMap.SetActive(true);
        }
    }

    public void _garage()
    {
        audio.play("click");
        main.SetActive(false);
        garage.SetActive(true);
        garageUp = true;
        setCars();
    }

    public void _selectCarSlection()
    {
        if (unlockedCars[carNames[selectedCar]])
        {
            audio.play("click");
            main.SetActive(true);
            garage.SetActive(false);
            cars.position = originalCarPos;
            garageUp = false;
        }
    }

    public void _nextCarSlection()
    {
        audio.play("click");
        if (selectedCar + 1 >= carNames.Length)
        {
            selectedCar = 0;
        }
        else { selectedCar++; }
        setCars();
    }

    public void _prevCarSlection()
    {
        audio.play("click");
        if (selectedCar - 1 < 0)
        {
            selectedCar = carNames.Length - 1;
        }
        else { selectedCar--; }
        setCars();
    }

    public void _selectMap()
    {
        audio.play("click");
        setMap();
        main.SetActive(false);
        garage.SetActive(false);
        mapSelection.SetActive(true);
        cars.position = Vector3.up * 50;
    }

    public void _nextMapSelection()
    {
        audio.play("click");
        if (selectedMap + 1 >= mapNames.Length)
        {
            selectedMap = 0;
        }
        else { selectedMap++; }
        setMap();
    }

    public void _prevMapSelection()
    {
        audio.play("click");
        if (selectedMap - 1 < 0)
        {
            selectedMap = mapNames.Length - 1;
        }
        else { selectedMap--; }
        setMap();
    }

    public void _backMapSelection()
    {
        audio.play("click");
        main.SetActive(true);
        garage.SetActive(false);
        mapSelection.SetActive(false);
        cars.position = originalCarPos;
    }

    public void _selectMapSelection() //-------------------race starts------------------
    {
        audio.play("click");
        FindObjectOfType<spawner>().carToSpawn = carNames[selectedCar];
        FindObjectOfType<spawner>().mapToSpawn = mapNames[selectedMap];
        SceneManager.LoadScene(1);
    }

    public void _quit()
    {
        audio.play("click");
        Application.Quit();
    }

    public void _buy()
    {
        if (money >= carPrizes[carNames[selectedCar]])
        {
            money -= carPrizes[carNames[selectedCar]];
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.SetInt(carNames[selectedCar], 1);
            unlockedCars[carNames[selectedCar]] = true;
            setCars();
            totalMoneyText.text = money.ToString();

        }
        else StartCoroutine(flash(notEnufMoneyText, 1));
    }

    void reset()
    {
        PlayerPrefs.SetInt("money", 0);
        foreach(string i in carNames)
        {
            if (i == "basicCar")
            {
                PlayerPrefs.SetInt(i, 1);
            }
            else PlayerPrefs.SetInt(i, 0);
        }
    }

    IEnumerator flash(GameObject obj,float time)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(time);
        obj.SetActive(false);
    }
}
