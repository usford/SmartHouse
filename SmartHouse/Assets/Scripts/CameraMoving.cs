using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetButton("Vertical")) MoveVertical();
        if (Input.GetButton("Horizontal")) MoveHorizontal();
    }

    private void MoveVertical()
    {
        //Debug.Log("Vertical");
        float vertical = Input.GetAxis("Vertical");
        Vector3 position = transform.position;

        if (vertical > 0)
        {
            mainCamera.transform.Translate(new Vector3(0f, 0.2f, 0));
        }
        else if (vertical < 0)
        {
            mainCamera.transform.Translate(new Vector3(0f, -0.2f, 0f));
        }

    }

    private void MoveHorizontal()
    {
        //Debug.Log("Horizontal");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 position = transform.position;

        if (horizontal > 0)
        {
            mainCamera.transform.Translate(new Vector3(0.2f, 0f, 0f));


        }
        else if (horizontal < 0)
        {
            mainCamera.transform.Translate(new Vector3(-0.2f, 0f, 0f));
        }

    }
}
