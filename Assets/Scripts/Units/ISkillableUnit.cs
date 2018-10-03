using UnityEngine;

namespace Marvest.Units
{
    public interface ISkillableUnit
    {
        // ベーススキル攻撃力
        int BaseSkillPower { get; }

        // ユニットのゲームオブジェクト
        GameObject UnitGameObject { get; }

        // 弾プレハブ名
        string BulletPrefabName { get; }
    }
}