using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceLogger.EF;
using TraceLogger.TraceExtensions;

namespace TraceLogger
{
    /// <summary>
    /// Trace Listener that writes log data to database
    /// </summary>
    public class TraceListenerDatabase : TraceListener
    {
        TraceLogEntities db = new TraceLogEntities();

        private const string TRACE_SWITCH_NAME = "DBTraceSwitch";
        private const string TRACE_SWITCH_DESCRIPTION = "Trace switch defined in config file for configuring trace output to database";
        private static readonly string DEFAULT_TRACE_TYPE = "Verbose";

        // Trace Switch object for controlling trace output, defaulting to Verbose
        private TraceSwitch TraceSwitch = new TraceSwitch(TRACE_SWITCH_NAME, TRACE_SWITCH_DESCRIPTION, DEFAULT_TRACE_TYPE);

        public TraceEventCache TraceEventCache { get; set; }
        public TraceEventType TraceEventType { get; set; }
        public string Source { get; set; }
        public int Id { get; set; }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void Write(string message)
        {
            this.WriteLine(message, null);
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public override void Write(string message, string category)
        {
            this.WriteLine(message, category);
        }

        /// <summary>
        /// Writes the specified o.
        /// </summary>
        /// <param name="o">The object.</param>
        public override void Write(object o)
        {
            if (o != null)
            {
                this.WriteLine(o.ToString(), null);
            }
        }

        /// <summary>
        /// Writes the specified o.
        /// </summary>
        /// <param name="o">The object.</param>
        /// <param name="category">The category.</param>
        public override void Write(object o, string category)
        {
            if (o != null)
            {
                this.WriteLine(o.ToString(), category);
            }
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        public override void WriteLine(string message, string category)
        {
            try
            {
                if (!this.ShouldLogTrace(TraceEventType.Verbose))
                    return;

                // IMPORTANT!!!!
                // DO NOT WRITE ANY Debug.WriteLine or Trace.WriteLine statements in this method
                this.WriteLineInternal(message, category, null);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                throw new Exception("Error writting to trace log database", ex); //ex.EntityValidationErrors;
            }
            catch (Exception ex)
            {
                throw new Exception("Error writting to trace log", ex);
            }
        }

        /// <summary>
        /// Writes the line internal.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="componentName">Name of the component.</param>
        private void WriteLineInternal(string message, string category, string componentName)
        {
            var log = new Log();
            log.Message = message;
            log.DateTime = DateTime.UtcNow;
            log.Source = componentName;
            log.Data = category;
            log.CallStack = this.TraceEventCache.Callstack.Substring(0, 4000);
            log.DateTime = this.TraceEventCache.DateTime;
            log.LogicalOperationStack = this.TraceEventCache.LogicalOperationStack.StackAsString();
            log.ProcessId = this.TraceEventCache.ProcessId;
            log.ThreadId = this.TraceEventCache.ThreadId;
            log.Timestamp = this.TraceEventCache.Timestamp;
            log.TraceEventType = this.TraceEventType.ToString();
            log.TraceId = ValidateTraceId(this.Id);
            log.Id = ValidateTraceId(this.Id);

            this.SaveToDatabase(log);
        }


        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="message">The message.</param>
        public override void WriteLine(string message)
        {
            this.WriteLine(message, null); //
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="o">The o.</param>
        public override void WriteLine(object o)
        {
            if (o != null)
            {
                this.WriteLine(o.ToString(), null);
            }
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="category">The category.</param>
        public override void WriteLine(object o, string category)
        {
            if (o != null)
            {
                this.WriteLine(o.ToString(), category);
            }
        }

        /// <summary>
        /// Traces the event.
        /// </summary>
        /// <param name="eventCache">The event cache.</param>
        /// <param name="source">The source.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="id">The identifier.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            this.TraceEventCache = eventCache;
            this.Source = source;
            this.TraceEventType = eventType;
            this.Id = ValidateTraceId(id);

            base.TraceEvent(eventCache, source, eventType, this.Id);
        }

        /// <summary>
        /// Traces the event.
        /// </summary>
        /// <param name="eventCache">The event cache.</param>
        /// <param name="source">The source.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            this.TraceEventCache = eventCache;
            this.Source = source;
            this.TraceEventType = eventType;
            this.Id = ValidateTraceId(id);

            base.TraceEvent(eventCache, source, eventType, this.Id, format, args);
        }

        /// <summary>
        /// Traces the event.
        /// </summary>
        /// <param name="eventCache">The event cache.</param>
        /// <param name="source">The source.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="message">The message.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            this.TraceEventCache = eventCache;
            this.Source = this.Source;
            this.TraceEventType = eventType;
            this.Id = ValidateTraceId(id);

            base.TraceEvent(eventCache, source, eventType, this.Id, message);
        }

        /// <summary>
        /// Saves to database.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        private void SaveToDatabase(Log logEntry)
        {
            db.Logs.Add(logEntry);
            db.SaveChanges();
        }

        /// <summary>
        /// Determines whether a trace event should be logged.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <returns>bool</returns>
        internal bool ShouldLogTrace(TraceEventType eventType)
        {
            bool shouldLog = true;
            switch (eventType)
            {
                case TraceEventType.Critical:
                case TraceEventType.Error:
                    shouldLog = this.TraceSwitch.TraceError;
                    break;

                case TraceEventType.Warning:
                    shouldLog = this.TraceSwitch.TraceWarning;
                    break;

                case TraceEventType.Information:
                    shouldLog = this.TraceSwitch.TraceInfo;
                    break;

                case TraceEventType.Start:
                case TraceEventType.Stop:
                case TraceEventType.Suspend:
                case TraceEventType.Resume:
                case TraceEventType.Transfer:
                case TraceEventType.Verbose:
                    shouldLog = this.TraceSwitch.TraceVerbose;
                    break;
            }
            return shouldLog;
        }

        /// <summary>
        /// Flushes this instance.
        /// </summary>
        public override void Flush()
        {
            base.Flush();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public override void Close()
        {
            base.Close();
        }

        /// <summary>
        /// Generates random number
        /// </summary>
        /// <returns></returns>
        internal int RandomNumberGenerator()
        {
            var ran = new Random();
            return ran.Next(0, 999999999);
        }

        /// <summary>
        /// Validates the trace identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        internal int ValidateTraceId(int id)
        {
            var value = id;

            while (db.Logs.Any(i => i.Id == value))
            {
                value = RandomNumberGenerator();
            }

            return value;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && db != null)
            {
                db.Dispose();
                db = null;
            }
            base.Dispose(disposing);
        }
    }
}
