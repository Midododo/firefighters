using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBlinker : MonoBehaviour
{

    public float f_interval = 0.2f;   // 点滅周期
    public int cnt = 15;   // 点滅周期

    private bool flash = true;
    private bool flag = true;


    // 点滅コルーチンを開始する
    void Start()
    {
        StartCoroutine("Blinks");
    }

    // 点滅コルーチン
    IEnumerator Blinks()
    {
        while (flash)
        {
            yield return new WaitForSeconds(f_interval);
            if (flag)
            {
                gameObject.GetComponent<Text>().color = new Color(252.0f / 255.0f, 255.0f / 255.0f, 182.0f / 255.0f, 0.0f);
                flag = !flag;
            }
            else if (!flag)
            {
                gameObject.GetComponent<Text>().color = new Color(252.0f / 255.0f, 255.0f / 255.0f, 182.0f / 255.0f, 1.0f);
                flag = !flag;
            }
        }
    }
}
