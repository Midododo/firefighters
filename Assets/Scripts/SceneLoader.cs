using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    //public Fade fade;
    public GameObject fade;

    // Update is called once per frame
    void Update()
    {
        // マウスを左クリックした瞬間
        if (Input.GetMouseButtonDown(0) == true && fade.GetComponent<Fade>().IsFading() == false)
        {
            // Unityバージョン 5.3以降
            //SceneManager.LoadScene("Game");
            if (SceneManager.GetActiveScene().name == "Title")
            {
                fade.GetComponent<Fade>().SetFadeOutFlag("Tutorial");
            }
            else if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                fade.GetComponent<Fade>().SetFadeOutFlag("Game");
            }
            else if (SceneManager.GetActiveScene().name == "Game")
            {
                fade.GetComponent<Fade>().SetFadeOutFlag("Result");
            }
            else if (SceneManager.GetActiveScene().name == "Result")
            {
                fade.GetComponent<Fade>().SetFadeOutFlag("Title");
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
