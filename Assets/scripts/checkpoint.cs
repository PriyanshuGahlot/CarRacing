using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "car")
        {
            raceController.checkpointMet[id-1] = true;
            FindObjectOfType<audioManager>().play("coin");
            Destroy(transform.gameObject);
        }
    }
}
