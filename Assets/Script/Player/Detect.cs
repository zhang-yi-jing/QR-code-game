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


    //ɨ���ά��
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

    void Start()
    {
        
        phone.SetActive(false);
        isscanning.SetActive(true);
        isfind.SetActive(false);
        isDecting = false;
        isShow = false;
    }


    void Update()
    {
        // ������������ʱ���м��
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





       

        if (Input.GetMouseButtonDown(1)&&!isShow)
        {
            phone.SetActive(!phone.activeSelf);
            isDecting = !isDecting;

        }
    }

    void DetectObjectUnderMouse()
    {
        // �����������һ�����ߵ����λ��
        Vector3 viewportCenter = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = Camera.main.ViewportPointToRay(viewportCenter);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.yellow);

        // �����������ĳ�����壬���Ҹ�������ָ����ͼ����
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            // ��ȡ����������
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
                        Debug.Log("Ŀ��������ɨ�����ڣ�");
                        isscanning.SetActive(false);
                        isfind.SetActive(true);
                        if (Input.GetMouseButtonDown(0))
                        {
                            Debug.Log(hitObject);
                            if (!isShow)
                            {
                                isShow = true;
                                _interObject.canvas.SetActive(true);
                            }
                            
                            

                        }


                    }
                    else
                    {
                        isscanning.SetActive(true);
                        isfind.SetActive(false);
                        Debug.Log("Ŀ�����岻��ɨ�����ڣ�");
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
                isscanning.SetActive(true);
                isfind.SetActive(false);
            }
            if (_interObject == null)
            {
                Debug.Log("no");
            }
            // �������ı�ǩ�Ƿ���Ŀ���ǩ
            
        }
        else
        {
            isscanning.SetActive(true);
            isfind.SetActive(false);
            // ��ȡ����Ŀ���ǩ�����岢���� OnPointerExit
            if (hitObject != null)
            {
                canvas = null;
                hitObject = null;

            }
        }
    }

    bool IsObjectInScanZone(GameObject obj)
    {
        // ��ȡĿ������ı߽��
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer == null) return false;

        Bounds bounds = renderer.bounds;

        // ��ȡ�߽��İ˸�����
        Vector3[] corners = new Vector3[8];
        corners[0] = bounds.min;
        corners[1] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        corners[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        corners[3] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        corners[4] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        corners[5] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        corners[6] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        corners[7] = bounds.max;

        // ��ȡ��Ļ�����е�ɨ������Χ
        Vector3[] scanZoneCorners = new Vector3[4];
        scanZoneRectTransform.GetWorldCorners(scanZoneCorners);

        Vector3 bottomLeft = scanZoneCorners[0];
        Vector3 topRight = scanZoneCorners[2];

        // ������ж����Ƿ���ɨ������
        foreach (Vector3 corner in corners)
        {
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(corner);
            if (screenPoint.x < bottomLeft.x || screenPoint.x > topRight.x ||
                screenPoint.y < bottomLeft.y || screenPoint.y > topRight.y ||
                screenPoint.z < mainCamera.nearClipPlane || screenPoint.z > mainCamera.farClipPlane)
            {
                return false; // �ж�����ɨ������
            }
        }

        return true; // ���ж��㶼��ɨ������
    }

















}
