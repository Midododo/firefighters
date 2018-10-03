using Marvest.SceneGlobalVariables.Stage;
using UnityEngine;
using UnityEngine.UI;

namespace Marvest.Gestures
{
    public class UiShooterArrow : MonoBehaviour
    {
        public static UiShooterArrow Create()
        {
            var gameObject = Instantiate(Resources.Load("Prefabs/UiShooterArrow")) as GameObject;
            var shooterArrow = gameObject.GetComponent<UiShooterArrow>();
            StageSceneGlobalVariables.Instance.UiBackgroundCanvas.AddToCanvas(shooterArrow.gameObject);
            shooterArrow.transform.SetAsLastSibling();
            shooterArrow.Hide();

            return shooterArrow;
        }

        public void SetPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        public void Refresh(Vector2 dragVector)
        {
            var direction = Quaternion.FromToRotation(-Vector3.up, dragVector);
            transform.rotation = direction;

            var length = 2 * dragVector.magnitude;
            var sizeDelta = GetComponent<RectTransform>().sizeDelta;
            GetComponent<RectTransform>().sizeDelta = new Vector2(sizeDelta.x, length);
        }

        public void Show()
        {
            GetComponent<Image>().enabled = true;
        }

        public void Hide()
        {
            GetComponent<Image>().enabled = false;
        }
    }
}