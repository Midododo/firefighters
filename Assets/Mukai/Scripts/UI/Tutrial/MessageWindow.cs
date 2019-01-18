using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MessageWindow : MonoBehaviour
{
    [SerializeField]
    private float speed_div = 10.0f;        // 何フレームで出現させるか
    public bool flag = false;               // 出現・非表示のフラグ
    public　bool end_flag = false;          // 終了フラグ


    private float speed_x;          // 拡大・縮小スピード
    private float speed_y;

    private float x_scl;            // 反映スケール
    private float y_scl;

    float X_Scl;                    // 指標スケール
    float Y_Scl;

    bool x_flag;                    // 完了フラグ
    bool y_flag;


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
        else //if (!flag)
        {
            DeleteWindow();
        }
    }

    //******************************************************************
    // ・ これ以降は内部処理関数(アクセス拒否)
    //*******************************************************************

    private void PopWindow()
    {
        if (!end_flag)
        {
            x_scl += speed_x;
            if (x_scl >= X_Scl) x_scl = X_Scl;
            y_scl += speed_y;
            if (y_scl >= Y_Scl) y_scl = Y_Scl;

            this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(x_scl, y_scl, 1.0f);

            if (x_scl == X_Scl && y_scl == Y_Scl) end_flag = true;
            if (y_scl == Y_Scl) y_flag = true;
        }
    }

    private void DeleteWindow()
    {
        if (!end_flag)
        {
            x_scl -= speed_x;
            if (x_scl <= 0.0f) x_scl = 0.0f;
            y_scl -= speed_y;
            if (y_scl <= 0.0f) y_scl = 0.0f;

            this.gameObject.GetComponent<RectTransform>().localScale = new Vector3(x_scl, y_scl, 1.0f);

            // めんどいけど後から判定
            if (x_scl == 0.0f && y_scl == 0.0f) end_flag = true;
        }
    }
}
