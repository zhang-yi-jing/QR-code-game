using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class TPSController : MonoBehaviour
{
    public float accelerate = 5.0f;
    public float maxSpeed = 5.0f;
    public float decelerate = 3.0f;
    public float jumpForce = 5.0f;
    public float mouseSensitivityX = 2f;
    public float mouseSensitivityY = 100.0f;
    public bool invertMouseX = false;
    public bool invertMouseY = false;
    public CinemachineFreeLook cinemachineCamera;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float turningSpeed = 50f;
    public float camReturnSpeed = 1f;
    public string instruction;


    private Rigidbody rb;
    private bool isJumping = false;
    private float pitch;
    private Vector3 movement;
    private Vector3 moveInWorld;
    private bool isCamReturning;

    void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        cinemachineCamera.m_YAxis.Value = 0;
        cinemachineCamera.gameObject.SetActive(true);
        GameObject.Find("OperatingInstructions").GetComponent<TextMeshProUGUI>().text = instruction;

    }

    void Update()
    {
        // 摄像机视角下键盘控制移动
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        movement = new Vector3(moveX, 0, moveZ);
        Vector3 viewForwardWorld = GetCameraForwordProjectOnGround();
        Vector3 viewRightWorld = new Vector3(viewForwardWorld.z, 0, -viewForwardWorld.x);
        moveInWorld = viewRightWorld * movement.x + viewForwardWorld * movement.z;
        rb.AddForce(moveInWorld * accelerate);
        

        // 鼠标控制视角的俯仰和旋转
        cinemachineCamera.m_YAxis.m_MaxSpeed = mouseSensitivityY;
        cinemachineCamera.m_XAxis.m_MaxSpeed = mouseSensitivityX * 100f;
        cinemachineCamera.m_YAxis.m_InvertInput = invertMouseY;
        cinemachineCamera.m_XAxis.m_InvertInput = invertMouseX;

        // 鼠标右键回到主视角
        if (Input.GetMouseButtonDown(1))
        {
            isCamReturning = true;
            cinemachineCamera.m_YAxis.m_MaxSpeed = 0;
            cinemachineCamera.m_YAxis.Value = 0;
        }
        if (isCamReturning)
        {
            if (Vector3.Angle(GetCameraForwordProjectOnGround(), transform.forward) < 5f)
            {
                isCamReturning = false;
                cinemachineCamera.m_YAxis.m_MaxSpeed = mouseSensitivityY;
                return;
            }
            cinemachineCamera.m_XAxis.Value += camReturnSpeed * (Vector3.Dot(Vector3.Cross(GetCameraForwordProjectOnGround(), transform.forward), Vector3.up) > 0 ? 1 : -1) * Time.deltaTime * 100;
        }

        // 空格键控制跳跃
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJumping = true;
        }

    }

    private void FixedUpdate()
    {
        // 最大速度
        if (GetVelocityOnGround().magnitude > maxSpeed)
        {
            Vector3 maxVelGround = GetVelocityOnGround().normalized * maxSpeed;
            rb.velocity = new Vector3(maxVelGround.x, rb.velocity.y, maxVelGround.z);
        }

        if (movement.magnitude > 0.1f)
        {
            // 角色转向行走方向
            if(!Input.GetKey(KeyCode.LeftShift))
            {
                Quaternion toRotation = Quaternion.LookRotation(moveInWorld, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turningSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            // 减速
            Vector3 dcl= Vector3.Lerp(GetVelocityOnGround(), Vector3.zero, decelerate * Time.fixedDeltaTime);
            rb.velocity = new Vector3(dcl.x, rb.velocity.y, dcl.z);
        }
    }

    private Vector3 GetCameraForwordProjectOnGround()
    {
        Vector3 cameraForwardWorld = (cinemachineCamera.State.FinalOrientation.normalized * Vector3.forward).normalized;
        Vector3 forwardOnGround = Vector3.ProjectOnPlane(cameraForwardWorld, Vector3.up);

        return forwardOnGround;
    }

    private Vector3 GetVelocityOnGround()
    {
        return new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    // 检测是否在地面上
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
