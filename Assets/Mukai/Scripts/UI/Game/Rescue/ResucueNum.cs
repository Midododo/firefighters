using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResucueNum : MonoBehaviour
{

    public int point;               // 救助者数
    private Text rescuetext;        // アタッチしてるオブジェクトのテキスト文格納用
    private float pos_y, Pos_Y;     // 変動用と保存用
    private float pos_x, pos_z;     // ここどうにかならんかな

    private const float F_MOVE = 0.8f;      // 初速
    private const float GRAVITY = 0.1f;     // 係数

    private bool flag;      // アニメーションスイッチ
    private float pre_pos;  // 前フレームのY座標格納用
    private int state;      // アニメーション段階

    // Use this for initialization
    void Start ()
    {
        rescuetext = this.gameObject.GetComponent<Text>();                               // 自身に付属しているテキストの取得
        pos_y = Pos_Y = this.gameObject.GetComponent<RectTransform>().localPosition.y;   // 自身のY座標の取得
        pos_x = this.gameObject.GetComponent<RectTransform>().localPosition.x;           // 自身のX座標の取得
        pos_z = this.gameObject.GetComponent<RectTransform>().localPosition.z;           // 自身のZ座標の取得

        point = 0;
        flag = false;
        pre_pos = 0.0f;
        state = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // スイッチがONになっているときのみアニメーションさせる
        if (flag)
        {
            PointAnimation();
        }
	}

    //******************************************************************
    // ・ 他スクリプトからのアクセス用関数
    //******************************************************************

    // ポイント加算
    public void AddPoint()
    {
        point++;        // ポイント加算
        rescuetext.text = point.ToString();     // 反映
        SetAnimation();
        //AudioManager.Instance.Volume.se = 1.0f;        // BGMを通常に戻す
        AudioManager.Instance.PlaySe("Rescue");
        //AudioManager.Instance.Volume.se = 1.0f;        // BGMを通常に戻す


    }


    //******************************************************************
    // ・ これ以降は内部処理関数(アクセス拒否)
    //******************************************************************

    // アニメーションセット
    private void SetAnimation()
    {
        if (!flag)
        {
            state = 0;          // 遷移情報の初期化
            flag = true;        // フラグをONにする
        }
        else
        {// アニメーションの最中で起こってしまった場合
            state = 0;          // 遷移情報の初期化
            pos_y = pre_pos;    // 位置の初期化
        }
    }
    // アニメーションの挙動
    private void PointAnimation()
    {
        if (state == 1)
        {   // 一回目以降の場合
            float temp = pos_y;     // 現在の座標を保存

            pos_y += (pos_y - pre_pos) - GRAVITY;
            pre_pos = temp;

            if (pos_y < Pos_Y)
            {// 完了したら
                pos_y = Pos_Y;      // 念のため
                flag = false;       // フラグをへし折る
            }
        }
        else if (state == 0)
        {   // 一回目の場合
            pre_pos = pos_y;    // 現在の座標の保存
            pos_y += F_MOVE;    // 移動
            state = 1;          // 次以降のステート移動
        }
        transform.localPosition = new Vector3(pos_x, pos_y, pos_z);     // 反映
    }
}
