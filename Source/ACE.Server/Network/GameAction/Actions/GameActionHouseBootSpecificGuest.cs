using System;

namespace ACE.Server.Network.GameAction.Actions
{
    /// <summary>
    /// Boots a specific player from your house, /house boot
    /// </summary>
    public static class GameActionHouseBootSpecificGuest
    {
        [GameAction(GameActionType.BootSpecificHouseGuest)]
        public static void Handle(ClientMessage message, Session session)
        {
            //Console.WriteLine("Received 0x24A - BootSpecificHouseGuest");

            var guestName = message.Payload.ReadString();   // player name to boot from your house
        }
    }
}