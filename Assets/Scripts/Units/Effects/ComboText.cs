using System.Collections;
using System.Collections.Generic;
using Marvest.SceneGlobalVariables.Stage;
using UnityEngine;
using UnityEngine.UI;

namespace Units.Effects
{
	public class ComboText : MonoBehaviour {

		public float EffectDuration;
		public float MaxScale;
		public float ScatteringDistance;
		public Text comboText;

		private float progress;
		private Vector3 direction;
		private Vector3 startPosition;

		private void Start()
		{
			StartCoroutine(Effect());
		}

		public static ComboText Create(GameObject parent, int combo)
		{
			var gameObject = Instantiate(Resources.Load("Prefabs/Effects/ComboTextEffect")) as GameObject;
			var effect = gameObject.GetComponent<ComboText>();
			effect.Initialize(parent, combo);
			StageSceneGlobalVariables.Instance.UiForegroundCanvas.AddToCanvas(effect.gameObject);
			return effect;
		}

		private void Initialize(GameObject parent, int combo)
		{
			var parentPosition = parent.transform.position;
			comboText.text = combo.ToString() + "Combo";
			startPosition = parentPosition;
			gameObject.transform.position = startPosition;
			direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0f, 1.0f)).normalized;
		}

		private IEnumerator Effect()
		{
			while (progress < EffectDuration)
			{
				progress += Time.deltaTime;
				gameObject.transform.localScale = Vector2.Lerp(Vector2.one, Vector2.one * MaxScale, progress / EffectDuration);
				gameObject.transform.position =
					startPosition +
					Vector3.Lerp(
						direction,
						direction * ScatteringDistance,
						progress / EffectDuration);
				yield return new WaitForEndOfFrame();
			}

			Destroy(gameObject);
		}
	}
}
