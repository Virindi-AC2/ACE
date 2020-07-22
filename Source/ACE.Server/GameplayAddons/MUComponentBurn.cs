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
	public static class MUComponentBurn
	{
		static float S_MAXIMUMREDUCTIONTRAINED = 1f;
		static float S_MAXIMUMREDUCTIONSPEC = 1f;
		static float S_MAXIMUMSKILL = 1f;

		public static void ReadSetting(XmlElement e)
		{
			string val = e.Attributes["prop"].Value.ToUpperInvariant();
			switch (val)
			{
				case "S_MAXIMUMSKILL":
					{
						S_MAXIMUMSKILL = float.Parse(e.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture);
						Console.WriteLine("MUComponentBurn S_MAXIMUMSKILL = " + S_MAXIMUMSKILL.ToString());
					} break;
				case "S_MAXIMUMREDUCTIONTRAINED":
					{
						S_MAXIMUMREDUCTIONTRAINED = float.Parse(e.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture);
						Console.WriteLine("MUComponentBurn S_MAXIMUMREDUCTIONTRAINED = " + S_MAXIMUMREDUCTIONTRAINED.ToString());
					} break;
				case "S_MAXIMUMREDUCTIONSPEC":
					{
						S_MAXIMUMREDUCTIONSPEC = float.Parse(e.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture);
						Console.WriteLine("MUComponentBurn S_MAXIMUMREDUCTIONSPEC = " + S_MAXIMUMREDUCTIONSPEC.ToString());
					} break;
			}
		}

		public static float GetPlayerBurnRateMultiplier(Player player, Skill spellskill)
		{
			MUSettings.Init();

			var creatureSkill = player.GetCreatureSkill(Skill.SpellEfficiency, false);
			if (creatureSkill == null) return 1.0f;
			if (creatureSkill.AdvancementClass < SkillAdvancementClass.Trained) return 1.0f;

			float maximumreduction = 0f;

			if (creatureSkill.AdvancementClass == SkillAdvancementClass.Trained)
				maximumreduction = S_MAXIMUMREDUCTIONTRAINED;
			else if (creatureSkill.AdvancementClass == SkillAdvancementClass.Specialized)
				maximumreduction = S_MAXIMUMREDUCTIONSPEC;

			float sk = (float)creatureSkill.Current / S_MAXIMUMSKILL;
			if (sk > 1.0f) sk = 1.0f;
			if (sk < 0.0f) sk = 0.0f;
			float reduction = maximumreduction * sk;

			string reductionmsgamount = ((int)Math.Round(100f * reduction)).ToString();
			player.Session.Network.EnqueueSend(new GameMessageSystemChat("Your Spell Efficiency skill reduced component burn by " + reductionmsgamount + "%.", ChatMessageType.Spellcasting));

			return 1f - reduction;
		}
	}
}