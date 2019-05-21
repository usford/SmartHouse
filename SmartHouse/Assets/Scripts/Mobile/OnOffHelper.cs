using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffHelper : MonoBehaviour
{
    public LightWebSocket lightWB;

    /// <summary>
    /// Включить весь свет
    /// </summary>
    public void OnLight()
    {
        foreach (Devices d in lightWB.devicesList.devices)
        {
            lightWB.Send("{\"deviceId\":\"" + d.deviceId + "\",\"deviceType\":29,\"value1\":0,\"value2\":0}");
        }
    }

    /// <summary>
    /// Выключить весь свет
    /// </summary>
    public void OffLight()
    {
        foreach (Devices d in lightWB.devicesList.devices)
        {
            lightWB.Send("{\"deviceId\":\"" + d.deviceId + "\",\"deviceType\":29,\"value1\":1,\"value2\":0}");
        }
    }
}
