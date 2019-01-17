using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PushLogoController : MonoBehaviour
{
    private bool fade = false;
    private bool fade_out = false;
    private bool push = false;

    private Fade FadeScript;

    // Use this for initialization
    void Start()
    {
        //SoundManager.Instance.Play(AudioKey.TitleBGM);
        FadeScript = GameObject.Find("Fade").GetComponent<Fade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            // 何かボタンを押したときの処理
            if (Input.GetMouseButtonDown(0) == true)
            // 何かボタンを押したときのアクション(いまはスペース)
            //if (Input.GetKeyDown(KeyCode.Space))
            {   
                if (!push)      // 一階も押されていなかったときのみ反応
                {
                    if (gameObject.GetComponent<AlphaChanger>().enabled)
                    {
                        SoundManager.Instance.PlayOneShot(AudioKey.Push);
                        // プッシュロゴの明滅アニメーションをやめ、一旦α値を1.0に再設定する
                        this.gameObject.GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
                        gameObject.GetComponent<AlphaChanger>().enabled = false;
                        gameObject.GetComponent<Blinker>().enabled = true;
                    }
                    push = !push;
                }
            }

            // 点滅アニメーションが終了したら
            else if (gameObject.GetComponent<Blinker>().Count == gameObject.GetComponent<Blinker>().cnt)
            {
                if(!FadeScript.IsFading())
                {
                    FadeScript.SetFadeOutFlag("Tutorial");
                    // ここで止めるかチュートリアルで止めるか検討中
                    SoundManager.Instance.Stop(AudioKey.TitleBGM);
                }
            }

        }

        else if (!fade)
        {        // フェードインが完了したら
            if (!FadeScript.IsFading())
            {
                SoundManager.Instance.Play(AudioKey.TitleBGM);
                this.gameObject.GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
                gameObject.GetComponent<AlphaChanger>().enabled = true;
                fade = !fade;
            }
        }
    }

    //Alpha値を更新してColorを返す
    Color ChangeAlphaColor(Color color)
    {
        color.a = 1.0f;
        return color;
    }
}
