using LitJson;
using MBLDefine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// サウンド設定の情報を取り扱うクラス
/// </summary>
public class SoundSetting
{
    private const string SE = "SE";
    private const string BGM = "BGM";

    private Dictionary<string, double> data = new Dictionary<string, double>();

    private readonly string filePath;

    /// <summary>
    /// SEのVolumeへのアクセサー
    /// </summary>
    public float SEVolume
    {
        get { return (float)data[SE]; }
        set { data[SE] = value; }
    }

    /// <summary>
    /// BGMのVolumeへのアクセサー
    /// </summary>
    public float BGMVolume
    {
        get { return (float)data[BGM]; }
        set { data[BGM] = value; }
    }

    /// <summary>
    /// サウンド設定を扱うクラスを生成する
    /// </summary>
    /// <param name="filePath"></param>
    public SoundSetting(string filePath)
    {
        this.filePath = filePath;
        data.Add(SE, 0);
        data.Add(BGM, 0);
    }

    public void LoadSettingFile()
    {
        //TODO:復号処理
        using (TextReader tr = new StreamReader(filePath, Encoding.UTF8))
            data = JsonMapper.ToObject<Dictionary<string, double>>(tr);
    }

    /// <summary>
    /// 設定を保存する
    /// </summary>
    public void SaveSettingFile()
    {
        //TODO:暗号化処理
        using (TextWriter tw = new StreamWriter(filePath, false, Encoding.UTF8))
            tw.Write(JsonMapper.ToJson(data));
    }
}