using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class MapManager : MonoBehaviour
{

    private const int m_MapSectionSize = 10;
    private int m_MapSizeX = 0;
    private int m_MapSizeZ = 0;

    public bool m_Active = false;
    public bool m_Visiable = false;
    public float m_Alpha = 0.0f;
    public float m_FadeTime = 2.0f;

    //=====================================================
    // マップを動かすのに必要な物
    public float m_TimeElapsed = 0.0f;
    private float m_MoveDistance = 10.0f;
    public bool m_isMove = false;
    private bool m_MoveDirection = false; // false down   true up

    //=====================================================
    // マップの読み込みに必要な物
    private const string MapDataPathHead = "Assets/Kawai/Resources/MapData";
    private const string MapDataPathExtention = ".txt";
    private const string MapSizeStart = "#MapSize";
    private const string MapWallStart = "#WallData";
    private const string MapFireStart = "#FireData";
    private const string MapEventStart = "#EventData";
    //=====================================================
    // マップ構成に使うプリファブの読み込み
    public GameObject prefab_GroundTile;
    public GameObject prefab_OutWallV;
    public GameObject prefab_OutWallH;


    public GameObject prefab_WallN;
    public GameObject prefab_WallS;
    public GameObject prefab_WallE;
    public GameObject prefab_WallW;

    public GameObject prefab_WallCornerNE;
    public GameObject prefab_WallCornerNW;
    public GameObject prefab_WallCornerSE;
    public GameObject prefab_WallCornerSW;

    public GameObject prefab_DoorN;
    public GameObject prefab_DoorS;
    public GameObject prefab_DoorE;
    public GameObject prefab_DoorW;

    public GameObject prefab_DoorNE;
    public GameObject prefab_DoorNW;
    public GameObject prefab_DoorSE;
    public GameObject prefab_DoorSW;


    public GameObject prefab_TestObject;

    public GameObject prefab_FireA;
    public GameObject prefab_FireRing;

    public GameObject prefab_AoEEvent;
    public GameObject prefab_ExitEvent;


    //=====================================================
    // マップオブジェクトの入れ物
    private GameObject[][] m_GroundTile;
    private GameObject[][] m_OutWallV;
    private GameObject[][] m_OutWallH;

    private Vector3[][] m_OgGroundPos;
    private Vector3[][] m_OgOutWallVPos;
    private Vector3[][] m_OgOutWallHPos;

    // 壁
    private GameObject[][] m_Wall;
    private Vector3[][] m_OgWallPos;

    // 炎
    private List<GameObject> m_Fire = new List<GameObject>();
    private List<Vector3> m_OgFirePos = new List<Vector3>(); 

    // イベント
    public List<GameObject> m_Event = new List<GameObject>();
    private List<Vector3> m_OgEventPos = new List<Vector3>();

    //=====================================================
    // ファイル読み込みのクラス
    private OpenTextFile m_OpenTextFile;


    //=========================================================================
    // 初期化処理
    void Awake ()
    {
        m_OpenTextFile = new OpenTextFile();
	}

    //=========================================================================
    // 最新処理
    void Update ()
    {
        UpdateVisibility();

        if (m_isMove)
        {
            if(m_MoveDirection)
            {
                FloorUpUpdate();
            }
            else
            {
                FloorDownUpdate();
            }
        }
    }


    //=========================================================================
    // マップ作製関数
    // int num : 読み込むファイル番号
    public void Create(int num)
    {
        // creating file path 
        var path = MapDataPathHead + string.Format("{0:D2}", num) + MapDataPathExtention;


        //reading from file path
        StreamReader reader = new StreamReader(path);
        string line;

        line = reader.ReadLine();

        //=======================================
        // search for mapsize start
        while (line != MapSizeStart)
        {
            line = reader.ReadLine();
        }
        line = reader.ReadLine();

        // once found read data to int array 
        int[] mapSizeArray = line.Split(',').Select(s => int.Parse(s)).ToArray();

        // save map size data
        m_MapSizeX = mapSizeArray[0];
        m_MapSizeZ = mapSizeArray[1];


        //=======================================
        // look for wall data
        while (line != MapWallStart)
        {
            line = reader.ReadLine();
        }
        //line = reader.ReadLine();

        int[][] WallData = new int[100][];

        // read data for the z length
        for (int i = 0; i < (mapSizeArray[1]); i++)
        {
            line = reader.ReadLine();

            var numArray = line.Split(',').Select(s => int.Parse(s)).ToArray();
            WallData[i] = numArray;
        }

        //=======================================
        // look for fire data
        while (line != MapFireStart)
        {
            line = reader.ReadLine();
        }
        //line = reader.ReadLine();

        int[][] FireData = new int[100][];

        // read data for the z length
        for (int i = 0; i < (mapSizeArray[1]); i++)
        {
            line = reader.ReadLine();

            var numArray = line.Split(',').Select(s => int.Parse(s)).ToArray();
            FireData[i] = numArray;
        }

        //=======================================
        // look for event data
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            while (line != MapEventStart)
            {
                line = reader.ReadLine();
            }

            int[][] EventData = new int[100][];

            for (int i = 0; i < (mapSizeArray[1]); i++)
            {
                line = reader.ReadLine();

                var numArray = line.Split(',').Select(s => int.Parse(s)).ToArray();
                EventData[i] = numArray;
            }

            CreateEventMap(EventData);
        }

        reader.Close();


        //=======================================
        // the creation functions
        CreateFloor();
        CreateWalls(WallData);
        CreateFire(FireData);

        int test = 0;
        test++;
    }

    //=========================================================================
    // 地面の生成処理
    void CreateFloor()
    {
        // 地面の構造体
        m_GroundTile = new GameObject[m_MapSizeZ][];
        m_OgGroundPos = new Vector3[m_MapSizeZ][];

        m_OutWallH = new GameObject[2][];   // マップの両端にしか外向けの壁が無いので２を設定
        m_OutWallV = new GameObject[m_MapSizeZ][];
        m_OgOutWallHPos = new Vector3[2][];
        m_OgOutWallVPos = new Vector3[m_MapSizeZ][];

        for (int z = 0; z < m_MapSizeZ; z++)
        {
            m_GroundTile[z] = new GameObject[m_MapSizeX];
            m_OgGroundPos[z] = new Vector3[m_MapSizeX];

            m_OutWallV[z] = new GameObject[2];  // 2 means east and west
            m_OgOutWallVPos[z] = new Vector3[2];

            if (z == 0) // is south wall
            {
                m_OutWallH[0] = new GameObject[m_MapSizeX];
                m_OgOutWallHPos[0] = new Vector3[m_MapSizeX];
            }
            else if (z == (m_MapSizeZ - 1)) // is north wall
            {
                m_OutWallH[1] = new GameObject[m_MapSizeX];
                m_OgOutWallHPos[1] = new Vector3[m_MapSizeX];
            }

            for (int x = 0; x < m_MapSizeX; x++)
            {

                m_GroundTile[z][x] = Instantiate(prefab_GroundTile);
                m_GroundTile[z][x].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, (float)(z * m_MapSectionSize));
                m_OgGroundPos[z][x] = m_GroundTile[z][x].transform.position;

                if (z == 0) // is south wall or is north wall
                {
                    m_OutWallH[0][x] = Instantiate(prefab_OutWallH);
                    m_OutWallH[0][x].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, 0.0f);
                    m_OgOutWallHPos[0][x] = m_OutWallH[0][x].transform.position;
                }
                else if (z == (m_MapSizeZ - 1)) // 
                {
                    m_OutWallH[1][x] = Instantiate(prefab_OutWallH);
                    m_OutWallH[1][x].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, (float)((z + 1) * m_MapSectionSize));
                    m_OgOutWallHPos[1][x] = m_OutWallH[1][x].transform.position;
                }

                if (x == 0)
                {
                    m_OutWallV[z][0] = Instantiate(prefab_OutWallV);
                    m_OutWallV[z][0].transform.position += new Vector3(0.0f, 0.0f, (float)(z * m_MapSectionSize));
                    m_OgOutWallVPos[z][0] = m_OutWallV[z][0].transform.position;
                }
                else if (x == (m_MapSizeX - 1))
                {
                    m_OutWallV[z][1] = Instantiate(prefab_OutWallV);
                    m_OutWallV[z][1].transform.position += new Vector3((float)((x + 1) * m_MapSectionSize), 0.0f, (float)(z * m_MapSectionSize));
                    m_OgOutWallVPos[z][1] = m_OutWallV[z][1].transform.position;
                }

            } // for x
        } // for z


    } // end function

    //=========================================================================
    // 壁の生成処理
    void CreateWalls(int[][] data)
    {

        m_Wall = new GameObject[m_MapSizeZ][];
        m_OgWallPos = new Vector3[m_MapSizeZ][];

        for (int z = 0; z < (m_MapSizeZ); z++)
        {
            m_Wall[z] = new GameObject[m_MapSizeX];
            m_OgWallPos[z] = new Vector3[m_MapSizeX];
            for (int x = 0; x < m_MapSizeX; x++)
            {
                switch(data[z][x])
                {
                    case 0:
                        break;
                    case 1:
                        m_Wall[z][x] = Instantiate(prefab_WallN);
                        break;
                    case 2:
                        m_Wall[z][x] = Instantiate(prefab_WallS);
                        break;
                    case 3:
                        m_Wall[z][x] = Instantiate(prefab_WallE);
                        break;
                    case 4:
                        m_Wall[z][x] = Instantiate(prefab_WallW);
                        break;
                    case 5:
                        m_Wall[z][x] = Instantiate(prefab_WallCornerNE);
                        break;
                    case 6:
                        m_Wall[z][x] = Instantiate(prefab_WallCornerNW);
                        break;
                    case 7:
                        m_Wall[z][x] = Instantiate(prefab_WallCornerSE);
                        break;
                    case 8:
                        m_Wall[z][x] = Instantiate(prefab_WallCornerSW);
                        break;
                    case 9:
                        m_Wall[z][x] = Instantiate(prefab_DoorN);
                        break;
                    case 10:
                        m_Wall[z][x] = Instantiate(prefab_DoorS);
                        break;
                    case 11:
                        m_Wall[z][x] = Instantiate(prefab_DoorE);
                        break;
                    case 12:
                        m_Wall[z][x] = Instantiate(prefab_DoorW);
                        break;
                    case 13:
                        m_Wall[z][x] = Instantiate(prefab_DoorNE);
                        break;
                    case 14:
                        m_Wall[z][x] = Instantiate(prefab_DoorNW);
                        break;
                    case 15:
                        m_Wall[z][x] = Instantiate(prefab_DoorSE);
                        break;
                    case 16:
                        m_Wall[z][x] = Instantiate(prefab_DoorSW);
                        break;
                }
                if (data[z][x] != 0)
                {
                    m_Wall[z][x].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, (float)(z * m_MapSectionSize));
                    m_OgWallPos[z][x] = m_Wall[z][x].transform.position;
                }
            } // for x
        } // for z
    } // end function


    //=========================================================================
    // 家具の生成処理
    void CreateFurniture(int sizeX, int sizeZ)
    {
        m_Wall = new GameObject[sizeZ][];

        string path = "Assets/Resources/WallTest.txt";

        int[][] Data = new int[100][];

        m_OpenTextFile.ConvertCSVDataToIntArray(path, Data);


        for (int z = 0; z < sizeZ; z++)
        {
            m_Wall[z] = new GameObject[sizeX];
            for (int x = 0; x < sizeX; x++)
            {
                switch (Data[z][x])
                {
                    case 0:
                        break;
                    case 1:
                        m_Wall[z][x] = Instantiate(prefab_TestObject);
                        break;
                }
                if (Data[z][x] != 0)
                {
                    //m_Wall[z][x].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, (float)(z * m_MapSectionSize));
                    m_Wall[z][x].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, (float)(z * m_MapSectionSize));
                }


            } // for x
        } // for z

    } // end function

    //=========================================================================
    // 炎の生成処理
    void CreateFire(int[][] data)
    {
        for (int z = 0; z < (m_MapSizeZ); z++)
        {
            for (int x = 0; x < m_MapSizeX; x++)
            {
                if(data[z][x] == 0)
                { continue; }

                switch (data[z][x])
                {
                    case 0:
                        break;
                    case 1:
                            m_Fire.Add(Instantiate(prefab_FireA));
                        break;
                }
                    m_Fire[(m_Fire.Count - 1)].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, (float)(z * m_MapSectionSize));
            } // for x
        } // for z
    } // end function

    //=========================================================================
    // 炎の生成処理
    void CreateEventMap(int[][] data)
    {
        for (int z = 0; z < m_MapSizeZ; z++)
        {
            for (int x = 0; x < m_MapSizeX; x++)
            {
                if (data[z][x] == 0)
                { continue; }

                switch(data[z][x])
                {
                    case 1:
                        m_Event.Add(Instantiate(prefab_AoEEvent));
                        break;
                    case 2:
                        m_Event.Add(Instantiate(prefab_ExitEvent));
                        break;
                }
                m_Event[(m_Event.Count - 1)].transform.position = new Vector3((float)(x * m_MapSectionSize) + (m_MapSectionSize / 2), 0.0f, (float)(z * m_MapSectionSize) + (m_MapSectionSize / 2));
            }
        }
    }
    //=========================================================================
    // 
    public void SetVisibility(bool isVisible)
    {
        m_Active = isVisible;
    }

    //=========================================================================
    // 
    public void UpdateAlpha()
    {
        // horizon
        for (int z = 0; z < 2; z++)
        {
            for (int x = 0; x < m_MapSizeX; x++)
            {
                //m_OutWallH[z][x].GetComponent<Renderer>().enabled = isVisible;
                m_OutWallH[z][x].GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, m_Alpha);
            }// for x
        }// for z

        // virtical
        for (int z = 0; z < m_MapSizeZ; z++)
        {
            for (int x = 0; x < 2; x++)
            {
                m_OutWallV[z][x].GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, m_Alpha);
            }// for x
        }// for z

        // actual map size
        for (int z = 0; z < m_MapSizeZ; z++)
        {
            for (int x = 0; x < m_MapSizeX; x++)
            {
                m_GroundTile[z][x].GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, m_Alpha);

                if (z >= (m_MapSizeZ))
                { continue; }


                if (m_Wall[z][x] != null) // check if object exists
                {
                    //m_Wall[z][x].GetComponent<Renderer>().enabled = isVisible;

                    var chiledRend = m_Wall[z][x].GetComponentsInChildren<Renderer>();

                    foreach (var Rend in chiledRend)
                    {
                        Rend.material.color = new Color(1.0f, 1.0f, 1.0f, m_Alpha);
                    }// foreach
                }// if

                //if (m_Fire[z][x] != null)
                //{
                //    var childRend = m_Fire[z][x].GetComponentsInChildren<Renderer>();

                //    foreach (var Rend in childRend)
                //    {
                //        Rend.material.color = new Color(1.0f, 1.0f, 1.0f, m_Alpha);
                //    }// foreach
                //}// if
            }// for x
        }// for z
    }// end function

    //=========================================================================
    // 
    void UpdateVisibility()
    {
        if (m_Active == m_Visiable)
        {
            return;
        }

        if (m_Active)
        {
            if (m_Alpha < 1.0f)
            {
                m_Alpha += Time.deltaTime / m_FadeTime; 
            }
            else
            {
                m_Alpha = 1.0f;
                m_Visiable = true;
            }
        }
        else
        {
            if (m_Alpha > 0.0f)
            {
                m_Alpha -= Time.deltaTime / m_FadeTime;
            }
            else
            {
                m_Alpha = 0.0f;
                m_Visiable = false;
            }
        }

        UpdateAlpha();
    }

    //=========================================================================
    // マップの移動
    public void Move(Vector3 vel)
    {
        // horizon
        for (int z = 0; z < 2; z++)
        {
            for (int x = 0; x < m_MapSizeX; x++)
            {
                m_OutWallH[z][x].transform.position += vel;
            }// for x
        }// for z

        // virtical
        for (int z = 0; z < m_MapSizeZ; z++)
        {
            for (int x = 0; x < 2; x++)
            {
                m_OutWallV[z][x].transform.position += vel;
            }// for x
        }// for z

        // actual map size
        for (int z = 0; z < m_MapSizeZ; z++)
        {
            for (int x = 0; x < m_MapSizeX; x++)
            {
                m_GroundTile[z][x].transform.position += vel;

                if (z >= (m_MapSizeZ))
                { continue; }

                if (m_Wall[z][x] != null) // check if object exists
                {
                    m_Wall[z][x].transform.position += vel;
                }// if
            }// for x
        }// for z

        // move fire
        for (int i = 0; i < m_Fire.Count; i++)
        {
            m_Fire[i].transform.localPosition = vel;
        }

    }// end function

    //=========================================================================
    // マップの位置のセット
    public void SetPos(Vector3 pos)
    {
        // horizon
        for (int z = 0; z < 2; z++)
        {
            for (int x = 0; x < m_MapSizeX; x++)
            {
                m_OutWallH[z][x].transform.localPosition = pos + m_OgOutWallHPos[z][x];
            }// for x
        }// for z

        // virtical
        for (int z = 0; z < m_MapSizeZ; z++)
        {
            for (int x = 0; x < 2; x++)
            {
                m_OutWallV[z][x].transform.localPosition = pos + m_OgOutWallVPos[z][x];
            }// for x
        }// for z

        // actual map size
        for (int z = 0; z < m_MapSizeZ; z++)
        {
            for (int x = 0; x < m_MapSizeX; x++)
            {
                m_GroundTile[z][x].transform.localPosition = pos + m_OgGroundPos[z][x];

                if (z >= (m_MapSizeZ))
                { continue; }

                if (m_Wall[z][x] != null) // check if object exists
                {
                    m_Wall[z][x].transform.localPosition = pos + m_OgWallPos[z][x];
                }// if

            }// for x
        }// for z

        // set fire pos
        for (int i = 0; i < m_Fire.Count; i++)
        {
            m_Fire[i].transform.localPosition = pos + m_OgFirePos[i];
        }

    }// end function

    //=========================================================================
    // マップの移動
    public void SetActive(bool Active)
    {

        if (m_Active == Active)
        {
            return;
        }
        m_Active = Active;
        // horizon
        for (int z = 0; z < 2; z++)
        {
            for (int x = 0; x < m_MapSizeX; x++)
            {
                m_OutWallH[z][x].SetActive(Active);
            }// for x
        }// for z

        // virtical
        for (int z = 0; z < m_MapSizeZ; z++)
        {
            for (int x = 0; x < 2; x++)
            {
                m_OutWallV[z][x].SetActive(Active);
            }// for x
        }// for z

        // actual map size
        for (int z = 0; z < m_MapSizeZ; z++)
        {
            for (int x = 0; x < m_MapSizeX; x++)
            {
                m_GroundTile[z][x].SetActive(Active);

                if (z >= (m_MapSizeZ))
                { continue; }

                if (m_Wall[z][x] != null) // check if object exists
                {
                    m_Wall[z][x].SetActive(Active);
                }// if

                //if (m_Fire[z][x] != null) // check if exist
                //{
                //    m_Fire[z][x].SetActive(Active);
                //}
            }// for x
        }// for z
    }// end function

    //=========================================================================
    // マップのα値のゲット
    public float GetAlpha()
    {
        return m_Alpha;
    }

    //=========================================================================
    // マップのアクティブ状況を取得
    public bool GetActive()
    {
        return m_Active;
    }

    //=========================================================================
    // マップのアクティブ状況を取得
    public bool GetMoving()
    {
        return m_isMove;
    }

    //=========================================================================
    // マップを一部屋下げる
    public void MoveFloorDown()
    {
        m_isMove = true;
        m_MoveDirection = true;
        m_TimeElapsed = 0.0f;
    }
    //=========================================================================
    // マップを一部屋下げるアップデート用
    void FloorDownUpdate()
    {
        float time = Time.deltaTime;

        m_TimeElapsed += time;

        if (m_TimeElapsed >= m_FadeTime)
        {
            m_isMove = false;

            time -= m_TimeElapsed - m_FadeTime;
        }

        var fTemp = m_MoveDistance / m_FadeTime;
        fTemp *= time;

        Move(new Vector3(0.0f, -fTemp, 0.0f));
    }

    //=========================================================================
    // マップを一部屋上げる
    public void MoveFloorUp()
    {
        m_isMove = true;
        m_MoveDirection = false;
        m_TimeElapsed = 0.0f;
    }

    //=========================================================================
    // マップを一部屋上げるアップデート用
    void FloorUpUpdate()
    {
        float time = Time.deltaTime;

        m_TimeElapsed += time;

        if (m_TimeElapsed >= m_FadeTime)
        {
            m_isMove = false;

            time -= m_TimeElapsed - m_FadeTime;
        }

        var fTemp = m_MoveDistance / m_FadeTime;
        fTemp *= time;

        Move(new Vector3(0.0f, fTemp, 0.0f));
    }


    public void YogaFire(Vector3 pos)
    {
        m_Fire.Add(Instantiate(prefab_FireRing));
        m_Fire[(m_Fire.Count - 1)].transform.position = pos;
    }


} // end class
