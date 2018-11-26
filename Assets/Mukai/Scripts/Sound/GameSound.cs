using UnityEngine;
using System.Collections;

public class GameSound : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SoundManager.Instance.Play(AudioKey.GameBGM_ON);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
