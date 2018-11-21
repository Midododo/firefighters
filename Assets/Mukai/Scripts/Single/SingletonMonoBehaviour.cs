using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 常に単一のインスタンスしか存在しないことを保証する
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static bool instantiated;

    private void Awake()
    {
        if (FindObjectsOfType<T>().Length > 1)
            DestroyImmediate(this);
    }

    /// <summary>
    /// インスタンスへのアクセスを提供する
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instantiated)
                return instance;

            var type = typeof(T);
            var objects = FindObjectsOfType<T>();

            if (objects.Length > 0)
            {
                Instance = objects[0];
                if (objects.Length > 1)
                {
                    Debug.LogWarning("複数の\"" + type + "\"が存在したため、削除されました");
                    for (var i = 1; i < objects.Length; i++)
                        DestroyImmediate(objects[i].gameObject);
                }
                instantiated = true;
                return instance;
            }

            Debug.LogWarning("ゲーム内に" + type + "型インスタンスが存在しないためResources内のPrefabからの作成処理を行います");

            var attribute = Attribute.GetCustomAttribute(type, typeof(PrefabAttribute)) as PrefabAttribute;
            if (attribute == null)
                Debug.LogError("PrefabAttributeが付加されていない型\"" + type + "\"が参照されました");
            else
            {
                var prefabName = attribute.Path;
                if (String.IsNullOrEmpty(prefabName))
                {
                    Debug.LogError("PrefabAttributeのnameが空です \"" + type + "\"");
                    return null;
                }

                var gameObject = Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject;
                if (gameObject == null)
                {
                    Debug.LogError("\"type\"" + "型のPrefab\"" + prefabName + "\"を生成できませんでした");
                    return null;
                }
                gameObject.name = prefabName;
                Instance = gameObject.GetComponent<T>();
                if (!instantiated)
                {
                    Debug.LogWarning("\"" + type + "\"型のComponentが\"" + prefabName + "\"に存在しなかったため追加されました");
                    Instance = gameObject.AddComponent<T>();
                }
                if (attribute.Persistent)
                    DontDestroyOnLoad(instance.gameObject);
                return instance;
            }

            Debug.LogWarning("Prefabからの生成に失敗したため、強制的にオブジェクトを生成します");
            var createObject = new GameObject("SingletonCreateObject", type);
            Instance = createObject.GetComponent<T>();
            return instance;
        }

        private set
        {
            instance = value;
            instantiated = value != null;
        }
    }

    private void OnDestroy()
    {
        instantiated = false;
    }
}
