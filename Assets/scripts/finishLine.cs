using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLine : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "car")
        {

            if (raceController.allCheckpointsMet())
            {
                //race completed
                FindObjectOfType<audioManager>().play("raceFinish");
                FindObjectOfType<raceController>().GetComponent<raceController>().raceCompleted();
                FindObjectOfType<car>().GetComponent<AudioSource>().Stop();
                Destroy(transform.gameObject);
            }
        }
    }

}
