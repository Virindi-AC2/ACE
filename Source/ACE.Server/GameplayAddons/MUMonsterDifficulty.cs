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
	static class MUMonsterDifficulty
	{
		public static float S_MONSTERHPINCREASE = 0f;
		public static float S_MONSTERMELEEDMGINCREASE = 1f;
		public static float S_MONSTERMISSILEDMGINCREASE = 1f;

		public static void ReadSetting(XmlElement e)
		{
			string val = e.Attributes["prop"].Value.ToUpperInvariant();
			switch (val)
			{
				case "S_MONSTERHPINCREASE":
					{
						S_MONSTERHPINCREASE = float.Parse(e.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture);
						Console.WriteLine("MUMonsterDifficulty S_MONSTERHPINCREASE = " + S_MONSTERHPINCREASE.ToString());
					} break;
				case "S_MONSTERMELEEDMGINCREASE":
					{
						S_MONSTERMELEEDMGINCREASE = float.Parse(e.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture);
						Console.WriteLine("MUMonsterDifficulty S_MONSTERMELEEDMGINCREASE = " + S_MONSTERMELEEDMGINCREASE.ToString());
					} break;
				case "S_MONSTERMISSILEDMGINCREASE":
					{
						S_MONSTERMISSILEDMGINCREASE = float.Parse(e.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture);
						Console.WriteLine("MUMonsterDifficulty S_MONSTERMISSILEDMGINCREASE = " + S_MONSTERMISSILEDMGINCREASE.ToString());
					} break;
			}
		}


		public static void Scale(ACE.Server.WorldObjects.Creature c)
		{
			if (!c.IsMonster) return;

			c.Health.Ranks += (uint)((float)c.Health.MaxValue * S_MONSTERHPINCREASE);
			c.Health.Current = c.Health.MaxValue;
			//c.Stamina.MaxValue = (int)((float)c.Stamina.MaxValue * 1.2f);
			//c.Stamina.Current = c.Stamina.MaxValue;
			//c.Mana.MaxValue = (int)((float)c.Mana.MaxValue * 1.2f);
			//c.Mana.Current = c.Mana.MaxValue;
		}

		public static void ScaleDamageMelee(ACE.Server.WorldObjects.Creature c, ACE.Server.Entity.DamageEvent d)
		{
			if (!c.IsMonster) return;

			if (d.HasDamage)
			{
				d.Damage *= S_MONSTERMELEEDMGINCREASE;
			}
		}

		public static void ScaleDamageRanged(ACE.Server.WorldObjects.Creature c, ACE.Server.Entity.BaseDamageMod d)
		{
			if (!c.IsMonster) return;

			d.BaseDamage.MaxDamage = (int)((float)d.BaseDamage.MaxDamage * S_MONSTERMISSILEDMGINCREASE);
		}
	}
}
