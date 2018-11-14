using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  ////ここを追加////


public class TimeText : MonoBehaviour
{

    [SerializeField]
    public int score = 100;           // スコア

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = "点数" + score.toString() + "点";
    }
}