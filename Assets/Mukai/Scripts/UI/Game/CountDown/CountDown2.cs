using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown2 : MonoBehaviour
{

    [SerializeField]
    private Text _textCountdown;

    [SerializeField]
    private Image _imageMask;

    void Start()
    {
        _textCountdown.text = "";
    }

    public void SetCountDown()
    {
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        //_imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _textCountdown.text = "3";
        AudioManager.Instance.PlaySe("Count1");
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "2";
        AudioManager.Instance.PlaySe("Count1");
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "1";
        AudioManager.Instance.PlaySe("Count1");
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "GO!";
        AudioManager.Instance.PlaySe("Go2");
        yield return new WaitForSeconds(0.8f);

        AudioManager.Instance.Volume.bgm = 1.0f;        // BGMを通常に戻す

        _textCountdown.text = "";
        _textCountdown.gameObject.SetActive(false);
        _imageMask.gameObject.SetActive(false);
    }
}
