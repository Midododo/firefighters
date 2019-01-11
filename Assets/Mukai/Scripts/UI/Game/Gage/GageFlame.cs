using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GageFlame : MonoBehaviour
{
    public Sprite max;      // ゲージMAXの時のテクスチャ
    public Sprite min;      // それ以外の時のテクスチャ
    public Gage gage;       // それぞれの格納用

    private Image flame;
    // Use this for initialization
    void Start()
    {
        // 取得
        flame = this.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // テクスチャを差し替えるか差し替えないかの判断
        if (gage.CheckPoint() != 0)
        {
            if (gage.CheckPoint() == 1)
            {
                flame.sprite = max;
            }
            else if (gage.CheckPoint() == 2)
            {
                flame.sprite = min;
            }
        }
        //// 前フレームと比較して今回のフレームでMAXに行ったら
        //if (pre_point < GAGE_MAX && point == GAGE_MAX) { gage.sprite = Resources.Load("Texture/Game/Gage/sasikae/gage_Max", typeof(Sprite)) as Sprite; }
        //else if (pre_point == GAGE_MAX && point < GAGE_MAX) { gage.sprite = Resources.Load("Texture/Game/Gage/sasikae/gage_notMax", typeof(Sprite)) as Sprite; }
    }
}
