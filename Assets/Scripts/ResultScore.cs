using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour {

    private Text targetText; // <---- 追加2

    // Use this for initialization
    void Start () {
        this.targetText = this.GetComponent<Text>(); // <---- 追加3
        this.targetText.text = ScoreController.point.ToString(); // <---- 追加4
    }
	
	// Update is called once per frame
	void Update () {
	}
}
