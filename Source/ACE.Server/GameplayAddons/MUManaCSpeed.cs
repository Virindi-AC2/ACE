using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

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
		static float S_MAXIMUMSKILL = 1f;
		static float S_MAXIMUMREDUCTION = 1f;

		public static void ReadSetting(XmlElement e)
		{
			string val = e.Attributes["prop"].Value.ToUpperInvariant();
			switch (val)
			{
				case "S_MAXIMUMSKILL": S_MAXIMUMSKILL = float.Parse(e.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture); break;
				case "S_MAXIMUMREDUCTION": S_MAXIMUMREDUCTION = float.Parse(e.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture); break;
			}
		}

		public static float GetPlayerSpellWindupSpeedMultiplier(Player player, Spell spell, bool showmessage = true)
		{
			var creatureSkill = player.GetCreatureSkill(Skill.ManaConversion, false);
			if (creatureSkill == null) return 1.0f;
			if (creatureSkill.AdvancementClass != SkillAdvancementClass.Specialized) return 1.0f;

			//Is the spell eligible?
			if (spell.IsHarmful) return 1.0f;


			float sk = (float)creatureSkill.Current / S_MAXIMUMSKILL;
			if (sk > 1.0f) sk = 1.0f;
			if (sk < 0.0f) sk = 0.0f;
			float reduction = S_MAXIMUMREDUCTION * sk;

			if (showmessage)
			{
				string reductionmsgamount = ((int)Math.Round(100f * reduction)).ToString();
				player.Session.Network.EnqueueSend(new GameMessageSystemChat("Your specialized Mana Conversion skill reduced casting time by " + reductionmsgamount + "%.", ChatMessageType.Spellcasting));
			}

			return 1f + reduction;
		}
	}
}