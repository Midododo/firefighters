using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Marvest.SceneGlobalVariables.Stage;

namespace Marvest{
	public class Score : MonoBehaviour {

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
			GetComponent<Text> ().text = StageSceneGlobalVariables.Instance.Score.ToString();
		}
	}
}
