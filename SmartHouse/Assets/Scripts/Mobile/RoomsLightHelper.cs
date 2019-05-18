using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomsLightHelper : MonoBehaviour
{
    public Toggle toggle;

    private void Start()
    {
        string name = GetComponentInChildren<Dropdown>().captionText.text;
        bool light = GameObject.Find(name).GetComponentInChildren<Light>().enabled;
        toggle.isOn = light;
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
    }
}
