using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{

    public int m_MapScaleX = 5;
    public int m_MapScaleZ = 5;
    public int m_MapSectionSize = 10;

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

    private GameObject[][] m_GroundTile;
    private GameObject[][] m_OutWallV;
    private GameObject[][] m_OutWallH;

    private GameObject[][] m_Wall;

    public OpenTextFile openTextFile;

	// Use this for initialization
	void Awake ()
    {
        CreateFloor();

        CreateWalls();
	}


    //=========================================================================
    // フロアの生成処理
    void CreateFloor()
    {
        // create as array so i can manage
        m_GroundTile = new GameObject[m_MapScaleZ][];


        m_OutWallH = new GameObject[2][];   // 2 means north and south
        m_OutWallV = new GameObject[m_MapScaleZ][];

        for (int z = 0; z < m_MapScaleZ; z++)
        {
            m_GroundTile[z] = new GameObject[m_MapScaleX];
            m_OutWallV[z] = new GameObject[2];  // 2 means east and west

            if (z == 0) // is south wall
            {
                m_OutWallH[0] = new GameObject[m_MapScaleX];
            }
            else if (z == (m_MapScaleZ - 1)) // is north wall
            {
                m_OutWallH[1] = new GameObject[m_MapScaleX];
            }

            for (int x = 0; x < m_MapScaleX; x++)
            {

                m_GroundTile[z][x] = Instantiate(prefab_GroundTile);
                m_GroundTile[z][x].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, (float)(z * m_MapSectionSize));

                if (z == 0) // is south wall or is north wall
                {
                    m_OutWallH[0][x] = Instantiate(prefab_OutWallH);
                    m_OutWallH[0][x].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, 0.0f);
                }
                else if (z == (m_MapScaleZ - 1)) // 
                {
                    m_OutWallH[1][x] = Instantiate(prefab_OutWallH);
                    m_OutWallH[1][x].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, (float)((z + 1) * m_MapSectionSize));
                }

                if (x == 0)
                {
                    m_OutWallV[z][0] = Instantiate(prefab_OutWallV);
                    m_OutWallV[z][0].transform.position += new Vector3(0.0f, 0.0f, (float)(z * m_MapSectionSize));
                }
                else if (x == (m_MapScaleX - 1))
                {
                    m_OutWallV[z][1] = Instantiate(prefab_OutWallV);
                    m_OutWallV[z][1].transform.position += new Vector3((float)((x + 1) * m_MapSectionSize), 0.0f, (float)(z * m_MapSectionSize));
                }

            } // for x
        } // for z
    } // end function

    void CreateWalls()
    {

        m_Wall = new GameObject[m_MapScaleZ][];

        string path = "Assets/Kawai/Resources/WallTest.txt";

        int[][] Data = new int[100][];

        openTextFile.ConvertCSVDataToIntArray(path, Data);


        for (int z = 0; z < m_MapScaleZ; z++)
        {
            m_Wall[z] = new GameObject[m_MapScaleX];
            for (int x = 0; x < m_MapScaleX; x++)
            {
                switch(Data[z][x])
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
                if (Data[z][x] != 0)
                {
                    m_Wall[z][x].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, (float)(z * m_MapSectionSize));
                }


            } // for x
        } // for z

    } // end function



    void CreateFurniture()
    {
        m_Wall = new GameObject[m_MapScaleZ][];

        string path = "Assets/Kawai/Resources/WallTest.txt";

        int[][] Data = new int[100][];

        openTextFile.ConvertCSVDataToIntArray(path, Data);


        for (int z = 0; z < m_MapScaleZ; z++)
        {
            m_Wall[z] = new GameObject[m_MapScaleX];
            for (int x = 0; x < m_MapScaleX; x++)
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
                    m_Wall[z][x].transform.position += new Vector3((float)(x * m_MapSectionSize), 0.0f, (float)(z * m_MapSectionSize));
                }


            } // for x
        } // for z

    } // end function

} // end class
