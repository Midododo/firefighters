using UnityEngine;
using System.Collections;

public class GameSound : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        AudioManager.Instance.StopBgm();
        AudioManager.Instance.PlayBgm("Game_ON_BGM");
        AudioManager.Instance.Volume.bgm = 0.22f;           // カウントダウンが終了するまでは小さい音路湯で流す
    }

    // Update is called once per frame
    void Update()
    {

    }
}
