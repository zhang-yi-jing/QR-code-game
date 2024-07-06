using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Detect : MonoBehaviour
{
    // Start is called before the first frame update
   
    // Ҫ����ͼ��
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
        // ������������ʱ���м��
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
        // �����������һ�����ߵ����λ��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // �����������ĳ�����壬���Ҹ�������ָ����ͼ����
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // ��ȡ����������
            
            hitObject = hit.collider.gameObject;
            InterObject _interObject = hitObject.GetComponent<InterObject>();
            if (_interObject != null)
            {
                Debug.Log("no");
            }
            // �������ı�ǩ�Ƿ���Ŀ���ǩ
            Debug.Log(_interObject.name);
        }
        else
        {
            
            // ��ȡ����Ŀ���ǩ�����岢���� OnPointerExit
            if (hitObject != null)
            {
               

            }
        }
    }
}
