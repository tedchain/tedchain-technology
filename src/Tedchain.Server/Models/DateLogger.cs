// Copyright (c) 2010-2018 The Bitcoin developers
// Original code was distributed under the MIT software license.
// Copyright (c) 2014-2018 TEDLab Sciences Ltd
// Tedchain code distributed under the GPLv3 license, see COPYING file.

using System;
using System.Globalization;
using Microsoft.Extensions.Logging;

namespace Tedchain.Server.Models
{
    public class DateLogger : ILogger
    {
        private readonly ILogger logger;

        public DateLogger(ILogger logger)
        {
            this.logger = logger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this.logger.BeginScope(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return this.logger.IsEnabled(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string date = DateTime.UtcNow.ToString("u", CultureInfo.InvariantCulture);

            this.logger.Log(
                logLevel,
                eventId,
                state,
                exception,
                (logState, ex) => $"[{date}] {formatter(logState, ex)}");
        }
    }
}
