using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PushLogoController : MonoBehaviour
{
    private bool fade;
    private bool fade_out;
    private bool push;

    private Fade FadeScript;
    private Blinker Blink;

    // Use this for initialization
    void Start()
    {
        //SoundManager.Instance.Play(AudioKey.TitleBGM);
        FadeScript = GameObject.Find("Fade").GetComponent<Fade>();
        Blink = gameObject.GetComponent<Blinker>();

        fade = false;
        fade_out = false;
        push = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            // 何かボタンを押したときのアクション(いまはスペース)
            if (Input.GetKeyDown(KeyCode.Space))
            {   
                if (!push)      // 一階も押されていなかったときのみ反応
                {
                    if (gameObject.GetComponent<AlphaChanger>().enabled)
                    {
                        //SoundManager.Instance.PlayOneShot(AudioKey.Push);
                        AudioManager.Instance.PlaySe("push");
                        // プッシュロゴの明滅アニメーションをやめ、一旦α値を1.0に再設定する
                        this.gameObject.GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
                        gameObject.GetComponent<AlphaChanger>().enabled = false;
                        Blink.enabled = true;
                    }
                    push = !push;
                }
            }

            // 点滅アニメーションが終了したら
            if (Blink.Count == Blink.cnt)
            {
                if(!FadeScript.IsFading())
                {
                    FadeScript.SetFadeOutFlag("Tutorial");
                    // ここで止めるかチュートリアルで止めるか検討中
                    //SoundManager.Instance.Stop(AudioKey.TitleBGM);
                }
            }
        }

        else if (!fade)
        {        // フェードインが完了したら
            if (!FadeScript.IsFading())
            {
                //SoundManager.Instance.Play(AudioKey.TitleBGM);
                AudioManager.Instance.PlayBgm("Title_BGM");
                this.gameObject.GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
                gameObject.GetComponent<AlphaChanger>().enabled = true;
                fade = !fade;
            }
        }
    }


    // アクションに対してのレスポンス
    public void PushAnyBotton()
    {
        if (!push)      // 一階も押されていなかったときのみ反応
        {
            if (gameObject.GetComponent<AlphaChanger>().enabled)
            {
                //SoundManager.Instance.PlayOneShot(AudioKey.Push);
                AudioManager.Instance.PlaySe("push");
                // プッシュロゴの明滅アニメーションをやめ、一旦α値を1.0に再設定する
                this.gameObject.GetComponent<Image>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
                gameObject.GetComponent<AlphaChanger>().enabled = false;
                Blink.enabled = true;
            }
            push = !push;
        }
    }


    //Alpha値を更新してColorを返す
    Color ChangeAlphaColor(Color color)
    {
        color.a = 1.0f;
        return color;
    }

}
