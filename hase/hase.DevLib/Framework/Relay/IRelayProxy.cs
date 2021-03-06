﻿using hase.DevLib.Framework.Contract;

namespace hase.DevLib.Framework.Relay
{
    public interface IRelayProxy<TRequest, TResponse> : IService<TRequest, TResponse>
        where TRequest : class
        where TResponse : class
    {
        string Abbr { get; }
    }
}
