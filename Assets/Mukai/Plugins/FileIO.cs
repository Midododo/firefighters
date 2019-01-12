using UnityEngine;
using System.Collections;
using System.IO;
using System;

/// <summary>
/// Unityでテキストファイルを読み書きする.
/// このファイルは静的クラスのため「Standard Assets」または「Plugins」フォルダーに配置する.
/// このファイルは静的クラスのため特定のオブジェクトにアタッチしてはいけない.
/// </summary>
public static class FileIO
{
    /// <summary>
    /// ファイルを読み込む.ファイルが存在しない場合は作成する.
    /// </summary>
    private static string FileRead(string dataPath)
    {
        string readData = "";
        Debug.Log("ロードします");
        try
        {
            using (StreamReader sr = new StreamReader(dataPath))
            {
                readData = sr.ReadToEnd();
            }
            Debug.Log(dataPath + "から" + readData + "を読み込みました.");
            return readData;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        Debug.Log("空っぽにしてやったぜ!!");
        return null;
    }

    /// <summary>
    /// ファイルを書き込む.
    /// </summary>
    /// <param name="writeData">書き込むデータ.</param>
    private static void FileWrite(string dataPath, string writeData)
    {
        Debug.Log("セーブします.");
        try
        {
            using (StreamWriter sw = new StreamWriter(dataPath))
            {
                sw.Write(writeData);
            }
            Debug.Log(dataPath + "に" + writeData + "を書き込みました.");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// ファイルを初期化する.
    /// </summary>
    public static void InitSaveData(string dataPath)
    {
        Debug.Log("セーブデータを初期化します.");
        FileWrite(dataPath, "");
    }

    /// <summary>
    /// 指定したファイルを読み込む.
    /// </summary>
    /// <param name="dataPath">ファイルパス.</param>
    public static string Read(string dataPath)
    {
        return FileRead(dataPath);
    }

    /// <summary>
    /// 指定したファイルに書き込む.
    /// </summary>
    /// <param name="dataPath">ファイルパス.</param>
    /// <param name="writeData">書き込むデータ（文字列）.</param>
    public static void Write(string dataPath, string writeData)
    {
        FileWrite(dataPath, writeData);
    }
}