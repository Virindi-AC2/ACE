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
	public static class MUManaCSpeed
	{
		public static float GetPlayerSpellWindupSpeedMultiplier(Player player, Spell spell, bool showmessage = true)
		{
			var creatureSkill = player.GetCreatureSkill(Skill.ManaConversion, false);
			if (creatureSkill == null) return 1.0f;
			if (creatureSkill.AdvancementClass != SkillAdvancementClass.Specialized) return 1.0f;

			float maximumreduction = 0.8f;

			float sk = (float)creatureSkill.Current / 500f;
			if (sk > 1.0f) sk = 1.0f;
			float reduction = maximumreduction * sk;

			string reductionmsgamount = ((int)Math.Round(100f * reduction)).ToString();
			player.Session.Network.EnqueueSend(new GameMessageSystemChat("Your specialized Mana Conversion skill reduced casting time by " + reductionmsgamount + "%.", ChatMessageType.Spellcasting));

			return 1f - reduction;
		}
	}
}