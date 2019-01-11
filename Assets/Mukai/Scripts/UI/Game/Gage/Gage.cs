using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Gage : MonoBehaviour
{
    [SerializeField]
    private int GAGE_MAX = 100;     // ゲージの最大値
    [SerializeField]
    private int GAGE_MIN = 0;       // ゲージの最小値

    private int point;              // 中身格納
    int pre_point;                  // 前フレームの中身
    int type;                       // 現在のゲージの状態
                                    // 0 : フレームに変化がないとき
                                    // 1 : ゲージがMAXに切り替わった時の処理
                                    // 2 : ゲージがMAXより下回った時の処理
    private Image gage;

    // Use this for initialization
    void Start()
    {
        // 取得
        gage = this.gameObject.GetComponent<Image>();

        point = 0;
        pre_point = point;
        type = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (pre_point < GAGE_MAX && point == GAGE_MAX) { type = 1; }             // MAXに行ったら
        else if (pre_point == GAGE_MAX && point < GAGE_MAX) { type = 2; }        // MAXから減ったら
        else { type = 0; }                                                       // 他

        // 変数領域を判定
        if (point > GAGE_MAX) { point = GAGE_MAX; }
        else if (point < GAGE_MIN) { point = GAGE_MIN; }

        // ゲージに反映
        gage.fillAmount = ConvertPoint(point);

        // 比較用に値を保存
        pre_point = point;
    }

    private float ConvertPoint(int value)
    {
        float ratio2;
        ratio2 = ((float)value / 100.00f);
        return ratio2;
    }

    public int  CheckPoint()
    {
        return type;
    }

    public void AddPoint(int p)
    {
        point += p;
    }
    public void SubPoint(int p)
    {
        point -= p;
    }

}
