using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// オブジェクトを点滅させるクラス
public class Blinker : MonoBehaviour
{
    public float f_interval = 0.1f;   // 点滅周期
    public int cnt = 6;   // 点滅周期

    private bool flash = true;
    private bool flag = true;
    private int count = 0;


    // 点滅コルーチンを開始する
    void Start()
    {
        StartCoroutine("Blink");
    }

    // 点滅コルーチン
    IEnumerator Blink()
    {
        while (flash)
        {
            yield return new WaitForSeconds(f_interval);
            if (flag)
            {
                gameObject.GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
                flag = !flag;
            }
            else if (!flag)
            {
                gameObject.GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
                flag = !flag;
                count++;
            }
            if (count == cnt)
            {
                flash = false;
                gameObject.GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
                gameObject.GetComponent<Blinker>().enabled = false;

            }
        }
    }

    public int Count
    {
        get { return this.count; }  //取得用
    }
}