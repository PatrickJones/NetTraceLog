using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceLogger.Interfaces;

namespace TraceLogger
{
    /// <summary>
    /// Manages tracing and logging infromation, errors, warnings, & critical messages
    /// </summary>
    public class Logger : ITraceLogger
    {
        /// <summary>
        /// Writes an informational message to the trace listeners in the System.Diagnostics.Trace.Listeners
        /// collection using the specified message.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        public void Informational(string message)
        {
            Trace.TraceInformation(message);
        }

        /// <summary>
        /// Writes an informational message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified array of objects and formatting information.
        /// </summary>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        public void Informational(string fmt, params object[] vars)
        {
            Trace.TraceInformation(fmt, vars);
        }

        /// <summary>
        /// Writes an informational message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified array of objects, formatting information and Exception.
        /// </summary>
        /// <param name="exception">An Exception object that thrown during the trace.</param>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        public void Informational(Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceInformation(FormatExceptionMessage(exception, fmt, vars));
        }

        /// <summary>
        /// Writes a warning message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified message.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        public void Warning(string message)
        {
            Trace.TraceWarning(message);
        }

        /// <summary>
        /// Writes a warning message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified array of objects and formatting information.
        /// </summary>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        public void Warning(string fmt, params object[] vars)
        {
            Trace.TraceWarning(fmt, vars);
        }

        /// <summary>
        /// Writes a warning message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified array of objects, formatting information and Exception.
        /// </summary>
        /// <param name="exception">An Exception object that thrown during the trace.</param>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceWarning(FormatExceptionMessage(exception, fmt, vars));
        }

        /// <summary>
        ///  Writes an error message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified message.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        public void Error(string message)
        {
            Trace.TraceError(message);
        }

        /// <summary>
        /// Writes an error message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified array of objects and formatting information.
        /// </summary>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        public void Error(string fmt, params object[] vars)
        {
            Trace.TraceError(fmt, vars);
        }

        /// <summary>
        /// Writes an error message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified array of objects formatting information and Exception.
        /// </summary>
        /// <param name="exception">An Exception object that thrown during the trace.</param>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        public void Error(Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceError(FormatExceptionMessage(exception, fmt, vars));
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the System.Diagnostics.Trace.Listeners collection.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        /// <param name="categoryName">A category name used to organize the output.</param>
        public void LogCustom(string message, string categoryName)
        {
            Trace.Write(message, categoryName);
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified array of objects and formatting information.
        /// </summary>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="categoryName">A category name used to organize the output.</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        public void LogCustom(string fmt, string categoryName, params object[] vars)
        {
            Trace.Write(FormatExceptionMessage(null, fmt, vars), categoryName);
        }

        /// <summary>
        /// Writes a category name and a message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified array of objects formatting information and Exception.
        /// </summary>
        /// <param name="exception">An Exception object that thrown during the trace.</param>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="categoryName">A category name used to organize the output.</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        public void LogCustom(Exception exception, string fmt, string categoryName, params object[] vars)
        {
            Trace.Write(FormatExceptionMessage(exception, fmt, vars), categoryName);
        }

        /// <summary>
        ///  Writes an critical error message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified message.
        /// </summary>
        /// <param name="message">The informative message to write.</param>
        public void Critical(string message)
        {
            Trace.Write(message, "Critical");
        }

        /// <summary>
        /// Writes an critical error message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified array of objects and formatting information.
        /// </summary>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        public void Critical(string fmt, params object[] vars)
        {
            Trace.Write(FormatExceptionMessage(null, fmt, vars), "Critical");
        }

        /// <summary>
        /// Writes an critical error message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the specified array of objects formatting information and Exception.
        /// </summary>
        /// <param name="exception">An Exception object that thrown during the trace.</param>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        public void Critical(Exception exception, string fmt, params object[] vars)
        {
            Trace.Write(FormatExceptionMessage(exception, fmt, vars), "Critical");
        }

        /// <summary>
        /// Writes a custom concatenated message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the component name, method and time span.
        /// </summary>
        /// <param name="componentName">Name of the source component</param>
        /// <param name="method">Name of the method that invoked the log trace</param>
        /// <param name="timespan">Elasped time of method call. Primarily for external call, such as to a database. Use TimeSpan.Zero if not using parameter.</param>
        public void TraceApi(string componentName, string method, TimeSpan timespan)
        {
            TraceApi(componentName, method, timespan, "");
        }

        /// <summary>
        /// Writes a custom concatenated message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the component name, method, time span, specified array of objects formatting information.
        /// </summary>
        /// <param name="componentName">Name of the source component</param>
        /// <param name="method">Name of the method that invoked the log trace</param>
        /// <param name="timespan">Elasped time of method call. Primarily for external call, such as to a database. Use TimeSpan.Zero if not using parameter.</param>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        public void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            TraceApi(componentName, method, timespan, string.Format(fmt, vars));
        }

        /// <summary>
        /// Writes a custom concatenated message to the trace listeners in the System.Diagnostics.Trace.Listeners collection using the component name, method, time span and properties.
        /// </summary>
        /// <param name="componentName">Name of the source component</param>
        /// <param name="method">Name of the method that invoked the log trace</param>
        /// <param name="timespan">Elasped time of method call. Primarily for external call, such as to a database. Use TimeSpan.Zero if not using parameter.</param>
        /// <param name="properties"></param>
        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties)
        {
            string message = String.Concat("Component:", componentName, ";Method:", method, ";Timespan:", timespan.ToString(), ";Properties:", properties);
            if (timespan == TimeSpan.Zero)
            {
                Trace.Write(message, "Custom");
            }
            else
            {
                Trace.Write(message, "External Call");
            }
        }

        /// <summary>
        /// Formats an exception message to allow writting to a log.
        /// </summary>
        /// <param name="exception">An Exception object that thrown during the trace.</param>
        /// <param name="fmt">A format string that contains zero or more format items, which correspond to objects in the args array</param>
        /// <param name="vars">An object array containing zero or more objects to format.</param>
        /// <returns>A string representing the formated message</returns>
        private static string FormatExceptionMessage(Exception exception, string fmt, object[] vars)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format(fmt, vars));
            sb.Append(" Exception: ");
            sb.Append(exception.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// Publishes to database.
        /// </summary>
        public void PublishToDatabase()
        {
            var listener = new TraceListenerDatabase();
            listener.Flush();
        }
    }
}
