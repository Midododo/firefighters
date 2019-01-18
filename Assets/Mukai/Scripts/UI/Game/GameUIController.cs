﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ResucueNum Resucue;
    public TimeCountSystem Timer;
    public GageController Gage;
    public ScoreController Score;
    public WaterTypeController Type;
    public CountDown2 Count;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ////テスト
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Resucue.AddPoint();
        //}

        //// テスト
        //if (Input.GetKeyDown(KeyCode.W)) Type.type_right.ChangeWaterType();
        //if (Input.GetKeyDown(KeyCode.S)) Type.type_left.ChangeWaterType();


        // テスト
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Count.SetCountDown();
        }


    }
}