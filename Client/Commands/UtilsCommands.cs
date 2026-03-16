using SSMP.Api.Command.Client;
using SSMP.Api.Command.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSMPUtils.Client.Commands
{
    internal class UtilsCommands : IClientCommand
    {
        public bool AuthorizedOnly => false;
        public string Trigger => "/huddle";
        public string[] Aliases => [];
        public void Execute(string[] arguments)
        {
            PacketSender.SendHuddle();
        }
    }
}
