using SSMPUtils.Client;
using SSMPUtils.Server.Packets;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SSMPUtils.Utils.Tests
{
    internal class WarpTest : BaseTest
    {
        public override KeyCode KeyCode => KeyCode.Alpha1;

        public override void Execute()
        {
            var packet = new WarpPacket
            {
                Scene = "Bonetown",
                Position = new Vector2(100, 15),
            };

            PacketReceiver.OnHuddle(packet);
        }
    }
}
