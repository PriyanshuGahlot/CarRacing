using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    Transform car;
    Transform camPosObj;
    Transform lookAtObj;
    public float camResetingSpeed = 1;

    Rigidbody rb;
    Vector3 correctPos;

    void Start()
    {
        car = FindObjectOfType<car>().transform;
        camPosObj = car.Find("camPosObj");
        lookAtObj = car.Find("lookAtObj");

        rb = transform.GetComponent<Rigidbody>();
        correctPos = camPosObj.position;
        transform.position = correctPos;
    }

    
    void Update()
    {
        transform.LookAt(lookAtObj);
        Vector3 carPos = car.position;
        correctPos = camPosObj.position;
        //rb.velocity =  (correctPos - transform.position) * car.GetComponent<Rigidbody>().velocity.magnitude;
        rb.velocity =  car.GetComponent<Rigidbody>().velocity + (correctPos - transform.position) * camResetingSpeed;
    }
}
