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
	static class MUSalvageValue
	{
		public static void ModifySalvageOuputValue(ref int v)
		{
			v = (int)((float)v * 0.1f);
		}
	}
}