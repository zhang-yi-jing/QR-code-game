using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorID;
    private float y;
    private void Start()
    {
        EventSystem.instance.onDoorEnter += DoorOpen;

        EventSystem.instance.onDoorEnter2 += DoorOpen2;
        EventSystem.instance.onDoorExit += DoorClose;
        y = transform.rotation.eulerAngles.y;
    }


    public void DoorOpen(int _id)
    {
        if(doorID == _id)
        {
            Debug.Log(y);
            LeanTween.rotateY(gameObject, y+120f, 0.5f).setEaseInOutSine();
        }
        
    }
    public void DoorOpen2(int _id)
    {
        if (doorID == _id)
        {
            LeanTween.rotateY(gameObject, y-120f, 0.5f).setEaseInOutSine();
        }
    }
    public void DoorClose(int _id)
    {
        if (doorID == _id)
        {
            LeanTween.rotateY(gameObject, y, 0.5f).setEaseInOutSine();
        }
    }



    // Start is called before the first frame update

}
