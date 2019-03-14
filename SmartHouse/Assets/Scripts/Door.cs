using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject cube;
    public GameObject door;
    bool change = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerStay()
    {
        //Для первой двери
        if (door.name == "door1")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (cube.GetComponent<Animation>().isPlaying == false && change == false)
                {
                    cube.GetComponent<Animation>().Play("DoorOpen");
                    change = !change;
                }
                else if (cube.GetComponent<Animation>().isPlaying == false)
                {
                    cube.GetComponent<Animation>().Play("DoorClose");
                    change = !change;
                }
            }
        }

        //Для второй двери и третьей двери
        if (door.name == "door2" || door.name == "door3")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (cube.GetComponent<Animation>().isPlaying == false && change == false)
                {
                    cube.GetComponent<Animation>().Play("DoorOpen2");
                    change = !change;
                }
                else if (cube.GetComponent<Animation>().isPlaying == false)
                {
                    cube.GetComponent<Animation>().Play("DoorClose2");
                    change = !change;
                }
            }
        }
    }
}
