using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int m_MapData = 1;
    public int m_FloorCnt = 1;
    public int m_CurrentFloor = 1;
    private int m_MapScaleX = 5;
    private int m_MapScaleZ = 5;

    public int Player_Num = 2;

    public GameObject prefab_Map;
    public GameObject prefab_Player;
    public GameObject prefab_GameCanvas;

    private GameObject[] m_Map;
    private GameObject[] m_Player;
    private GameObject GameCanvas;

    public float m_TimeElapsed = 0;
    public bool flg = false;
    public bool flgOver = false;

    void Awake()
    {
        m_Map = new GameObject[m_FloorCnt];

        m_Map[0] = Instantiate(prefab_Map);
        //m_Map[1] = Instantiate(prefab_Map);

        m_Map[0].GetComponent<MapManager>().Create(m_MapData);
        //m_Map[1].GetComponent<MapManager>().Create(1);

        m_Map[0].GetComponent<MapManager>().SetVisibility(true);
        //m_Map[1].GetComponent<MapManager>().SetVisibility(false);

        //m_Map[1].GetComponent<MapManager>().MoveFloorDown();

        m_Player = new GameObject[Player_Num];

        m_Player[0] = Instantiate(prefab_Player, this.transform.position, transform.rotation);
        m_Player[0].tag = "Player1";
        m_Player[0].GetComponent<Player>().m_PlayerIdx = 0;
        m_Player[1] = Instantiate(prefab_Player, this.transform.position, transform.rotation);
        m_Player[1].tag = "Player2";
        m_Player[1].GetComponent<Player>().m_PlayerIdx = 1;
        GameObject.Find("CameraRig").GetComponent<CameraController2>().SetCamera(m_Player[0], 0);
        GameObject.Find("CameraRig").GetComponent<CameraController2>().SetCamera(m_Player[1], 1);

        GameCanvas = Instantiate(prefab_GameCanvas, this.transform.position, transform.rotation);

        m_Player[0].GetComponent<Player>().m_Input = gameObject;
        m_Player[1].GetComponent<Player>().m_Input = gameObject;// プレイヤー二ジョイコンのインプット情報の場所を教える

        //int count = 0;
        //foreach (Transform child in prefab_GameCanvas.transform)
        //{
        //    //child is your child transform
        //    GameObject prefab = (GameObject)Instantiate(child.gameObject);
        //    prefab.transform.SetParent(GameCanvas.transform, false);

        //    Debug.Log("Child[" + count + "]:" + child.name);
        //    count++;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        m_TimeElapsed += Time.deltaTime;
        if (m_TimeElapsed >= 4)
        {
            flgOver = true;
            m_TimeElapsed = 0.0f;
        }


        if (gameObject.GetComponent<JoyconInput>().GetTrigger(Joycon.Button.DPAD_UP, 0, true))
        {
            if (!m_Map[0].GetComponent<MapManager>().GetMoving()) // map 0 will always exist
            {
                //MoveFloorUp();
            }
        }

        if (gameObject.GetComponent<JoyconInput>().GetTrigger(Joycon.Button.DPAD_DOWN, 0, true))
        {
            if (!m_Map[0].GetComponent<MapManager>().GetMoving()) // map 0 will always exist
            {
                //MoveFloorDown();
            }
        }

        for (int i = 0; i < m_FloorCnt; i++)
        {
            if (m_Map[i].GetComponent<MapManager>().GetAlpha() == 0.0f)
            {
                //m_Map[i].GetComponent<MapManager>().SetActive(false);
            }
            else //if (m_Map[i].GetComponent<MapManager>().GetActive() != true)
            {
                //m_Map[i].GetComponent<MapManager>().SetActive(true);
                if (m_Map[i].GetComponent<MapManager>().GetMoving())
                {
                    //m_Map[i].GetComponent<MapManager>().SetPos(new Vector3(0.0f, 0.0f, 0.0f));
                }
            }
        }
    }


    //=========================================================================
    // 炎の生成処理
    void MoveFloorUp()
    {
        m_CurrentFloor++; // change the current floor 

        if (m_CurrentFloor > m_FloorCnt) // if the new floor is grater then the max floor count then return
        {
            m_CurrentFloor = m_FloorCnt;
            return;
        }

        for (int i = 0; i < m_FloorCnt; i++)
        {
            if (i == (m_CurrentFloor - 1)) // if the right floor show
            {
                m_Map[i].GetComponent<MapManager>().SetVisibility(true);
            }
            else // if not then dont show
            {
                m_Map[i].GetComponent<MapManager>().SetVisibility(false);
            }
            m_Map[i].GetComponent<MapManager>().MoveFloorUp(); // move up
        }
    }

    //=========================================================================
    // 炎の生成処理
    void MoveFloorDown()
    {
        m_CurrentFloor--; // change the current floor 

        if (m_CurrentFloor <= 0) // if the floor count goes in to the ground 
        {
            m_CurrentFloor = 1;
            return;
        }

        for (int i = 0; i < m_FloorCnt; i++)
        {
            if (i == (m_CurrentFloor - 1)) // if the right floor show
            {
                m_Map[i].GetComponent<MapManager>().SetVisibility(true);
            }
            else // if not then dont show
            {
                m_Map[i].GetComponent<MapManager>().SetVisibility(false);
            }
            m_Map[i].GetComponent<MapManager>().MoveFloorDown(); // move up
        }

    }

}
