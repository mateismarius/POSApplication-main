
using NLog;
using NLog.Config;
using NLog.Targets;
using RetailManagerUI.ViewModels.Common.Interfaces;
using System;
using System.Runtime.CompilerServices;

namespace RetailManagerUI.ViewModels.Common.Logging
{
    public class LoggerManager<T> : ILoggerManager<T>
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
     
        #region ================================================================== CTOR =====================================================================================
        /// <summary>
        /// Default C-tor
        /// </summary>
        public LoggerManager()
        {
            SetLoggingEnvironment();
        }
        #endregion

        /// <summary>
        /// Initializes NLog
        /// </summary>
        private void SetLoggingEnvironment()
        {
            // create NLog configuration  
            LoggingConfiguration _config = new LoggingConfiguration();
            FileTarget _file_target = new FileTarget("target2")
            {
                FileName = (AppDomain.CurrentDomain.BaseDirectory + @"Logs\" + DateTime.Now.ToString("yyMMdd") + ".log").Replace("\\", "/"),
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            _config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, _file_target));
            _config.AddTarget(_file_target);
            LogManager.Configuration = _config;
            // enable when debugging NLog itself:
            // LogManager.ThrowExceptions = true;
        }

        /// <summary>
        /// Writes the diagnostic message at Debug level
        /// </summary>
        /// <param name="_message">The message to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        public void LogDebug(string _message, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null)
        {
            logger.Debug(_message +
                Environment.NewLine + "\t\t\t\t'File was " + _file + "'" +
                Environment.NewLine + "\t\t\t\t'Method was " + _caller + "()'" +
                Environment.NewLine + "\t\t\t\t'Line was " + _line + "'" +
                Environment.NewLine);
        }

        /// <summary>
        /// Writes the diagnostic exception at Debug level
        /// </summary>
        /// <param name="_exception">The exception to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        public void LogDebug(Exception _exception, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null)
        {
            logger.Debug(_exception +
                Environment.NewLine + "\t\t\t\t'File was " + _file + "'" +
                Environment.NewLine + "\t\t\t\t'Method was " + _caller + "()'" +
                Environment.NewLine + "\t\t\t\t'Line was " + _line + "'" +
                Environment.NewLine);
        }

        /// <summary>
        /// Writes the diagnostic message at Error level
        /// </summary>
        /// <param name="_message">The exception to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        public void LogError(string _message, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null)
        {
            logger.Error(_message +
                Environment.NewLine + "\t\t\t\t'Stacktrace: " + Environment.StackTrace +
                Environment.NewLine + "\t\t\t\t'File was " + _file + "'" +
                Environment.NewLine + "\t\t\t\t'Method was " + _caller + "()'" +
                Environment.NewLine + "\t\t\t\t'Line was " + _line + "'" +
                Environment.NewLine);
        }

        /// <summary>
        /// Writes the diagnostic exception at Error level
        /// </summary>
        /// <param name="_exception">The exception to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        public void LogError(Exception _exception, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null)
        {
            logger.Error(_exception +
                Environment.NewLine + "\t\t\t\t'Stacktrace: " + Environment.StackTrace +
                Environment.NewLine + "\t\t\t\t'File was " + _file + "'" +
                Environment.NewLine + "\t\t\t\t'Method was " + _caller + "()'" +
                Environment.NewLine + "\t\t\t\t'Line was " + _line + "'" +
                Environment.NewLine);
        }

        /// <summary>
        /// Writes the diagnostic message at Info level
        /// </summary>
        /// <param name="_message">The message to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        public void LogInfo(string _message, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null)
        {
            logger.Info(_message +
                Environment.NewLine + "\t\t\t\t'File was " + _file + "'" +
                Environment.NewLine + "\t\t\t\t'Method was " + _caller + "()'" +
                Environment.NewLine + "\t\t\t\t'Line was " + _line + "'" +
                Environment.NewLine);
        }

        /// <summary>
        /// Writes the diagnostic exception at Info level
        /// </summary>
        /// <param name="_exception">The exception to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        public void LogInfo(Exception _exception, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null)
        {
            logger.Info(_exception +
                Environment.NewLine + "\t\t\t\t'File was " + _file + "'" +
                Environment.NewLine + "\t\t\t\t'Method was " + _caller + "()'" +
                Environment.NewLine + "\t\t\t\t'Line was " + _line + "'" +
                Environment.NewLine);
        }

        /// <summary>
        /// Writes the diagnostic message at Warn level
        /// </summary>
        /// <param name="_message">The message to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        public void LogWarn(string _message, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null)
        {
            logger.Warn(_message +
                Environment.NewLine + "\t\t\t\t'File was " + _file + "'" +
                Environment.NewLine + "\t\t\t\t'Method was " + _caller + "()'" +
                Environment.NewLine + "\t\t\t\t'Line was " + _line + "'" +
                Environment.NewLine);
        }

        /// <summary>
        /// Writes the diagnostic exception at Warn level
        /// </summary>
        /// <param name="_exception">The exception to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        public void LogWarn(Exception _exception, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null)
        {
            logger.Warn(_exception +
                Environment.NewLine + "\t\t\t\t'File was " + _file + "'" +
                Environment.NewLine + "\t\t\t\t'Method was " + _caller + "()'" +
                Environment.NewLine + "\t\t\t\t'Line was " + _line + "'" +
                Environment.NewLine);
        }
    }
}
