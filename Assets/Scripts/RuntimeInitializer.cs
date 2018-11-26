using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeSceneLoad()
    {
           // ScriptableObjectテーブルから情報を取得し設定する。
           var table = InitializerTable.Instance;
        if (table != null)
        {
            Debug.Log("確認");

            foreach (var objData in table.Objects)
            {
                var obj = GameObject.Instantiate(objData.Object);
                if (objData.DontDestroy)
                {
                    GameObject.DontDestroyOnLoad(obj);
                }
            }
        }
    }

} // class RuntimeInitializer