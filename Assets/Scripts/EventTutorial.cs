using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTutorial : MonoBehaviour
{

    private TutorialMessageController Message;
    private Fade FadeScript;
    private CameraController2 Camera;

    // Use this for initialization
    void Start()
    {
        FadeScript = GameObject.Find("Fade").GetComponent<Fade>();

        Message = GameObject.Find("Message").GetComponent<TutorialMessageController>();

        Camera = GameObject.Find("CameraRig").GetComponent<CameraController2>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.name == "EventFire")
        {
            Message.ChangeText(2);
        }
        else if (this.name == "EventGoGame")
        {
            FadeScript.SetFadeOutFlag("Game");
            Camera.TutorialZoomCamera();
        }
        else if (this.name == "EventSplash")
        {
            Message.ChangeText(1);
        }
        else if (this.name == "EventObject")
        {
            Message.ChangeText(3);
        }
    }
}
