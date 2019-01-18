using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    private int m_MapData = 0;
    private int m_FloorCnt = 1;
    private int m_CurrentFloor = 1;
    private int m_MapScaleX = 5;
    private int m_MapScaleZ = 5;

    public int Player_Num = 2;

    public GameObject prefab_Map;
    public GameObject prefab_Player;
    public GameObject prefab_GameCanvas;

    public GameObject prefab_FireRing;


    private GameObject m_Map;
    private GameObject[] m_Player;
    private GameObject GameCanvas;

    private GameObject m_Fire;

    private Fade FadeScript;

    void Awake()
    {
        FadeScript = GameObject.Find("Fade").GetComponent<Fade>();

        m_Player = new GameObject[Player_Num];

        // マップの生成
        m_Map = Instantiate(prefab_Map);// 入れ物の準備
        m_Map.GetComponent<MapManager>().Create(m_MapData);// マップの生成
        m_Map.GetComponent<MapManager>().SetVisibility(true);// マップを表示

        m_Player[0] = Instantiate(prefab_Player);
        m_Player[0].tag = "Player1";
        m_Player[0].GetComponent<Player>().m_PlayerIdx = 0;
//<<<<<<< HEAD
        m_Player[1] = Instantiate(prefab_Player);
//=======
//        //m_Player[1] = Instantiate(prefab_Player);
//        m_Player[1] = Instantiate(prefab_Player, new Vector3(10.0f, 3.4f, 10.0f), transform.rotation) as GameObject;
//>>>>>>> 75f2b0850f05a9ff30af3608c15415fbdbe1019d
        m_Player[1].tag = "Player2";
        m_Player[1].GetComponent<Player>().m_PlayerIdx = 1;
        GameObject.Find("CameraRig").GetComponent<CameraController2>().SetCamera(m_Player[0], 0);
        GameObject.Find("CameraRig").GetComponent<CameraController2>().SetCamera(m_Player[1], 1);

        GameCanvas = Instantiate(prefab_GameCanvas, this.transform.position, transform.rotation);

        m_Player[0].GetComponent<Player>().m_Input = gameObject;
        m_Player[1].GetComponent<Player>().m_Input = gameObject;// プレイヤー二ジョイコンのインプット情報の場所を教える

        //m_Player = Instantiate(prefab_Player);// プレイヤーの制作
        //m_Player.GetComponent<Player>().m_Input = gameObject;// プレイヤー二ジョイコンのインプット情報の場所を教える
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ExitEvent()
    {
        if (FadeScript.IsFading() == false)
        {
            FadeScript.SetFadeOutFlag("Game");
        }
    }
}
