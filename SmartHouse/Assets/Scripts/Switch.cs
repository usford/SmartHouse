using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Switch : MonoBehaviour
{
    public GameObject light;
    public GameObject ceilingLight;

    private bool onStay = false;
    private bool onMouse = false;
    bool onOff = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (onMouse == true)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                try
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
                        ceilingLight.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(255f, 255f, 255f));
                    }
                    onOff = !onOff;
                }
                catch (Exception)
                {
                }
            }
        }
    }

    void OnMouseEnter()
    {
        onMouse = true;
    }

    void OnMouseExit()
    {
        onMouse = false;
    }
}
