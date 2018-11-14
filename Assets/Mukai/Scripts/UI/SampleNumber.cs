using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleNumber : MonoBehaviour
{

    NumberImageRenderer numberImageRenderer = null;

    // Use this for initialization
    void Start()
    {
        numberImageRenderer = GetComponent<NumberImageRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // 浮動小数点
        numberImageRenderer.Render(100.0);

        // 整数
        //numberImageRenderer.Render(100);
    }
}
