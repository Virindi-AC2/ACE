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
		public static void Scale(ACE.Server.WorldObjects.Creature c)
		{
			if (!c.IsMonster) return;

			c.Health.Ranks += (uint)((float)c.Health.MaxValue * 0.2f);
			c.Health.Current = c.Health.MaxValue;
			//c.Stamina.MaxValue = (int)((float)c.Stamina.MaxValue * 1.2f);
			//c.Stamina.Current = c.Stamina.MaxValue;
			//c.Mana.MaxValue = (int)((float)c.Mana.MaxValue * 1.2f);
			//c.Mana.Current = c.Mana.MaxValue;
		}
	}
}