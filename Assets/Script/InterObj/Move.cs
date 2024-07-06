using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f; // �ƶ��ٶ�
    public float rangeStart = -10f; // ��Χ���
    public float rangeEnd = 10f; // ��Χ�յ�

    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        // ��ʼ�������յ�
        startPosition = new Vector3(transform.position.x, transform.position.y, rangeStart);
        endPosition = new Vector3(transform.position.x, transform.position.y, rangeEnd);

        // ������λ������Ϊ���
       // transform.position = startPosition;
    }

    void Update()
    {
        // �����������λ��
        if (!Detect.isDecting && !Detect.isShow)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);

            // ��������Ƿ񵽴��յ�
            if (transform.position == endPosition)
            {
                // ��������λ�õ����
                transform.position = startPosition;
            }
        }


       
    }
}
