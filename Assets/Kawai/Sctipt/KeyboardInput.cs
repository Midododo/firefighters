using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour {


    public float m_VelMax = 0.1f;

    public GameObject m_Camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 tVel = new Vector3(0.0f, 0.0f, 0.0f);


        if (Input.GetKey("up"))
        {
            tVel.z += 0.1f;
        }
        if (Input.GetKey("left"))
        {
            tVel.x -= 0.1f;
        }
        if (Input.GetKey("down"))
        {
            tVel.z -= 0.1f;
        }
        if (Input.GetKey("right"))
        {
            tVel.x += 0.1f;
        }

        if (tVel.magnitude != 0.0f)
        {
            float tAng = Mathf.Atan2(tVel.z, tVel.x);
            tVel.x = Mathf.Cos(tAng + m_Camera.transform.rotation.y) * m_VelMax;
            tVel.z = Mathf.Sin(tAng + m_Camera.transform.rotation.y) * m_VelMax;
            transform.position += tVel;
        }
    }
}
