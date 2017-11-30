using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceLogger.Interfaces
{
    /// <summary>
    /// Interface for tracing and logging
    /// </summary>
    interface ITraceLogger
    {
        void LogCustom(string message, string categoryName);
        void LogCustom(string fmt, string categoryName, params object[] vars);
        void LogCustom(Exception exception, string fmt, string categoryName, params object[] vars);

        void Informational(string message);
        void Informational(string fmt, params object[] vars);
        void Informational(Exception exception, string fmt, params object[] vars);

        void Warning(string message);
        void Warning(string fmt, params object[] vars);
        void Warning(Exception exception, string fmt, params object[] vars);

        void Error(string message);
        void Error(string fmt, params object[] vars);
        void Error(Exception exception, string fmt, params object[] vars);

        void Critical(string message);
        void Critical(string fmt, params object[] vars);
        void Critical(Exception exception, string fmt, params object[] vars);

        void TraceApi(string componentName, string method, TimeSpan timespan);
        void TraceApi(string componentName, string method, TimeSpan timespan, string properties);
        void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars);
    }
}
