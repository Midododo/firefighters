using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeCountSystem : MonoBehaviour
{
    [SerializeField]
    private float time = 60;
    [SerializeField]
    private bool flag = false;

    //public ballScript BallScript;

    void Start()
    {
        //初期値60を表示
        //float型からint型へCastし、String型に変換して表示
        GetComponent<Text>().text = ((int)time).ToString();
    }

    void Update()
    {
        if (flag)
        {
            //1秒に1ずつ減らしていく
            time -= Time.deltaTime;
            GetComponent<Text>().text = ((int)time).ToString();
            //0行ったら終了
            if (time == 0) flag = false;
        }
    }
}

