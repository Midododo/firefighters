using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;     //UIを使用可能にする

public class AlphaChange : MonoBehaviour {

    [SerializeField]
    GameObject stoneRef;
    private StoneMove objectalpha;

    private float alpha = 0.0f;

    // Use this for initialization
    void Start () {
        objectalpha = stoneRef.GetComponent<StoneMove>();
        this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        //ストーンが接触したらタイトルバックを表示
        if(objectalpha.GetStoneHit())
        {
            this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
            alpha += 0.02f;
        }

    }
}
