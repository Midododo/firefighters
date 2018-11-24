using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PushLogoController : MonoBehaviour
{
    public string next;


    private bool fade = false;
    private bool push = false;

    // Use this for initialization
    void Start()
    {

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
                GameObject.Find("Fade").GetComponent<Fade>().SetFadeOutFlag(next);
                if (!GameObject.Find("Fade").GetComponent<Fade>().IsFading())
                {
                    SoundManager.Instance.Stop(AudioKey.TitleBGM);
                }
            }

        }

        else if (!fade)
        {        // フェードインが完了したら
            if (!GameObject.Find("Fade").GetComponent<Fade>().IsFading())
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
