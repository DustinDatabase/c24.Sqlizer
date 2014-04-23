using System;

namespace c24.Sqlizer.Infrastructure.Logging
{
    public interface ILogger
    {
        void Log(string text);

        void Log(string text, params object[] args);

        void Log(Exception e);
    }
}