using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class raceController : MonoBehaviour
{
    [HideInInspector]
    public static bool[] checkpointMet;


    void Start()
    {
        int numberOfChecpoints = transform.childCount;
        checkpointMet = new bool[numberOfChecpoints];
    }

    public void raceCompleted()
    {
        FindObjectOfType<car>().GetComponent<car>().stopCar();
        FindObjectOfType<ingameUiController>().raceCompleted();
    }

    public static bool allCheckpointsMet()
    {
        foreach (bool i in checkpointMet)
        {
            if (i == false) return false;
        }
        return true;
    }
}
