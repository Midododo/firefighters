using Marvest.SceneGlobalVariables.Stage;
using Marvest.Units.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Marvest.Units
{
    public class UiPlayerUnitSkillButton : MonoBehaviour
    {
        public Image UnitImage;

		public Text WaitSkilTimeText;
		public int WaitSkilTime;
		private int WaitTime;
		private bool Play;
		public int SkilNumber;

        public void AttachUnit(PlayerUnit unit)
        {
            var spriteRenderer = unit.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                UnitImage.sprite = spriteRenderer.sprite;
                UnitImage.color = spriteRenderer.color;
				WaitSkilTimeText.text = WaitSkilTime.ToString();
				WaitTime = WaitSkilTime;
				Play = false;
            }

            UnitImage.enabled = UnitImage.sprite != null;
        }

		public void WaitSkilTimeDown(PlayerUnit unit)
		{
			WaitSkilTime--;
			WaitSkilTimeText.text = WaitSkilTime.ToString();
			if (WaitSkilTime <= 0) {
				WaitSkilTime = 0;
				WaitSkilTimeText.text = "OK";
			}
		}

		public void PlaySkill(PlayerUnit unit)
		{
			if (Play == true) {
				if (SkilNumber == 1) {
					unit.AttackPower *= 2;
				}
				else if (SkilNumber == 2) {
					unit.AttackPower *= 2;
				}
				else if (SkilNumber == 3) {
					StageSceneGlobalVariables.Instance.PlayerParty.HitPoint += 5;
					StageSceneGlobalVariables.Instance.PlayerParty.HitpointGauge.Refresh ();
				}
				else if (SkilNumber == 4) {
					unit.AttackPower *= 2;
					unit.bairitu = 2.0f;
				}

				WaitSkilTime = WaitTime;
				WaitSkilTimeText.text = WaitSkilTime.ToString();
				Play = false;
			}
		}

        public void OnClickSkillButton()
        {
			if (WaitSkilTime == 0) {
				Debug.Log ("スキル発動ボタンが押されました");
				Play = true;
			}
        }
    }
}