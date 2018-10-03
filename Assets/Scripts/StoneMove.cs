using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMove : MonoBehaviour {

    [SerializeField]
    GameObject titleRef;        //タイトルロゴのゲームオブジェクト
    private ObjectMove objectmove;

    [SerializeField]
    private Vector2 move;       //移動量

    [SerializeField]
    private Vector2 endPos;    //終点座標

    [SerializeField]
    private AudioSource hitSE;  //ストーンがぶつかった時の音

    [SerializeField]
    private AudioSource sodaneSE;  //ストーンがぶつかった時にそだねー

    [SerializeField]
    private AudioSource moveSE;  //ストーンの移動時の音

    [SerializeField]
    private float waitTimeSE;         //そだねーSEを再生するオフセット

    private Vector2 rectPos;    //XY座標
    private Vector2 twoVec;     //2点のベクトル
    private float timeCnt = 0.0f;     //タイムカウント

    private bool stoneHit = false;        //2つのストーンが接触したら
    private bool flugHitSE = false;     //音がなったか
    private bool flugSoudaneSE = false;     //そだねー音がなったか


    // Use this for initialization
    void Start () {
        objectmove = titleRef.GetComponent<ObjectMove>();

        //初期座標取得
        rectPos = this.GetComponent<RectTransform>().anchoredPosition;

        //ベクトル作成
        twoVec = endPos - rectPos;
        twoVec = twoVec.normalized;
    }
	
	// Update is called once per frame
	void Update () {
        float acc = 0.9f;   //ストーンの減速値

        if (objectmove.GetFlug())
        {
            //終点座標と違うなら動き続ける
            if (Mathf.Abs(this.GetComponent<RectTransform>().anchoredPosition.x) > Mathf.Abs(endPos.x))
            {
                this.GetComponent<RectTransform>().anchoredPosition += Vector2.Scale(move, twoVec);
                move *= acc;

                //NULLチェック
                if(moveSE)
                {
                    //移動時のSE再生
                    moveSE.PlayOneShot(moveSE.clip);
                }
            }
            else
            {
                stoneHit = true;

                //NULLチェック
                if (moveSE)
                {
                    //移動時のSE停止
                    moveSE.Stop();
                }

                //NULLチェック
                if (hitSE && flugHitSE == false)
                {
                    //Hit時のSE再生
                    hitSE.PlayOneShot(hitSE.clip);

                    flugHitSE = true;
                }

                //ストーンがHITしてたらSE再生待ち時間の更新
                if(flugHitSE == true)
                {
                    timeCnt += Time.deltaTime;
                }

                //そだねーチェック＆2秒待ったら
                if (flugSoudaneSE == false && sodaneSE && timeCnt > waitTimeSE)
                {
                    //そだねーのSE再生
                    hitSE.PlayOneShot(sodaneSE.clip);

                    flugSoudaneSE = true;
                }
            }
        }
    }

    public bool GetStoneHit()
    {
        return stoneHit;
    }
}
