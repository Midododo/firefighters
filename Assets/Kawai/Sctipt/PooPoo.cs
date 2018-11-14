using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooPoo : MonoBehaviour {


    public float m_MoveValue = 0.2f;

    public float m_Range = 1.0f;

    public GameObject m_Target;


	// Use this for initialization
	void Start () {

        //transform.position = m_Target.transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 tVec3 = new Vector3(0.0f, 0.0f, 0.0f);
        tVec3 = m_Target.transform.position - transform.position;
        if (tVec3.magnitude > m_Range)
        {
            float tAng = Mathf.Atan2(tVec3.z, tVec3.x);
            tVec3.x = Mathf.Cos(tAng) * m_MoveValue;
            tVec3.z = Mathf.Sin(tAng) * m_MoveValue;
            transform.position += tVec3;
        }
    }
}
