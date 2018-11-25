using MBLDefine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;


/// <summary>
/// サウンドを管理する
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private const float MAX_DECIBEL = 0f;
    private const float MIN_DECIBEL = -80f;

    [SerializeField]
    private AudioMixer mixer = null;

    [SerializeField]
    private List<string> audioNames = new List<string>();

    [SerializeField]
    private List<AudioSource> audioDatas = new List<AudioSource>();

    private SoundSetting soundSetting = new SoundSetting(ExternalFilePath.SOUND_SETTING);

    /// <summary>
    /// SEのボリューム
    /// </summary>
    public float SE
    {
        get
        { return soundSetting.SEVolume; }
        set
        {
            mixer.SetFloat(AudioMixerParamName.SEVolume.String(), GetDecibelConversion(value));
            soundSetting.SEVolume = value;
        }
    }

    /// <summary>
    /// BGMのボリューム
    /// </summary>
    public float BGM
    {
        get { return soundSetting.BGMVolume; }
        set
        {
            mixer.SetFloat(AudioMixerParamName.BGMVolume.String(), GetDecibelConversion(value));
            soundSetting.BGMVolume = value;
        }
    }

    private void Awake()
    {
        try
        {
            LoadSettingFile();
        }
        catch (IOException e)
        {
            Debug.LogWarning(e.Message);
            soundSetting.SEVolume = 1f;
            soundSetting.BGMVolume = 1f;
        }
    }

    private void Start()
    {
        SE = soundSetting.SEVolume;
        BGM = soundSetting.BGMVolume;
    }

    /// <summary>
    /// サウンドを再生する
    /// </summary>
    /// <param name="key">サウンドのキー</param>
    public void Play(AudioKey key)
    {
        audioDatas[(int)key].Play();
    }

    /// <summary>
    /// サウンドを再生する
    /// </summary>
    /// <param name="key">サウンドのキー</param>
    public void Play(string key)
    {
        Play(ConvertAudioKey(key));
    }

    /// <summary>
    /// サウンドを再生する
    /// 同一キーのサウンドを同時に複数鳴らすことが可能
    /// </summary>
    /// <param name="key">サウンドのキー</param>
    public void PlayOneShot(AudioKey key)
    {
        audioDatas[(int)key].PlayOneShot(audioDatas[(int)key].clip);
    }

    /// <summary>
    /// サウンドを再生する
    /// 同一キーのサウンドを同時に複数鳴らすことが可能
    /// </summary>
    /// <param name="key">サウンドのキー</param>
    public void PlayOneShot(string key)
    {
        PlayOneShot(ConvertAudioKey(key));
    }

    /// <summary>
    /// 再生を停止する
    /// </summary>
    /// <param name="key">サウンドのキー</param>
    public void Stop(AudioKey key)
    {
        audioDatas[(int)key].Stop();
    }

    /// <summary>
    /// 再生を停止する
    /// </summary>
    /// <param name="key">サウンドのキー</param>
    public void Stop(string key)
    {
        Stop(ConvertAudioKey(key));
    }

    /// サウンドをミュートする
    /// <param name="key">サウンドのキー</param>
    public void Mute(AudioKey key)
    {
        audioDatas[(int)key].mute = true;
    }
    /// サウンドをミュートする
    /// <param name="key">サウンドのキー</param>
    public void Mute(string key)
    {
        Mute(ConvertAudioKey(key));
    }

    /// サウンドをミュート解除する
    public void UnMute(AudioKey key)
    {
        audioDatas[(int)key].mute = false;
    }
    /// サウンドをミュート解除する
    /// <param name="key">サウンドのキー</param>
    public void UnMute(string key)
    {
        UnMute(ConvertAudioKey(key));
    }


    /// <summary>
    /// 設定ファイルを読み込む
    /// </summary>
    public void LoadSettingFile()
    {
        soundSetting.LoadSettingFile();
    }

    /// <summary>
    /// 設定ファイルを保存する
    /// </summary>
    public void SaveSettingFile()
    {
        soundSetting.SaveSettingFile();
    }

    /// <summary>
    /// 文字列をAudioKeyに変換する
    /// </summary>
    /// <param name="key">AudioKeyに変換する文字列</param>
    /// <returns>該当するAudioKey</returns>
    private AudioKey ConvertAudioKey(string key)
    {
        var keyIndex = audioNames.IndexOf(key);
        if (keyIndex < 0)
            throw new Exception(key + "のAudioKeyへの変換に失敗しました");
        return (AudioKey)keyIndex;
    }

    /// <summary>
    /// デシベル変換
    /// </summary>
    private float DecibelConversion(float volume)
    {
        return 20f * Mathf.Log10(volume);
    }

    /// <summary>
    /// デシベル変換後の値を得る
    /// 変換後の値は適切な値を取るようClampされる
    /// </summary>
    private float GetDecibelConversion(float value)
    {
        return Mathf.Clamp(DecibelConversion(value), MIN_DECIBEL, MAX_DECIBEL);
    }



    #region CustomInspector

