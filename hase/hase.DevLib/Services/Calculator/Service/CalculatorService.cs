﻿using hase.DevLib.Framework.Contract;
using hase.DevLib.Services.Calculator.Contract;
using System.Threading.Tasks;

namespace hase.DevLib.Services.Calculator.Service
{
    public class CalculatorService : IService<CalculatorRequest, CalculatorResponse>
    {
        // todo: consider making this an async method.
        public Task<CalculatorResponse> Execute(CalculatorRequest request)
        {
            int result = 0;
            switch (request.Operation)
            {
                case CalculatorOpEnum.Add:
                    result = request.I1 + request.I2;
                    break;
                case CalculatorOpEnum.Sub:
                    result = request.I1 - request.I2;
                    break;
            }

            var response = new CalculatorResponse(request, result);
            return Task.FromResult(response);
        }
    }
}
