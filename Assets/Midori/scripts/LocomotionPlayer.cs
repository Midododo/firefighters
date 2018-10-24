 /// <summary>
/// 
/// </summary>

using UnityEngine;
using System;
using System.Collections;
  
[RequireComponent(typeof(Animator))]  

//Name of class must be name of file as well

public class LocomotionPlayer : MonoBehaviour {

    protected Animator animator;

    private float speed = 0;
    private float direction = 0;
    private Locomotion locomotion = null;
    GameObject player;
    public int player_num = 0;

	// Use this for initialization
	void Start () 
	{
        animator = GetComponent<Animator>();
        locomotion = new Locomotion(animator);
	}
    
	void Update () 
	{
        if (animator && Camera.main)
        {
            if (transform.tag == "Player1")
            {
                player_num = 1;
            }
            else if (transform.tag == "Player2")
            {
                player_num = 2;
            }
            JoystickToEvents.Do(transform, Camera.main.transform, ref speed, ref direction, player_num);
            locomotion.Do(speed * 6, direction * 180);
        }	
	}
}