#if UNITY_EDITOR

    [CustomEditor(typeof(SoundManager))]
    private class SoundManagetInspector : Editor
    {
        private SoundManager soundManager;
        private int selectAudioSourceIndex;
        private string newAudioDataName = string.Empty;
        private List<bool> foldSoundDatas = new List<bool>();
        private string outputScriptPath = string.Empty;
        private const string SCRIPT_FILE_NAME = "AudioSourceKeyMap.cs";

        private const string SCRIPT_FILE_CONTENT =
        @"
/*手動で変更しないでください*/
public enum AudioKey
{
[CONTENT]
}";

        public override void OnInspectorGUI()
        {
            soundManager = target as SoundManager;
            soundManager.mixer = EditorGUILayout.ObjectField("AudioMixer", soundManager.mixer, typeof(AudioMixer), false) as AudioMixer;

            EditorGUILayout.Separator();

            #region AudioData

            for (int i = 0; i < soundManager.audioDatas.Count; ++i)
            {
                if (foldSoundDatas.Count < i + 1)
                    foldSoundDatas.Add(false);

                if (foldSoundDatas[i] = EditorGUILayout.Foldout(foldSoundDatas[i], soundManager.audioNames[i]))
                {
                    EditorGUILayout.BeginVertical("box");
                    EditorGUILayout.BeginHorizontal();
                    //再生ボタン表示
                    if (soundManager.audioDatas[i] != null && soundManager.audioDatas[i].isPlaying)
                    {
                        if (GUILayout.Button("■"))
                        {
                            soundManager.audioDatas[i].Stop();
                        }
                    }
                    else if (GUILayout.Button("▶"))
                    {
                        soundManager.audioDatas[i].Play();
                    }
                    EditorGUILayout.EndHorizontal();
                    soundManager.audioDatas[i].clip = EditorGUILayout.ObjectField("AudioClip", soundManager.audioDatas[i].clip, typeof(AudioClip), false) as AudioClip;
                    soundManager.audioDatas[i].loop = EditorGUILayout.Toggle("Loop", soundManager.audioDatas[i].loop);
                    soundManager.audioDatas[i].playOnAwake = EditorGUILayout.Toggle("PlayOnAwake", soundManager.audioDatas[i].playOnAwake);
                    soundManager.audioDatas[i].volume = EditorGUILayout.Slider("Volume", soundManager.audioDatas[i].volume, 0, 1);
                    soundManager.audioDatas[i].pitch = EditorGUILayout.Slider("Pitch", soundManager.audioDatas[i].pitch, -3, 3);
                    soundManager.audioDatas[i].bypassEffects = EditorGUILayout.Toggle("BypassEffects", soundManager.audioDatas[i].bypassEffects);
                    soundManager.audioDatas[i].bypassListenerEffects = EditorGUILayout.Toggle("BypassListenerEffects", soundManager.audioDatas[i].bypassListenerEffects);
                    soundManager.audioDatas[i].bypassReverbZones = EditorGUILayout.Toggle("BypassReverbZone", soundManager.audioDatas[i].bypassReverbZones);
                    soundManager.audioDatas[i].reverbZoneMix = EditorGUILayout.Slider("ReverbZoneMix", soundManager.audioDatas[i].reverbZoneMix, 0, 1.1f);
                    soundManager.audioDatas[i].spatialBlend = EditorGUILayout.Slider("SpatialBlend", soundManager.audioDatas[i].spatialBlend, 0, 1);
                    soundManager.audioDatas[i].mute = EditorGUILayout.Toggle("Mute", soundManager.audioDatas[i].mute);
                    soundManager.audioDatas[i].outputAudioMixerGroup = EditorGUILayout.ObjectField("OutPut", soundManager.audioDatas[i].outputAudioMixerGroup, typeof(AudioMixerGroup), false) as AudioMixerGroup;
                    if (soundManager.audioDatas[i].outputAudioMixerGroup == null)
                    {
                        EditorGUILayout.HelpBox("Outputが設定されていないオーディオは音量の一括設定等が適用できません", MessageType.Error);
                    }
                    EditorGUILayout.EndVertical();
                }
            }

            #endregion AudioData

            #region Add/Remove Button

            EditorGUILayout.BeginHorizontal();
            newAudioDataName = EditorGUILayout.TextField(newAudioDataName);
            if (GUILayout.Button("追加"))
            {
                if (soundManager.audioNames.Contains(newAudioDataName) || string.IsNullOrEmpty(newAudioDataName))
                {
                    GUIContent content = new GUIContent("既にKeyが存在するか、Keyが空です");
                    EditorWindow.focusedWindow.ShowNotification(content);
                }
                else
                {
                    soundManager.audioNames.Add(newAudioDataName);
                    var newAudioSource = soundManager.gameObject.AddComponent<AudioSource>();
                    newAudioSource.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
                    soundManager.audioDatas.Add(newAudioSource);
                }
                newAudioDataName = string.Empty;
            }
            EditorGUILayout.Space();
            selectAudioSourceIndex = EditorGUILayout.Popup(selectAudioSourceIndex, soundManager.audioNames.ToArray());
            if (GUILayout.Button("削除"))
            {
                if (soundManager.audioNames.Count > selectAudioSourceIndex)
                {
                    DestroyImmediate(soundManager.audioDatas[selectAudioSourceIndex]);
                    soundManager.audioNames.RemoveAt(selectAudioSourceIndex);
                    soundManager.audioDatas.RemoveAt(selectAudioSourceIndex);
                    foldSoundDatas.RemoveAt(selectAudioSourceIndex);
                }
            }
            EditorGUILayout.EndHorizontal();

            DebugEditorScript();

            #endregion Add/Remove Button

            EditorGUILayout.Space();

            #region CreateScriptFile

            if (GUILayout.Button("適用"))
            {
                outputScriptPath = Directory.GetFiles(Directory.GetCurrentDirectory(), "SoundManager.cs", SearchOption.AllDirectories).FirstOrDefault();
                if (string.IsNullOrEmpty(outputScriptPath))
                    outputScriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets");

                var fullPath = Path.Combine(Directory.GetParent(outputScriptPath).FullName, SCRIPT_FILE_NAME);
                //Assets以下でなければエラーポップアップ
                if (!fullPath.Contains(Path.Combine(Directory.GetCurrentDirectory(), "Assets")))
                    Debug.LogError("スクリプトはAssets以下に配置してください : " + fullPath);
                //ファイルが保存できる場合
                else
                    using (var writer = new StreamWriter(fullPath, false))
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var key in soundManager.audioNames)
                            sb.Append("\t" + key + ",\r\n");
                        var innerContent = sb.ToString();
                        var content = SCRIPT_FILE_CONTENT.Replace("[CONTENT]", innerContent);
                        writer.Write(content);
                        AssetDatabase.Refresh();
                        Debug.Log(string.Format("保存しました : {0}", fullPath));
                    }
            }

            #endregion CreateScriptFile

            EditorUtility.SetDirty(soundManager);
        }

        /// <summary>
        /// エディタスクリプトデバッグ用機能
        /// </summary>
        [System.Diagnostics.Conditional("DEBUG")]
        private void DebugEditorScript()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("AudioSoueceの表示"))
            {
                var allAudioSource = soundManager.GetComponents<AudioSource>();
                foreach (var data in allAudioSource)
                    data.hideFlags = HideFlags.None | HideFlags.NotEditable;
            }
            if (GUILayout.Button("AudioSourceの非表示"))
            {
                var allAudioSource = soundManager.GetComponents<AudioSource>();
                foreach (var data in allAudioSource)
                    data.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.NotEditable;
            }
            if (GUILayout.Button("AudioSourceのクリア"))
            {
                var allAudioSource = soundManager.GetComponents<AudioSource>();
                //非表示中は削除できない
                if (allAudioSource.Any(s => (s.hideFlags & HideFlags.HideInInspector) != HideFlags.HideInInspector))
                {
                    GUIContent content = new GUIContent("AudioSourceを非表示にしてから実行してください");
                    EditorWindow.focusedWindow.ShowNotification(content);
                }
                else
                {
                    for (int i = 0; i < allAudioSource.Count(); ++i)
                        DestroyImmediate(allAudioSource[i]);
                    soundManager.audioDatas.Clear();
                    soundManager.audioNames.Clear();
                    foldSoundDatas.Clear();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }

#endif

    #endregion CustomInspector
}
