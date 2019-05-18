using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMobile : MonoBehaviour
{
    private Camera mainCamera;
    private bool pressed;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void Rotation(string rotation)
    {
        if (pressed)
        {
            switch (rotation)
            {
                case "Left":
                    {
                        mainCamera.transform.Rotate(Vector3.forward, 4f);
                        break;
                    }
                case "Right":
                    {
                        mainCamera.transform.Rotate(Vector3.forward, -4f);
                        break;
                    }
            }
        }      
    }

    public void Click()
    {
        pressed = true;
    }

    public void NoClick()
    {
        pressed = false;
    }
}
