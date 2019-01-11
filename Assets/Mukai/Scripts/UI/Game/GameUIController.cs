using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public ResucueNum Resucue;
    public TimeCountSystem Timer;
    public GageController Gage;
    public ScoreController Score;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //テスト
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Resucue.AddPoint();
        }

    }
}
