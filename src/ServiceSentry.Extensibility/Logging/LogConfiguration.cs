using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ServiceSentry.Extensibility.Logging
{
    [DataContract, KnownType(typeof (LogConfigImplementation))]
    public abstract class LogConfiguration : NotifyPropertyChanged
    {
        #region Constructors

        /// <summary>
        ///     Creates an instance of the <see cref="LogConfiguration" />
        ///     class, populated with the default values.
        /// </summary>
        public static LogConfiguration Default
        {
            get
            {
                return Create(EnabledLevels.All,
                              FileNameFormatter.Default,
                              AppDomain.CurrentDomain.BaseDirectory,
                              "{0}.{1}.log",
                              FileNameFormattingOption.AssemblyName,
                              FileNameFormattingOption.SortableDate);
            }
        }

        /// <summary>
        ///     Creates an instance of the <see cref="LogConfiguration" />
        ///     class, populated with the null values.
        /// </summary>
        public static LogConfiguration Null
        {
            get { return Create(EnabledLevels.None, FileNameFormatter.Default, string.Empty, string.Empty); }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LogConfiguration" /> class,
        ///     using the specified folder and file name formatting.
        /// </summary>
        /// <param name="levels"></param>
        /// <param name="formatter"></param>
        /// <param name="logFilePath">
        ///     The path of the directory in which the log will
        ///     reside, without any trailing backslash.
        /// </param>
        /// <param name="fileNameFormat">
        ///     A string that indicates the
        ///     formatting of the filename.
        /// </param>
        /// <param name="args"></param>
        /// <returns>
        ///     The <see cref="T:ServiceSentry.Extensibility.Logging.LogConfiguration" />.
        /// </returns>
        internal static LogConfiguration Create(EnabledLevels levels,
                                                FileNameFormatter formatter,
                                                string logFilePath,
                                                string fileNameFormat,
                                                params FileNameFormattingOption[] args)
        {
            return new LogConfigImplementation(levels, formatter, logFilePath, fileNameFormat, args);
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        ///     The folder in which the log file will reside.
        /// </summary>
        [DataMember(IsRequired = true)]
        public abstract string LogFolder { get; set; }

        /// <summary>
        ///     The formatting string of the log file name.
        /// </summary>
        [DataMember(IsRequired = false)]
        public abstract string FileNameFormat { get; set; }

        /// <summary>
        ///     The formatting options to apply.
        /// </summary>
        public abstract FileNameFormattingOption[] FormattingOptions { get; set; }

        [DataMember(IsRequired = false)]
        public abstract List<string> FormattingOptionStrings { get; set; }

        [DataMember(IsRequired = false)]
        public abstract EnabledLevels Levels { get; set; }

        /// <summary>
        ///     The full, formatted path of the log file.
        /// </summary>
        public abstract string FullPath { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>TRACE</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>TRACE</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        public abstract bool IsTRACEEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>Debug</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>Debug</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        public abstract bool IsDebugEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>Info</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>Info</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        public abstract bool IsInfoEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>Warn</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>Warn</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        public abstract bool IsWarnEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>Error</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>Error</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        public abstract bool IsErrorEnabled { get; }

        /// <summary>
        ///     Gets a value indicating whether logging is enabled for the <c>Fatal</c> level.
        /// </summary>
        /// <returns>
        ///     A value of <see langword="true" /> if logging is enabled for the <c>Fatal</c> level, otherwise it returns
        ///     <see
        ///         langword="false" />
        ///     .
        /// </returns>
        public abstract bool IsFatalEnabled { get; }

        /// <summary>
        ///     Determines whether the specified level is enabled.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>
        ///     A value of <c>true</c> if the specified level is enabled; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsEnabled(LogLevel level);

        #endregion

        [DataContract]
        private sealed class LogConfigImplementation : LogConfiguration
        {
            private FileNameFormattingOption[] _args;
            private string _fileNameFormat;
            private FileNameFormatter _formatter;
            private List<string> _formattingStrings;
            private EnabledLevels _levels;
            private string _logFileFolder;

            internal LogConfigImplementation(EnabledLevels levels,
                                             FileNameFormatter formatter,
                                             string logPath,
                                             string fileNameFormat,
                                             params FileNameFormattingOption[] args)
            {
                //Contract.Requires(logPath != null);
                //Contract.Requires(fileNameFormat != null);
                //Contract.Requires(formatter != null);
                _levels = levels;
                _formatter = formatter;
                _logFileFolder = logPath;
                _fileNameFormat = fileNameFormat;
                FormattingOptions = args;
            }

            public override string LogFolder
            {
                get { return _logFileFolder; }
                set
                {
                    if (_logFileFolder == value) return;
                    _logFileFolder = value;
                    OnPropertyChanged();
                }
            }

            public override string FileNameFormat
            {
                get { return _fileNameFormat; }
                set
                {
                    if (_fileNameFormat == value) return;
                    _fileNameFormat = value;
                    OnPropertyChanged();
                }
            }

            public override FileNameFormattingOption[] FormattingOptions
            {
                get { return _args; }
                set
                {
                    if (_args == value) return;
                    _args = value;

                    if (value == null || value.Length == 0)
                    {
                        _formattingStrings = new List<string>();
                    }
                    else
                    {
                        var output = new List<string>();
                        foreach (var item in value)
                        {
                            var converted = item.Name;
                            if (string.IsNullOrEmpty(converted)) continue;
                            output.Add(converted);
                        }
                        _formattingStrings = output;
                    }

                    OnPropertyChanged("FormattingOptions");
                    OnPropertyChanged("FormattingOptionStrings");
                }
            }

            public override List<string> FormattingOptionStrings
            {
                get { return _formattingStrings; }
                set
                {
                    if (_formattingStrings == value) return;
                    _formattingStrings = value;

                    if (value == null || value.Count == 0)
                    {
                        _args = new FileNameFormattingOption[] {};
                        return;
                    }

                    var output = new List<FileNameFormattingOption>();
                    foreach (var item in value)
                    {
                        var converted = FileNameFormattingOption.ByName(item);
                        if (converted == null) continue;
                        output.Add(converted);
                    }
                    _args = output.ToArray();

                    OnPropertyChanged("FormattingOptions");
                    OnPropertyChanged("FormattingOptionStrings");
                }
            }

            public override EnabledLevels Levels
            {
                get { return _levels; }
                set
                {
                    if (_levels == value) return;
                    _levels = value;
                    OnPropertyChanged();
                }
            }

            public override string FullPath
            {
                get
                {
                    return Environment.ExpandEnvironmentVariables(
                        String.Format(
                            "{0}\\{1}",
                            LogFolder,
                            _formatter.Format(_fileNameFormat, _args)
                            )
                        );
                }
            }

            #region isenabled
            
            public override bool IsTRACEEnabled
            {
                get { return IsEnabled(LogLevel.Trace); }
            }

            public override bool IsDebugEnabled
            {
                get { return IsEnabled(LogLevel.Debug); }
            }

            public override bool IsInfoEnabled
            {
                get { return IsEnabled(LogLevel.Info); }
            }

            public override bool IsWarnEnabled
            {
                get { return IsEnabled(LogLevel.Warn); }
            }

            public override bool IsErrorEnabled
            {
                get { return IsEnabled(LogLevel.Error); }
            }

            public override bool IsFatalEnabled
            {
                get { return IsEnabled(LogLevel.Fatal); }
            }

            public override bool IsEnabled(LogLevel level)
            {
                return _levels.IsEnabled(level);
            }

            #endregion

            [OnDeserializing]
            private void BeforeDeserialization(StreamingContext context)
            {
                _formatter = FileNameFormatter.Default;
            }
        }
    }
}