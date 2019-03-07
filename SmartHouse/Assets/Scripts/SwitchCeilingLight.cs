using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchCeilingLight : MonoBehaviour
{
    public GameObject light;
    public GameObject ceilingLight;
    bool onOff = false;
    void Start()
    {
    }

    void OnTriggerStay()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            gameObject.GetComponent<AudioSource>().Play();
            if (onOff)
            {
                light.GetComponent<Light>().enabled = false;
                ceilingLight.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
            }
            else
            {
                light.GetComponent<Light>().enabled = true;
                ceilingLight.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
            }
            onOff = !onOff;
        }
    }
}
