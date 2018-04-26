﻿using hase.DevLib.Framework.Contract;
using ProtoBuf;
using System;
using System.IO.Pipes;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.NamedPipe
{
    public class NamedPipeRelayDispatcher<TService, TRequest, TResponse> : RelayDispatcherBase<TService, TRequest, TResponse>
        where TService : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        public override string Abbr => "nprdc";
        private NamedPipeClientStream pipe = null;

        public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
        {
            try
            {
                pipe = new NamedPipeClientStream(".", ChannelName, PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
                await pipe.ConnectAsync(timeoutMs, ct);
            }
            catch (Exception ex)
            {
                var e = ex; // no compiler warning please
            }
        }
        public override TRequest DeserializeRequest()
        {
            return Serializer.DeserializeWithLengthPrefix<TRequest>(pipe, PrefixStyle.Base128);
        }
        public override void SerializeResponse(TResponse response)
        {
            Serializer.SerializeWithLengthPrefix(pipe, response, PrefixStyle.Base128);
        }
    }
}