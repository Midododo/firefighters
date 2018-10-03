using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField]
    private Vector2 move;       //移動量

    [SerializeField]
    private Vector2 endPos;    //終点座標

    private Vector2 rectPos;    //XY座標
    private Vector2 twoVec;     //2点のベクトル

    private bool flugStone = false;     //石動かすフラグ

    // Use this for initialization
    void Start()
    {
        //初期座標取得
        rectPos = this.GetComponent<RectTransform>().anchoredPosition;

        //ベクトル作成
        twoVec = endPos - rectPos;
        twoVec = twoVec.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //終点座標と違うなら動き続ける
        if (Mathf.Abs(this.GetComponent<RectTransform>().anchoredPosition.x) > Mathf.Abs(endPos.x))
        {
            this.GetComponent<RectTransform>().anchoredPosition += Vector2.Scale(move, twoVec);
        }
        else
        {
            flugStone = true;
        }
    }

    //フラグ取得
    public bool GetFlug()
    {
        return flugStone;
    }

}
