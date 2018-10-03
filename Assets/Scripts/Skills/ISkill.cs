using Marvest.Units;
using UnityEngine;

namespace Marvest.Skills
{
    public interface ISkill
    {
        // スキル使用者
        ISkillableUnit Subject { get; set; }

        // スキル発動
        void Invoke();

        // スキル終了判定
        bool IsComplete();

        // スキルの後始末
        void SelfDestroy();
    }
}