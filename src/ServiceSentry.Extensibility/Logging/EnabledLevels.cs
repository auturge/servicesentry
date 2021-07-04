﻿using System.Collections.ObjectModel;
using System.Runtime.Serialization;

// ReSharper disable UnusedMemberInSuper.Global
// ReSharper disable UnusedMember.Global

namespace ServiceSentry.Extensibility.Logging
{
    [DataContract, KnownType(typeof (EnabledLevelsImplementation))]
    public abstract class EnabledLevels : NotifyPropertyChanged
    {
        public static EnabledLevels All =>
            new EnabledLevelsImplementation
            {
                TRACE = true,
                Debug = true,
                Info = true,
                Warn = true,
                Error = true,
                Fatal = true
            };

        public static EnabledLevels DebugLevels =>
            new EnabledLevelsImplementation
            {
                Debug = true,
                Info = true,
                Warn = true,
                Error = true,
                Fatal = true
            };

        public static EnabledLevels InfoLevels => new EnabledLevelsImplementation {Info = true, Warn = true, Error = true, Fatal = true};

        public static EnabledLevels WarnLevels => new EnabledLevelsImplementation {Warn = true, Error = true, Fatal = true};

        public static EnabledLevels ErrorLevels => new EnabledLevelsImplementation {Error = true, Fatal = true};

        public static EnabledLevels FatalLevels => new EnabledLevelsImplementation {Fatal = true};

        public static EnabledLevels None => new EnabledLevelsImplementation();

        [DataMember]
        public abstract bool TRACE { get; set; }

        [DataMember]
        public abstract bool Debug { get; set; }

        [DataMember]
        public abstract bool Info { get; set; }

        [DataMember]
        public abstract bool Warn { get; set; }

        [DataMember]
        public abstract bool Error { get; set; }

        [DataMember]
        public abstract bool Fatal { get; set; }

        public abstract ObservableCollection<LogLevel> Collection { get; set; }

        public abstract bool IsEnabled(LogLevel level);

        [DataContract]
        private sealed class EnabledLevelsImplementation : EnabledLevels
        {
            public override bool TRACE { get; set; }
            public override bool Debug { get; set; }
            public override bool Info { get; set; }
            public override bool Warn { get; set; }
            public override bool Error { get; set; }
            public override bool Fatal { get; set; }

            public override ObservableCollection<LogLevel> Collection
            {
                get
                {
                    var output = new ObservableCollection<LogLevel>();
                    if (TRACE) output.Add(LogLevel.Trace);
                    if (Debug) output.Add(LogLevel.Debug);
                    if (Info) output.Add(LogLevel.Info);
                    if (Warn) output.Add(LogLevel.Warn);
                    if (Error) output.Add(LogLevel.Error);
                    if (Fatal) output.Add(LogLevel.Fatal);
                    return output;
                }
                set
                {
                    TRACE = value.Contains(LogLevel.Trace);
                    Debug = value.Contains(LogLevel.Debug);
                    Info = value.Contains(LogLevel.Info);
                    Warn = value.Contains(LogLevel.Warn);
                    Error = value.Contains(LogLevel.Error);
                    Fatal = value.Contains(LogLevel.Fatal);

                    OnPropertyChanged();
                }
            }

            public override bool IsEnabled(LogLevel level)
            {
                return (Collection.Contains(level));
            }
        }
    }
}