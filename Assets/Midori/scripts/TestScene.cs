using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScene : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        //Invoke("ChangeScene", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // マウスを左クリックした瞬間
        if (Input.GetMouseButtonDown(0) == true)
        {
            // Unityバージョン 5.3以降
            SceneManager.LoadScene("Game");
        }
    }

    //void ChangeScene()
    //{
    //    SceneManager.LoadScene("Game");
    //}
}
