using System;
using System.Collections.Generic;
using System.Linq;

using ACE.Common;
using ACE.DatLoader;
using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Managers;
using ACE.Server.Network.GameEvent.Events;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.Physics;
using ACE.Server.WorldObjects;

namespace ACE.Server.GameplayAddons
{
	public static class MUComponentBurn
	{
		public static float GetPlayerBurnRateMultiplier(Player player, Skill spellskill)
		{
			var creatureSkill = player.GetCreatureSkill(Skill.SpellEfficiency, false);
			if (creatureSkill == null) return 1.0f;
			if (creatureSkill.AdvancementClass < SkillAdvancementClass.Trained) return 1.0f;

			float maximumreduction = 0f;

			if (creatureSkill.AdvancementClass == SkillAdvancementClass.Trained)
				maximumreduction = 0.2f; //20% reduction
			else if (creatureSkill.AdvancementClass == SkillAdvancementClass.Specialized)
				maximumreduction = 0.7f; //70% reduction

			float sk = (float)playerSkill.Current / 500f;
			if (sk > 1.0f) sk = 1.0f;
			float reduction = maximumreduction * sk;

			return 1f - reduction;
		}
	}
}