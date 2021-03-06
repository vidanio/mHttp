﻿using System;

namespace m.Logging
{
    using Provider = Func<Type, LoggingProvider.ILogger>;
    
    public static class LoggingProvider
    {
        public interface ILogger
        {
            void Debug(string msg, params object[] args);
            void Info(string msg, params object[] args);
            void Warn(string msg, params object[] args);
            void Error(string msg, params object[] args);
            void Fatal(string msg, params object[] args);
        }

        static readonly NullLogger nullLoggerInstance = new NullLogger();
        static readonly TextWriterLogger consoleLoggerInstance = new TextWriterLogger(Console.Out);

        public static readonly Provider ConsoleLoggingProvider = _ => consoleLoggerInstance;
        public static readonly Provider NullLoggingProvider = _ => nullLoggerInstance;

        static Provider getLogger;

        public static ILogger GetLogger(Type type)
        {
            return getLogger(type);
        }

        public static void Use(Provider loggerProvider)
        {
            getLogger = loggerProvider;
        }

        static LoggingProvider()
        {
            Use(NullLoggingProvider);
        }
    }
}
