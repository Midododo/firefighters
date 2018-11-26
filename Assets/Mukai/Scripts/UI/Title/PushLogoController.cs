using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PushLogoController : MonoBehaviour
{
    private bool fade = false;
    private bool push = false;

    private Fade FadeScript;

    // Use this for initialization
    void Start()
    {
        FadeScript = GameObject.Find("Fade").GetComponent<Fade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fade)
        {
            // 何かボタンを押したときの処理
            if (Input.GetKeyDown(KeyCode.Space))
            {   
                if (!push)
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
<<<<<<< HEAD
                if(FadeScript.IsFading() == false)
=======
                GameObject.Find("Panel").GetComponent<Fade>().SetFadeOutFlag(next);
                if (!GameObject.Find("Panel").GetComponent<Fade>().IsFading())
>>>>>>> remotes/origin/Mukai2
                {
                    FadeScript.SetFadeOutFlag("Tutorial");
                    SoundManager.Instance.Stop(AudioKey.TitleBGM);
                }
            }

        }

        else if (!fade)
        {        // フェードインが完了したら
<<<<<<< HEAD
            if (!FadeScript.IsFading())
=======
            if (!GameObject.Find("Panel").GetComponent<Fade>().IsFading())
>>>>>>> remotes/origin/Mukai2
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
