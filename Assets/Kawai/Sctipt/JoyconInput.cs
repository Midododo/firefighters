using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JoyconInput : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    // ジョイコンの入れ物を準備
    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    public int m_HighFrec = 320;
    public int m_LowFrec = 60;


    //===============================================================
    // 初期化
    private void Start()
    {
        m_joycons = JoyconManager.Instance.JoyconList;

        if (m_joycons == null || m_joycons.Count <= 0) return;


        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);
    }

    private void Update()
    {

    }

    //===============================================================
    // エラー表示
    private void OnGUI()
    {
        var style = GUI.skin.GetStyle("label");
        style.fontSize = 24;

        if (m_joycons == null || m_joycons.Count <= 0)
        {
            GUILayout.Label("Joy-Con が接続されていません");
            return;
        }

        if (!m_joycons.Any(c => c.isLeft))
        {
            GUILayout.Label("Joy-Con (L) が接続されていません");
            return;
        }

        if (!m_joycons.Any(c => !c.isLeft))
        {
            GUILayout.Label("Joy-Con (R) が接続されていません");
            return;
        }
    }

    //===============================================================
    // ボタンが押されたかを確認
    // Joycon.Button reqButton : 確認したいボタン(JoyconManagerのenum)
    // int           reqIdx    : 確認したいコントローラーのインデックス番号(プレイヤー番号)
    // bool          isLeft    : 確認したいコントローラーが左であるかの確認
    // return bool
    //===============================================================
    public bool GetTrigger(Joycon.Button reqButton, int reqIdx, bool isLeft)
    {   // 使用してるコントローラがあるか
        if (m_joycons == null || m_joycons.Count <= 0) return false;

        // ジョイコンのリストを確認するロープ
        foreach (var joycon in m_joycons)
        {   // インデックス番号、また確認したい側であるかを確認
            if (joycon.Id != reqIdx || joycon.isLeft != isLeft)
            { continue; }

            // ボタンのリストの中を確認する
            foreach (var button in m_buttons)
            {   // 要求しているボタンと一致しているかを確認
                if (button != reqButton)
                { continue; }

                // ボタンが押されたかを確認
                if (joycon.GetButtonDown(button))
                { return true; }
                else
                { return false; }
            }
        }
        return false;
    }

    //===============================================================
    // ボタンが押されている状態かを確認
    // Joycon.Button reqButton : 確認したいボタン(JoyconManagerのenum)
    // int           reqIdx    : 確認したいコントローラーのインデックス番号(プレイヤー番号)
    // bool          isLeft    : 確認したいコントローラーが左であるかの確認
    // return bool
    //===============================================================
    public bool GetPress(Joycon.Button reqButton, int reqIdx, bool isLeft)
    {   // 使用してるコントローラがあるか
        if (m_joycons == null || m_joycons.Count <= 0) return false;

        // ジョイコンのリストを確認するロープ
        foreach (var joycon in m_joycons)
        {   // インデックス番号、また確認したい側であるかを確認
            if (joycon.Id != reqIdx || joycon.isLeft != isLeft)
            { continue; }

            // ボタンのリストの中を確認する
            foreach (var button in m_buttons)
            {   // 要求しているボタンと一致しているかを確認
                if (button != reqButton)
                { continue; }

                // ボタンが押されている状態化を確認
                if (joycon.GetButton(button))
                {
                    return true;
                }
            }
        }
        return false;
    }

    //===============================================================
    // 押されていたボタンがリリースされたかを確認
    // Joycon.Button reqButton : 確認したいボタン(JoyconManagerのenum)
    // int           reqIdx    : 確認したいコントローラーのインデックス番号(プレイヤー番号)
    // bool          isLeft    : 確認したいコントローラーが左であるかの確認
    // return bool
    //===============================================================
    public bool GetRelease(Joycon.Button reqButton, int reqIdx, bool isLeft)
    {   // 使用してるコントローラがあるか
        if (m_joycons == null || m_joycons.Count <= 0) return false;

        // ジョイコンのリストを確認するロープ
        foreach (var joycon in m_joycons)
        {   // インデックス番号、また確認したい側であるかを確認
            if (joycon.Id != reqIdx || joycon.isLeft != isLeft)
            { continue; }

            // ボタンのリストの中を確認する
            foreach (var button in m_buttons)
            {   // 要求しているボタンと一致しているかを確認
                if (button != reqButton)
                { continue; }

                // ボタンがリリースされたかを確認
                if (joycon.GetButtonUp(button))
                {
                    return true;
                }
            }
        }
        return false;
    }

    //===============================================================
    // コントローラーのスティックを取得
    // int  reqIdx : 確認したいコントローラーのインデックス番号(プレイヤー番号)
    // bool isLeft : 確認したいコントローラーが左であるかの確認
    // return Vector2 : スティックの押されている方向と強さを取得 
    //===============================================================
    public Vector2 GetStick(int reqIdx, bool isLeft)
    {   // 使用してるコントローラがあるか
        if (m_joycons == null || m_joycons.Count <= 0) return new Vector2(0.0f, 0.0f);

        // ジョイコンのリストを確認するロープ
        foreach (var joycon in m_joycons)
        {   // インデックス番号、また確認したい側であるかを確認
            if (joycon.Id != reqIdx || joycon.isLeft != isLeft)
            { continue; }

            // floatの配列で保存ざれてるからベクトルに直す
            var stick = joycon.GetStick();
            return new Vector2(stick[0], stick[1]);
        }
        return new Vector2(0.0f, 0.0f);
    }

    //===============================================================
    // コントローラーのジャイロを取得
    // int  reqIdx : 確認したいコントローラーのインデックス番号(プレイヤー番号)
    // bool isLeft : 確認したいコントローラーが左であるかの確認
    // return Vector3 : ジャイロ
    //===============================================================
    public Vector3 GetGyro(int reqIdx, bool isLeft)
    {   // 使用してるコントローラがあるか
        if (m_joycons == null || m_joycons.Count <= 0) return new Vector3(0.0f, 0.0f, 0.0f);

        // ジョイコンのリストを確認するロープ
        foreach (var joycon in m_joycons)
        {   // インデックス番号、また確認したい側であるかを確認
            if (joycon.Id != reqIdx || joycon.isLeft != isLeft)
            { continue; }

            // おっぱい
            return joycon.GetGyro();
        }
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    //===============================================================
    // コントローラーの加速度を取得
    // int  reqIdx : 確認したいコントローラーのインデックス番号(プレイヤー番号)
    // bool isLeft : 確認したいコントローラーが左であるかの確認
    // return Vector3 : 加速度
    //===============================================================
    public Vector3 GetAccel(int reqIdx, bool isLeft)
    {   // 使用してるコントローラがあるか
        if (m_joycons == null || m_joycons.Count <= 0) return new Vector3(0.0f, 0.0f, 0.0f);

        // ジョイコンのリストを確認するロープ
        foreach (var joycon in m_joycons)
        {   // インデックス番号、また確認したい側であるかを確認
            if (joycon.Id != reqIdx || joycon.isLeft != isLeft)
            { continue; }

            // イニシャルD
            return joycon.GetAccel();
        }
        return new Vector3(0.0f, 0.0f, 0.0f);
    }

    //===============================================================
    // コントローラーの向きを取得
    // int  reqIdx : 確認したいコントローラーのインデックス番号(プレイヤー番号)
    // bool isLeft : 確認したいコントローラーが左であるかの確認
    // return Quaternion : コントローラが向いている方向
    //===============================================================
    public Quaternion GetOrientation(int reqIdx, bool isLeft)
    {   // 使用してるコントローラがあるか
        if (m_joycons == null || m_joycons.Count <= 0) return new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);

        // ジョイコンのリストを確認するロープ
        foreach (var joycon in m_joycons)
        {   // インデックス番号、また確認したい側であるかを確認
            if (joycon.Id != reqIdx || joycon.isLeft != isLeft)
            { continue; }
            
            // (✿◕‿◕)
            return joycon.GetVector();
        }
        return new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
    }

    //===============================================================
    // コントローラーの振動をセット
    // int   reqIdx   : 確認したいコントローラーのインデックス番号(プレイヤー番号)
    // bool  isLeft   : 確認したいコントローラーが左であるかの確認
    // float highFreq : 早い振動の強さ
    // float lowFreq  : 思い振動の強さ
    // float amp      : よくわからない 0.0f～1.0fっぽい
    // int   time     : 振動する時間
    //===============================================================
    public void SetRumble(int reqIdx, bool isLeft, float highFreq, float lowFreq, float amp, int time = 0)
    {   // 使用してるコントローラがあるか
        if (m_joycons == null || m_joycons.Count <= 0) return;

        // ジョイコンのリストを確認するロープ
        foreach (var joycon in m_joycons)
        {   // インデックス番号、また確認したい側であるかを確認
            if (joycon.Id != reqIdx || joycon.isLeft != isLeft)
            { continue; }

            // 振動のをセット
            joycon.SetRumble(highFreq, lowFreq, amp, time);
            return;
        }
    }
}
