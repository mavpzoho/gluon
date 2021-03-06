﻿using Gluon.Relay.Signalr.Client;
using Gluon.Tester.Server.Library;
using System;

namespace Gluon.Tester.Server.Conosle
{
    public class Program
    {
        public static string HubChannelUri => "http://localhost:5000/messagehub";
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Gluon Relay Tester Server...");
            //test atom

            var svcHost = new AppServiceHost<FileSystemQueryService>("FileSystemQueryServiceHost", HubChannelUri);

            Console.WriteLine("Press Enter to stop Gluon Relay Tester Server: ");
            Console.ReadLine();

            svcHost.Dispose();
        }
    }
}
