﻿namespace ServiceSentry.Common.Enumerations
{
    public enum ServiceState
    {
        None = 0,
        Stopped = 1,
        StartPending = 2,
        StopPending = 3,
        Running = 4,
        ContinuePending = 5,
        PausePending = 6,
        Paused = 7,
        Error = 8,
    }
}