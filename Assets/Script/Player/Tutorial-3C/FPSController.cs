using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;


public class FPSController : MonoBehaviour
{

    private Vector3 targetPosition;
    public float runSpeed = 12.0f;
   
   
    public float mouseSensitivity = 2.0f;
    public Transform cameraRoot;

    private Vector3 moveDirection = Vector3.zero;
    private float verticalVelocity = 0.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    
  
    public static bool isdect = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isdect = false;
      
      
    }

    void Update()
    {
       


        moveDirection = Vector3.zero;

        // 控制视角
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        rotationY = Mathf.Clamp(rotationY, -60, 60);



        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        // 控制移动


        Vector3 forwardMovement = transform.forward * verticalInput;
        Vector3 strafeMovement = transform.right * horizontalInput;

        moveDirection = (forwardMovement + strafeMovement) * runSpeed;
        Vector3 moveDirection2 = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        // Debug.Log(strafeMovement);
      



        // moveDirection.y = verticalVelocity;
        targetPosition = transform.position + moveDirection * Time.deltaTime;
        // 移动玩家
        //transform.position += moveDirection * Time.deltaTime;
       
         cameraRoot.localRotation = Quaternion.Euler(rotationY, 0, 0);
         transform.localRotation = Quaternion.Euler(0, rotationX, 0);
         transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        

        // 解锁/隐藏鼠标
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

   
}
