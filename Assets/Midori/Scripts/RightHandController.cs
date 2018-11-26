using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 右手の上げ下げ
public class RightHandController : MonoBehaviour {

    private GameObject parentPlayer;

    [SerializeField]
    private float RightHandLength = 10.0f;

    // Use this for initialization
    void Start () {
        // 親(プレイヤー)の取得
        parentPlayer = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // 親(プレイヤー)の取得
        parentPlayer = transform.root.gameObject;

        // 右手の向きをプレイヤーの向きと同じにする
        this.transform.rotation = parentPlayer.transform.rotation;

        // 右手の位置をプレイヤーの前方にする
        this.transform.position = parentPlayer.transform.position + transform.forward * RightHandLength;
    }   
}
