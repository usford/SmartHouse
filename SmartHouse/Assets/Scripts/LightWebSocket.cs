using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Web;
using System.Net.Sockets;
using System.IO;
using System;
using WebSocketSharp;

[Serializable]
public class Devices
{
    public string deviceId;
    public int deviceType;
    public int value1;
}

[Serializable]
public class DevicesList
{
    public List<Devices> devices = new List<Devices>();
}

[Serializable]
public class DeviceReport
{
    public string deviceId;
    public int deviceType;
    public int value1;
}



public class LightWebSocket : MonoBehaviour
{
    public WebSocket ws;
    
    private List<GameObject> switchesAll = new List<GameObject>();
    private List<GameObject> switches = new List<GameObject>();

    public DevicesList devicesList = new DevicesList();
    public DeviceReport deviceReport = new DeviceReport();

    delegate void CallBack(string msg);
    CallBack callback;

    private SwitchReal sr;

    [Tooltip("Адрес сервера")]
    [SerializeField]
    private string url;
    
    public string Url
    {
        get
        {
            return url;
        }
        set
        {
            url = value;
        }
    }

    public void Awake()
    {      
        ws = new WebSocket(url);
        ws.OnOpen += OnOpenHandler;
        ws.OnError += OnErrorHandler;
        ws.OnMessage += OnMessageHandler;
        ws.OnClose += OnCloseHandler;
        
        switchesAll.AddRange(GameObject.FindGameObjectsWithTag("Switch"));
        
        foreach (GameObject go in switchesAll)
        {
            if (go.GetComponent<SwitchReal>() != null)
            {
                switches.Add(go);
            }
        }


    }

    public void Start()
    {
        callback = Accepted;
        Connect();
    }

    private void Accepted(string msg)
    {
        Debug.Log(msg);
    }

    private void OnApplicationQuit()
    {
        Close();
    }

    public void Send(string ev)
    {
        ws.Send(ev);
        Debug.Log("Отправлено: " + ev);
    }

    public void Connect()
    {
        ws.Connect();
    }

    public void Close()
    {
        ws.Close();
    }

    public void OnOpenHandler(object sender, EventArgs e)
    {
        Debug.Log("WebSocket connected!");
    }

    public void OnErrorHandler(object sender, WebSocketSharp.ErrorEventArgs e)
    {

        Debug.Log(e.Message);
    }

    public void OnCloseHandler(object sender, CloseEventArgs e)
    {

        Debug.Log("WebSocket disconnected!");
    }

    string fixDeviceReport(string value)
    {
        value = value.Replace("{\"deviceReport\":", "");      
        value = value.TrimEnd('}');
        value = value + '}';
        return value;
    }

    public void OnMessageHandler(object sender, MessageEventArgs e)
    {
        Debug.Log("Получено: " + e.Data);
        string[] split = e.Data.Split(new char[] { '"' });
        //Debug.Log("Type: " + split[1]);
        switch (split[1])
        {
            case "devices":
                devicesList = JsonUtility.FromJson<DevicesList>(e.Data);

                foreach (Devices d in devicesList.devices)
                {
                    print("id устройства: " + d.deviceId);
                    print("value1: " + d.value1);
                    print("deviceType: " + d.deviceType);
                }
                Debug.Log("Количество устройств: " + devicesList.devices.Count);
                break;
            case "deviceReport":
                {
                    string jsonString = fixDeviceReport(e.Data);
                    deviceReport = JsonUtility.FromJson<DeviceReport>(jsonString);
                    print("id устройства: " + deviceReport.deviceId);
                    print("value1: " + deviceReport.value1);
                    print("deviceType: " + deviceReport.deviceType);
                    foreach (Devices d in devicesList.devices)
                    {
                        if (d.deviceId == deviceReport.deviceId)
                        {
                            d.value1 = deviceReport.value1;
                        }
                    }
                    break;
                }
        }

    }
}
