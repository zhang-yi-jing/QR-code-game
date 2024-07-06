using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScan : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 100f; // 移动速度
    public float startY = 600f; // 初始Y位置
    public float endY = -600f; // 结束Y位置

    private RectTransform rectTransform;

    void Start()
    {
        // 获取 RectTransform 组件
        rectTransform = GetComponent<RectTransform>();
        // 设置初始位置
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY);
    }

    void Update()
    {
        // 移动图片
        rectTransform.anchoredPosition += new Vector2(0, -speed * Time.deltaTime);

        // 如果图片到达结束位置，则重置到初始位置
        if (rectTransform.anchoredPosition.y <= endY)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY);
        }
    }
}
