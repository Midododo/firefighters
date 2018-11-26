using UnityEngine;
using System.Collections;

public class TutrialSound : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SoundManager.Instance.Play(AudioKey.GameBGM_OFF);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
