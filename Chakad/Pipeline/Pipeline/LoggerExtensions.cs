﻿using Chakad.Logging.Core;
using Chakad.Logging.Core.Null;

namespace Chakad.Pipeline
{
    public class LoggerBuilder
    {
        private ILoggerFactory loggerFactory = new NullLoggerFactory();
        internal static LoggerBuilder Instance { get; } = new LoggerBuilder();
        public ILoggerFactory LoggerFactory
        {
            get { return loggerFactory; }
        }
        internal void SetFactory(ILoggerFactory factory)
        {
            loggerFactory = factory;
        }
    }
}