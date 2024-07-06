using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    [SerializeField] private int areaId;
    private bool isopen = false;
    private bool isEnter = false;
    private void Start()
    {
        Transform father = transform.parent;
      
        Door door = father.GetComponent<Door>();
        areaId = door.doorID;
       




    }

    private void Update()
    {
        if (isEnter && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("isOpen");
            if (!isopen )
            {
                isopen = true;
                if (IsInZAxisDirection(player, gameObject))
                {
                    EventSystem.instance.OpenDoor(areaId);
                }
                else
                {
                    EventSystem.instance.OpenDoor2(areaId);
                }
            }
            else
            {
                isopen = false;
                EventSystem.instance.CloseDoor(areaId);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("isEnter");
            isEnter = true;




        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("isEnter");
            isEnter = true;
          
           

           
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        isEnter = false;
        if (other.CompareTag("Player") )
        {
           
        }
       
    }



    bool IsInZAxisDirection(GameObject a, GameObject b)
    {
        // 从B指向A的向量
        Vector3 directionToA = a.transform.position - b.transform.position;

        // B的z轴方向向量
        Vector3 bZAxis = b.transform.forward; // 使用transform.forward表示物体的本地z轴方向

        // 计算点积
        float dotProduct = Vector3.Dot(directionToA, bZAxis);

        // 如果点积大于0，A在B的z轴正方向；否则，A在B的z轴负方向
        return dotProduct > 0;
    }
}
