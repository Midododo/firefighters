using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTutorial : MonoBehaviour
{
    //private TutorialMessageController Message;
    private MessageController Window;
    private Fade FadeScript;
    private CameraController2 Camera;

    public GameObject prefab_FireRing;
    private GameObject FireRing;
    private bool FireRingFlg = false;

    // Use this for initialization
    void Start()
    {
        FadeScript = GameObject.Find("Fade").GetComponent<Fade>();

        //Message = GameObject.Find("Message").GetComponent<TutorialMessageController>();

        Window = GameObject.Find("Window").GetComponent<MessageController>();

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
            if (FireRingFlg == false)
            {
                Window.DeleteWindow();
                Window.ShowWindow();

                FireRingFlg = true;
                //Message.ChangeText(2);
                FireRing = Instantiate(prefab_FireRing);// 入れ物の準備
                this.gameObject.SetActive(false);
            }
        }
        else if (this.name == "EventFire_Extinguish")
        {
            Window.DeleteWindow();
            Window.ShowWindow();
            this.gameObject.SetActive(false);
        }
        else if (this.name == "EventGoGame")
        {
            FadeScript.SetFadeOutFlag("Game");
            Camera.TutorialZoomCamera();
            this.gameObject.SetActive(false);
        }
        else if (this.name == "EventSplash")
        {
            Window.DeleteWindow();
            Window.ShowWindow();
            //Message.ChangeText(1);
            this.gameObject.SetActive(false);
        }
        else if (this.name == "EventObject")
        {
            Window.DeleteWindow();
            Window.ShowWindow();
            //Message.ChangeText(3);
            this.gameObject.SetActive(false);
        }
    }
}
