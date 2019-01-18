using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadWaterController : MonoBehaviour {

    private ParticleSystem particle;

	// Use this for initialization
	void Start () {
        particle = this.GetComponent<ParticleSystem>();	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Z))
            particle.Play();
        if (Input.GetKey(KeyCode.X))
            particle.Stop();
    }
}
