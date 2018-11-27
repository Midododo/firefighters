using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMessageController : MonoBehaviour
{

    private Text targetText; // <---- 追加2

    [SerializeField]
    private string tex1;

    [SerializeField]
    private string tex2;

    [SerializeField]
    private string tex3;

    // Use this for initialization
    void Start()
    {
        this.targetText = this.GetComponent<Text>(); // <---- 追加3
        //this.targetText.text = ScoreController.point.ToString(); // <---- 追加4
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            ChangeText(1);
        }
        if (Input.GetKey(KeyCode.F2))
        {
            ChangeText(2);
        }
        if (Input.GetKey(KeyCode.F3))
        {
            ChangeText(3);
        }
    }

    public void ChangeText(int Num)
    {
        switch (Num)
        {
            case 1:
                targetText.text = tex1;
                break;

            case 2:
                targetText.text = tex2;
                break;

            case 3:
                targetText.text = tex3;
                break;
        }
    }
}