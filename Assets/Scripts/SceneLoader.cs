using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    //public Fade fade;
    public GameObject fade;
    private Fade FadeScript;

    private void Start()
    {
        FadeScript = GameObject.Find("Fade").GetComponent<Fade>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(FadeScript.IsFading());
        // マウスを左クリックした瞬間
        if (Input.GetMouseButtonDown(0) == true && FadeScript.IsFading() == false)
        {
            // Unityバージョン 5.3以降
            //SceneManager.LoadScene("Game");
            if (SceneManager.GetActiveScene().name == "Title")
            {
                FadeScript.SetFadeOutFlag("Tutorial");
            }
            else if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                FadeScript.SetFadeOutFlag("Game");
            }
            else if (SceneManager.GetActiveScene().name == "Game")
            {
                FadeScript.SetFadeOutFlag("Result");
            }
            else if (SceneManager.GetActiveScene().name == "Result")
            {
                FadeScript.SetFadeOutFlag("Title");
            }
        }
    }

 //   void OnGUI()
	//{
	//	GUI.Box(new Rect(10 , Screen.height - 100 ,100 ,90), "Change Scene");
	//	if(GUI.Button( new Rect(20 , Screen.height - 70 ,80, 20), "Next"))
	//		LoadNextScene();
	//	if(GUI.Button(new Rect(20 ,  Screen.height - 40 ,80, 20), "Back"))
	//		LoadPreScene();
	//}

	//void LoadPreScene()
	//{
	//	int nextLevel = Application.loadedLevel + 1;
	//	if( nextLevel <= 1)
	//		nextLevel = Application.levelCount;

	//	Application.LoadLevel(nextLevel);
	//}

	//void LoadNextScene()
	//{
	//	int nextLevel = Application.loadedLevel + 1;
	//	if( nextLevel >= Application.levelCount)
	//		nextLevel = 1;

	//	Application.LoadLevel(nextLevel);

	//}
}
