using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireChildController : MonoBehaviour {

    private GameObject parent;

    // Use this for initialization
    void Start()
    {
        //親オブジェクトを取得
        parent = transform.parent.gameObject;

        Debug.Log("Parent:" + parent.name);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // 自身のパーティクルシステムのカラーを取得
        Color32 colorTemp = this.GetComponent<ParticleSystem>().main.startColor.color;

        // 親のカラーのα値を取得
        float Alpha = parent.GetComponent<ParticleSystem>().main.startColor.color.a;

        ParticleSystem.MinMaxGradient color = new ParticleSystem.MinMaxGradient();
        color.mode = ParticleSystemGradientMode.Color;
        color.color = new Color32(colorTemp.r, colorTemp.g, colorTemp.b, (byte)Alpha);
        ParticleSystem.MainModule MainSystem = this.GetComponent<ParticleSystem>().main;
        MainSystem.startColor = color;
    }
}
