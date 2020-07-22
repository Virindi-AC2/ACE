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
	public static class MUSettings
	{
		static MUSettings()
		{
			ReadSettings();
		}

		public static void ReadSettings()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load("musettings.xml");
			XmlElement docelem = doc.DocumentElement;
			foreach (XmlElement e in docelem.ChildNodes)
			{
				if (e.Name.ToUpperInvariant() != "PARAM") continue;
				string group = e.Attributes["group"].Value.ToUpperInvariant();
				switch (group)
				{
					//case "TESTCLASS": ReadSetting_testclass(e); break;
					case "MUCOMPONENTBURN": MUComponentBurn.ReadSetting(e); break;
					case "MUMANACSPEED": MUManaCSpeed.ReadSetting(e); break;
				}
			}
		}
	}
}