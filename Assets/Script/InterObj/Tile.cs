using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab; // Ҫ�����Ԥ����
    public Vector2Int gridSize = new Vector2Int(3, 3); // ����������С (����, ����)
    public float spacing = 1f; // ���

    // ���һ���Ҽ��˵�ѡ������ڱ༭�����ֶ����������������
    [ContextMenu("Generate Tiles")]
    void GenerateTiles()
    {
        // ����Ӷ���
        while (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            DestroyImmediate(child.gameObject);
        }

        // ѭ������Ԥ���岢������������
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector3 position = new Vector3(i * (prefab.transform.localScale.x + spacing), 0, j * (prefab.transform.localScale.z + spacing));
                GameObject obj = Instantiate(prefab, transform.position + position, Quaternion.identity);
                obj.transform.parent = transform; // ���ø���Ϊ��ǰ����ʹ������ƶ�����ת
            }
        }
    }

    // �ڱ༭�����޸Ĳ���ʱ��������
    void OnValidate()
    {
        // ����ֱ����������� GenerateTiles()
        // �����Ҫ�Զ��������ɵĹ��ܣ����Ը�����������߼�
    }
}
