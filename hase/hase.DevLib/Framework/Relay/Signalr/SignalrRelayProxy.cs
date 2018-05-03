﻿using hase.DevLib.Framework.Contract;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace hase.DevLib.Framework.Relay.Signalr
{
    public class SignalrRelayProxy<TRequest, TResponse> : RelayProxyBase<TRequest, TResponse>
        where TRequest : ApplicationRequestMessage
        where TResponse : ApplicationResponseMessage
    {
        public override string Abbr => "srrpc";
        //private SignalrClientStream pipe = null;
        HubConnection _hub = null;
        private object _tmpResponse = null;

        public SignalrRelayProxy(string proxyChannelName) : base(proxyChannelName) { }

        public async override Task ConnectAsync(int timeoutMs, CancellationToken ct)
        {
            _hub = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/route")
                .Build();

            try
            {
                await _hub.StartAsync();
            }
            catch (Exception ex)
            {
                await _hub.DisposeAsync();
                var e = ex;
            }
        }

        public override void SerializeRequest(TRequest request)
        {
            //Serializer.SerializeWithLengthPrefix(pipe, request, PrefixStyle.Base128);

            request.Headers.SourceChannel = this.ChannelName;

            var wrapper = request.ToTransportRequest();

            _tmpResponse = _hub.InvokeAsync<object>("ProcessProxyRequestAsync", wrapper).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public override TResponse DeserializeResponse()
        {
            //return Serializer.DeserializeWithLengthPrefix<TResponse>(pipe, PrefixStyle.Base128);

            //return JsonConvert.DeserializeObject<TResponse>(_tmpResponse.ToString());
            var wrapper = JsonConvert.DeserializeObject<HttpResponseMessageWrapperEx>(_tmpResponse.ToString());
            var response = wrapper.ToApplicationResponse<TResponse>();
            return response;
        }

        public override void Disconnect()
        {
            //pipe.Dispose();
            _hub.DisposeAsync().Wait();
        }
    }
}
