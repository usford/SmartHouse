using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampHelper : MonoBehaviour
{
    [Tooltip("ID устройства")]
    [SerializeField]
    private string deviceId;

    public string DeviceId
    {
        get
        {
            return deviceId;
        }
        set
        {
            deviceId = value;
        }
    }
}
