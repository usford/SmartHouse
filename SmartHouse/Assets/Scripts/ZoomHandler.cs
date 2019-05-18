using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomHandler : MonoBehaviour
{
    float zoom;

    private void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        
        zoom = Input.GetAxis("Mouse ScrollWheel");
        Vector3 position = transform.position;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (zoom > 0 && position.y >= 2f)
        {
            // Debug.Log("Приближаем");
            position.y -= +1f;
            if (Physics.Raycast(transform.position, ray.direction, out hit, 20f))
            {
                //Debug.Log("Попали");
                transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * 100f);
            }
        }
        else if (zoom < 0 && position.y < 16f)
        {
            //Debug.Log("Отдаляем");
            position.y += +1f;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 15.84f, transform.position.z), Time.deltaTime * 100f);
        }
        //transform.position = position;
    }
}

