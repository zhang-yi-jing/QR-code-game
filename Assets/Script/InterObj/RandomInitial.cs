using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInitial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float randomValue = Random.value;
        float randomX = Random.Range(80f, 100f);
        float randomY = Random.Range(-90f, 90f);
        float randomZ = Random.Range(-90f, 90f);
        transform.rotation = Quaternion.Euler(randomX, randomY, randomZ);
        // ��������С�� 0.5������������Ϊ����Ծ״̬
        if (randomValue < 0.4f)
        {
            gameObject.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
