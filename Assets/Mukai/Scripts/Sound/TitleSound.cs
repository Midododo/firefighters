using UnityEngine;
using System.Collections;

public class TitleSound : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //SoundManager.Instance.Play(AudioKey.ResultBGM);
        SoundManager.Instance.Play(AudioKey.TitleBGM);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
