using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("UnitTest")]

/// <summary>
/// MBLで定義する定数を扱う
/// </summary>
namespace MBLDefine
{
    /// <summary>
    /// 外部ファイルへの参照に必要なパス群
    /// </summary>
    internal struct ExternalFilePath
    {
        internal const string KEY_CONFIG = "keyconf.dat";
        internal const string SOUND_SETTING = "soundset.dat";

        internal const string EVENT = "eventdat.xml";
        internal const string EVENT_SCHEMA = "eventdat.xsd";
        //TODO:セーブデータ等追加
    }

    /// <summary>
    /// Resoures内のリソースへのパス
    /// </summary>
    internal struct ResourcePath
    {
        internal const string GLOBAL_SCRIPTS = "Prefab/System/GlobalScripts";
        internal const string MENU = "Prefab/UI/MenuCanvas";
        internal const string CHAT = "Prefab/UI/ChatCanvas";
    }

    /// <summary>
    /// 入力値の基底クラス
    /// </summary>
    public class InputValue : MonoBehaviour
    {
        public readonly string String;

        protected InputValue(string name)
        {
            String = name;
        }
    }

    /// <summary>
    /// 使用するキーを表すクラス
    /// </summary>
    public sealed class Key : InputValue
    {
        public readonly List<KeyCode> DefaultKeyCode;
        public readonly static List<Key> AllKeyData = new List<Key>();

        private Key(string keyName, List<KeyCode> defaultKeyCode)
          : base(keyName)
        {
            DefaultKeyCode = defaultKeyCode;
            AllKeyData.Add(this);
        }

        public override string ToString()
        {
            return String;
        }

        public static readonly Key Action = new Key("Action", new List<KeyCode> { KeyCode.Z });
        public static readonly Key Jump = new Key("Jump", new List<KeyCode> { KeyCode.Space });
        public static readonly Key Balloon = new Key("Balloon", new List<KeyCode> { KeyCode.X });
        public static readonly Key Squat = new Key("Squat", new List<KeyCode> { KeyCode.C });
        public static readonly Key Menu = new Key("Menu", new List<KeyCode> { KeyCode.Escape });
    }

    /// <summary>
    /// 使用する軸入力を表すクラス
    /// </summary>
    public sealed class Axis : InputValue
    {
        public readonly static List<Axis> AllAxisData = new List<Axis>();

        private Axis(string AxisName)
          : base(AxisName)
        {
            AllAxisData.Add(this);
        }

        public override string ToString()
        {
            return String;
        }

        public static Axis Horizontal = new Axis("Horizontal");
        public static Axis Vertical = new Axis("Vertical");
    }

    /// <summary>
    /// アニメーションパラメーターの型を表すクラス
    /// </summary>
    public enum AnimationParamType
    {
        Float,
        Int,
        Bool,
        Trigger
    }

    /// <summary>
    /// アニメーションパラメータの基底クラス
    /// </summary>
    internal class AnimationParam : MonoBehaviour
    {
        public readonly string String;
        public readonly AnimationParamType ParamType;

        protected AnimationParam(string stateName, AnimationParamType type)
        {
            String = stateName;
            ParamType = type;
        }

        public override string ToString()
        {
            return String;
        }
    }

    /// <summary>
    /// プレイヤーのアニメーションパラメーター
    /// </summary>
    internal sealed class PlayerAnimationParam : AnimationParam
    {
        private PlayerAnimationParam(string stateName, AnimationParamType type)
          : base(stateName, type)
        {
        }

        public static PlayerAnimationParam Walk = new PlayerAnimationParam("Walk", AnimationParamType.Bool);
        public static PlayerAnimationParam JumpStart = new PlayerAnimationParam("JumpStart", AnimationParamType.Trigger);
        public static PlayerAnimationParam JumpEnd = new PlayerAnimationParam("JumpEnd", AnimationParamType.Trigger);
        public static PlayerAnimationParam TakeObject = new PlayerAnimationParam("TakeObject", AnimationParamType.Bool);
        public static PlayerAnimationParam TakeBalloon = new PlayerAnimationParam("TakeBalloon", AnimationParamType.Bool);
        public static PlayerAnimationParam Squat = new PlayerAnimationParam("Squat", AnimationParamType.Bool);
        public static PlayerAnimationParam Put = new PlayerAnimationParam("Put", AnimationParamType.Trigger);
    }

    /// <summary>
    /// 入力イベントグループ名
    /// </summary>
    internal enum InputEventGroupName
    {
        /// <summary>
        /// プレイヤー操作イベント
        /// </summary>
        Player,

        /// <summary>
        /// メニュー表示・非表示イベント
        /// </summary>
        SwitchMenu,

        /// <summary>
        /// メニュー内操作イベント
        /// </summary>
        InMenu,
    }

    internal enum AudioMixerParamName
    {
        BGMVolume,
        SEVolume,
    }

    /// <summary>
    /// 列挙型の拡張メソッドを保有する
    /// </summary>
    internal static class ExtensionEnum
    {
        public static string String(this InputEventGroupName group)
        {
            return group.ToString();
        }

        public static string String(this AudioMixerParamName param)
        {
            return param.ToString();
        }
    }
}