using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour {

    public GameObject m_Input;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(Input.GetKey(KeyCode.JoystickButton0));
        Debug.Log(Input.GetKey(KeyCode.JoystickButton1));
        Debug.Log(Input.GetKey(KeyCode.JoystickButton2));
        Debug.Log(Input.GetKey(KeyCode.JoystickButton3));
        Debug.Log(m_Input.GetComponent<JoyconInput>().GetTrigger(Joycon.Button.DPAD_RIGHT, 0, false));
        Debug.Log(m_Input.GetComponent<JoyconInput>().GetStick(0, true).x);
        Debug.Log(Input.GetAxis("Horizontal_Joystick1_Left"));
        Debug.Log(Input.GetAxis("Horizontal_Joystick1_Right"));
        Debug.Log(Input.GetAxis("Vertical_Joystick1_Left"));
        Debug.Log(Input.GetAxis("Vertical_Joystick1_Right"));

        Debug.Log("a" + Input.GetAxis("Horizontal_Joystick1_Left"));
        Debug.Log("a" + Input.GetAxis("Vertical_Joystick1_Left"));
        Debug.Log("d" + Input.GetAxis("Horizontal_Joystick1_Right"));
        Debug.Log("d" + Input.GetAxis("Vertical_Joystick1_Right"));
    }
}
