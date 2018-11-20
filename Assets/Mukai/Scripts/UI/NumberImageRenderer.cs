using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class NumberImageRenderer : MonoBehaviour
{
    [System.Serializable]
    public struct TextNumRenderData
    {
        public TextNumRenderData(string fileName, Transform parentTrasform, Color color, float justification)
            : this()
        {
            this.fileName = fileName;
            this.parentTrasform = parentTrasform;
            this.color = color;
            this.justification = justification;
        }

        public string fileName;
        public Transform parentTrasform;
        public Color color;

        /// <summary>
        /// 位置揃いする間隔
        /// </summary>
        public float justification;
    }

    struct CreateData
    {
        public RectTransform rectTrans;
        public Image image;
    }

    int digit = 0;
    int oldDigit = 0;

    Sprite[] sprites = null;
    List<CreateData> createList = new List<CreateData>();

    [SerializeField]
    TextNumRenderData data;

    CreateData Create(Sprite sprite)
    {
        var instance = new GameObject();
        instance.name = data.fileName + "_" + sprite.name;
        instance.transform.SetParent(data.parentTrasform);

        var rectTrans = instance.AddComponent<RectTransform>();
        instance.transform.localScale = Vector3.one;
        rectTrans.sizeDelta = data.parentTrasform.GetComponent<RectTransform>().sizeDelta;

        var image = instance.AddComponent<Image>();
        image.sprite = sprite;
        image.color = data.color;

        var createData = new CreateData() { rectTrans = rectTrans, image = image };

        return createData;
    }

    void ResourcesLoad()
    {
        if (sprites == null)
        {
            createList.Clear();
            sprites = Resources.LoadAll<Sprite>(data.fileName);
        }
    }

    /// <summary>
    /// 描画処理
    /// </summary>
    /// <param name="text"></param>
    void Rendering(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            var sprite = Array.Find(sprites, s => s.name == text[i].ToString());

            digit++;
            if (digit > oldDigit)
            {
                oldDigit = digit;
                var createData = Create(sprite);
                createData.rectTrans.anchoredPosition3D =
                    new Vector3(i * (createData.rectTrans.sizeDelta.x - data.justification), 0, 0);
                createList.Add(createData);
            }
            else
            {
                var createData = createList[i];
                createData.image.sprite = sprite;
                createData.rectTrans.name = data.fileName + "_" + sprite.name;
            }
        }

        digit = 0;
    }

    /// <summary>
    /// 数字を画像で描画する関数
    /// </summary>
    public void Render(double score)
    {
        if (score <= -1) return;

        ResourcesLoad();

        Rendering(score.ToString("f2"));
    }

    /// <summary>
    /// 数字を画像で描画する関数
    /// </summary>
    public void Render(int score)
    {
        if (score <= -1) return;

        ResourcesLoad();

        Rendering(score.ToString());
    }

    /// <summary>
    /// 色をすべて変更する。
    /// </summary>
    /// <param name="color"></param>
    public void ChnageColor(Color color)
    {
        data.color = color;
        foreach (var list in createList)
        {
            list.image.color = data.color;
        }
    }

}
