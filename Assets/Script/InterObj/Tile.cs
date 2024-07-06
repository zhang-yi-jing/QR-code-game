using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab; // 要铺设的预制体
    public Vector2Int gridSize = new Vector2Int(3, 3); // 铺设的网格大小 (行数, 列数)
    public float spacing = 1f; // 间距

    // 添加一个右键菜单选项，用于在编辑器中手动触发生成铺设操作
    [ContextMenu("Generate Tiles")]
    void GenerateTiles()
    {
        // 清空子对象
        while (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            DestroyImmediate(child.gameObject);
        }

        // 循环生成预制体并排列在网格上
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector3 position = new Vector3(i * (prefab.transform.localScale.x + spacing), 0, j * (prefab.transform.localScale.z + spacing));
                GameObject obj = Instantiate(prefab, transform.position + position, Quaternion.identity);
                obj.transform.parent = transform; // 设置父级为当前对象，使其跟随移动或旋转
            }
        }
    }

    // 在编辑器中修改参数时更新铺设
    void OnValidate()
    {
        // 避免直接在这里调用 GenerateTiles()
        // 如果需要自动重新生成的功能，可以根据需求添加逻辑
    }
}
