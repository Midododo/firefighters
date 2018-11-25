using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GageFlameLeft : MonoBehaviour
{
    public Sprite max;
    public Sprite min;

    private Image flame;
    private Gage gage;
    // Use this for initialization
    void Start()
    {
        // 取得
        flame = this.gameObject.GetComponent<Image>();
        gage = GameObject.Find("Nakami_Left").GetComponent<Gage>();
    }

    // Update is called once per frame
    void Update()
    {
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
        //gage.CheckPoint()
        //// 前フレームと比較して今回のフレームでMAXに行ったら
        //if (pre_point < GAGE_MAX && point == GAGE_MAX) { gage.sprite = Resources.Load("Texture/Game/Gage/sasikae/gage_Max", typeof(Sprite)) as Sprite; }
        //else if (pre_point == GAGE_MAX && point < GAGE_MAX) { gage.sprite = Resources.Load("Texture/Game/Gage/sasikae/gage_notMax", typeof(Sprite)) as Sprite; }

    }
}
