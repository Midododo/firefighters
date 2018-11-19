using System;

/// <summary>
/// Resourcesフォルダ配下のPrefabに付加される情報を表す
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public class PrefabAttribute : Attribute
{
    /// <summary>
    /// Resourcesフォルダ以降のPrefabのパス
    /// </summary>
    public readonly string Path;

    /// <summary>
    /// シーンロード時に消失しないオブジェクトであるべきかどうか
    /// </summary>
    public readonly bool Persistent;

    /// <summary>
    /// プレハブ情報を表すデータを生成する
    /// </summary>
    /// <param name="path">Resources.Loadで呼び出せるプレハブのへのパス</param>
    /// <param name="persistent">シーン変更時に消失しないオブジェクトにするかどうか</param>
    public PrefabAttribute(string path, bool persistent = false)
    {
        Path = path;
        Persistent = persistent;
    }
}