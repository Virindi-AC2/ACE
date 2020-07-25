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
	//ACE.Server.GameplayAddons.MUTinkeringChanges
	static class MUTinkeringChanges
	{
		public static float S_TINKERFIXEDBURDENINCREASE = 0f;
		public static float S_TINKERMULTVALUEINCREASE = 1.0f;
		public static float S_TINKERFIXEDVALUEINCREASE = 0f;

		public static void ReadSetting(XmlElement e)
		{
			string val = e.Attributes["prop"].Value.ToUpperInvariant();
			switch (val)
			{
				case "S_TINKERFIXEDBURDENINCREASE":
					{
						S_TINKERFIXEDBURDENINCREASE = float.Parse(e.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture);
						Console.WriteLine("MUMonsterDifficulty S_TINKERFIXEDBURDENINCREASE = " + S_TINKERFIXEDBURDENINCREASE.ToString());
					} break;
				case "S_TINKERMULTVALUEINCREASE":
					{
						S_TINKERMULTVALUEINCREASE = float.Parse(e.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture);
						Console.WriteLine("MUMonsterDifficulty S_TINKERMULTVALUEINCREASE = " + S_TINKERMULTVALUEINCREASE.ToString());
					} break;
				case "S_TINKERFIXEDVALUEINCREASE":
					{
						S_TINKERFIXEDVALUEINCREASE = float.Parse(e.Attributes["value"].Value, System.Globalization.CultureInfo.InvariantCulture);
						Console.WriteLine("MUMonsterDifficulty S_TINKERFIXEDVALUEINCREASE = " + S_TINKERFIXEDVALUEINCREASE.ToString());
					} break;
			}
		}

		public static bool BeforeTinkering(MaterialType mat, Player player, WorldObject tool, WorldObject target, bool incItemTinkered = true)
		{
			return true;
		}

		public static void AfterTinkering(MaterialType mat, Player player, WorldObject tool, WorldObject target, bool incItemTinkered = true)
		{
			if (
				(mat != MaterialType.Linen)
				&& (mat != MaterialType.Ivory)
				&& (mat != MaterialType.Leather)
				&& (mat != MaterialType.Sandstone)
				)
			{
				target.EncumbranceVal = (int)Math.Round((target.EncumbranceVal ?? 1) + S_TINKERFIXEDBURDENINCREASE);
			}

			if (
				(mat != MaterialType.Pine)
				&& (mat != MaterialType.Gold)
				&& (mat != MaterialType.Ivory)
				&& (mat != MaterialType.Leather)
				&& (mat != MaterialType.Sandstone)
				)
			{
				target.Value = (int)Math.Round((target.Value ?? 1) * S_TINKERMULTVALUEINCREASE);
				target.Value = (int)Math.Round((target.Value ?? 1) + S_TINKERFIXEDVALUEINCREASE);
			}

			return;
		}
	}
}