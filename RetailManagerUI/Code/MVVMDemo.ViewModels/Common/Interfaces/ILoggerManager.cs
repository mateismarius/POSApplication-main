using System;
using System.Runtime.CompilerServices;

namespace RetailManagerUI.ViewModels.Common.Interfaces
{
    public interface ILoggerManager<T>
    {
        /// <summary>
        /// Writes the diagnostic message at Debug level
        /// </summary>
        /// <param name="_message">The message to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        void LogDebug(string _message, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null);

        /// <summary>
        /// Writes the diagnostic exception at Debug level
        /// </summary>
        /// <param name="_exception">The exception to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        void LogDebug(Exception _exception, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null);

        /// <summary>
        /// Writes the diagnostic message at Error level
        /// </summary>
        /// <param name="_message">The exception to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        void LogError(string _message, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null);

        /// <summary>
        /// Writes the diagnostic exception at Error level
        /// </summary>
        /// <param name="_exception">The exception to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        void LogError(Exception _exception, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null);

        /// <summary>
        /// Writes the diagnostic message at Info level
        /// </summary>
        /// <param name="_message">The message to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        void LogInfo(string _message, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null);

        /// <summary>
        /// Writes the diagnostic exception at Info level
        /// </summary>
        /// <param name="_exception">The exception to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        void LogInfo(Exception _exception, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null);

        /// <summary>
        /// Writes the diagnostic message at Warn level
        /// </summary>
        /// <param name="_message">The message to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        void LogWarn(string _message, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null);

        /// <summary>
        /// Writes the diagnostic exception at Warn level
        /// </summary>
        /// <param name="_exception">The exception to log</param>
        /// <param name="_line">Obtains the line number in the source file at which the method is called</param>
        /// <param name="_caller">Obtains the method or property name of the caller to the method</param>
        /// <param name="_file">Obtains the full path of the source file that contains the caller at the time of compile</param>
        void LogWarn(Exception _exception, [CallerLineNumber] int _line = 0, [CallerMemberName] string _caller = null, [CallerFilePath] string _file = null);
    }
}
