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
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "2";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "1";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        _textCountdown.text = "";
        _textCountdown.gameObject.SetActive(false);
        _imageMask.gameObject.SetActive(false);
    }
}
