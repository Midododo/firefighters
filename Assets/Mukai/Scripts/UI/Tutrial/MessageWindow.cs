using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MessageWindow : MonoBehaviour
{
    [SerializeField]
    private float speed_div = 10.0f;
    private float speed_x;
    private float speed_y;

    private float x_scl;
    private float y_scl;

    float X_Scl;
    float Y_Scl;

    bool x_flag;
    bool y_flag;

    private bool flag = true;

    // Use this for initialization
    void Start()
    {
        x_scl = X_Scl = this.gameObject.GetComponent<RectTransform>().localScale.x;
        y_scl = Y_Scl = this.gameObject.GetComponent<RectTransform>().localScale.y;

        this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.0f, 0.0f, 1);

        speed_x = X_Scl / speed_div;
        speed_y = Y_Scl / speed_div;

        x_scl = 0.0f;
        y_scl = 0.0f;

        x_flag = false;
        y_flag = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (flag)
        {
            PopWindow();
        }
        else if (!flag)
        {
            DeleteWindow();
        }
    }

    public void PopWindow()
    {
        if (!x_flag)
        {
            x_scl += speed_x;
            if (x_scl >= X_Scl) x_scl = X_Scl;
        }
        if (!y_flag)
        {
            y_scl += speed_y;
            if (y_scl >= Y_Scl) y_scl = Y_Scl;
        }

        // サイズ変更
        if (!x_flag || !y_flag)
        {
            this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(x_scl, y_scl, 1.0f);
        }

        // めんどいけど後から判定
        if (x_scl == X_Scl) x_flag = true;
        if (y_scl == Y_Scl) y_flag = true;
    }

    public void DeleteWindow()
    {
        if (x_flag)
        {
            x_scl -= speed_x;
            if (x_scl <= 0.0f) x_scl = 0.0f;
        }
        if (y_flag)
        {
            y_scl -= speed_y;
            if (y_scl <= 0.0f) y_scl = 0.0f;
        }

        // サイズ変更
        if (x_flag || y_flag)
        {
            this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(x_scl, y_scl, 1.0f);
        }

        // めんどいけど後から判定
        if (x_scl == 0.0f) x_flag = false;
        if (y_scl == 0.0f) y_flag = false;
    }

    public void SetWindow(bool Flag)
    {
        flag = Flag;
    }

}
