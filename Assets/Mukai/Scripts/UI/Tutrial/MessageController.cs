using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    // 格納用
    [SerializeField]
    private MessageWindow win1;
    [SerializeField]
    private MessageWindow win2;
    [SerializeField]
    private MessageWindow win3;
    [SerializeField]
    private MessageWindow win4;
    [SerializeField]
    private MessageWindow win5;

    private MessageWindow win;   // 表示するやつ
    int num;                    // window番号

    // Use this for initialization
    void Start ()
    {
        win = GameObject.Find("Window1").GetComponent<MessageWindow>();
        num = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //******************************************************************
    // ・ 他スクリプトからのアクセス用関数
    //******************************************************************

    // windowを出現させる
    public void ShowWindow()
    {
        if (!win.flag)
        {
            win.flag = true;
            win.end_flag = false;
            // 音鳴らす
            AudioManager.Instance.PlaySe("Pop1");
        }
    }

    // windowを消す
    public void DeleteWindow()
    {
        if (win.flag)
        {
            if (num < 5)
            {
                win.flag = false;
                win.end_flag = false;
                num++;
                win = SetWindow(num);
                AudioManager.Instance.PlaySe("Down1");
            }
        }
    }

    //******************************************************************
    // ・ これ以降は内部処理関数(アクセス拒否)
    //*******************************************************************

    // windowの設定
    private MessageWindow SetWindow(int no)
    {
        MessageWindow temp = null;

        switch (no)
        {
            case 1:
                temp = GameObject.Find("Window1").GetComponent<MessageWindow>();
                break;
            case 2:
                temp = GameObject.Find("Window2").GetComponent<MessageWindow>();
                break;
            case 3:
                temp = GameObject.Find("Window3").GetComponent<MessageWindow>();
                break;
            case 4:
                temp = GameObject.Find("Window4").GetComponent<MessageWindow>();
                break;
            case 5:
                temp = GameObject.Find("Window5").GetComponent<MessageWindow>();
                break;
        }
        return temp;
    }
}
