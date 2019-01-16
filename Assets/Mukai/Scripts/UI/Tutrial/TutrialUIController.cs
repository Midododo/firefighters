using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutrialUIController : MonoBehaviour
{
    public GageController Gage;
    public MessageController Window;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        // テスト
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Window.ShowWindow();    // 第一メッセージから順に表示(DeleteWindowを一回挟むと次のメッセージが出る)
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Window.DeleteWindow();  // 必ずShowWIndowのとワンセット。先にShowWindow
        }
    }
}
