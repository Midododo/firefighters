using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterType : MonoBehaviour
{

    public Sprite straight;     // まっすぐのスプライト
    public Sprite kakusan;      // 拡散のスプライト

    private Image type_tex;     // 自身のimage格納用 
    private bool type;          // まっすぐか拡散か
                                // false : まっすぐ
                                // true  : 拡散


    // Use this for initialization
    void Start ()
    {
        type_tex = this.gameObject.GetComponent<Image>();       // 格納
        type = false;                                           // 最初は直線
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ChangeWaterType()
    {
        type = !type;       // 現在と違うタイプにする
        AudioManager.Instance.PlaySe("Type1");
        SetWaterTypeTexture();      // テクスチャの変更
    }

    private void SetWaterTypeTexture()
    {
        if (!type)
        {// 直線に切り替えた場合
            //type_tex.color = new Color(42.0f, 191.0f, 241.0f, 178.0f);
            //this.gameObject.GetComponent<Image>().color = new Color(42.0f, 191.0f, 70.0f, 178.0f / 255.0f);
            type_tex.sprite = straight;
        }
        else 
        {// 拡散に切り替えた場合
            type_tex.sprite = kakusan;
            //type_tex.color = new Color(42.0f, 146.0f, 241.0f, 244.0f);
        }
    }
}
