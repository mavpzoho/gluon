﻿using hase.DevLib.Contract;
using ProtoBuf;
using System;
using System.IO.Pipes;

namespace hase.DevLib.Relay.NamedPipe
{
    public class NamedPipeRelayProxyClient<TService, TServiceProxy, TRequest, TResponse> : IRelayClient<TService, TServiceProxy, TRequest, TResponse>
        where TServiceProxy : IServiceProxy<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public TResponse Execute(TRequest request)
        {
            TResponse response = null;
            var pipeName = typeof(TServiceProxy).Name;

            var pipe = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.None);
            Console.WriteLine($"nprc:{pipeName} connecting to relay.");
            pipe.ConnectAsync(5000).Wait();
            Console.WriteLine($"nprc:{pipeName} connected.");

            Console.WriteLine($"nprc:Sending {pipeName} request: {request}.");
            Serializer.SerializeWithLengthPrefix(pipe, request, PrefixStyle.Base128);
            //Console.WriteLine($"nprc:Sent {pipeName} request.");

            //Console.WriteLine($"nprc:Receiving {pipeName} response.");
            response = Serializer.DeserializeWithLengthPrefix<TResponse>(pipe, PrefixStyle.Base128);
            Console.WriteLine($"nprc:Received {pipeName} response: {response}.");

            return response;
        }
    }
}
