using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GageController : MonoBehaviour
{
    [SerializeField]
    private Gage gage_right;       // 右ゲージ
    [SerializeField]
    private Gage gage_left;        // 左ゲージ

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
        //// テスト
        //if (Input.GetKey(KeyCode.W)) gage_left.AddPoint(1);
        //if (Input.GetKey(KeyCode.S)) gage_left.SubPoint(1);
        //if (Input.GetKey(KeyCode.O)) gage_right.AddPoint(1);
        //if (Input.GetKey(KeyCode.L)) gage_right.SubPoint(1);
}
