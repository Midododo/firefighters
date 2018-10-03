using UnityEngine;

namespace Marvest.Canvases
{
    public class UiCanvas
    {
        private readonly string canvasName;

        public UiCanvas(string canvasName)
        {
            this.canvasName = canvasName;
        }

        private Canvas canvas;

        public Canvas Canvas
        {
            get { return canvas ?? (canvas = GameObject.Find(canvasName).GetComponent<Canvas>()); }
        }

        public bool HasCamera
        {
            get { return Canvas.renderMode == RenderMode.ScreenSpaceCamera && Canvas.worldCamera != null; }
        }

        public Vector3 PositionToCanvas(Vector3 position)
        {
            if (HasCamera)
            {
                position = Canvas.worldCamera.WorldToScreenPoint(position) -
                           (Vector3) Canvas.GetComponent<RectTransform>().sizeDelta / 2;
            }

            return position;
        }

        public Vector2 SizeToCanvas(Vector2 size)
        {
            if (HasCamera)
            {
                size = canvas.worldCamera.WorldToScreenPoint(size);
            }

            return size;
        }

        public void AddToCanvas(GameObject child)
        {
            child.transform.SetParent(Canvas.transform, false);
        }
    }
}