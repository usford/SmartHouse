using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationHandler : MonoBehaviour
{
    public Transform house;
    float mouseHorizontal;
    float mouseVertical;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            RotationZ();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void RotationZ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Debug.Log("Вращаем");

        mouseHorizontal = Input.GetAxis("Mouse X");
        mouseVertical = Input.GetAxis("Mouse Y");


        if (Input.GetKey(KeyCode.E))
        {
            if (mouseHorizontal > 0)
            {
                //transform.Rotate(Vector3.up, 5f);
                mainCamera.transform.Rotate(Vector3.forward, -5f);
            }
            else if (mouseHorizontal < 0)
            {
                //transform.Rotate(Vector3.up, -5f);
                mainCamera.transform.Rotate(Vector3.forward, 5f);
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (mouseVertical > 0)
            {
                house.transform.Rotate(Vector3.right, 5f);
            }
            else if (mouseVertical < 0)
            {
                house.transform.Rotate(Vector3.right, -5f);
            }
        }

        

    }
}
