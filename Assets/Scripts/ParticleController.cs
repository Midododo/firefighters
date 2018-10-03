using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    [SerializeField]
    private ParticleSystem particle;

    // Use this for initialization
    void Start () {
        particle.Play();
    }
	
	// Update is called once per frame
	void Update () {
       
	}
}
