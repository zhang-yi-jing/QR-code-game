using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterObject : MonoBehaviour
{
    // Start is called before the first frame update
    public string Name;
    public GameObject canvas;

    void Start()
    {
        Name = transform.name;
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
















    }
}
