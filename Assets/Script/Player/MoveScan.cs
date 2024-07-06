using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScan : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 100f; // �ƶ��ٶ�
    public float startY = 600f; // ��ʼYλ��
    public float endY = -600f; // ����Yλ��

    private RectTransform rectTransform;

    void Start()
    {
        // ��ȡ RectTransform ���
        rectTransform = GetComponent<RectTransform>();
        // ���ó�ʼλ��
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY);
    }

    void Update()
    {
        // �ƶ�ͼƬ
        rectTransform.anchoredPosition += new Vector2(0, -speed * Time.deltaTime);

        // ���ͼƬ�������λ�ã������õ���ʼλ��
        if (rectTransform.anchoredPosition.y <= endY)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY);
        }
    }
}
