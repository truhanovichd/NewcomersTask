// <copyright file="ReceiveObserver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using log4net;
using MassTransit;
using Microsoft.Extensions.Logging.Log4Net.AspNetCore.Extensions;

namespace NewcomersTask.Web
{
    public class ReceiveObserver : IReceiveObserver
    {
        private readonly ILog _logger;

        public ReceiveObserver()
        {
            _logger = LogManager.GetLogger(typeof(ReceiveObserver));
        }

        public Task PreReceive(ReceiveContext context)
        {
            _logger.Debug(context.Body.ToString());
            return Task.CompletedTask;
        }

        public Task PostReceive(ReceiveContext context)
        {
            _logger.Debug(context.Body.ToString());
            return Task.CompletedTask;
        }

        Task IReceiveObserver.ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType, Exception exception)
        {
            _logger.Critical(exception.Message, exception);
            return Task.CompletedTask;
        }

        public Task ReceiveFault(ReceiveContext context, Exception exception)
        {
            _logger.Critical(exception.Message, exception);
            return Task.CompletedTask;
        }

        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
        {
            throw new NotImplementedException();
        }
    }
}