using UnityEngine;
using System.Collections;

public class ResultSound : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        AudioManager.Instance.StopBgm();
        AudioManager.Instance.PlayBgm("Result_BGM");
        AudioManager.Instance.Volume.bgm = 0.8f;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
