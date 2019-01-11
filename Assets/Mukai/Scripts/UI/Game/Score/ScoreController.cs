using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static int point;            // ポイント

    private Text scoretext;             // テキスト文字

    // Use this for initialization
    void Start ()
    {
        scoretext = this.gameObject.GetComponent<Text>();
        point = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    //******************************************************************
    // ・ 他スクリプトからのアクセス用関数
    //******************************************************************
    public void AddScore(int addpoint)
    {
        point += addpoint;
        scoretext.text = point.ToString();
    }

    public void SubScore(int addpoint)
    {
        point -= addpoint;
        scoretext.text = point.ToString();
    }

    //// テスト
    //if (Input.GetKey(KeyCode.UpArrow))
    //{
    //    AddScore(20);
    //}
    //if (Input.GetKey(KeyCode.DownArrow))
    //{
    //    SubScore(20);
    //}
}
