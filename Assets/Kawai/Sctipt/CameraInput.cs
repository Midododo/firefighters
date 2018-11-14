using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour {

    public float m_RotValue = 0.1f;
    public float m_OffsetX = 0.0f;
    public float m_OffsetY = 11.0f;
    public float m_OffsetZ = -11.0f;

    public GameObject m_FocusOne;
    public GameObject m_FocusTwo;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        SetPos();

        Quaternion tAng = transform.rotation;

        if (Input.GetKey("j"))
        {
            tAng.y += m_RotValue;
        }
        if (Input.GetKey("l"))
        {
            tAng.y -= m_RotValue;
        }

        transform.rotation = tAng;
    }




    void SetPos()
    {
        Vector3 tVec3 = new Vector3(0.0f, 0.0f, 0.0f);

        tVec3 = (((m_FocusOne.transform.position + m_FocusTwo.transform.position) / 2) - (transform.position - new Vector3(m_OffsetX, m_OffsetY, m_OffsetZ))) / 10;

        transform.position += tVec3;
    }

}
