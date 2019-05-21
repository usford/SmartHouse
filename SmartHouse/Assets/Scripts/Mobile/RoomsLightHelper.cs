using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomsLightHelper : MonoBehaviour
{
    public Toggle toggle;
    private LightWebSocket lightWB;

    [Tooltip("Наш WebSocket")]
    [SerializeField]
    private GameObject webSocket;

    private List<GameObject> switches = new List<GameObject>();
    private List<SwitchReal> sr = new List<SwitchReal>();
    private List<GameObject> lamps = new List<GameObject>();
    private List<LampHelper> lh = new List<LampHelper>();

    private int indexDevice = 0;
    int value1 = 0;

    private void Start()
    {
        lightWB = webSocket.GetComponent<LightWebSocket>();
        StartCoroutine(Begin());

        switches.AddRange(GameObject.FindGameObjectsWithTag("Switch"));
        lamps.AddRange(GameObject.FindGameObjectsWithTag("Lamp"));

        foreach (GameObject go in switches)
        {
            if (go.GetComponent<SwitchReal>() != null)
            {
                sr.Add(go.GetComponent<SwitchReal>());
            }
        }

        foreach (GameObject go in lamps)
        {
            if (go.GetComponent<LampHelper>() != null)
            {
                lh.Add(go.GetComponent<LampHelper>());
            }
        }
    }

    IEnumerator Begin()
    {
        yield return new WaitForSeconds(0.1f);
        ValueChangeDropDown();
    }

    public void Update()
    {
        ValueChangeDropDown();
    }

    public void ValueChangeDropDown()
    {
        string name = GetComponentInChildren<Dropdown>().captionText.text;
        bool light = GameObject.Find(name).GetComponentInChildren<Light>().enabled;
        toggle.isOn = light;
    }

    public void ValueChangeToggle()
    {
        bool check;
        check = GetComponentInChildren<Toggle>().isOn;
        string name = GetComponentInChildren<Dropdown>().captionText.text;
        GameObject go = GameObject.Find(name);
        go.GetComponentInChildren<Light>().enabled = check;
        GetComponent<AudioSource>().Play();

        int i = 0;
        foreach (Devices d in lightWB.devicesList.devices)
        {
            if (d.deviceId == go.GetComponent<LampHelper>().DeviceId)
            {
                indexDevice = i;
            }
            i++;
        }

        switch (check)
        {
            case true:
                lightWB.devicesList.devices[indexDevice].value1 = 1;
                value1 = 1;
                break;

            case false:
                lightWB.devicesList.devices[indexDevice].value1 = 0;
                value1 = 0;
                break;

        }

        if (value1 == 0) //Розетка включена
        {
            Debug.Log("Выключена");
            lightWB.Send("{\"deviceId\":\"" + go.GetComponent<LampHelper>().DeviceId + "\",\"deviceType\":29,\"value1\":1,\"value2\":0}");
        }
        else
        {
            Debug.Log("Включена");
            lightWB.Send("{\"deviceId\":\"" + go.GetComponent<LampHelper>().DeviceId + "\",\"deviceType\":29,\"value1\":0,\"value2\":0}");
        }
    }
}
