using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f; // 移动速度
    public float rangeStart = -10f; // 范围起点
    public float rangeEnd = 10f; // 范围终点

    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        // 初始化起点和终点
        startPosition = new Vector3(transform.position.x, transform.position.y, rangeStart);
        endPosition = new Vector3(transform.position.x, transform.position.y, rangeEnd);

        // 将物体位置设置为起点
       // transform.position = startPosition;
    }

    void Update()
    {
        // 计算物体的新位置
        if (!Detect.isDecting && !Detect.isShow)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);

            // 检查物体是否到达终点
            if (transform.position == endPosition)
            {
                // 重置物体位置到起点
                transform.position = startPosition;
            }
        }


       
    }
}
