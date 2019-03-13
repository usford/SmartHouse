using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject door;
    bool change = true;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerStay()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            door.GetComponent<Animation>().Play();
        }
    }
}
