using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ServiceSentry.Common.SingleInstance
{
    public static class WpfSingleInstance
    {
        /// <summary>
        /// Processing single instance in <see cref="SingleInstanceModes"/> <see cref="SingleInstanceModes.ForEveryUser"/> mode.
        /// </summary>
        internal static void Make()
        {
            Make(SingleInstanceModes.ForEveryUser);
        }

        /// <summary>
        /// Processing single instance.
        /// </summary>
        /// <param name="singleInstanceModes"></param>
        internal static void Make(SingleInstanceModes singleInstanceModes)
        {
            var appName = Application.Current.GetType().Assembly.ManifestModule.ScopeName;

            var windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var keyUserName = windowsIdentity.User != null ? windowsIdentity.User.ToString() : string.Empty;

            // Be careful! Max 260 chars!
            var eventWaitHandleName =
                $"{appName}{(singleInstanceModes == SingleInstanceModes.ForEveryUser ? keyUserName : string.Empty)}";

            try
            {
                using (var eventWaitHandle = EventWaitHandle.OpenExisting(eventWaitHandleName))
                {
                    // It informs first instance about other startup attempting.
                    eventWaitHandle.Set();
                }

                // Let's terminate this posterior startup.
                // For that exit no interceptions.
                Environment.Exit(0);
            }
            catch
            {
                // It's first instance.

                // Register EventWaitHandle.
                using (var eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, eventWaitHandleName))
                {
                    ThreadPool.RegisterWaitForSingleObject(eventWaitHandle, OtherInstanceAttemptedToStart, null, Timeout.Infinite, false);
                }

                RemoveApplicationsStartupDeadlockForStartupCrushedWindows();
            }
        }

        private static void OtherInstanceAttemptedToStart(object state, bool timedOut)
        {
            RemoveApplicationsStartupDeadlockForStartupCrushedWindows();
            Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                try
                {
                    Application.Current.MainWindow?.Activate();
                }
                catch
                {
                     /* ignored */
                }
            }));
        }

        internal static DispatcherTimer AutoExitApplicationIfStartupDeadlock;

        /// <summary>
        /// There are times when an error occurred at startup and not a single window appeared.
        /// In this case, the second instance of the application can no longer be started, and this one cannot be closed,
        /// except through the taskbar. Deadlock kind of worked out.
        /// </summary>
        public static void RemoveApplicationsStartupDeadlockForStartupCrushedWindows()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                AutoExitApplicationIfStartupDeadlock =
                    new DispatcherTimer(
                        TimeSpan.FromSeconds(6),
                        DispatcherPriority.ApplicationIdle,
                        (o, args) =>
                        {
                            if (Application.Current.Windows.Cast<Window>().Count(window => !Double.IsNaN(window.Left)) == 0)
                            {
                                // For that exit no interceptions.
                                Environment.Exit(0);
                            }
                        },
                        Application.Current.Dispatcher
                    );
            }),
                DispatcherPriority.ApplicationIdle
                );
        }
    }

    public enum SingleInstanceModes
    {
        /// <summary>
        /// Do nothing.
        /// </summary>
        NotInitialized = 0,

        /// <summary>
        /// Every user can have own single instance.
        /// </summary>
        ForEveryUser,
    }
}
