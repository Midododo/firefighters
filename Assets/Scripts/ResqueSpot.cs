using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResqueSpot : MonoBehaviour {

    private ParticleSystem Heart;

    private ScoreController score;
    public int ScoreAddNum = 100;

    private ResucueNum Rescue;

    // Use this for initialization
    void Start () {
        Heart = GameObject.Find("Resque_Heart").GetComponent<ParticleSystem>();

        if (this.gameObject.scene.name == "Game")
        {
            score = GameObject.Find("Score").GetComponent<ScoreController>();
            Rescue = GameObject.Find("ResucuerNum").GetComponent<ResucueNum>();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("aa");
        if(other.tag == "Human")
        {
            score.AddScore(ScoreAddNum);
            Rescue.AddPoint();
            other.gameObject.SetActive(false);
            Heart.Play();
        }
    }
}
