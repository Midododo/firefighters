using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialManager : MonoBehaviour
{
    public int m_MapData = 1;
    public int m_MapCnt = 3;

    private int m_MapScaleX = 5;
    private int m_MapScaleZ = 5;


    public GameObject prefab_Map;
    public GameObject prefab_Player;

    private GameObject m_Map;
    private GameObject m_Player;

    public float m_TimeElapsed = 4;


	void Awake ()
    {

        m_Map = Instantiate(prefab_Map);

        m_Map.GetComponent<MapManager>().Create(m_MapData);

        m_Map.GetComponent<MapManager>().SetVisibility(false);


        m_Player = Instantiate(prefab_Player);
        m_Player.GetComponent<Player>().m_Input = gameObject;
    }

    // Update is called once per frame
    void Update ()
    {
        m_TimeElapsed += Time.deltaTime;

		if (m_TimeElapsed > 4.0f)
        {
            m_Map.GetComponent<MapManager>().SetVisibility(true);
            m_Map.GetComponent<MapManager>().SetActive(true);
            //m_Map.GetComponent<MapManager>().SetPos(new Vector3(0.0f, 0.0f, 0.0f));

            m_TimeElapsed = 0.0f;
        }
        else if (m_TimeElapsed > 2.0f)
        {
            m_Map.GetComponent<MapManager>().SetVisibility(false);
            m_Map.GetComponent<MapManager>().SetActive(false);
            //m_Map.GetComponent<MapManager>().SetPos(new Vector3(0.0f, 10.0f, 0.0f));
            //m_Map.GetComponent<MapManager>().Move(new Vector3(0.0f, -1.0f, 0.0f));

        }
        else
        {
            //m_Map.GetComponent<MapManager>().Move(new Vector3(0.0f, 1.0f, 0.0f));
        }
        //m_Map.GetComponent<MapManager>().Move(new Vector3(1.0f, 0.0f, 1.0f));
    }





}
