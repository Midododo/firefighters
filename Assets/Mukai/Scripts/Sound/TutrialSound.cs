using UnityEngine;
using System.Collections;

public class TutrialSound : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        AudioManager.Instance.StopBgm();
        AudioManager.Instance.PlayBgm("Game_OFF_BGM");
        AudioManager.Instance.Volume.bgm = 0.7f;
        //SoundManager.Instance.Play(AudioKey.GameBGM_OFF);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
