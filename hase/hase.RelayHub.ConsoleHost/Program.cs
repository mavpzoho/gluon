﻿using hase.DevLib.Framework.Core;
using hase.DevLib.Framework.Relay.NamedPipe;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using hase.DevLib.Services.FileSystemQuery.Contract;
using hase.DevLib.Services.FileSystemQuery.Service;
using System;

namespace hase.RelayHub.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var fsqServicePipeName = typeof(FileSystemQueryService).Name;
            var fsqProxyPipeName = ServiceTypesUtil.GetServiceProxyName(fsqServicePipeName);
            var calcServicePipeName = typeof(CalculatorService).Name;
            var calcProxyPipeName = ServiceTypesUtil.GetServiceProxyName(calcServicePipeName);

            Console.WriteLine("Starting Named Pipe Relay.");
            var fsqRelay = new NamedPipeRelayHub<FileSystemQueryRequest, FileSystemQueryResponse>(fsqServicePipeName, fsqProxyPipeName);
            fsqRelay.StartAsync();
            var calcRelay = new NamedPipeRelayHub<CalculatorRequest, CalculatorResponse>(calcServicePipeName, calcProxyPipeName);
            calcRelay.StartAsync();
            Console.WriteLine("Named Pipe Relay started.");

            Console.WriteLine("Press <Enter> to stop relay.");
            Console.ReadLine();
            fsqRelay.StopAsync().Wait();
            calcRelay.StopAsync().Wait();
            Console.WriteLine("Relay stopped.");

            Console.Write("Press <Enter> to close window.");
            Console.ReadLine();
        }
    }
}