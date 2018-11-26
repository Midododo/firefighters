using UnityEngine;
using System.Collections;

public class ResultSound : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SoundManager.Instance.Play(AudioKey.ResultBGM);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
