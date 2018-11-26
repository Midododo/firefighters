using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Gage : MonoBehaviour
{
    [SerializeField]
    private int GAGE_MAX = 100;
    [SerializeField]
    private int GAGE_MIN = 0;

    public int point;
    int pre_point;
    private float ratio;
    private Image gage;
    int type;
    // Use this for initialization
    void Start()
    {
        // 取得
        gage = this.gameObject.GetComponent<Image>();

        point = 0;
        ratio = ConvertPoint(point);
        pre_point = point;
        type = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            point++;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            point--;
        }

        // 前フレームと比較して今回のフレームでMAXに行ったら
        if (pre_point < GAGE_MAX && point == GAGE_MAX) { type = 1; }
        else if (pre_point == GAGE_MAX && point < GAGE_MAX) { type = 2; }
        else { type = 0; }
        // 変数領域を判定
        if (point > GAGE_MAX) { point = GAGE_MAX; }
        else if (point < GAGE_MIN) { point = GAGE_MIN; }

        // 反映
        gage.fillAmount = ConvertPoint(point);

        pre_point = point;
    }


    private float ConvertPoint(int value)
    {
        ratio = ((float)value / 100.00f);
        return ratio;
    }

    public int  CheckPoint()
    {
        return type;
    }
}
