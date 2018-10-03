using System;
using UnityEngine;

namespace Marvest.TransitionEffects
{
    public class TransitionEffectSpriteMask : MonoBehaviour
    {
        private TransitionType transitionType;
        private SpriteMask spriteMask;

        public void SetTransitionType(TransitionType transitionType)
        {
            this.transitionType = transitionType;
            Sprite sprite =
                Instantiate(Resources.Load("Transitions/" + GetSpriteMaskImagePath(), typeof(Sprite))) as Sprite;
            GetSpriteMask().sprite = sprite;
        }

        public void SetAlphaCutOff(float alphaCutOff)
        {
            GetSpriteMask().alphaCutoff = Mathf.Clamp01(alphaCutOff);
        }

        private string GetSpriteMaskImagePath()
        {
            switch (transitionType)
            {
                case TransitionType.Circle:
                    return "SpriteMaskCircle";
                case TransitionType.Line:
                    return "SpriteMaskLine";
                default:
                    return "";
            }
        }

        private SpriteMask GetSpriteMask()
        {
            return spriteMask ?? (spriteMask = gameObject.GetComponent<SpriteMask>());
        }
    }
}