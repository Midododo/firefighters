using UnityEngine;
using Marvest.Units;

namespace Marvest.Parties
{
    public class UiPlayerPartySkillButtons : MonoBehaviour
    {
        public void AttachParty(PlayerParty party)
        {
            var buttons = GetComponentsInChildren<UiPlayerUnitSkillButton>();
            var index = 0;
            foreach (var unit in party.PlayerUnits)
            {
                buttons[index].AttachUnit(unit);
                index++;
            }
        }

		public void WaitSkilTimeDown(PlayerParty party)
		{
			var buttons = GetComponentsInChildren<UiPlayerUnitSkillButton>();
			var index = 0;
			foreach (var unit in party.PlayerUnits)
			{
				buttons[index].WaitSkilTimeDown(unit);
				index++;
			}
		}

		public void PlaySkill(PlayerParty party)
		{
			var buttons = GetComponentsInChildren<UiPlayerUnitSkillButton>();
			var index = 0;
			foreach (var unit in party.PlayerUnits)
			{
				buttons[index].PlaySkill(unit);
				index++;
			}
		}
    }
}