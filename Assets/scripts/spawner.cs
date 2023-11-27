using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spawner : MonoBehaviour
{
    [HideInInspector]
    public string carToSpawn;
    [HideInInspector]
    public string mapToSpawn;

    public GameObject basicCar;
    public GameObject muscleCar;
    public GameObject bananaCar;
    public GameObject raceCar;
    public GameObject monsterTruck;
    public GameObject countrySide;
    public GameObject desert;
    public GameObject cam;
    public GameObject eventSystemForGameScene;
    public GameObject ui;

    public bool spawned;

    void Start()
    {
        carToSpawn = "";
        mapToSpawn = "";
        spawned = false;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            spawn();
        }
    }

    public void spawn()
    {
        //Debug.Log(mapToSpawn);
        //Debug.Log(carToSpawn);
        GameObject[] allobj = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allobj)
        {
            //if (this.gameObject != obj || obj.GetComponent<playerHolder>()!=null) Destroy(obj);
            if (this.gameObject != obj) Destroy(obj);
        }
        Instantiate(eventSystemForGameScene, Vector3.zero, Quaternion.identity);
        Instantiate(ui, Vector3.zero, Quaternion.identity);
        switch (mapToSpawn)
        {
            case "countrySide":
                Instantiate(countrySide, Vector3.zero, Quaternion.identity);
                break;
            case "desert":
                Instantiate(desert, Vector3.zero, Quaternion.identity);
                break;
            default:
                Debug.Log("no map");
                break;
        }
        Transform carSpawnObj = GameObject.Find("carSpawnPos").transform;
        switch (carToSpawn)
        {
            case "basicCar":
                Instantiate(basicCar, carSpawnObj.position, carSpawnObj.rotation);
                break;
            case "raceCar":
                Instantiate(raceCar, carSpawnObj.position, carSpawnObj.rotation);
                break;
            case "muscleCar":
                Instantiate(muscleCar, carSpawnObj.position, carSpawnObj.rotation);
                break;
            case "bananaCar":
                Instantiate(bananaCar, carSpawnObj.position, carSpawnObj.rotation);
                break;
            case "monsterTruck":
                Instantiate(monsterTruck, carSpawnObj.position, carSpawnObj.rotation);
                break;
            default:
                Debug.Log("no car");
                break;
        }
        Instantiate(cam, Vector3.zero, Quaternion.identity);
    }
}
