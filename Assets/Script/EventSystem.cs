using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static EventSystem instance;
    public event Action<int> onDoorEnter;
    public event Action<int> onDoorEnter2;
    public event Action<int> onDoorExit;
    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
       // DontDestroyOnLoad(gameObject);








    }



    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor(int _id)
    {
        if (onDoorEnter != null)
        {
            onDoorEnter(_id);
        }

    }
    public void OpenDoor2(int _id)
    {
        if (onDoorEnter2 != null)
        {
            onDoorEnter2(_id);
        }

    }

    public void CloseDoor(int _id)
    {
        if (onDoorExit != null)
        {
            onDoorExit(_id);
        }
    }


}
