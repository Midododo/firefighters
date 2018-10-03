using UnityEngine;
using UnityEngine.UI;

namespace Marvest.Parties
{
    public class UiPlayerPartyHitPointGauge : MonoBehaviour
    {
        public Image HitpointGauge;

        private RectTransform gaugeRectTransform;
        private PlayerParty playerParty;

        private Vector2 startOffsetMax;
        private Vector2 endOffsetMax;
        private int maxHitPoint;

        public void Initialize(PlayerParty party)
        {
            playerParty = party;
            gaugeRectTransform = HitpointGauge.GetComponent<RectTransform>();
            var frameRectTransform = HitpointGauge.GetComponentInParent<RectTransform>();
            startOffsetMax = gaugeRectTransform.offsetMax;
            endOffsetMax = new Vector2(-frameRectTransform.rect.width - startOffsetMax.x, startOffsetMax.y);
            maxHitPoint = party.HitPoint;
        }

        public void Refresh()
        {
            gaugeRectTransform.offsetMax = Vector2.Lerp(endOffsetMax, startOffsetMax, playerParty.HitPoint / (float) maxHitPoint);
        }
    }
}