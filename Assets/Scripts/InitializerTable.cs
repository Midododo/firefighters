using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "MyGame/Create InitializerTable", fileName = "InitializerTable")]
public class InitializerTable : ScriptableObject
{
    private static readonly string RESOURCE_PATH = "InitializerTable";

    private static InitializerTable s_instance = null;
    public static InitializerTable Instance
    {
        get
        {
            if (s_instance == null)
            {
                InitializerTable asset = Resources.Load(RESOURCE_PATH) as InitializerTable;
                if (asset == null)
                {
                    // アセットが指定のパスに無い。
                    // 誰かが勝手に移動させたか、消しやがったな！
                    Debug.AssertFormat(false, "Missing InitializerTable! path={0}", RESOURCE_PATH);
                    asset = CreateInstance<InitializerTable>();
                }

                s_instance = asset;
            }

            return s_instance;
        }
    }

    [System.Serializable]
    public class ManagerList
    {
        public GameObject Object;
        public bool DontDestroy;
    };

    [SerializeField]
    public ManagerList[] Objects;

} // class InitializerTable