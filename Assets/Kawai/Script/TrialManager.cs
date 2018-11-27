using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialManager : MonoBehaviour
{
    private int m_MapData = 0;
    private int m_FloorCnt = 1;
    private int m_CurrentFloor = 1;
    private int m_MapScaleX = 5;
    private int m_MapScaleZ = 5;

    public GameObject prefab_Map;
    public GameObject prefab_Player;
    public GameObject ui;

    private GameObject m_Map;
    private GameObject m_Player;




	void Awake ()
    {
        // マップの生成
        m_Map = Instantiate(prefab_Map);// 入れ物の準備
        m_Map.GetComponent<MapManager>().Create(m_MapData);// マップの生成
        m_Map.GetComponent<MapManager>().SetVisibility(true);// マップを表示


        m_Player = Instantiate(prefab_Player);// プレイヤーの制作
        m_Player.GetComponent<Player>().m_Input = gameObject;// プレイヤー二ジョイコンのインプット情報の場所を教える
    }

    // Update is called once per frame
    void Update ()
    {
        
    }

    public void ExitEvent()
    {

    }
}
