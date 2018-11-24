using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int m_PlayerIdx;

    public float m_MaxVelocity = 20.0f;

    private Rigidbody m_Rbody;

    public GameObject m_Input;
    //public GameObject m_Camera;

    private Vector3 m_OldPos;


	// Use this for initialization
	void Awake()
    {
        m_Rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {

        Move();
	}


    void Action()
    {
        if (m_Input.GetComponent<JoyconInput>().GetTrigger(Joycon.Button.DPAD_RIGHT, m_PlayerIdx, false))
        {

        }
    }


    void Move()
    {

        Vector2 tVec2 = m_Input.GetComponent<JoyconInput>().GetStick(m_PlayerIdx, true);
        Vector2 tVec2Right = m_Input.GetComponent<JoyconInput>().GetStick(m_PlayerIdx, false);

        if (tVec2.magnitude == 0.0f && tVec2Right.magnitude == 0.0f)
        {
            m_Rbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            return;
        }

        float tStickAng = 0.0f;
        if (tVec2.magnitude != 0.0f)
        {
            tStickAng = Mathf.Atan2(tVec2.y, tVec2.x);
        }
        else
        {
            tStickAng = Mathf.Atan2(tVec2Right.y, tVec2Right.x);
        }
        //float tCamAng = Mathf.Atan2(m_Camera.transform.position.z, m_Camera.transform.position.x);

        //tVec2.x = Mathf.Cos(tStickAng - m_Camera.transform.rotation.y) * m_MaxVelocity;
        //tVec2.y = Mathf.Sin(tStickAng - m_Camera.transform.rotation.y) * m_MaxVelocity;
        tVec2.x = Mathf.Cos(tStickAng - 45) * m_MaxVelocity;
        tVec2.y = Mathf.Sin(tStickAng - 45) * m_MaxVelocity;


        m_OldPos = transform.position;

        //transform.position += new Vector3(tVec2.x, 0.0f, tVec2.y) * Time.deltaTime;


        //transform.Translate(new Vector3(tVec2.x, 0.0f, tVec2.y));
        //m_Rbody.AddForce(new Vector3(tVec2.x, 0.0f, tVec2.y));
        m_Rbody.velocity = new Vector3(tVec2.x, 0.0f, tVec2.y) * Time.deltaTime;
        //m_Rbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);

    }



    void DebugMove()
    {
        var tVec = new Vector3(0.0f, 0.0f, 0.0f);
        if(Input.GetKey(KeyCode.UpArrow))
        {
            tVec.z += m_MaxVelocity;
        }
        transform.position += tVec;
    }



    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Ground")
        { return; }

        print(gameObject.name + collision.collider.name + "enter");

        Vector3 tVec3 = new Vector3(0.0f, 0.0f, 0.0f);

        foreach (ContactPoint contact in collision.contacts)
        {

            //Debug.DrawRay(contact.point, contact.normal, Color.white);
            tVec3 = contact.point;
        }

        //transform.position = tVec3;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.name == "Ground")
        { return; }

        print(gameObject.name + collision.collider.name + "stay");
        foreach (ContactPoint contact in collision.contacts)
        {

            //Debug.DrawRay(contact.point, contact.normal, Color.white);
            //transform.position = contact.point + contact.normal;

        }

        //transform.position = m_OldPos;
        //transform.position = m_OldPos;// + (transform.position - collisionInfo.collider.ClosestPoint(transform.position));

    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.name == "Ground")
        { return; }

        print(gameObject.name + collision.collider.name + "exit");

    }
}
