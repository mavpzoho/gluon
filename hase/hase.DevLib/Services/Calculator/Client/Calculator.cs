﻿using hase.DevLib.Framework.Client;
using hase.DevLib.Services.Calculator.Contract;
using hase.DevLib.Services.Calculator.Service;
using System;
using System.Threading.Tasks;

namespace hase.DevLib.Services.Calculator.Client
{
    public class Calculator : ServiceClientBase<CalculatorService, CalculatorRequest, CalculatorResponse>, ICalculator
    {
        /// <summary>
        /// Create local service instance.
        /// </summary>
        public Calculator() { }
        /// <summary>
        /// Create proxied service instance.
        /// </summary>
        public Calculator(Type proxyType) : base(proxyType) { }

        public int? Add(int i1, int i2)
        {
            var request = new CalculatorRequest
            {
                Operation = CalculatorOpEnum.Add,
                I1 = i1,
                I2 = i2,
            };

            var response = Task.Run(async () => await Service.Execute(request)).Result;
            return response.Answer;
        }

        public int? Sub(int i1, int i2)
        {
            var request = new CalculatorRequest
            {
                Operation = CalculatorOpEnum.Sub,
                I1 = i1,
                I2 = i2,
            };

            var response = Task.Run<CalculatorResponse>(() => Service.Execute(request)).Result;
            return response.Answer;
        }
    }
}
