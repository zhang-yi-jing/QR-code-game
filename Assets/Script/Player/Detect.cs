using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detect : MonoBehaviour
{
    // Start is called before the first frame update
   
    // 要检测的图层
    public LayerMask layerMask;
    private GameObject hitObject;
    public GameObject phone;
    // public GameObject canvas;
    void Start()
    {
        
        phone.SetActive(false);

    }


    void Update()
    {
        // 当按下鼠标左键时进行检测
        if (Input.GetMouseButtonDown(0)&& phone.activeSelf)
        {
            DetectObjectUnderMouse();
        }

        if (Input.GetMouseButtonDown(1))
        {
            phone.SetActive(!phone.activeSelf);


        }
    }

    void DetectObjectUnderMouse()
    {
        // 从摄像机发射一条射线到鼠标位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // 如果射线碰到某个物体，并且该物体在指定的图层中
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // 获取碰到的物体
            
            hitObject = hit.collider.gameObject;
            InterObject _interObject = hitObject.GetComponent<InterObject>();
            if (_interObject != null)
            {
                Debug.Log("no");
            }
            // 检查物体的标签是否是目标标签
            Debug.Log(_interObject.name);
        }
        else
        {
            
            // 获取所有目标标签的物体并调用 OnPointerExit
            if (hitObject != null)
            {
               

            }
        }
    }
}
