using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using WebSocketSharp;

public class SwitchReal : MonoBehaviour
{
    private bool onMouse = false;
    private bool onOff = false;

    private LightWebSocket lightWB; //Работа со скриптов на объекте WebSocket

    [Tooltip("ID устройства")]
    [SerializeField]
    private string deviceId;

    public string DeviceId
    {
        get
        {
            return deviceId;
        }
    }

    [Tooltip("Наша лампа")]
    [SerializeField]
    private GameObject myLight;

    [Tooltip("Наш свет от лампы")]
    [SerializeField]
    private Light ceilingLight;

    [Tooltip("Наш WebSocket")]
    [SerializeField]
    private GameObject webSocket;

    private List<GameObject> switchesAll = new List<GameObject>();
    private List<GameObject> switches = new List<GameObject>();

    //Поля для работы с выключателем
    private int value1;
    private int indexDevice = 0;

    public void Awake()
    {
        lightWB = webSocket.GetComponent<LightWebSocket>();
        switchesAll.AddRange(GameObject.FindGameObjectsWithTag("Switch"));
        Invoke("LightCheck", 0.5f);
    }

    private void Update()
    {
        LightCheck();
        if (onMouse == true)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                lightOnOff();
                gameObject.GetComponent<AudioSource>().Play();
                int i = 0;
                foreach (Devices d in lightWB.devicesList.devices)
                {
                    if (d.deviceId == deviceId)
                    {
                        indexDevice = i;
                    }
                    i++;
                }
                Debug.Log(indexDevice);
                switch(value1)
                {
                    case 0:
                        lightWB.devicesList.devices[indexDevice].value1 = 1;
                        value1 = 1;
                        break;

                    case 1:
                        lightWB.devicesList.devices[indexDevice].value1 = 0;
                        value1 = 0;
                        break;

                }
                            
            }
        }
    }

    private void LightCheck()
    {
        foreach (Devices d in lightWB.devicesList.devices)
        {
            if (deviceId == d.deviceId)
            {
                value1 = d.value1;
            }
        }
        if (value1 == 0) //Розетка включена
        {          
            //Debug.Log("Включена");
            myLight.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
            ceilingLight.enabled = true;
        }
        else
        {
            //Debug.Log("Выключена");
            myLight.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
            ceilingLight.enabled = false;
        }
    }

    private void lightOnOff()
    {
        foreach (Devices d in lightWB.devicesList.devices)
        {
            if (deviceId == d.deviceId)
            {
                value1 = d.value1;
            }
        }

        if (value1 == 0) //Розетка включена
        {
            //Debug.Log("Выключена");
            lightWB.Send("{\"deviceId\":\"" + deviceId + "\",\"deviceType\":29,\"value1\":1,\"value2\":0}");
            myLight.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
            ceilingLight.enabled = false;
        }
        else
        {
            //Debug.Log("Включена");
            lightWB.Send("{\"deviceId\":\"" + deviceId + "\",\"deviceType\":29,\"value1\":0,\"value2\":0}");
            myLight.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.white);
            ceilingLight.enabled = true;
        }
    }

    private void OnMouseEnter()
    {
        onMouse = true;
    }

    private void OnMouseExit()
    {
        onMouse = false;
    }
}
