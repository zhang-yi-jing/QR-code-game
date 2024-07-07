using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Detect : MonoBehaviour
{
    // Start is called before the first frame update
   
    // 要检测的图层
    public LayerMask layerMask;
    public LayerMask layerMaskFruit;


    private GameObject hitObject;
    private GameObject hitObject1;
    public GameObject phone;
    // public GameObject canvas;


    //扫描二维码
    public Camera mainCamera;
    public GameObject targetObject;
    public RectTransform scanZoneRectTransform;
    public float scanZoneSize = 0.2f;
    public GameObject isfind;
    public GameObject isscanning;

    public float maxDetectionDistance = 10f;
    public static bool isDecting = false;
    public static bool isShow = false;

    private GameObject canvas;

    public enum Item
    {
        Potato1,
        Tomato1,
        Cabbage1,
        Garlic1,
        Onion1,
        Apple1,
        Watermelon1,
        Orange1,
        Grapes1,
        Strawberry1,
        sum,
        Count // 总数
    }
    private int[] itemQuantities;
    private float moveDistance = 0.2f; // 上升的距离
    private float moveDuration = 0.3f; // 上升的持续时间

    private Vector3 originalPosition;
    private bool isMoving = false;
    void Start()
    {
        
        phone.SetActive(false);
        isscanning.SetActive(true);
        isfind.SetActive(false);
        isDecting = false;
        isShow = false;
        Inventory();
    }


    void Update()
    {
        // 当按下鼠标左键时进行检测
        if (phone.activeSelf)
        {
                
            if (isShow)
            {
                if (Input.anyKeyDown)
                {
                    canvas.SetActive(false);
                    isShow = false;
                }



            }
            else
            {
                DetectObjectUnderMouse();
            }




        }
        DetectObjectUnderMouseFruit();







        if (Input.GetMouseButtonDown(1)&&!isShow)
        {
            phone.SetActive(!phone.activeSelf);
            isDecting = !isDecting;

        }
    }

    void DetectObjectUnderMouse()
    {
        // 从摄像机发射一条射线到鼠标位置
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = Camera.main.ViewportPointToRay(viewportCenter);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow);

        // 如果射线碰到某个物体，并且该物体在指定的图层中
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // 获取碰到的物体
            Debug.DrawLine(ray.origin, hit.point, Color.red);
            hitObject = hit.collider.gameObject;
            InterObject _interObject = hitObject.GetComponent<InterObject>();
            float hitDistance = hit.distance;
            canvas = _interObject.canvas;
            //Debug.Log("Hit distance: " + hitDistance);
            if (hitDistance <= maxDetectionDistance)
            {
                if (hitObject.CompareTag("QR"))
                {
                    if (IsObjectInScanZone(hitObject))
                    {
                        Debug.Log("目标物体在扫描区内！");
                        isscanning.SetActive(false);
                        isfind.SetActive(true);


                        if (Input.GetMouseButtonDown(0))
                        {
                            if (_interObject.name == "Start")
                            {
                                SceneManager.LoadScene("level2");
                            }
                            else
                            {
                                if (!isShow)
                                {
                                    isShow = true;
                                    _interObject.canvas.SetActive(true);
                                }



                            }
                            Debug.Log(hitObject);
                        }
                       
                        


                       


                    }
                    else
                    {
                        isscanning.SetActive(true);
                        isfind.SetActive(false);
                        Debug.Log("目标物体不在扫描区内！");
                    }
                }
                else
                {
                    isscanning.SetActive(true);
                    isfind.SetActive(false);


                }
            }
            else
            {
                if (hitObject != null)
                {
                    canvas = null;
                    hitObject = null;

                }
                isscanning.SetActive(true);
                isfind.SetActive(false);
            }
            if (_interObject == null)
            {
                Debug.Log("no");
            }
            // 检查物体的标签是否是目标标签
            
        }
        else
        {
            isscanning.SetActive(true);
            isfind.SetActive(false);
            // 获取所有目标标签的物体并调用 OnPointerExit
            if (hitObject != null)
            {
                canvas = null;
                hitObject = null;

            }
        }
    }
    IEnumerator MoveObject(GameObject obj)
    {
        isMoving = true;

        Vector3 originalPosition = obj.transform.position;
        Vector3 targetPosition = originalPosition + Vector3.up * moveDistance;

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            obj.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.2f); // 等待一段时间

        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            obj.transform.position = Vector3.Lerp(targetPosition, originalPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = originalPosition; // 确保回到原始位置
        isMoving = false;
    }

    void DetectObjectUnderMouseFruit()
    {
        // 从摄像机发射一条射线到鼠标位置
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = Camera.main.ViewportPointToRay(viewportCenter);
        RaycastHit hit;
       

        // 如果射线碰到某个物体，并且该物体在指定的图层中
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMaskFruit))
        {
            // 获取碰到的物体
            hitObject1 = hit.collider.gameObject;
            InterObject _interObject = hitObject1.GetComponent<InterObject>();
            float hitDistance = hit.distance;
            //canvas = _interObject.canvas;
            //Debug.Log("Hit distance: " + hitDistance);
            if (hitDistance <= maxDetectionDistance/2.0f)
            {
                Debug.Log("1" );
                if(Input.GetKeyDown(KeyCode.E) && _interObject.name== "bingxiang" && itemQuantities[10] != 0)
                {
                    if (itemQuantities[0]==3)
                    {
                        Gamewin();




                    }
                    else
                    {
                        Gamelose();
                    }


                }




                if (Input.GetKeyDown(KeyCode.E) && itemQuantities[10] < 3)
                {
                    Debug.Log(_interObject.name);

                    if (Enum.TryParse(_interObject.name, out Item item))
                    {
                        Debug.Log("3");
                        StartCoroutine(MoveObject(hitObject1));

                        itemQuantities[(int)item]++;
                        itemQuantities[10]++;
                        Debug.Log($"{_interObject.name} detected. Quantity: {itemQuantities[(int)item]}");
                    }

                }

            }
            else
            {
                if (hitObject1 != null)
                {

                    hitObject1 = null;

                }
            }
            if (_interObject == null)
            {
                //Debug.Log("no");
            }
            // 检查物体的标签是否是目标标签

        }
        else
        {
           
            // 获取所有目标标签的物体并调用 OnPointerExit
            if (hitObject1 != null)
            {
                
                hitObject1 = null;

            }
        }
    }
    public void Inventory()
    {
        itemQuantities = new int[(int)Item.Count];

        // 初始化每个物品的数量为0
        for (int i = 0; i < itemQuantities.Length; i++)
        {
            itemQuantities[i] = 0;
        }
    }




    bool IsObjectInScanZone(GameObject obj)
    {
        // 获取目标物体的边界框
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null) return false;

        Bounds bounds = renderer.bounds;

        // 获取边界框的八个顶点
        Vector3[] corners = new Vector3[8];
        corners[0] = bounds.min;
        corners[1] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        corners[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        corners[3] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        corners[4] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        corners[5] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        corners[6] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        corners[7] = bounds.max;

        // 获取屏幕坐标中的扫描区范围
        Vector3[] scanZoneCorners = new Vector3[4];
        scanZoneRectTransform.GetWorldCorners(scanZoneCorners);

        Vector3 bottomLeft = scanZoneCorners[0];
        Vector3 topRight = scanZoneCorners[2];

        // 检查所有顶点是否都在扫描区内
        foreach (Vector3 corner in corners)
        {
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(corner);
            if (screenPoint.x < bottomLeft.x || screenPoint.x > topRight.x ||
                screenPoint.y < bottomLeft.y || screenPoint.y > topRight.y ||
                screenPoint.z < mainCamera.nearClipPlane || screenPoint.z > mainCamera.farClipPlane)
            {
                return false; // 有顶点在扫描区外
            }
        }

        return true; // 所有顶点都在扫描区内
    }

    public void Gamewin()
    {
        Debug.Log("win");
        SceneManager.LoadScene("levelWin");
        // 显示鼠标光标
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
    public void Gamelose()
    {
        Debug.Log("lose");
        SceneManager.LoadScene("levelLose");
        // 显示鼠标光标
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }














}
